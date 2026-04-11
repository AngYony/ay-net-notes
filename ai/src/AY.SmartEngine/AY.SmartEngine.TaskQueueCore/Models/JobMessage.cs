using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.TaskQueueCore.Models
{
    /// <summary>
    /// 任务消息
    /// </summary>
    public class JobMessage
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid JobId { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; } = "default";

        /// <summary>
        /// 任务优先级
        /// </summary>
        public JobPriority JobPriority { get; set; }

        /// <summary>
        /// 进入队列时间
        /// </summary>
        public DateTime EnqueuedAt { get; set; } = DateTime.Now;

    }
}
