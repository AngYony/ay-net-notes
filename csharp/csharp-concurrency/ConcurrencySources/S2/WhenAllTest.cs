using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace S2
{
    public class WhenAllTest
    {
        //一般调用
        private static async Task Run1()
        {
            Task t1 = Task.Delay(TimeSpan.FromSeconds(1));
            Task t2 = Task.Delay(TimeSpan.FromSeconds(2));
            Task t3 = Task.Delay(TimeSpan.FromSeconds(3));

            await Task.WhenAll(t1, t2, t3);
        }

        //返回存有每个任务执行结果的数组
        private static async Task<int[]> Run2()
        {
            //注意：Task<T>派生自Task，因此下述代码不会报错
            Task t0 = Task.FromResult(0);
            //但要通过WhenAll方法返回执行的结果时，必须使用Task<T>来声明变量
            Task<int> t1 = Task.FromResult(1);
            Task<int> t2 = Task.FromResult(2);
            Task<int> t3 = Task.FromResult(3);
            //使用Task.WhenAll返回每个任务的结果
            int[] result = await Task.WhenAll(t1, t2, t3);
            return result;
        }

        //Task.WhenAll()带有IEnumerable类型参数的方法的使用
        private static async Task<string> DownloadAllAsync(IEnumerable<string> urls)
        {
            var httpClient = new HttpClient();

            //定义每一个url的使用方法
            var downloads = urls.Select(url => httpClient.GetStringAsync(url));
            //注意：到这里，Select具有延迟执行，序列还没有计算求值，所以所有任务都还没有真正启动。

            //下面，所有的URL下载同步开始
            Task<string>[] downloadTasks = downloads.ToArray();
            //到这里，所有的任务已经开始执行了

            //用异步方式等待所有下载完成
            string[] htmlPages = await Task.WhenAll(downloadTasks);

            return string.Concat(htmlPages);
        }

        private static async Task ThrowNotImplementedExceptionAsync()
        {
            throw new NotImplementedException();
        }

        private static async Task ThrowInvalidOperationExceptionAsync()
        {
            throw new InvalidOperationException();
        }

        private static async Task ObserveOneExceptionAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            try
            {
                //这里使用了await，就只会抛出其中的一个异常
                await Task.WhenAll(task1, task2);
            }
            catch (Exception ex)
            {
                //ex要么是NotImplementedException，要么是InvalidOperationException
            }
        }

        private static async Task ObserveAllExceptionsAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            Task allTasks = Task.WhenAll(task1, task2);

            try
            {
                await allTasks;
            }
            catch (Exception ex)
            {
                AggregateException allException = allTasks.Exception;
            }
        }
    }
}