namespace Barriers.Sample
{
    /*
    * 代码来源：《C# 7.0 核心技术指南》中的22.6章节。
    * 3个线程都会打印从0到4的数字，并与其他线程保持步调一致
    */
    internal class Program
    {
        static Barrier _barrier = new Barrier(3, barrier => Console.WriteLine());
        static void Speak()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write(i + " ");
                _barrier.SignalAndWait();
            }
        }
        static void Main(string[] args)
        {
            new Thread(Speak).Start();
            new Thread(Speak).Start();
            new Thread(Speak).Start();
        }
    }
}
