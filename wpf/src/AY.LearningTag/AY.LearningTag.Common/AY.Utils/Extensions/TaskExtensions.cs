using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AY.Utils.Extensions
{
    public static class TaskExtensions
    {
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeoutDelay)
        {
            using var cts = new CancellationTokenSource(timeoutDelay);
            Task completedTask = await Task.WhenAny(task, Task.Delay(Timeout.InfiniteTimeSpan, cts.Token));
            if (completedTask != task)
            {
                throw new TimeoutException("The operation has timed out.");
            }
            return await task;

        }

        /// <summary>
        /// 超时的 Task 扩展方法
        /// </summary>
        /// <param name="task"></param>
        /// <param name="timeoutDelay"></param>
        /// <returns></returns>
        /// <exception cref="TimeoutException"></exception>
        public static async Task TimeoutAfter(this Task task, TimeSpan timeoutDelay)
        {
            using var cts = new CancellationTokenSource();
            Task completedTask = await Task.WhenAny(task, Task.Delay(timeoutDelay, cts.Token));
            if (completedTask != task)
            {
                cts.Cancel(); // Cancel the delay task to avoid unnecessary resource usage
                throw new TimeoutException("The operation has timed out.");
            }
            await task;
        }

        /*
         * .NET6.0 中已经有了类似的功能，可以使用 `Task.WaitAsync` 方法来实现超时等待。
         * 1. 适用于所有 async 方法，尤其是 IO 操作。
         * 2. 可以使代码更简洁。
         */
        public static async Task Net6TimeOutSample()
        {
            using var cts = new CancellationTokenSource();
            try
            {
                // 使用 Task.WaitAsync 方法来实现超时等待，实际使用时可以替换为任何需要超时的异步操作。
                await Task.Delay(1000, cts.Token).WaitAsync(TimeSpan.FromSeconds(5));
            }
            catch (TimeoutException)
            {
                cts.Cancel(); // Cancel the delay task to avoid unnecessary resource usage 
            }
            catch (OperationCanceledException)
            {
                // 处理取消操作的逻辑
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }



        /// <summary>
        /// 准确捕获异常的操作
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onCompleted"></param>
        /// <param name="onError"></param>
        public static async Task Await(this Task task, Action? onCompleted = null, Action<Exception>? onError = null)
        {
            try
            {
                await task;
                onCompleted?.Invoke();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }



        public static async Task ChannelSample()
        {
            //var channel = Channel.CreateUnbounded<int>(new UnboundedChannelOptions()
            //{
            //    SingleReader = true, // 只允许一个读取器
            //    SingleWriter = true, // 只允许一个写入器
            //});

            var channel = Channel.CreateUnbounded<int>();
            var sender = SendThreadAsync(channel.Writer, "Sender 1");
            var sender2 = SendThreadAsync(channel.Writer, "Sender 2");
            var reciver = ReceiveThreadAsync(channel.Reader, "Receiver 1");
            var reciver2 = ReceiveThreadAsync(channel.Reader, "Receiver 2");
            await Task.WhenAll(sender, sender2);

            channel.Writer.Complete(); // 完成写入器，通知读取器没有更多数据了
            await Task.WhenAll(reciver, reciver2);

            Console.ReadLine();



            async Task SendThreadAsync(ChannelWriter<int> writer, string threadName)
            {
                for (int i = 0; i < 10; i++)
                {
                    await writer.WriteAsync(i);
                    Console.WriteLine($"{threadName}发送数据: {i}");
                    await Task.Delay(100); // 模拟发送间隔
                }
            }

            async Task ReceiveThreadAsync(ChannelReader<int> reader, string threadName)
            {
                try
                {
                    while (!reader.Completion.IsCompleted)
                    {
                        var data = await reader.ReadAsync();
                        Console.WriteLine($"{threadName}接收到数据: {data}");
                    }
                }
                catch (ChannelClosedException)
                {
                    Console.WriteLine("结束");
                }
                //await foreach(var item in reader.ReadAllAsync())
                //{
                //    Console.WriteLine($"接收到数据: {item}");
                //}
            }
        }


    }

}
