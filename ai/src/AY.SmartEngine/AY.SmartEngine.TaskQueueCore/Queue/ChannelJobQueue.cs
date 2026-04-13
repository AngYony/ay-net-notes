using AY.SmartEngine.Domain.TaskQueue.Entities;
using Enums = AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using AY.SmartEngine.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using AY.SmartEngine.TaskQueueCore.Models;

namespace AY.SmartEngine.TaskQueueCore.Queue
{
    public class ChannelJobQueue : IJobQueue, IAsyncDisposable
    {
        private volatile Enums.QueueStatus _queueStatus = Enums.QueueStatus.Active;
        private readonly IJobStorageRepository _jobStorageRepository;
        private readonly ILogger<ChannelJobQueue> _logger;
        private readonly Channel<JobMessage> _channel;

        //用于控制暂停/恢复的信号量
        private TaskCompletionSource<bool> _pauseSignal = new(TaskCreationOptions.RunContinuationsAsynchronously);


        public string QueueName { get; }
        public Enums.QueueStatus QueueStatus => _queueStatus;
        //暴露原始 Channel Reader，供高级用户直接消费
        public ChannelReader<JobMessage> Reader => _channel.Reader;

        public ChannelJobQueue(
            string queueName,
            IJobStorageRepository jobStorageRepository,
            ILogger<ChannelJobQueue> logger,
            int capacity = 10_000)
        {
            QueueName = queueName;
            this._jobStorageRepository = jobStorageRepository;
            this._logger = logger;

            _channel = Channel.CreateBounded<JobMessage>(new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleReader = false,   // 允许多个 Worker 消费
                SingleWriter = false    // 允许多个生产者写入
            });

            // 初始状态下如果是 Active，则释放信号
            if (_queueStatus == Enums.QueueStatus.Active)
            {
                _pauseSignal.SetResult(true);
            }
        }

        /// <summary>
        /// 将任务写入到管道中
        /// </summary>
        /// <param name="job"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async ValueTask WriteToChannelInternalAsync(JobEntity job, CancellationToken ct)
        {
            var msg = new JobMessage
            {
                JobId = job.JobId,
                QueueName = QueueName,
                JobPriority = job.Priority,
                EnqueuedAt = DateTime.UtcNow
            };
            await _channel.Writer.WriteAsync(msg, ct);
        }

        /// <summary>
        /// 从 Channel 读取下一条消息（供 Worker 调用）
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async ValueTask<JobMessage> ReadAsync(CancellationToken ct = default)
        {
            await _pauseSignal.Task;  //如果队列暂停，此处会异步等待信号
            return await _channel.Reader.ReadAsync(ct);
        }

        public bool TryRead(out JobMessage? message)
        {
            if (_queueStatus != Enums.QueueStatus.Active)
            {
                message = null;
                return false;
            }
            return _channel.Reader.TryRead(out message);
        }

        public ValueTask<bool> WaitToReadAsync(CancellationToken ct = default) => _channel.Reader.WaitToReadAsync(ct);



        /// <summary>
        /// 提交新的任务到队列中（同步写入到数据库和管道中）
        /// </summary>
        /// <param name="job"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Guid> EnqueueAsync(JobEntity job, CancellationToken ct = default)
        {
            job.QueueName = QueueName;
            job.JobStatus = Enums.JobStatus.Pending;
            await _jobStorageRepository.InsertJobAsync(job, ct);
            await _jobStorageRepository.InsertHistoryAsync(new JobHistoryEntity
            {
                JobId = job.JobId,
                JobStatus = Enums.JobStatus.Pending,
                Message = "Job enqueued"
            }, ct);
            //判断当前任务的管道是否是激活状态
            if (_queueStatus == Enums.QueueStatus.Active)
                await WriteToChannelInternalAsync(job, ct);
            _logger.LogDebug("Enqueued job {JobId} ({Name}) to queue {Queue}", job.JobId, job.JobName, QueueName);
            return job.JobId;
        }

        /// <summary>
        /// 批量提交新的任务到队列中（同步写入数据库和管道中）
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Guid>> EnqueueBatchAsync(IEnumerable<JobEntity> jobs, CancellationToken ct = default)
        {
            var ids = new List<Guid>();
            foreach (var job in jobs)
            {
                ids.Add(await EnqueueAsync(job, ct));
            }
            return ids;
        }

