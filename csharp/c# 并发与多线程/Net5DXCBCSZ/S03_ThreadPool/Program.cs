using System;
using System.Threading;

namespace S03_ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            // 不传递参数
            ThreadPool.QueueUserWorkItem(_=> {
                Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}");
            });

            //传递参数
            ThreadPool.QueueUserWorkItem(obj => {
                Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}，值为：{obj}");
            },"张三");


            //泛型参数
            ThreadPool.QueueUserWorkItem<Student>(stu => {
                Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}，值为：{stu.Name}");
            }, new Student { Name = "张三" }, true);

            Console.WriteLine($"主线程：{Environment.CurrentManagedThreadId}");
            Console.WriteLine("当前线程数量："+ ThreadPool.ThreadCount);

           
            Console.ReadLine();
        }
    }

    class Student{
        public string Name{ get; set; }
    }
}
