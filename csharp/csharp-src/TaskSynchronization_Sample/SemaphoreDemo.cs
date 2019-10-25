using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class SemaphoreDemo
    {
        static void TaskMain(SemaphoreSlim semaphore)
        {
            bool isCompleted = false;
            while (!isCompleted)
            {
                //锁定信号量，定义最长等待时间为600毫秒
                if (semaphore.Wait(600))
                {
                    try
                    {
                        Console.WriteLine($"Task {Task.CurrentId} locks the semaphore");
                        Task.Delay(2000).Wait();
                    }
                    finally
                    {
                        Console.WriteLine($"Task {Task.CurrentId} releases the semaphore");
                        semaphore.Release();
                        isCompleted = true;
                    } 
                }
                else{
                    Console.WriteLine($"Timeout for task {Task.CurrentId}; wait again");
                }
            }
        }

        public static void Run()
        {
            int taskCount = 6;
            int semaphoreCount = 3;
            //创建计数为3的信号量
            //该构造函数第一个参数表示最初释放的锁定量，第二个参数定义了锁定个数的计数
            var semaphore = new SemaphoreSlim(semaphoreCount, semaphoreCount);
            var tasks = new Task[taskCount];
            for(int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(()=>TaskMain(semaphore));
            }

            Task.WaitAll(tasks);
            Console.WriteLine("All tasks finished");
            
            
        }
    }
}