        /// <summary>
        /// 暂停队列（并同步状态到数据库中）
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> PauseQueueAsync(CancellationToken ct = default)
        {
            if (_queueStatus == Enums.QueueStatus.Paused) return true;
            _queueStatus = Enums.QueueStatus.Paused;
            // 重置信号量，后续 ReadAsync 将等待
            Interlocked.Exchange(ref _pauseSignal, new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously));
            return await _jobStorageRepository.UpdateQueueStatusAsync(QueueName, Enums.QueueStatus.Paused, ct) > 0;
        }

        /// <summary>
        /// 还原队列，并同步到数据库和管道中
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task ResumeQueueAsync(CancellationToken ct = default)
        {
            if (_queueStatus == Enums.QueueStatus.Active) return;

            _queueStatus = Enums.QueueStatus.Active;
            _pauseSignal.TrySetResult(true); // 唤醒等待读取的 Worker
            await _jobStorageRepository.UpdateQueueStatusAsync(QueueName, Enums.QueueStatus.Active, ct);

            // 将 SQLite 中 Pending 的任务重新写入 Channel
            var pendingJobs = await _jobStorageRepository.GetPendingJobsAsync(QueueName, ct: ct);
            foreach (var job in pendingJobs)
                await WriteToChannelInternalAsync(job, ct);
            _logger.LogInformation("Queue {Queue} resumed with {Count} jobs", QueueName, pendingJobs.Count);
        }

        /// <summary>
        /// 停止当前队列
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task StopQueueAsync(CancellationToken ct = default)
        {
            _queueStatus = Enums.QueueStatus.Stopped;
            _pauseSignal.TrySetResult(false); // 停止等待
            await _jobStorageRepository.UpdateQueueStatusAsync(QueueName, Enums.QueueStatus.Stopped, ct);
            // 取消所有 Pending 任务
            var pending = await _jobStorageRepository.GetPendingJobsAsync(QueueName, ct: ct);
            var ids = pending.Select(j => j.JobId).ToList();
            if (ids.Any())
                await _jobStorageRepository.BatchUpdateJobStatusAsync(ids, Enums.JobStatus.Cancelled, ct);

            _channel.Writer.TryComplete();
            _logger.LogInformation("Queue {Queue} stopped, cancelled {Count} pending jobs", QueueName, ids.Count);
        }

        /// <summary>
        /// 任务暂停
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task PauseJobAsync(Guid jobId, CancellationToken ct = default)
        {
            return _jobStorageRepository.UpdateJobStatusAsync(jobId, Enums.JobStatus.Paused, ct: ct);
        }

        /// <summary>
        /// 还原任务，并同步状态到数据库和重新添加到队列中
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ResumeJobAsync(Guid jobId, CancellationToken ct = default)
        {
            var job = await _jobStorageRepository.GetJobAsync(jobId, ct);
            if (job == null) return;
            await _jobStorageRepository.UpdateJobStatusAsync(jobId, Enums.JobStatus.Pending, ct: ct);
            if (_queueStatus == Enums.QueueStatus.Active)
                await WriteToChannelInternalAsync(job, ct);
        }

        /// <summary>
        /// 单个任务的取消状态修改
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task CancelJobAsync(Guid jobId, CancellationToken ct = default)
        {
            return _jobStorageRepository.UpdateJobStatusAsync(jobId, Enums.JobStatus.Cancelled, ct: ct);
        }

        /// <summary>
        /// 多个任务的批量暂停
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task BatchPauseJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default)
        {
            return _jobStorageRepository.BatchUpdateJobStatusAsync(jobIds, Enums.JobStatus.Paused, ct: ct);
        }

        /// <summary>
        /// 多个任务的批量还原
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task BatchResumeJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default)
        {
            var ids = jobIds.ToList();
            await _jobStorageRepository.BatchUpdateJobStatusAsync(ids, Enums.JobStatus.Pending, ct);
            if (_queueStatus == Enums.QueueStatus.Active)
            {
                foreach (var id in ids)
                {
                    var job = await _jobStorageRepository.GetJobAsync(id, ct);
                    if (job != null) await WriteToChannelInternalAsync(job, ct);
                }
            }
        }


        /// <summary>
        /// 多个任务的批量取消
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task BatchCancelJobsAsync(IEnumerable<Guid> jobIds, CancellationToken ct = default)
        {
            return _jobStorageRepository.BatchUpdateJobStatusAsync(jobIds, Enums.JobStatus.Cancelled, ct: ct);
        }

        public async ValueTask DisposeAsync()
        {
            _channel.Writer.TryComplete();
            _pauseSignal.TrySetResult(false);
            await ValueTask.CompletedTask;
        }
    }
}
