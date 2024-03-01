using System;
using System.Threading.Tasks;

namespace S2
{
    internal interface IMyAsyncInterface
    {
        Task<int> GetValueAsync();
    }

    public class FromResultTest : IMyAsyncInterface
    {
        //该方法是一个没有使用async关键字作为声明的方法，因此是同步方法
        //只是返回的是具有异步特性的Task<int>
        public Task<int> GetValueAsync()
        {
            return Task.FromResult(1);
        }
        private static Task<T> NotImplementedAsync<T>()
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetException(new NotImplementedException());
            return tcs.Task;
        }
    }
}