using Polly;
using Polly.Timeout;

namespace AY.Utils.Http
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 自动重试 3 次（指数回退）
        /// </summary>
        private static readonly IAsyncPolicy<HttpResponseMessage> _policy =
            Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: i => TimeSpan.FromMilliseconds(300 * i)
                );

        public static async Task<string> GetStringAsync(string url, CancellationToken token)
        {
            var resp = await _policy.ExecuteAsync(async () =>
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(token);
                cts.CancelAfter(TimeSpan.FromSeconds(15));   // 每次请求最多 15 秒

                var response = await HttpService.Client.GetAsync(url, cts.Token);

                response.EnsureSuccessStatusCode();
                return response;

            });


            return await resp.Content.ReadAsStringAsync();
        }
    }
}
