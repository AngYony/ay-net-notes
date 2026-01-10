using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AY.Utils.Http
{
    public static class HttpService
    {
        private static readonly Lazy<HttpClient> _lazyClient = new Lazy<HttpClient>(() =>
        {
            var handler = new SocketsHttpHandler
            {
                // ★ 连接复用配置（避免 DNS 过期）
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),

                // ★ 自动 GZip/Deflate/Brotli 压缩
                AutomaticDecompression =
                    DecompressionMethods.GZip |
                    DecompressionMethods.Deflate |
                    DecompressionMethods.Brotli,

                // ★ 并发限制
                MaxConnectionsPerServer = 20,

                // ★ 代理（如不需要，可关闭）
                UseProxy = false
            };

            return new HttpClient(handler)
            {
                Timeout = Timeout.InfiniteTimeSpan // 统一由 CancellationToken 控制
            };
        });

        public static HttpClient Client => _lazyClient.Value;
    }
}
