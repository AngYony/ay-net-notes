using AY.SmartEngine.Domain.TaskQueue.Entities;
using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.Repositories
{
    public interface IJobStorageRepository
    {
        /// <summary>
        /// 新增单条任务记录
        /// </summary>
        /// <param name="job"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<JobEntity> InsertJobAsync(JobEntity job, CancellationToken ct = default);

        /// <summary>
        /// 根据jobId获取任务信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<JobEntity?> GetJobAsync(Guid jobId, CancellationToken ct = default);

        /// <summary>
        /// 根据队列名称或者任务状态获取任务信息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="jobStatus"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<JobEntity>> GetJobListAsync(string? queueName = null, JobStatus? jobStatus = null, CancellationToken ct = default);

        /// <summary>
        /// 根据父级任务Id获取该任务下的所有子任务列表
        /// </summary>
        /// <param name="parentJobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<JobEntity>> GetChildJobsAsync(Guid parentJobId, CancellationToken ct = default);

        /// <summary>
        /// 更新指定任务的状态信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="errorMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> UpdateJobStatusAsync(Guid jobId, JobStatus jobStatus, string? errorMessage = null, CancellationToken ct = default);

        /// <summary>
        /// 更新指定任务的进度信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="progress"></param>
        /// <param name="progressMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> UpdateJobProgressAsync(Guid jobId, int progress, string progressMessage = "", CancellationToken ct = default);

        /// <summary>
        /// 更新任务的开始运行状态
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> UpdateJobStartedAsync(Guid jobId, CancellationToken ct = default);

        /// <summary>
        /// 更新任务的完成状态
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="resultJson"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> UpdateJobCompletedAsync(Guid jobId, string? resultJson = null, CancellationToken ct = default);


        /// <summary>
        /// 更新任务的失败状态
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="errorMessage"></param>
        /// <param name="retryCount"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> UpdateJobFailedAsync(Guid jobId, string errorMessage, int retryCount, CancellationToken ct = default);

        /// <summary>
        /// 根据队列名称获取所有Pending状态的任务列表
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<JobEntity>> GetPendingJobsAsync(string queueName, CancellationToken ct = default);

        /// <summary>
        /// 获取队列下面指定状态的任务数量
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="jobStatus"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> GetJobsCountAsync(string? queueName = null, JobStatus? jobStatus = null, CancellationToken ct = default);

        /// <summary>
        /// 批量更新任务状态
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="jobStatus"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> BatchUpdateJobStatusAsync(IEnumerable<Guid> jobIds, JobStatus jobStatus, CancellationToken ct = default);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> BatchDeleteJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default);

        /// <summary>
        /// 新增JobQueue
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<JobQueueEntity> InsertQueueAsync(JobQueueEntity entity, CancellationToken ct = default);

        /// <summary>
        /// 根据队列名称获取队列
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<JobQueueEntity?> GetQueueAsync(string queueName, CancellationToken ct = default);

        /// <summary>
        /// 获取所有队列列表
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<JobQueueEntity>> GetQueuesAsync(CancellationToken ct = default);

        /// <summary>
        /// 更新指定名称的队列状态
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="queueStatus"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> UpdateQueueStatusAsync(string queueName, QueueStatus queueStatus, CancellationToken ct = default);

        /// <summary>
        /// 添加任务历史记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<JobHistoryEntity> InsertHistoryAsync(JobHistoryEntity entity, CancellationToken ct = default);

        /// <summary>
        /// 根据任务Id获取该任务的历史信息
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<JobHistoryEntity>> GetJobHistoryAsync(Guid jobId, CancellationToken ct = default);
    }
}
