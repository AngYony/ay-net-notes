namespace ReaderWriterLock.Sample
{
    /*
     * 本示例来源于《C#7.0 核心技术指南》的22.4.2章节。
     * 三个线程将持续读取列表中的元素，另外两个线程每隔100毫秒生成一个随机数写入到列表中
     */
    internal class Program
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static List<int> _items = new List<int>();
        static Random _rand = new Random();

        static void Read()
        {
            while (true)
            {
                _rw.EnterReadLock();
                foreach (int i in _items) Thread.Sleep(10);
                _rw.ExitReadLock();
            }
        }

        static void Write(object threadID)
        {
            while (true)
            {
                int newNumber = GetRandNum(100);
                //升级锁，实现在一个原子操作中奖读锁转换为写锁
                _rw.EnterUpgradeableReadLock();
                //获取写入锁
                _rw.EnterWriteLock();
                _items.Add(newNumber);
                //释放写入锁
                _rw.ExitWriteLock();
                Console.WriteLine("Thread " + threadID + " added " + newNumber);
                //释放升级锁
                _rw.ExitUpgradeableReadLock();
                Thread.Sleep(100);
            }
        }

        static int GetRandNum(int max) { lock (_rand) return _rand.Next(max); }

        private static void Main(string[] args)
        {
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Read).Start();

            new Thread(Write).Start("A");
            new Thread(Write).Start("B");
        }
    }
}