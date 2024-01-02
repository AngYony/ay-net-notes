# ASP.NET Core Web Server —— Kestrel

Kestrel是一个跨平台的适用于ASP.NET Core的Web Server，默认包含在ASP.NET Core项目模板中，Kestrel支持以下这些功能：

- HTTPS
- 用于启用WebSocket的不透明升级（通过WebSocket实现应用的不公开升级）
- 用于获得Nginx高性能的Unix套接字
- HTTP/2（macOS未来版本将支持）

.NET Core支持的所有平台和版本均支持Kestrel。



## HTTP/2支持

HTTP/2：Hypertext Transfer Protocol Version 2 ，即超文本传输协议版本2，简称为HTTP/2，关于HTTP/2的详细介绍可参阅：https://httpwg.org/specs/rfc7540.html

如果满足以下基本要求，Kestrel将为ASP.NET Core应用提供HTTP/2的支持：

- 操作系统：Windows 10+、Windows Server2016+、具有 OpenSSL 1.0.2 或更高版本的 Linux（例如，Ubuntu 16.04 或更高版本）
- 目标框架：.NET Core 2.2或更高版本
- 应用程序层协议协商 (ALPN) 连接
- TLS 1.2 或更高版本的连接

注意：Kestrel对HTTP/2的支持受操作系统的影响，如果已建立HTTP/2连接，HttpRequest.Protocol会报告HTTP/2，默认情况下，禁用HTTP/2，可通过Kestrel选项中的ListenOptions.Protocols进行启动（Kestrel选项将在下文进行讲解）。



## Kestrel和反向代理

反向代理（Reverse Proxy）方式是指以代理服务器来接受internet上的连接请求，然后将请求转发给内部网络上的服务器，并将从服务器上得到的结果返回给internet上请求连接的客户端，此时代理服务器对外就表现为一个反向代理服务器。

可以单独将Kestrel作为边缘服务器使用，处理来自网络（包括Internet）的请求。

![kestrel-to-internet2](assets/kestrel-to-internet2.png)

也可以将Kestrel与反向代理服务器（如IIS、Nginx或Apache）结合使用。反向代理服务器接收来自 Internet 的 HTTP 请求，并在进行一些初步处理后将这些请求转发到 Kestrel。

![kestrel-to-internet2](assets/kestrel-to-internet.png)



## 何时结合使用Kestrel和反向代理

首先确定一点，无论使用或不使用反向代理服务器进行配置，对 ASP.NET Core 2.0 或更高版本的应用来说都是有效且受支持的托管配置。

当在单个服务器上运行多个应用，这些应用共享相同的IP和端口时，此时就需要一个反向代理方案。而Kestrel不支持这种方案，因为Kestrel不支持在多个应用的不同进程之间共享相同的IP和端口。如果将Kestrel配置为侦听某个端口，Kestrel会无视请求的主机标头，而处理该端口的所有流量。为了支持这种方案，结合可以共享端口的反向代理服务器，就能够实现在唯一的IP和端口上将请求转发至Kestrel。

即使在不需要使用反向代理服务器的情况下，使用反向代理服务器也是一个不错的选择：

- 它可以限制所承载的应用中的公开的公共外围应用。
- 它可以提供额外的配置和防护层。
- 它可以更好地与现有基础结构集成。
- 它可以简化负载均衡和 SSL 配置。 仅反向代理服务器需要 SSL 证书，并且该服务器可使用普通 HTTP 在内部网络上与应用服务器通信。



## 调用UseKestrel()在ASP.NET Core 应用中使用 Kestrel

默认情况下，ASP.NET Core项目模板使用Kestrel。在Program.cs中，如果调用CreateDefaultBuilder方法，该方法的内部会调用UseKestrel方法，如果要在调用CreateDefaultBuilder后提供Kestrel的其他配置，可以再次调用UseKestrel方法：

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel(options =>
        {
            // Set properties and call methods on options
        });
```



## Kestrel选项

Kestrel Web Server具有约束配置选项，这些选项在面向Internet的部署中尤其重要。

可在 KestrelServerOptions 类的 Limits 属性上设置约束。 Limits 属性包含 KestrelServerLimits 类的实例。

### 设置客户端最大连接数

使用MaxConcurrentConnections或MaxConcurrentUpgradedConnections进行设置。

使用MaxConcurrentConnections为整个应用设置并发打开的最大TCP连接数：

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel(options =>
        {
            options.Limits.MaxConcurrentConnections = 100;
        });
```

