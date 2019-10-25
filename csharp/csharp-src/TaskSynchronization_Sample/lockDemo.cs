using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class lockDemo
    {
        
        public static void Run()
        {
            int numTasks = 20;
            //在循环外声明一个SharedState实例，所有的Task都将接收该实例对象
            var state = new SharedState();
            //声明Task数组
            var tasks = new Task[numTasks];
            for(int i = 0; i < numTasks; i++)
            {
                //传入共用的SharedState实例
                tasks[i] = Task.Run(() => new Job(state).DoTheJob());
            }
            //等待所有任务的执行
            Task.WaitAll(tasks);
            Console.WriteLine("结果："+state.State);
        }

        
        public void InterlockedRun()
        {
            
            
 
        }
    }
}
