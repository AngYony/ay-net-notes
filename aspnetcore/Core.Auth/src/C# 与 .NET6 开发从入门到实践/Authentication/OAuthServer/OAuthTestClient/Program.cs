using OpenIddict.Abstractions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace OAuthTestClient;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();

        try
        {
            // 申请访问令牌
            var token = await GetTokenAsync(client);
            Console.WriteLine("Access token: {0}", token);
            Console.WriteLine();

            // 请求API
            var resource = await GetResourceAsync(client, token);
            Console.WriteLine("API response: {0}", resource);
            Console.ReadLine();
        }
        catch (HttpRequestException exception)
        {
            var builder = new StringBuilder();
            builder.AppendLine("+++++++++++++++++++++");
            builder.AppendLine(exception.Message);
            builder.AppendLine(exception.InnerException?.Message);
            builder.AppendLine("请确保授权服务器已经启动。");
            builder.AppendLine("+++++++++++++++++++++");
            Console.WriteLine(builder.ToString());
        }

        static async Task<string> GetTokenAsync(HttpClient client)
        {
            // 向令牌端点申请令牌
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:<授权服务器端口>/connect/token");
            // 准备申请表单
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                // 使用客户端证书模式
                ["grant_type"] = "client_credentials",
                // 在OAuth服务备案过的客户端信息
                ["client_id"] = "console",
                ["client_secret"] = "388D45FA-B36B-4988-BA59-B187D329C207"
            });

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            var payload = await response.Content.ReadFromJsonAsync<OpenIddictResponse>();

            if (!string.IsNullOrEmpty(payload.Error))
            {
                throw new InvalidOperationException("接收访问令牌时发生错误");
            }

            return payload.AccessToken;
        }

        static async Task<string> GetResourceAsync(HttpClient client, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:<API服务的端口>/Test ");
            // 把令牌添加到请求的身份验证标头
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
