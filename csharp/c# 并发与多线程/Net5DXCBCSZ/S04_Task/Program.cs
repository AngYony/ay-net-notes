using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_Task
{
    class Program
    {
        static void Main(string[] args)
        {


            //Thread串行（顺序执行）
            //Thread父子关系，子Thread执行完之后，才能结束父Thread。
            //Thread编码

            // 任务之间如何串行、并行、嵌套、父子

            // 1.使用构成函数实例化一个task
            //var task = new Task(() =>
            //{
            //    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            //});
            //task.Start();
            //Console.WriteLine($"task.id={task.Id}，task.status={task.Status}");

            //task.Wait();
            //Console.WriteLine($"task.id={task.Id}，task.status={task.Status}");




            //var task2 = new Task((obj) =>
            //{
            //    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}，传入的值为：{obj}");
            //}, "张三");
            //// 同步运行，只有任务运行完成之后，才会输出后续内容
            //task2.RunSynchronously(); // 等待任务的完成，类似于wait操作

            //Console.WriteLine($"task.id={task2.Id}，task.status={task2.Status}");



            //var task3 = Task.Factory.StartNew(()=> {
            //    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            //});
            //task3.Wait();
            //Console.WriteLine($"task.id={task3.Id}，task.status={task3.Status}");


            //var task4 = Task.Factory.StartNew((obj) => {
            //    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}，传入的值为：{obj}");
            //},"张三");
            //Console.WriteLine($"task.id={task4.Id}，task.status={task4.Status}");


            //var task5 = Task.Run(()=> {
            //    Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}");
            //});

            Console.WriteLine("开始");
            var task6 = Task.Factory.StartNew(()=> {
                Thread.Sleep(1000 * 3);
                return "张三";
            });
            Console.WriteLine($"task.id={task6.Id}，task.status={task6.Status}");
            // 不建议使用Result，有可能造成死锁
            Console.WriteLine(task6.Result); // 相当于wait
            Console.WriteLine($"task.id={task6.Id}，task.status={task6.Status}");
            Console.WriteLine("主线程执行");

            Console.ReadLine();



        }
    }
}
