using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AY.SmartEngine.Domain.TaskQueue.Entities
{
    /// <summary>
    /// 单个任务实体
    /// </summary>
    [Table("Jobs")]
    public class JobEntity
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [Key]
        public Guid JobId { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required]
        public string JobName { get; set; } = string.Empty;

        /// <summary>
        /// 所属队列（队列名称）
        /// </summary>
        public string QueueName { get; set; } = "default";


        #region 反射调用相关的信息信息
        /// <summary>
        /// 类型名
        /// </summary>
        public string TypeName { get; set; } = string.Empty;
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; } = string.Empty;
        /// <summary>
        /// 参数 JSON
        /// </summary>
        public string? ParametersJson { get; set; }
        #endregion

        /// <summary>
        /// 状态：0=Pending, 1=Running, 2=Completed, 3=Failed, 4=Paused, 5=Cancelled, 6=WaitingForChildren
        /// </summary>
        public JobStatus JobStatus { get; set; } = JobStatus.Pending;
        /// <summary>
        /// 优先级：0=Low, 5=Normal, 10=High, 20=Critical
        /// </summary>
        public JobPriority Priority { get; set; } = JobPriority.Normal;

        /// <summary>
        /// 父级任务Id
        /// </summary>
        public Guid? ParentJobId { get; set; }

        /// <summary>
        /// 当前重试次数
        /// </summary>
        public int RetryCount { get; set; } = 0;
        /// <summary>
        /// 最大重试次数
        /// </summary>
        public int MaxRetries { get; set; } = 3;
        /// <summary>
        /// 重试延迟（秒）
        /// </summary>
        public int RetryDelaySeconds { get; set; } = 5;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime? ScheduledAt { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartedAt { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// 结果 JSON
        /// </summary>
        public string? ResultJson { get; set; }

        /// <summary>
        /// 标签（逗号分隔）
        /// </summary>
        // 标签（逗号分隔）
        public string? Tags { get; set; }

        /// <summary>
        /// 进度 0-100
        /// </summary>
        public int Progress { get; set; } = 0;

        /// <summary>
        /// 进度消息
        /// </summary>
        public string? ProgressMessage { get; set; }


        // 自引用父任务
        [ForeignKey(nameof(ParentJobId))]
        public JobEntity? ParentJob { get; set; }

        public ICollection<JobEntity> ChildJobs { get; set; } = new List<JobEntity>();


    }
}
