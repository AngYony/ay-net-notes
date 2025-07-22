using System;
using System.Threading;
using System.Threading.Tasks;

namespace xbd.s7netplus.Internal
{
    internal class TaskQueue
    {
        private static readonly object Sentinel = new object();

        private Task prev = Task.FromResult(Sentinel);

        public async Task<T> Enqueue<T>(Func<Task<T>> action)
        {
            var tcs = new TaskCompletionSource<object>();
            await Interlocked.Exchange(ref prev, tcs.Task).ConfigureAwait(false);

            try
            {
                return await action.Invoke().ConfigureAwait(false);
            }
            finally
            {
                tcs.SetResult(Sentinel);
            }
        }
    }
}