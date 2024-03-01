using System;
using System.Threading;
using System.Threading.Tasks;

namespace S05_TaskSharp {

    internal class TaskSample1 {

        public static void SayA() {
            Console.WriteLine("A开始执行");
            Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            Thread.Sleep(3000);
            Console.WriteLine("A执行完毕");
        }

        public static void SayB() {
            Console.WriteLine("B开始执行");
            Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine("B执行完毕");
        }

        public static void SayC() {
            Console.WriteLine("C开始执行");
            Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine("C执行完毕");
        }


        // 串行化Task
        public static void ShunXu() {
            Console.WriteLine("串行执行任务");
            var task = Task.Factory.StartNew(() => {
                SayA();
            }).ContinueWith(t => {
                SayB();
            }).ContinueWith(t => {
                SayC();
            });

            task.Wait();
            Console.WriteLine("全部执行完成!");
            Console.Read();
        }


        // 并行执行A和B，执行完成之后再执行C
        public static void BingXing() {
            var tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() => {
                SayA();
            });
            tasks[1] = Task.Factory.StartNew(() => {
                SayB();
            });
            Task.WhenAll(tasks).ContinueWith(t => {
                SayC();
            }).Wait();
            Console.WriteLine("全部执行完成!");
            Console.Read();
        }

        public static void SayChildA() {
            Console.WriteLine("ChildA开始执行");
            Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine("ChildA执行完毕");
        }
        public static void SayChildB() {
            Console.WriteLine("ChildB开始执行");
            Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            Thread.Sleep(3000);
            Console.WriteLine("ChildB执行完毕");
        }

        //一个父Task包含2个子Task，子任务不执行完，父级任务是不能结束的
        public static void FuZiTask() {
            var parentTask = Task.Factory.StartNew(() => {
                // 子task1
                var child1Task = Task.Factory.StartNew(() => {
                    SayChildA();
                }, TaskCreationOptions.AttachedToParent);

                var child2Task = Task.Factory.StartNew(() => {
                    SayChildB();
                }, TaskCreationOptions.AttachedToParent);
            });

            parentTask.ContinueWith(t => {
                SayB();
            }).Wait();

            Console.WriteLine("全部执行完成!");
            Console.Read();
        }

    }
}