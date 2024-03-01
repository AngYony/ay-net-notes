namespace Spin.Sample
{
    /*
     * 代码来源：https://threads.whuanle.cn/2.thread_sync/10.spinwait.html
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                for (int i = 0; i < 1000_0000; i++)
                {
                    sum++;
                }
                isCompleted = true;

            }).Start();

            // 等待上面的线程完成工作
            MySleep();
            Console.WriteLine("sum = " + sum);
            Console.ReadKey();
        }

        private static int sum = 0;
        
        //被多个线程访问的变量
        private static bool isCompleted = false;
        private static void MySleep()
        {
            SpinWait wait = new SpinWait();
            while (!isCompleted)
            {
                wait.SpinOnce();
            }
        }
    }
}