对于已从 HTTP 或 HTTPS 升级到另一个协议（例如，Websocket 请求）的连接，有一个单独的限制。 连接升级后，不会计入 MaxConcurrentConnections 限制。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel(options =>
        {
            options.Limits.MaxConcurrentUpgradedConnections = 100;
        });
```

默认情况下，最大连接数不受限制 (NULL)。

### 设置请求正文的最大大小

默认的请求正文最大大小为 30,000,000 字节，大约 28.6 MB，可以使用MaxRequestBodySize设置请求正文的最大值：

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = 10 * 1024;
        });
```

上述代码将会对应用上的每个请求配置约束，在ASP.NET Core MVC应用中，推荐的方法是在操作方法上使用RequestSizeLimit属性：

```c#
[RequestSizeLimit(100000000)]
public IActionResult MyActionMethod(){}
```

还可以在中间件中替代特定请求的设置：

```c#
app.Run(async (context) =>
{
    context.Features.Get<IHttpMaxRequestBodySizeFeature>()
        .MaxRequestBodySize = 10 * 1024;
    ...
        });
```

注意：如果在应用开始读取请求后尝试配置请求限制，则会引发异常。 IsReadOnly 属性指示MaxRequestBodySize 属性处于只读状态，意味已经无法再配置限制。

### 设置请求正文的最小数据速率

Kestrel 每秒检查一次数据是否以指定的速率（字节/秒）传入。 如果速率低于最小值，则连接超时。宽限期是 Kestrel 提供给客户端用于将其发送速率提升到最小值的时间量；在此期间不会检查速率。 宽限期有助于避免最初由于 TCP 慢启动而以较慢速率发送数据的连接中断。

默认的最小速率为 240 字节/秒，包含 5 秒的宽限期。

最小速率也适用于响应。 除了属性和接口名称中具有 RequestBody 或 Response 以外，用于设置请求限制和响应限制的代码相同。可以使用MinRequestBodyDataRate或MinResponseDataRate配置请求或响应的最小数据速率：

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel(options =>
        {
            options.Limits.MinRequestBodyDataRate =
                new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
            options.Limits.MinResponseDataRate =
                new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
        });
