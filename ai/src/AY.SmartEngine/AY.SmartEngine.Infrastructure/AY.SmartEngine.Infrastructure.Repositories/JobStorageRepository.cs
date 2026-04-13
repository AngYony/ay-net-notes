using AY.SmartEngine.Domain.Repositories;
using AY.SmartEngine.Domain.TaskQueue.Entities;
using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using AY.SmartEngine.Infrastructure.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Infrastructure.Repositories
{
    /// <summary>
    /// 关于队列的所有实现均在该类中，而不是一个实体一个仓储文件
    /// </summary>
    public class JobStorageRepository : IJobStorageRepository
    {
        private readonly IDbContextFactory<TaskQueueDbContext> _dbContextFactory;

        public JobStorageRepository(IDbContextFactory<TaskQueueDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }


        /// <summary>
        /// 新增单条任务记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<JobEntity> InsertJobAsync(JobEntity entity, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            //使用 Add (内存操作)，只有 SaveChangesAsync 是真正的 I/O 异步
            context.Jobs.Add(entity);
            //此时 SaveChanges 会填充自增 ID 到 entity 对象中
            await context.SaveChangesAsync(ct);
            return entity;
        }

        /// <summary>
        /// 根据JobId获取任务信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<JobEntity?> GetJobAsync(Guid jobId, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            //Find方法专门按照主键搜索，带缓存优化（会优先从跟踪的实体中获取）
            return await context.Set<JobEntity>().FindAsync(new object[] { jobId }, ct);
        }

        public async Task<List<JobEntity>> GetJobListAsync(
            string? queueName = null, JobStatus? jobStatus = null, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            var query = context.Set<JobEntity>().AsNoTracking();
            if (queueName != null)
            {
                query = query.Where(a => a.QueueName == queueName);
            }
            if (jobStatus != null)
            {
                query = query.Where(a => a.JobStatus == jobStatus);
            }
            //int total = await query.CountAsync(ct);
            //按照优先级倒序，按照创建时间升序
            query = query.OrderByDescending(a => a.Priority).ThenBy(a => a.CreatedAt);

            return await query.ToListAsync(ct);
        }

        /// <summary>
        /// 根据父级任务Id获取该任务下的所有子任务列表
        /// </summary>
        /// <param name="parentJobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<JobEntity>> GetChildJobsAsync(Guid parentJobId, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            var query = context.Set<JobEntity>().AsNoTracking()
                .Where(a => a.ParentJobId == parentJobId).OrderByDescending(a => a.Priority).ThenBy(a => a.CreatedAt);
            return await query.ToListAsync(ct);
        }


        /// <summary>
        /// 更新指定任务的状态信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="errorMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> UpdateJobStatusAsync(Guid jobId, JobStatus jobStatus, string? errorMessage = null, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return await context.Set<JobEntity>()
                .Where(a => a.JobId == jobId)
                .ExecuteUpdateAsync(t =>
                {
                    t.SetProperty(x => x.JobStatus, jobStatus)
                    .SetProperty(x => x.ErrorMessage, errorMessage);
                }, ct);
        }

        /// <summary>
        /// 更新指定任务的进度信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="progress"></param>
        /// <param name="progressMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> UpdateJobProgressAsync(Guid jobId, int progress, string progressMessage = "", CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return await context.Set<JobEntity>()
                .Where(a => a.JobId == jobId)
                .ExecuteUpdateAsync(t =>
                {
                    t.SetProperty(x => x.Progress, progress)
                    .SetProperty(x => x.ProgressMessage, progressMessage);
                }, ct);
        }

        /// <summary>
        /// 更新任务的开始运行状态
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> UpdateJobStartedAsync(Guid jobId, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return await context.Set<JobEntity>()
                .Where(a => a.JobId == jobId)
                .ExecuteUpdateAsync(t =>
                {
                    t.SetProperty(x => x.JobStatus, JobStatus.Running)
                    .SetProperty(x => x.StartedAt, DateTime.Now);
                }, ct);
        }

        /// <summary>
        /// 更新任务的完成状态
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="resultJson"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> UpdateJobCompletedAsync(Guid jobId, string? resultJson = null, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return await context.Set<JobEntity>()
                .Where(a => a.JobId == jobId)
                .ExecuteUpdateAsync(t =>
                {
                    t.SetProperty(x => x.JobStatus, JobStatus.Completed)
                    .SetProperty(x => x.CompletedAt, DateTime.Now)
                    .SetProperty(x => x.ResultJson, resultJson)
                    .SetProperty(x => x.Progress, 100);
                }, ct);
        }

        /// <summary>
        /// 更新任务的失败状态
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="errorMessage"></param>
        /// <param name="retryCount"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> UpdateJobFailedAsync(Guid jobId, string errorMessage, int retryCount, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return await context.Set<JobEntity>()
                .Where(a => a.JobId == jobId)
                .ExecuteUpdateAsync(t =>
                {
                    t.SetProperty(x => x.JobStatus, JobStatus.Failed)
                    .SetProperty(x => x.ErrorMessage, errorMessage)
                    .SetProperty(x => x.RetryCount, retryCount);
                }, ct);
        }

        /// <summary>
        /// 根据队列名称获取所有Pending状态的任务列表
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<JobEntity>> GetPendingJobsAsync(string queueName, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            var query = context.Set<JobEntity>().AsNoTracking()
                .Where(a => a.QueueName == queueName && a.JobStatus == JobStatus.Pending)
                .Where(a => a.ScheduledAt == null || a.ScheduledAt <= DateTime.Now)
                .OrderByDescending(a => a.Priority).ThenBy(a => a.CreatedAt);

            return await query.ToListAsync(ct);
        }


        /// <summary>
        /// 获取队列下面指定状态的任务数量
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="jobStatus"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> GetJobsCountAsync(string? queueName = null, JobStatus? jobStatus = null, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            var query = context.Set<JobEntity>().AsNoTracking();
            if (queueName != null)
            {
                query = query.Where(a => a.QueueName == queueName);
            }
            if (jobStatus != null)
            {
                query = query.Where(a => a.JobStatus == jobStatus);
            }
            int total = await query.CountAsync(ct);
            return total;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="jobStatus"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdateJobStatusAsync(IEnumerable<Guid> jobIds, JobStatus jobStatus, CancellationToken ct = default)
        {
            var ids = jobIds.Distinct().ToArray();
            if (ids.Length == 0) return 0;
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            int affected = 0;
            foreach (var batch in ids.Chunk(1000))
            {
                affected += await context.Set<JobEntity>()
                    .Where(a => batch.Contains(a.JobId))
                    .ExecuteUpdateAsync(t => t.SetProperty(x => x.JobStatus, jobStatus), ct);
            }
            return affected;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> BatchDeleteJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default)
        {
            var ids = jobIds.Distinct().ToArray();
            if (ids.Length == 0) return 0;
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            int affected = 0;
            foreach (var batch in ids.Chunk(1000))
            {
                affected += await context.Set<JobEntity>()
                    .Where(a => batch.Contains(a.JobId))
                    .ExecuteDeleteAsync(ct);
            }
            return affected;
        }


        /// <summary>
        /// 新增JobQueue
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<JobQueueEntity> InsertQueueAsync(JobQueueEntity entity, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            //使用 Add (内存操作)，只有 SaveChangesAsync 是真正的 I/O 异步
            context.JobQueues.Add(entity);
            //此时 SaveChanges 会填充自增 ID 到 entity 对象中
            await context.SaveChangesAsync(ct);
            return entity;
        }

        /// <summary>
        /// 根据队列名称获取队列
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<JobQueueEntity?> GetQueueAsync(string queueName, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            var query = context.Set<JobQueueEntity>().AsNoTracking()
                .Where(a => a.QueueName == queueName);
            return await query.FirstOrDefaultAsync(ct);
        }

        /// <summary>
        /// 获取所有队列列表
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<JobQueueEntity>> GetQueuesAsync(CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            var query = context.Set<JobQueueEntity>().AsNoTracking().OrderBy(x => x.QueueName);
            return await query.ToListAsync(ct);
        }


        /// <summary>
        /// 更新指定名称的队列状态
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="queueStatus"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<int> UpdateQueueStatusAsync(string queueName, QueueStatus queueStatus, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return await context.Set<JobQueueEntity>()
                .Where(a => a.QueueName == queueName)
                .ExecuteUpdateAsync(t => t.SetProperty(x => x.QueueStatus, queueStatus), ct);
        }

        /// <summary>
        /// 添加任务历史记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<JobHistoryEntity> InsertHistoryAsync(JobHistoryEntity entity, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            //使用 Add (内存操作)，只有 SaveChangesAsync 是真正的 I/O 异步
            context.JobHistories.Add(entity);
            //此时 SaveChanges 会填充自增 ID 到 entity 对象中
            await context.SaveChangesAsync(ct);
            return entity;
        }

        /// <summary>
        /// 根据任务Id获取该任务的历史信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<JobHistoryEntity>> GetJobHistoryAsync(Guid jobId, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            var query = context.Set<JobHistoryEntity>().AsNoTracking()
                .Where(a => a.JobId == jobId).OrderByDescending(x => x.CreatedAt);
            return await query.ToListAsync(ct);
        }

    }
}
