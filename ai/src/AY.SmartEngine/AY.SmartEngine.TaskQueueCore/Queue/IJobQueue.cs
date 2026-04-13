using AY.SmartEngine.Domain.TaskQueue.Entities;
using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.TaskQueueCore.Queue
{
    public interface IJobQueue
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        string QueueName { get; }
        QueueStatus QueueStatus { get; }

        /// <summary>
        /// 入队（持久化 + Channel）
        /// </summary>
        /// <param name="job"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Guid> EnqueueAsync(JobEntity job, CancellationToken ct = default);

        /// <summary>
        /// 批量入队
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<Guid>> EnqueueBatchAsync(IEnumerable<JobEntity> jobs, CancellationToken ct = default);

        /// <summary>
        /// 暂停队列（停止消费，不影响已运行任务）
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> PauseQueueAsync(CancellationToken ct = default);

        /// <summary>
        /// 恢复队列
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task ResumeQueueAsync(CancellationToken ct = default);

        /// <summary>
        /// 停止队列（停止消费 + 取消所有 Pending）
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task StopQueueAsync(CancellationToken ct = default);

        /// <summary>
        /// 暂停指定任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task PauseJobAsync(Guid jobId, CancellationToken ct = default);

        /// <summary>
        /// 恢复指定任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task ResumeJobAsync(Guid jobId, CancellationToken ct = default);

        /// <summary>
        /// 取消指定任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task CancelJobAsync(Guid jobId, CancellationToken ct = default);

        /// <summary>批量操作</summary>
        Task BatchPauseJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default);
        Task BatchResumeJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default);
        Task BatchCancelJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default);


    }
}
