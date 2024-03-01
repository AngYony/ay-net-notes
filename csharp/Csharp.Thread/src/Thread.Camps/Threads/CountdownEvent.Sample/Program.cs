namespace CountdownEvents.Sample
{
    /*
    * 代码来源：《C# 7.0 核心技术指南》中的22.5.3章节。
    */
    internal class Program
    {
        //等待三个线程
        static CountdownEvent _countdown = new CountdownEvent(3);
        static void Main(string[] args)
        {
            new Thread(SaySomething).Start("第一个线程");
            new Thread(SaySomething).Start("第二个线程");
            new Thread(SaySomething).Start("第三个线程");
            //等待CountdownEvent的计数清零
            _countdown.Wait();   // Blocks until Signal has been called 3 times
            Console.WriteLine("所有的线程都结束运行!");
            Console.ReadLine();
        }
        static void SaySomething(object thing)
        {
            Console.WriteLine(thing);
            Console.ReadLine();
            Console.WriteLine(thing + "按下回车键结束运行");
            // 通知可以减少一个线程的等待了将CountdownEvent计数减一
            _countdown.Signal();
        }
    }
}
