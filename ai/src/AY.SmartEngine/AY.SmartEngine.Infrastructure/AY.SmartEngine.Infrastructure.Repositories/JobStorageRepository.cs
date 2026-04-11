using AY.SmartEngine.Domain.Repositories;
using AY.SmartEngine.Domain.TaskQueue.Entities;
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


    }
}
