using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.TaskQueue.Entities.Enums
{
    public enum QueueStatus
    {
        /// <summary>
        /// 生效的
        /// </summary>
        Active = 1,

        /// <summary>
        /// 已暂停
        /// </summary>
        Paused = 2,

        /// <summary>
        /// 已停止
        /// </summary>
        Stopped = 3
    }
}