```

可以在中间件中替代每个请求的最低速率限制：

```c#
app.Run(async (context) =>
{
    context.Features.Get<IHttpMaxRequestBodySizeFeature>()
        .MaxRequestBodySize = 10 * 1024;

    var minRequestRateFeature = 
        context.Features.Get<IHttpMinRequestBodyDataRateFeature>();
    var minResponseRateFeature = 
        context.Features.Get<IHttpMinResponseDataRateFeature>();

    if (minRequestRateFeature != null)
    {
        minRequestRateFeature.MinDataRate = new MinDataRate(
            bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
    }

    if (minResponseRateFeature != null)
    {
        minResponseRateFeature.MinDataRate = new MinDataRate(
            bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
    }
});
```



## 终结点（Endpoint）配置

终结点配置主要包括配置Kestrel的URL前缀、端口和HTTS等。

通过在KestrelServerOptions 上调用 Listen 或 ListenUnixSocket 方法，从而配置 Kestrel 的 URL 前缀和端口。

注意：也可以使用UseUrls、--urls 命令行参数、urls 主机配置键以及 ASPNETCORE_URLS 环境变量进行设置，但是存在一定的限制，必须要有可用于 HTTPS 终结点配置的默认证书。

### KestrelServerOptions类

KestrelServerOptions类提供Kestrel特定功能的编程配置，主要有以下成员：

#### `ConfigureEndpointDefaults(Action<ListenOptions>)`

指定一个为每个指定的终结点运行的配置 Action。 多次调用 ConfigureEndpointDefaults，用最新指定的 Action 替换之前的 Action。

#### `ConfigureHttpsDefaults(Action<HttpsConnectionAdapterOptions>)`

指定一个为每个 HTTPS 终结点运行的配置 Action。 多次调用 ConfigureHttpsDefaults，用最新指定的 Action 替换之前的 Action。

#### `Configure(IConfiguration)`

创建配置加载程序，用于设置将 IConfiguration 作为输入的 Kestrel。 配置必须针对 Kestrel 的配置节。

#### Listen()

Listen 方法绑定至 TCP 套接字，并可通过选项 lambda 配置 SSL 证书：

```c#
public static void Main(string[] args)
{
    CreateWebHostBuilder(args).Build().Run();
}

public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel(options =>
        {
            options.Listen(IPAddress.Loopback, 5000);
            options.Listen(IPAddress.Loopback, 5001, listenOptions =>
            {
                listenOptions.UseHttps("testCert.pfx", "testPassword");
            });
        });
```

该示例为带有 ListenOptions.的终结点配置 SSL。 可使用相同 API 为特定终结点配置其他 Kestrel 设置。
在 Windows 上，可以使用 New-SelfSignedCertificate PowerShell cmdlet 创建自签名证书。 有关不支持的示例，请参阅 UpdateIISExpressSSLForChrome.ps1。
在 macOS、Linux 和 Windows 上，可以使用 OpenSSL 创建证书。

#### ListenUnixSocket()

可通过 ListenUnixSocket 侦听 Unix 套接字以提高 Nginx 的性能：

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel(options =>
        {
            options.ListenUnixSocket("/tmp/kestrel-test.sock");
            options.ListenUnixSocket("/tmp/kestrel-test.sock", listenOptions =>
            {
                listenOptions.UseHttps("testCert.pfx", "testpassword");
            });
        });
```

### ListenOptions类

该类用于描述IPEndPoint、Unix域套接字路径或已经打开的套接字的文件描述符，Kestrel应该绑定或打开该套接字。

#### ListenOptions.UseHttps()

该方法将 Kestrel 配置为使用 HTTPS，采用默认证书。 如果没有配置默认证书，则会引发异常。UseHttps()有许多的扩展版本，这些方法可参阅：https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.server.kestrel.core.listenoptions，这里只对该方法的一些参数进行概述：

ListenOptions.UseHttps 参数：

- filename 是证书文件的路径和文件名，关联包含应用内容文件的目录。
- password 是访问 X.509 证书数据所需的密码。
- configureOptions 是配置 HttpsConnectionAdapterOptions 的 Action。 返回 ListenOptions。
- storeName 是从中加载证书的证书存储。
- subject 是证书的主题名称。
- allowInvalid 指示是否存在需要留意的无效证书，例如自签名证书。
- location 是从中加载证书的存储位置。
- serverCertificate 是 X.509 证书。

在生产中，必须显式配置 HTTPS。 至少必须提供默认证书。

#### 与配置HTTPS相关的配置

##### 无显式配置（默认配置）

默认情况下，ASP.NET Core绑定到：

- http://localhost:5000
- https://localhost:5001（存在本地开发证书时，即默认证书）

开发证书会在以下情况下被创建：

- 安装了 .NET Core SDK 时。
- 使用dev-certs tool用于创建证书。

部分浏览器需要获取信任本地开发证书的显示权限。ASP.NET Core 2.1 及更高版本的项目模板将应用配置为默认情况下在 HTTPS 上运行，并包括 HTTPS 重定向和 HSTS 支持。

如果默认证书可用，就可以使用以下内容指定URL：

- ASPNETCORE_URLS 环境变量。
- --urls 命令行参数。
- urls 主机配置键。
- UseUrls 扩展方法。

采用这些方法提供的值可以是一个或多个 HTTP 和 HTTPS 终结点（如果默认证书可用，则为 HTTPS）。 将值配置为以分号分隔的列表（例如 "Urls": "http://localhost:8000; http://localhost:8001"）。

##### 从配置中替换默认证书

WebHost.CreateDefaultBuilder 在默认情况下调用 serverOptions.Configure(context.Configuration.GetSection("Kestrel")) 来加载 Kestrel 配置。 Kestrel 可以使用默认 HTTPS 应用设置配置架构。 从磁盘上的文件或从证书存储中配置多个终结点，包括要使用的 URL 和证书。

在以下 appsettings.json 示例中：

- 将 AllowInvalid 设置为 true，从而允许使用无效证书（例如自签名证书）。
- 任何未指定证书的 HTTPS 终结点（下例中的 HttpsDefaultCert）会回退至在 Certificates > Default 下定义的证书或开发证书。

```json
{
"Kestrel": {
  "EndPoints": {
    "Http": {
      "Url": "http://localhost:5000"
    },

    "HttpsInlineCertFile": {
      "Url": "https://localhost:5001",
      "Certificate": {
        "Path": "<path to .pfx file>",
        "Password": "<certificate password>"
      }
    },

    "HttpsInlineCertStore": {
      "Url": "https://localhost:5002",
      "Certificate": {
        "Subject": "<subject; required>",
        "Store": "<certificate store; defaults to My>",
        "Location": "<location; defaults to CurrentUser>",
        "AllowInvalid": "<true or false; defaults to false>"
      }
    },

    "HttpsDefaultCert": {
      "Url": "https://localhost:5003"
    },

    "Https": {
      "Url": "https://*:5004",
      "Certificate": {
      "Path": "<path to .pfx file>",
      "Password": "<certificate password>"
      }
    }
    },
    "Certificates": {
      "Default": {
        "Path": "<path to .pfx file>",
        "Password": "<certificate password>"
      }
    }
  }
}
```

此外还可以使用任何证书节点的 Path 和 Password，采用证书存储字段指定证书。 例如，可将 Certificates > Default 证书指定为：

```json
"Default": {
  "Subject": "<subject; required>",
  "Store": "<cert store; defaults to My>",
  "Location": "<location; defaults to CurrentUser>",
  "AllowInvalid": "<true or false; defaults to false>"
}
```

json文件内容结构的注意事项：

- 终结点的名称不区分大小写。 例如，`HTTPS` 和 `Https` 都是有效的。

- 每个终结点都要具备 `Url` 参数。 此参数的格式和顶层 `Urls` 配置参数一样，只不过它只能有单个值。

- 这些终结点不会添加进顶层 `Urls` 配置中定义的终结点，而是替换它们。 通过 `Listen` 在代码中定义的终结点与在配置节中定义的终结点相累积。

- `Certificate` 部分是可选的。 如果为指定 `Certificate` 部分，则使用在之前的方案中定义的默认值。 如果没有可用的默认值，服务器会引发异常且无法启动。

- `Certificate` 支持 Path–Password 和 Subject–Store 证书。

- 只要不会导致端口冲突，就能以这种方式定义任何数量的终结点。

- `options.Configure(context.Configuration.GetSection("Kestrel"))` 通过 `.Endpoint(string name, options => { })` 方法返回 `KestrelConfigurationLoader`，可以用于补充已配置的终结点设置：

  ```c#
  options.Configure(context.Configuration.GetSection("Kestrel"))
      .Endpoint("HTTPS", opt =>
      {
          opt.HttpsOptions.SslProtocols = SslProtocols.Tls12;
      });
  ```

  也可以直接访问 KestrelServerOptions.ConfigurationLoader 在现有加载程序上保持迭代，例如由 WebHost.CreateDefaultBuilder 提供的加载程序。

- 每个终结点的配置节都可用于 `Endpoint` 方法中的选项，以便读取自定义设置。

- 通过另一节再次调用 `options.Configure(context.Configuration.GetSection("Kestrel"))` 可能加载多个配置。 只使用最新配置，除非之前的实例上显式调用了 `Load`。 元包不会调用 `Load`，所以可能会替换它的默认配置节。

- KestrelConfigurationLoader 从 KestrelServerOptions 将 API 的 Listen反射为 Endpoint 重载，因此可在同样的位置配置代码和配置终结点。 这些重载不使用名称，且只使用配置中的默认设置。

##### 更改代码中的默认值

可以使用 ConfigureEndpointDefaults 和 ConfigureHttpsDefaults 更改 ListenOptions 和 HttpsConnectionAdapterOptions 的默认设置，包括重写之前的方案指定的默认证书。 需要在配置任何终结点之前调用 ConfigureEndpointDefaults 和 ConfigureHttpsDefaults。

```c#
options.ConfigureEndpointDefaults(opt =>
{
    opt.NoDelay = true;
});

options.ConfigureHttpsDefaults(httpsOptions =>
{
    httpsOptions.SslProtocols = SslProtocols.Tls12;
});
```

##### SNI 的 Kestrel 支持

服务器名称指示 (SNI) 可用于承载相同 IP 地址和端口上的多个域。 为了运行 SNI，客户端在 TLS 握手过程中将进行安全会话的主机名发送至服务器，从而让服务器可以提供正确的证书。 在 TLS 握手后的安全会话期间，客户端将服务器提供的证书用于与服务器进行加密通信。

Kestrel 通过 ServerCertificateSelector 回调支持 SNI。 每次连接调用一次回调，从而允许应用检查主机名并选择合适的证书。

SNI 支持要求：

- 在目标框架 `netcoreapp2.1` 上运行。 在 `netcoreapp2.0` 和 `net461` 上，回调也会调用，但是 `name` 始终为 `null`。 如果客户端未在 TLS 握手过程中提供主机名参数，则 `name` 也为 `null`。
- 所有网站在相同的 Kestrel 实例上运行。 Kestrel 在无反向代理时不支持跨多个实例共享一个 IP 地址和端口。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseKestrel((context, options) =>
        {
            options.ListenAnyIP(5005, listenOptions =>
            {
                listenOptions.UseHttps(httpsOptions =>
                {
                    var localhostCert = CertificateLoader.LoadFromStoreCert(
                        "localhost", "My", StoreLocation.CurrentUser, 
                        allowInvalid: true);
                    var exampleCert = CertificateLoader.LoadFromStoreCert(
                        "example.com", "My", StoreLocation.CurrentUser, 
                        allowInvalid: true);
                    var subExampleCert = CertificateLoader.LoadFromStoreCert(
                        "sub.example.com", "My", StoreLocation.CurrentUser, 
                        allowInvalid: true);
                    var certs = new Dictionary<string, X509Certificate2>(
                        StringComparer.OrdinalIgnoreCase);
                    certs["localhost"] = localhostCert;
                    certs["example.com"] = exampleCert;
                    certs["sub.example.com"] = subExampleCert;

                    httpsOptions.ServerCertificateSelector = (connectionContext, name) =>
                    {
                        if (name != null && certs.TryGetValue(name, out var cert))
                        {
                            return cert;
                        }

                        return exampleCert;
                    };
                });
            });
        })
        .Build();
```

### 端口0

如果指定端口号 0，Kestrel 将动态绑定到可用端口。 以下示例演示如何确定 Kestrel 在运行时实际绑定到的端口：

```c#
public void Configure(IApplicationBuilder app)
{
    var serverAddressesFeature = 
        app.ServerFeatures.Get<IServerAddressesFeature>();

    app.UseStaticFiles();

    app.Run(async (context) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response
            .WriteAsync("<!DOCTYPE html><html lang=\"en\"><head>" +
                "<title></title></head><body><p>Hosted by Kestrel</p>");

        if (serverAddressesFeature != null)
        {
            await context.Response
                .WriteAsync("<p>Listening on the following addresses: " +
                    string.Join(", ", serverAddressesFeature.Addresses) +
                    "</p>");
        }

        await context.Response.WriteAsync("<p>Request URL: " +
            $"{context.Request.GetDisplayUrl()}<p>");
    });
}
```

### 限制

使用以下方法配置终结点：

- [UseUrls](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.hosting.hostingabstractionswebhostbuilderextensions.useurls)
- `--urls` 命令行参数
- `urls` 主机配置键
- `ASPNETCORE_URLS` 环境变量

若要将代码用于 Kestrel 以外的服务器，这些方法非常有用。 不过，请注意以下限制：

- SSL 不能使用这些方法，除非 HTTPS 终结点配置中提供了默认证书（如本主题前面的部分所示，使用 `KestrelServerOptions` 配置或配置文件）。
- 如果同时使用 `Listen` 和 `UseUrls` 方法，`Listen` 终结点将覆盖 `UseUrls` 终结点。

### IIS终结点配置

使用 IIS 时，由 Listen 或 UseUrls 设置用于 IIS 覆盖绑定的 URL 绑定。



## 传输配置

对于 ASP.NET Core 2.1 版，Kestrel 默认传输不再基于 Libuv，而是基于托管的套接字。 这是 ASP.NET Core 2.0 应用升级到 2.1 时的一个重大更改，它调用 [WebHostBuilderLibuvExtensions.UseLibuv](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.hosting.webhostbuilderlibuvextensions.uselibuv) 并依赖于以下包中的一个：

- [Microsoft.AspNetCore.Server.Kestrel](https://www.nuget.org/packages/Microsoft.AspNetCore.Server.Kestrel/)（直接包引用）
- [Microsoft.AspNetCore.App](https://www.nuget.org/packages/Microsoft.AspNetCore.App/)

对于使用 [Microsoft.AspNetCore.App 元包](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/metapackage-app?view=aspnetcore-2.1)且需要使用 Libuv 的 ASP.NET Core 2.1 或更高版本的项目：

- 将用于 [Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv](https://www.nuget.org/packages/Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv/) 包的依赖项添加到应用的项目文件：

  ```xml
  <PackageReference Include="Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv" 
                    Version="<LATEST_VERSION>" />
  ```

- 调用 WebHostBuilderLibuvExtensions.UseLibuv：

  ```c#
  public class Program
  {
      public static void Main(string[] args)
      {
          CreateWebHostBuilder(args).Build().Run();
      }
  
      public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseLibuv()
              .UseStartup<Startup>();
  }
  ```

### URL前缀

如果使用 `UseUrls`、`--urls` 命令行参数、`urls` 主机配置键或 `ASPNETCORE_URLS` 环境变量，URL 前缀可采用以下任意格式。

仅 HTTP URL 前缀是有效的。 使用 `UseUrls` 配置 URL 绑定时，Kestrel 不支持 SSL。

- 包含端口号的 IPv4 地址

  ```
  http://65.55.39.10:80/
  ```

  0.0.0.0 是一种绑定到所有 IPv4 地址的特殊情况。

- 包含端口号的 IPv6 地址

  ```
  http://[0:0:0:0:0:ffff:4137:270a]:80/
  ```

  `[::]` 是 IPv4 `0.0.0.0` 的 IPv6 等效项。

- 包含端口号的主机名

  ```
  http://contoso.com:80/
  http://*:80/
  ```

  主机名、*和 + 并不特殊。 没有识别为有效 IP 地址或 localhost 的任何内容都将绑定到所有 IPv4 和 IPv6 IP。 若要将不同主机名绑定到相同端口上的不同 ASP.NET Core 应用，请使用 HTTP.sys 或 IIS、Nginx 或 Apache 等反向代理服务器。

- 包含端口号的主机 localhost 名称或包含端口号的环回 IP

  ```
  http://localhost:5000/
  http://127.0.0.1:5000/
  http://[::1]:5000/
  ```

  指定 localhost 后，Kestrel 将尝试绑定到 IPv4 和 IPv6 环回接口。 如果其他服务正在任一环回接口上使用请求的端口，则 Kestrel 将无法启动。 如果任一环回接口出于任何其他原因（通常是因为 IPv6 不受支持）而不可用，则 Kestrel 将记录一个警告。

## 主机筛选

尽管 Kestrel 支持基于前缀的配置（例如 `http://example.com:5000`），但 Kestrel 在很大程度上会忽略主机名。 主机 `localhost` 是一个特殊情况，用于绑定至环回地址。 除了显式 IP 地址以外的所有主机都绑定至所有公共 IP 地址。 这些信息都不用于验证请求 `Host`标头。

解决方法是，使用主机筛选中间件。 主机筛选中间件由 Microsoft.AspNetCore.HostFiltering 包提供，此包包含在 Microsoft.AspNetCore.App 元包中（ASP.NET Core 2.1 或更高版本）。 该中间件由 CreateDefaultBuilder 添加，可调用 AddHostFiltering：

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}
```

默认情况下，主机筛选中间件处于禁用状态。 要启用该中间件，请在 appsettings.json/appsettings.<EnvironmentName>.json 中定义一个 `AllowedHosts` 键。 此值是以分号分隔的不带端口号的主机名列表：

appsettings.json：

```json
{
  "AllowedHosts": "example.com;localhost"
}
```

> 转接头中间件同样提供 ForwardedHeadersOptions.AllowedHosts 选项。 转接头中间件和主机筛选中间件具有适合不同方案的相似功能。 如果未保留主机标头，并且使用反向代理服务器或负载均衡器转接请求，则使用转接头中间件设置 AllowedHosts 比较合适。 将 Kestrel 用作面向公众的边缘服务器或直接转接主机标头时，使用主机筛选中间件设置 AllowedHosts 比较合适。













