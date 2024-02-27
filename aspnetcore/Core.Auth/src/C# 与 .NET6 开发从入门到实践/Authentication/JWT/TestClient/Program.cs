using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TestClient;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();

        var response = await client.GetAsync("https://localhost:7210/api/WeatherForecast");
        // API会返回401未授权响应
        Console.WriteLine($"{response.StatusCode}{Environment.NewLine}{await response.Content.ReadAsStringAsync()}");

        // 申请令牌
        response = await client.PostAsync("https://localhost:7047/api/Authentication/Login", JsonContent.Create(new { username = "bob", password = "123456" }));
        var token = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"token : {token}{Environment.NewLine}");

        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7210/api/WeatherForecast");
        // 把令牌添加到请求标头
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // 现在可以成功请求API
        response = await client.SendAsync(request);
        Console.WriteLine($"{response.StatusCode}{Environment.NewLine}{await response.Content.ReadAsStringAsync()}");

        Console.ReadKey();
    }
}
