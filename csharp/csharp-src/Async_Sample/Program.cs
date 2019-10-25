using System;
using System.Diagnostics;

namespace Async_Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("-----开始程序-----");
            //开始计时
            sw.Start();

            #region 同步调用
            //Console.WriteLine(AsyncDemo.Greeting("world"));
            #endregion

            #region 异步调用
            //AsyncDemo.CallerWithAsync();
            #endregion


            #region 使用ContinueWith延续任务
            //AsyncDemo.CallerWithContinuationTask();
            #endregion


            #region 使用await关键字按顺序调用异步方法
            //AsyncDemo.MultipleAsyncMehtods();
            #endregion

            #region 使用Task.WhenAll组合器
            //AsyncDemo.MultipleAsyncMethodsWithCombinators1();
            #endregion

            #region 改写
            //AsyncDemo.ConvertingAsyncPattern();
            #endregion

            //ErrorHanding.ThrowAfter2(3000, "ceshi");

            #region 错误处理
            //ErrorHanding.DontHandle();
            #endregion

            #region 多个异步方法错误处理
            //ErrorHanding.StartTwoTask();
            #endregion

            #region 多个异步方法并行调用错误处理
            ErrorHanding.StartTwoTaskParallel();
            #endregion

            #region 多个异步方法并行调用正确的错误处理
            //ErrorHanding.ShowAggregatedException();
            #endregion


            Console.WriteLine("总执行时间：" + sw.Elapsed.Seconds + "秒");
            sw.Stop();

            Console.WriteLine("-----结束程序-----");

            Console.Read();
        }

        
    }
}