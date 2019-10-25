using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Sample
{
    //取消框架代码示例
    internal class CancelTaskDemo
    {
        public static void CancelParallelFor()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("----token cancelled"));
            //在500毫秒后取消任务
            cts.CancelAfter(500);

            try
            {
                ParallelLoopResult result = Parallel.For(0, 10, new ParallelOptions
                {
                    CancellationToken = cts.Token
                }
                , x =>
                {
                    Console.WriteLine($"loop {x} started");

                    //等待200毫秒
                    Task.Delay(400).Wait();
                    Console.WriteLine($"loop {x} finished");
                });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CancelTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => { Console.WriteLine("--- task cancelled"); });

            cts.CancelAfter(500);
            try
            {
                Task t1 = Task.Run(() =>
                {
                    Console.WriteLine("in task");
                    for (int i = 0; i < 20; i++)
                    {
                        Task.Delay(100).Wait();
                        CancellationToken token = cts.Token;

                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine("得到取消请求");
                            token.ThrowIfCancellationRequested();
                            break;
                        }
                        Console.WriteLine("in loop");
                    }
                    Console.WriteLine("任务完成，没有取消");
                }, cts.Token); //把取消标记赋予TaskFactory

                t1.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"异常：{ex.GetType().Name},{ex.Message}");
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine($"异常详情：{ex.InnerException.GetType()}, {ex.InnerException.Message}");
                }
            }
        }
    }
}