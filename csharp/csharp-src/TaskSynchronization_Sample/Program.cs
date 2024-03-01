using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 模拟死锁问题
            //new SampleTask().RaceConditions();

            //var state1 = new StateObject();
            //var state2 = new StateObject();
            //new Task(new SampleTask(state1, state2).Deadlock1).Start();
            //new Task(new SampleTask(state1, state2).Deadlock2).Start();
            #endregion

            //lock语句的使用
            //lockDemo.Run();


            //WaitHandleDemo.Run();

            


            Console.WriteLine("-----程序执行完毕-----");
            Console.Read();

        }

    }
}
