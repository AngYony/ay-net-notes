namespace Semaphore.Sample
{
    /*
    * 本示例来源于《C#7.0 核心技术指南》的22.4.1章节。
    * 5个线程视图进入资源池，但最多只允许3个线程同时进入
    */
    internal class Program
    {
        static SemaphoreSlim _sem = new SemaphoreSlim(3);


        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                new Thread((a) =>
                {
                    Console.WriteLine(a + "想要进入资源池");
                    //阻塞当前线程，直到收到信号获取到进入权限
                    _sem.Wait();
                    Console.WriteLine(a + "已经进入");
                    Thread.Sleep((int)a * 1000);
                    Console.WriteLine(a + "正在离开");
                    //可以将此行代码注释看看效果
                    _sem.Release();
                }).Start(i);
            }
        }
    }
}
