namespace ThreadPools.Sample
{
   public  class Program
    {
        static void Main(string[] args)
        {
            GetThreadPoolInfo();
        }

        private static void GetThreadPoolInfo()
        {
            // 不断加入任务
            for (int i = 0; i < 8; i++)
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("");
                });
            for (int i = 0; i < 8; i++)
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine("");
                });

            Console.WriteLine("     此计算机处理器数量：" + Environment.ProcessorCount);

            // 工作项、任务代表同一个意思
            Console.WriteLine("     当前线程池存在线程数：" + ThreadPool.ThreadCount);
            Console.WriteLine("     当前已处理的工作项数：" + ThreadPool.CompletedWorkItemCount);
            Console.WriteLine("     当前已加入处理队列的工作项数：" + ThreadPool.PendingWorkItemCount);
            int count;
            int ioCount;
            ThreadPool.GetMinThreads(out count, out ioCount);
            Console.WriteLine($"     默认最小辅助线程数：{count}，默认最小异步IO线程数：{ioCount}");

            ThreadPool.GetMaxThreads(out count, out ioCount);
            Console.WriteLine($"     默认最大辅助线程数：{count}，默认最大异步IO线程数：{ioCount}");
            Console.ReadKey();
        }
    }
}
