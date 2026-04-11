using AY.SmartEngine.Domain.TaskQueue.Entities;
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
    }
}
