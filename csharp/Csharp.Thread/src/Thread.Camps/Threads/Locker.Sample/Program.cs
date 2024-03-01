namespace Lockers.Sample
{
    /*
     * 代码来源：《C#高级编程》
     */
    internal class Program
    {
        class SharedState
        {
            public int State { get; set; }
        }
        class Job
        {
            private SharedState _sharedState;
            public Job(SharedState sharedState)
            {
                this._sharedState = sharedState;
            }
            public void DoTheJob()
            {
                for (int i = 0; i < 50000; i++)
                {
                    lock (_sharedState)
                    {
                        _sharedState.State += 1;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int numTasks = 20;
            //在循环外声明一个SharedState实例，所有的Task都将接收该实例对象
            var state = new SharedState();
            //声明Task数组
            var tasks = new Task[numTasks];
            for (int i = 0; i < numTasks; i++)
            {
                //传入共用的SharedState实例
                tasks[i] = Task.Run(() => new Job(state).DoTheJob());
            }
            //等待所有任务的执行
            Task.WaitAll(tasks);
            Console.WriteLine("结果：" + state.State);
        }
    }
}
