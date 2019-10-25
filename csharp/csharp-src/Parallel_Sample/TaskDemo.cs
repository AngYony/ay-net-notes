using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Sample
{
    class TaskDemo
    {

        private static object s_logLock = new object();
        public static void Log(string title)
        {
            //使用lock可以让其他方法并行调用Log()，避免lock内的代码被多个线程或任务交叉调用
            lock (s_logLock)
            {
                Console.WriteLine(title);
                Console.WriteLine($"Task ID：{Task.CurrentId?.ToString() ?? "没有Task存在"}，Thread ID：{ Thread.CurrentThread.ManagedThreadId}");
                //如果不是.NET Core 1.0运行库就执行该语句
                #if (!DNXCORE)
                    Console.WriteLine("是否是线程池线程：" + Thread.CurrentThread.IsThreadPoolThread);
                #endif
                Console.WriteLine("是否是后台线程：" + Thread.CurrentThread.IsBackground);
                Console.WriteLine();
            }
        }

        public static void TaskMethod(object obj)
        {
            Log(obj?.ToString());
        }

        //不同版本创建Task
        public static void TasksUsingThreadPool()
        {
            Task t1 = new TaskFactory().StartNew(TaskMethod, "使用TaskFactory实例化形式创建任务");
            Task t2 = Task.Factory.StartNew(TaskMethod, "使用Task的Factory属性形式创建任务");
            Task t3 = new Task(TaskMethod, "使用Task构造函数并调用实例的Start()形式创建任务");
            t3.Start();
            Task t4 = Task.Run(() => { TaskMethod("使用Task.Run()形式创建任务"); });
        }


        public static void RunSynchronousTask()
        {
            TaskMethod("主线程运行");
            var t1 = new Task(TaskMethod, "同步调用");
            //同步运行Task
            t1.RunSynchronously();
        }


        public static void LongRunningTask()
        {
            var t1 = new Task(TaskMethod, "long runing", TaskCreationOptions.LongRunning);
            t1.Start();
        }

        //为了给Task构造函数传递Func<object, TResult>参数，所以此处方法参数定义为Object类型
        private static Tuple<int, int> TaskWithResult(object division)
        {
            Tuple<int, int> div = (Tuple<int, int>)division;
            int result = div.Item1 / div.Item2;
            int reminder = div.Item1 % div.Item2;
            Console.WriteLine("任务创建了一个结果...");
            return Tuple.Create(result, reminder);
        }


        public static void TaskWithResultDemo()
        {
            var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create(7, 3));
            t1.Start();
            Console.WriteLine(t1.Result);
            t1.Wait();
            Console.WriteLine($"result from task:{t1.Result.Item1} {t1.Result.Item2}");
        }


        //连续的任务
        private static void DoOnFirst()
        {
            Console.WriteLine("处理多个连续的任务，TaskID：" + Task.CurrentId);
            Task.Delay(3000).Wait();
        }

        private static void DoOnSecond(Task t)
        {
            Console.WriteLine($"该任务已经完成，TaskID： {t.Id} ");
            Console.WriteLine("当前TaskID： " + Task.CurrentId);
            Console.WriteLine("模拟清理");
            Task.Delay(3000).Wait();
        }
        //任务如果出现异常，需要执行的任务
        private static void DoOnError(Task obj)
        {
            throw new NotImplementedException();
        }
        public static void ContinuationTasks()
        {
            Task t1 = new Task(DoOnFirst);
            Task t2 = t1.ContinueWith(DoOnSecond);
            //模拟启动多个任务
            Task t3 = t1.ContinueWith(DoOnSecond);
            //连续任务启动另一个连续任务
            Task t4 = t2.ContinueWith(DoOnSecond);
            Task t5 = t1.ContinueWith(DoOnError, TaskContinuationOptions.OnlyOnFaulted);
            t1.Start();
        }



        private static void ParentTask()
        {
            Console.WriteLine("task id " + Task.CurrentId);
            var child = new Task(ChildTask);
            child.Start();
            Task.Delay(1000).Wait();
            Console.WriteLine("父任务执行结束");
            
        }

        private static void ChildTask()
        {
            Console.WriteLine("开始执行子任务方法");
            Task.Delay(5000).Wait();
            Console.WriteLine("子任务方法执行结束");
        }

        //任务层次结构
        public static void ParentAndChild()
        {
            var parent = new Task(ParentTask);
            parent.Start();
            Task.Delay(2000).Wait();
            Console.WriteLine(parent.Status);
            Task.Delay(4000).Wait();
            Console.WriteLine(parent.Status);
        }

        //从方法中返回任务
        public Task<IEnumerable<string>> TaskMethodAsync()
        {
            return Task.FromResult<IEnumerable<string>>(
                new List<string> { "one", "two" });
        }


    }
}
