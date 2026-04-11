using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.TaskQueue.Entities.Enums
{
    /// <summary>
    /// 任务优先级
    /// </summary>
    public enum JobPriority
    {
        /// <summary>
        /// 最低的
        /// </summary>
        Low = 0,
        
        /// <summary>
        /// 通常、标准的
        /// </summary>
        Normal = 5,

        /// <summary>
        /// 较高的
        /// </summary>
        High = 10,

        /// <summary>
        /// 及其重要的
        /// </summary>
        Critical = 20
    }
}
