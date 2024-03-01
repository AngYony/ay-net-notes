namespace Threads.Sample
{
    /// <summary>
    /// 代码来源：《.NET5多线程编程实战》
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("错误方式一");
            for (int i = 0; i < 10; i++)
                new Thread(() =>
                {
                    int tmp = i;
                    Console.WriteLine($"i = {tmp}");
                }).Start();
            Console.ReadLine();
            Console.WriteLine("错误方式二");
            for (int i = 0; i < 10; i++)
            {
                new Thread(() =>
                {
                    var value = i;
                    Console.WriteLine(value);
                }).Start();
            }

            Console.ReadLine();
            Console.WriteLine("正确方式一");

            for (int i = 0; i <10; i++)
            {
                int tmp = i;
                new Thread(() =>
                {
                    Console.WriteLine($"i = {tmp}");
                }).Start();
            }

            Console.ReadLine();
            Console.WriteLine("最好的写法");
            for (int i = 0; i < 10; i++)
            {
                new Thread((a) =>
                {
                    Console.WriteLine(a + "进入到了资源池");
                }).Start(i);
            }



            // 无参数Thread
            var th = new Thread(() =>
            {
                Console.WriteLine($"当前的线程：{Environment.CurrentManagedThreadId}");
            });
            th.Start();



            // 带参数的Thread
            var pth = new Thread(obj => {
                Thread.Sleep(1000 * 5);
                Console.WriteLine($"当前的线程：{Environment.CurrentManagedThreadId}");
                Console.WriteLine($"接收到的参数值：{obj}");
            });

            // 传入参数到线程内
            pth.Start("wy");



            pth.IsBackground = false;
            // Join的使用
            // 多个线程支持存在交互和等待

            //实现先输出其他线程再输出主线程
            // 阻止调用线程（这里是主线程），直到子线程终止。
            pth.Join(1000 * 3); // 等待3秒，过后依旧执行主线程

            Console.WriteLine($"主线程：{Environment.CurrentManagedThreadId}");
            //Console.ReadLine();

            // IsBackground为true时，表示后台线程，当主线程退出时，该线程也会被终止。
            // IsBackground为false时，表示前台线程，此时如果该前台线程不退出，主线程也无法退出。只有所有的前台线程结束后，主线程才能退出。



            // 不建议直接使用Thread，有时候线程太多，造成上下文切换太过频繁，导致CPU爆高。
            // 太多的线程会造成GC负担过大，托管堆很多“死thread”。


            //Task建立在ThreadPool之上，做了封装



        }
    }
}
