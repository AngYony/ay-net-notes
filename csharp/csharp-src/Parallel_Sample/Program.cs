using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Parallel.For调用
            //ParalleDemo.ParallelFor2();
            #endregion

            #region 提前停止Parallel.For
            //ParalleDemo.StopParallelForEarly();
            #endregion

            #region Parallel.For的初始化
            //ParalleDemo.ParallelForWithInit();
            #endregion

            #region 使用Parallel.ForEach()方法循环
            //ParalleDemo.ParallelForEach();
            #endregion


            #region 通过Parallel.Invoke()方法调用多个方法
            //ParalleDemo.ParallelInvoke();
            #endregion

            #region
            //TaskDemo.TaskMethod("AAAA");

            #endregion
            
            #region 不同方式创建任务
            //TaskDemo.TasksUsingThreadPool();
            #endregion

            #region 同步任务
            //TaskDemo.RunSynchronousTask();
            #endregion

            #region 使用单独线程的任务
            //TaskDemo.LongRunningTask();
            #endregion

            #region 任务的结果
            //TaskDemo.TaskWithResultDemo();
            #endregion

            #region 执行连续的任务
            //TaskDemo.ContinuationTasks();
            #endregion

            #region
            //TaskDemo.ParentAndChild();
            #endregion

            #region Parallel.For的取消
            //CancelTaskDemo.CancelParallelFor();
            #endregion

            #region Task的取消
            //CancelTaskDemo.CancelTask();
            #endregion

            #region 数据流 
            //TPLDataFlowDemo.ActionBlockRun();
            #endregion

            #region BufferBlock 简单示例
            //TPLDataFlowDemo.BufferBlockRun();
            #endregion

            #region 连接块
            TPLDataFlowDemo.TransformBlockRun();
            #endregion

            #region
            #endregion

            #region
            #endregion

            #region
            #endregion

            #region
            #endregion

            #region
            #endregion

            #region
            #endregion

            #region
            #endregion
            Console.WriteLine("-----程序执行完毕-----");
            Console.Read();

        }
    }
}
