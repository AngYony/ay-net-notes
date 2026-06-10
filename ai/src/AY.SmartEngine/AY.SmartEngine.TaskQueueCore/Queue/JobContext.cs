using AY.SmartEngine.Domain.TaskQueue.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.TaskQueueCore.Queue
{
    /// <summary>
    /// 任务上下文，执行时传入
    /// </summary>
    public class JobContext
    {
        public JobEntity Job { get; init; } = null!;
        public IServiceProvider Services { get; init; } = null!;

        public void ReportProgress(int percent, string? message = null) => ProgressChanged?.Invoke(percent, message);

        internal event Action<int, string?>? ProgressChanged;
    }


    /// <summary>
    /// 任务执行委托
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public delegate Task JobExecuteDelegate(JobContext context, CancellationToken ct);
}
