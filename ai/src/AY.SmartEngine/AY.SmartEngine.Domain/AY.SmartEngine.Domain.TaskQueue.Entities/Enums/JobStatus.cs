using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.TaskQueue.Entities.Enums
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 执行中
        /// </summary>
        Running = 1,
        
        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3,

        /// <summary>
        /// 已暂停
        /// </summary>
        Paused = 4,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 5,

        /// <summary>
        /// 等待子任务
        /// </summary>
        WaitingForChildren = 6
    }
}
