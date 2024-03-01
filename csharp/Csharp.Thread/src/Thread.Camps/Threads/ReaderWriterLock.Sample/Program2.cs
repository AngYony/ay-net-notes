namespace ReaderWriterLock.Sample
{
    /*
     * 本示例来源于 ：https://threads.whuanle.cn/2.thread_sync/9.reader_writer_lock.html
     */

    internal class Program2
    {
        // 订单模型
        public class DoWorkModel
        {
            public int Id { get; set; }     // 订单号
            public string UserName { get; set; }    // 客户名称
            public DateTime DateTime { get; set; }  // 创建时间
        }

        private static ReaderWriterLockSlim tool = new ReaderWriterLockSlim();   // 读写锁

        private static int MaxId = 1;
        public static List<DoWorkModel> orders = new List<DoWorkModel>();       // 订单表

        // 分页查询订单
        private static DoWorkModel[] DoSelect(int pageNo, int pageSize)
        {
            try
            {
                DoWorkModel[] doWorks;
                // 在读取前使用 EnterReadLock() 获取锁；
                tool.EnterReadLock();
                doWorks = orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToArray();
                return doWorks;
            }
            catch { }
            finally
            {
                // 读取完毕后，使用 ExitReadLock() 释放锁
                tool.ExitReadLock();
            }
            return default;
        }

        // 创建订单
        private static DoWorkModel DoCreate(string userName, DateTime time)
        {
            try
            {
                // 升级
                tool.EnterUpgradeableReadLock();
                try
                {
                    // 获取写入锁
                    tool.EnterWriteLock();

                    // 写入订单，这里没有使用Interlocked.Increment，因为有读写锁的存在，所以操作也是原子性的。
                    MaxId += 1;                         // Interlocked.Increment(ref MaxId);

                    DoWorkModel model = new DoWorkModel
                    {
                        Id = MaxId,
                        UserName = userName,
                        DateTime = time
                    };
                    orders.Add(model);
                    return model;
                }
                catch { }
                finally
                {
                    // 释放写入锁
                    tool.ExitWriteLock();
                }
            }
            catch { }
            finally
            {
                tool.ExitUpgradeableReadLock();         // 降级
            }
            return default;
        }

        private static void Main2(string[] args)
        {
            // 开 5 个线程，不断地读，
            for (int i = 0; i < 5; i++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        var result = DoSelect(1, MaxId);
                        if (result is null)
                        {
                            Console.WriteLine("获取失败");
                            continue;
                        }
                        foreach (var item in result)
                        {
                            Console.Write($"{item.Id}|");
                        }
                        Console.WriteLine("\n");
                        Thread.Sleep(1000);
                    }
                }).Start();
            }

            //开 2 个线程不断地创建订单。
            for (int i = 0; i < 2; i++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        var result = DoCreate((new Random().Next(0, 100)).ToString(), DateTime.Now);      // 模拟生成订单
                        if (result is null)
                            Console.WriteLine("创建失败");
                        else Console.WriteLine("创建成功");
                    }
                }).Start();
            }
        }
    }
}