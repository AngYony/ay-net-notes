using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AY.SmartEngine.Domain.TaskQueue.Entities
{
    /// <summary>
    /// 任务队列实体
    /// </summary>
    [Table("JobQueues")]
    public class JobQueueEntity
    {
        [Key]
        public Guid QueueId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; } = string.Empty;
        /// <summary>
        /// 队列状态：1=Active, 2=Paused, 3=Stopped
        /// </summary>
        public QueueStatus QueueStatus { get; set; } = QueueStatus.Active;
        /// <summary>
        /// 最大并发数
        /// </summary>
        public int MaxConcurrency { get; set; } = 5;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
