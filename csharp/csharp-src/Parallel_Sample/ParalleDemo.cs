using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Sample
{
    class ParalleDemo
    {
        public static void log(string prefix)
        {
            Console.WriteLine($"{prefix} \t任务ID: {Task.CurrentId}\t线程ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }

        public static void ParallelFor()
        {
            ParallelLoopResult result = Parallel.For(0, 10,
                i =>
                {
                    log("开始：" + i);
                    //这行代码为了创建更多的线程
                    Task.Delay(1000).Wait();
                    log("结束：" + i);
                });
            Console.WriteLine("循环是否结束:" + result.IsCompleted);
        }

        //使用await关键字进行改写
        public static void ParallelFor2()
        {
            ParallelLoopResult result = Parallel.For(0, 10,
                async i =>
                {
                    log("开始：" + i);
                    await Task.Delay(1000);
                    log("结束：" + i);
                });
            Console.WriteLine("循环是否结束:" + result.IsCompleted);
        }

        //提前停止Parallel.For
        public static void StopParallelForEarly()
        {
            ParallelLoopResult result = Parallel.For(0, 10,
                (int i, ParallelLoopState pls) =>
                {
                    
                    log("开始：" + i);
                    if (i > 5)
                    {
                        pls.Break();
                        log("中断：" + i);
                    }
                    Task.Delay(5000).Wait();
                    log("结束：" + i);
                });

            Console.WriteLine("循环是否完成运行：" + result.IsCompleted);
            Console.WriteLine("最小迭代索引：" + result.LowestBreakIteration);
        }

        //Parallel.For的初始化
        public static void ParallelForWithInit()
        {
            Parallel.For<string>(0, 10, () =>
            {
                log("初始化线程");
                return "线程ID：" + Thread.CurrentThread.ManagedThreadId;
            },
            (i, pls, str1) =>
            {
                log($"迭代调用：{i} ，字符串：{str1}");
                Task.Delay(5000).Wait();
                return "i " + i;
            },
            (str1) =>
            {
                log("finally " + str1);
            });

        }

        //使用Parallel.ForEach()方法循环
        public static void ParallelForEach()
        {
            string[] data = { "zero", "one", "two", "three", "four",
                "five", "six", "seven", "eight", "nine",
                "ten", "eleven", "twelve" };

            ParallelLoopResult result = Parallel.ForEach<string>(
                data, s => { Console.WriteLine(s); });
            //如果需要终端循环，可以使用重载版本和ParallelLoopState参数
            ParallelLoopResult result2 = Parallel.ForEach<string>(
                data, (s, pls, l) => { Console.WriteLine(s); });
        }

        //通过Parallel.Invoke()方法调用多个方法
        public static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar);
        }
        public static void Foo()
        {
            Console.WriteLine("Foo");
        }
        public static void Bar()
        {
            Console.WriteLine("Bar");
        }
    }
}
