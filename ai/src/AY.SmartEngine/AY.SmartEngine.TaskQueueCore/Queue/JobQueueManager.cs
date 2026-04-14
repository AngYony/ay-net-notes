using AY.SmartEngine.Domain.Repositories;
using AY.SmartEngine.Domain.TaskQueue.Entities;
using AY.SmartEngine.Domain.TaskQueue.Entities.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.TaskQueueCore.Queue
{
    public sealed class JobQueueManager : IAsyncDisposable
    {
        private readonly IJobStorageRepository _jobStorageRepository;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<JobQueueManager> _logger;
        // 使用 SemaphoreSlim 进行颗粒度较小的锁控制
        private readonly SemaphoreSlim _lock = new(1, 1);
        // 改用 ConcurrentDictionary 提高读取性能
        private readonly ConcurrentDictionary<string, ChannelJobQueue> _queues = new();
        // 默认配置常数化
        private const int DefaultCapacity = 50_000;

        public JobQueueManager(IJobStorageRepository jobStorageRepository, ILoggerFactory loggerFactory)
        {
            this._jobStorageRepository = jobStorageRepository;
            this._loggerFactory = loggerFactory;
            this._logger = loggerFactory.CreateLogger<JobQueueManager>();

        }


        public async Task<ChannelJobQueue> GetOrCreateQueueAsync(string queueName, int maxConcurrency = 5, CancellationToken ct = default)
        {
            // 1. 第一层检查：快速读取（无锁）
            if (_queues.TryGetValue(queueName, out var existing))
                return existing;

            // 2. 需创建时加锁
            await _lock.WaitAsync(ct).ConfigureAwait(false);


            try
            {
                // 3. 第二层检查（双检锁 pattern）
                if (_queues.TryGetValue(queueName, out existing))
                    return existing;

                // 4. 确保 DB 中有该队列记录
                var dbQueue = await _jobStorageRepository.GetQueueAsync(queueName, ct).ConfigureAwait(false);
                if (dbQueue == null)
                {
                    dbQueue = new JobQueueEntity { QueueName = queueName, MaxConcurrency = maxConcurrency };
                    await _jobStorageRepository.InsertQueueAsync(dbQueue, ct).ConfigureAwait(false);
                }

                var queue = new ChannelJobQueue(
                    queueName, 
                    _jobStorageRepository,
                    _loggerFactory.CreateLogger<ChannelJobQueue>(), 
                    capacity: DefaultCapacity);

                _queues[queueName] = queue;
                return queue;

            }
            finally
            {
                _lock.Release();
            }

        }

        public ChannelJobQueue? GetQueue(string queueName) => _queues.GetValueOrDefault(queueName);

        public IReadOnlyDictionary<string, ChannelJobQueue> AllQueues => _queues;

        /// <summary>
        /// 启动时从存储恢复所有 Pending 任务
        /// </summary>
        public async Task RestoreFromStorageAsync(CancellationToken ct = default)
        {
            try
            {
                _logger.LogInformation("Starting queue restoration from storage...");
                var queues = await _jobStorageRepository.GetQueuesAsync(ct).ConfigureAwait(false);

                // 并行初始化队列，提高启动速度，如果恢复的队列特别多，建议使用Parallel.ForEachAsync + MaxDegreeOfParallelism 防止瞬间压垮数据库或内存
                var tasks = queues.Select(async q =>
                {
                    try
                    {
                        var channelQueue = await GetOrCreateQueueAsync(q.QueueName, q.MaxConcurrency, ct).ConfigureAwait(false);

                        if (q.QueueStatus == QueueStatus.Active)
                        {
                            var pendingJobs = await _jobStorageRepository.GetPendingJobsAsync(q.QueueName, ct: ct).ConfigureAwait(false);
                            foreach (var job in pendingJobs)
                            {
                                await channelQueue.EnqueueAsync(job, ct).ConfigureAwait(false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to restore queue {QueueName}", q.QueueName);
                    }
                });

                await Task.WhenAll(tasks).ConfigureAwait(false);
                _logger.LogInformation("Queue restoration completed.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Critical error during RestoreFromStorageAsync");
                throw;
            }
        }
        public async ValueTask DisposeAsync()
        {
            _lock.Dispose();
            // 如果 ChannelJobQueue 实现了 IDisposable，在此遍历并释放
            foreach (var queue in _queues.Values)
            {
                if (queue is IAsyncDisposable ad) await ad.DisposeAsync();
                else if (queue is IDisposable d) d.Dispose();
            }
            _queues.Clear();
        }
    }
}
