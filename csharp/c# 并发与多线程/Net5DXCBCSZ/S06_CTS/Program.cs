using System;
using System.Threading;
using System.Threading.Tasks;

namespace S06_CTS {
    class Program {
        static void Main(string[] args) {
            Sample2();
        }

        static void Run(CancellationToken token) {
            int i = 1;
            while (!token.IsCancellationRequested) {
                Console.WriteLine("正在执行..." + (++i));
                Thread.Sleep(1000);
            }
        }

        static void Sample1() {
            CancellationTokenSource cts = new CancellationTokenSource();

            var task = Task.Factory.StartNew(() => {
                Run(cts.Token);
            });

            // 当在控制台输入ctrl+c是触发
            Console.CancelKeyPress += (sender, e) => {
                cts.Cancel();
                task.Wait(); //此处Wait调用必不可少，在同一个任务中进行了取消操作，必须在该任务中等待取消的任务完成。
            };
            // 此处wait()的调用也必不可少
            task.Wait();
            Console.WriteLine("执行完成！");
            Console.Read();
        }

        static void Sample2() {

            CancellationTokenSource source = new CancellationTokenSource();
            // 启动任务A
            var task = Task.Factory.StartNew(() => {
                for (int i = 0; i < 5; i++) {
                    Console.WriteLine($"启动了任务A，当前线程：{Environment.CurrentManagedThreadId}，{DateTime.Now}，执行时间需要5秒");
                    Thread.Sleep(1000);
                }
                
            })
            // 任务A执行完，继续执行任务B
            .ContinueWith(t => {
                Console.WriteLine($"任务B，当前线程：{Environment.CurrentManagedThreadId},我是延续任务");
            }, source.Token);

            //Thread.Sleep(3000);
            //// 暂停三秒后取消任务B的执行，任务A仍然会执行完成
            //source.Cancel();

            source.CancelAfter(3000); // 在3秒后执行取消操作
            //取消回调
            source.Token.Register(() => {
                Console.WriteLine("取消啦，我是回调函数！");
            });

            Console.WriteLine("主线程要取消你了...");
            Console.ReadLine();
        }

    }
}
