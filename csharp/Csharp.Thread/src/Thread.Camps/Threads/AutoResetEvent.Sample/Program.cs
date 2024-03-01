namespace AutoResetEvents.Sample
{
    /*
     * 代码来源：《C# 7.0 核心技术指南》中的22.5.1章节。
     */

    internal class Program
    {
        private static void Main(string[] args)
        {
            TwoWaySignaling();
            Console.ReadLine();
        }

        /// <summary>
        /// 简单示例
        /// </summary>
        private static void Sample1()
        {
            EventWaitHandle _waitHandle = new AutoResetEvent(false);
            new Thread(() =>
            {
                Console.WriteLine("等待中...");
                _waitHandle.WaitOne();                // Wait for notification
                Console.WriteLine("收到通知");
            }).Start();

            //Thread.Sleep(1000);                  // Pause for a second...
            Console.WriteLine("按下回车键发送通知");
            Console.ReadLine();
            _waitHandle.Set();
            Console.ReadLine();
        }

        /// <summary>
        /// 双向通知，主线程等待工作线程准备就绪之后再发送信号
        /// </summary>
        private static void TwoWaySignaling()
        {
            EventWaitHandle _ready = new AutoResetEvent(false);
            EventWaitHandle _go = new AutoResetEvent(false);
            object _locker = new object();
            string _message = "";

            new Thread(() =>
            {
                while (true)
                {
                    //表示我们已经准备好了
                    _ready.Set();
                    //等待通知
                    _go.WaitOne();
                    lock (_locker)
                    {
                        //使用null消息来停止工作线程的运行
                        if (_message == null) return;
                        Console.WriteLine(_message);
                    }
                }
            }).Start();
            //先等到工人准备好
            _ready.WaitOne();
            lock (_locker) _message = "ooo";
            _go.Set();

            _ready.WaitOne();
            lock (_locker) _message = "ahhh";  // Give the worker another message
            _go.Set();

            _ready.WaitOne();
            lock (_locker) _message = null;    // Signal the worker to exit
            _go.Set();
        }
    }
}