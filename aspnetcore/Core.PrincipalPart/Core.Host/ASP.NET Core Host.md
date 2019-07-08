# ASP.NET Core Host

Host也被称为托管或主机。Host负责应用程序启动和生存期管理，主要有以下两种：

- Web Host：Web主机，适用于托管Web应用。为托管 ASP.NET Core Web 应用，开发人员应使用基于 `IWebHostBuilder` 的 Web 主机。
- Generic Host：通用主机， 适用于托管非 Web 应用（例如，运行后台任务的应用）。 在未来的版本中，通用主机将适用于托管任何类型的应用，包括 Web 应用。 通用主机最终将取代 Web 主机。为托管非 Web 应用，开发人员应使用基于 `HostBuilder`的通用主机。



## Web Host（IWebHostBuilder）

ASP.NET Core应用需要配置和启动Host，Host负责应用程序的启动和生存期管理，Host至少要配置服务器和请求处理管道。在ASP.NET Core中，使用ASP.NET Core Web主机 （IWebHostBuilder）托管Web应用。

### 设置Host

通常Program.cs中的Main方法在应用的入口点首先被执行，典型的Program.cs中的代码如下：

```c#
public static void Main(string[] args)
{
    CreateWebHostBuilder(args).Build().Run();
}

public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
```

它通过WebHost.CreateDefaultBuilder的调用开始设置Host，CreateDefaultBuilder将会执行以下任务：

###### 配置Web服务器

使用应用程序的Host配置提供程序将Kestrel服务器配置为web服务器。

###### 设置内容根目录

将内容根目录设置为Directory.GetCurrentDirectory返回的路径。

内容根确定主机搜索内容文件（如 MVC 视图文件）的位置。 应用从项目的根文件夹启动时，会将项目的根文件夹用作内容根。 这是 Visual Studio 和 dotnet new 模板中使用的默认值。

###### 加载Host配置

主要通过以下方式加载Host配置：

- 以ASPNETCORE_作为前缀的环境变量（例如，ASPNETCORE_ENVIRONMENT）。
- 命令行参数。

###### 加载应用配置

将按照以下顺序加载应用配置：

- appsettings.json。
- appsettings.{Environment}.json。
- 应用在使用入口程序集的 Development 环境中运行时的机密管理器（Secret Manager）。
- 环境变量。
- 命令行参数。

###### 配置控制台和调试输出的日志记录

日志记录包含 appsettings.json 或 appsettings.{Environment}.json 文件的日志记录配置部分中指定的日志筛选规则。

###### 在IIS后方运行时，启用IIS集成

当使用ASP.NET Core 模块时，可以配置基路径和被服务器侦听的端口。ASP.NET Core模块创建IIS与Kestrel之间的反向代理，还配置应用启动错误的捕获。

###### 设置作用域验证

如果应用环境为“开发”，则 CreateDefaultBuilder 将 ServiceProviderOptions.ValidateScopes 设为 true。

#### 其他方法

除了CreateDefaultBuilder定义的配置外，还可以使用ConfigureAppConfiguration、ConfigureLogging 以及 IWebHostBuilder 的其他方法和扩展方法重写和增强 CreateDefaultBuilder 定义的配置。

##### ConfigureAppConfiguration 

ConfigureAppConfiguration用于指定应用的其他IConfiguration。

下面的示例代码中，ConfigureAppConfiguration调用一个委托，向应用添加appsettings.xml文件中的配置，可以多次调用ConfigureAppConfiguration方法。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) => {
        config.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);
    })
    .UseStartup<Startup>();
```

##### ConfigureLogging

添加委托以配置提供的ILoggingBuilder，可以被多次调用。

下面的示例代码中，ConfigureLogging 调用添加委托，以将最小日志记录级别 (SetMinimumLevel) 配置为 LogLevel.Warning。 此设置重写了CreateDefaultBuilder在appsettings.Development.json和appsettings.Production.json中配置的设置，这两个配置项分别为 LogLevel.Debug 和 LogLevel.Error。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
    .ConfigureLogging(logging => {
        logging.SetMinimumLevel(LogLevel.Warning);
    })
    .UseStartup<Startup>();
```

##### ConfigureKestrel

用于重写CreateDefaultBuilder中的Kestrel配置。

下面的示例调用ConfigureKestrel来重写CreateDefaultBuilder在配置Kestrel时，Limits.MaxRequestBodySize默认指定的30000000字节。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
    .ConfigureKestrel((context,options)=> {
        options.Limits.MaxRequestBodySize = 20_000_000;
    })
    .UseStartup<Startup>();
```

### Host配置值

WebHostBuilder派生自IWebHostBuilder接口，WebHostBuilder依赖于以下几种形式设置Host配置值：

- 基于Host生成器配置，Host生成器配置会读取设置的环境变量，其中包括格式ASPNETCORE_{configurationKey} 的环境变量， 例如 ASPNETCORE_ENVIRONMENT。因此可以通过环境变量进行设置Host配置值。
- 使用UseContentRoot和UseConfiguration等扩展方法显式的设置Host配置值。
- 使用UseSetting方法，该方法需要指定要设置的Host配置值对应的配置键，该值来自于WebHostDefaults的成员变量，同时还要指定要设置的字符串值。

WebHostDefaults的静态成员变量如下，注意它们是只读的：

```c#
public static class WebHostDefaults
{
	public static readonly string ApplicationKey = "applicationName";
	public static readonly string StartupAssemblyKey = "startupAssembly";
	public static readonly string HostingStartupAssembliesKey = "hostingStartupAssemblies";
	public static readonly string HostingStartupExcludeAssembliesKey = "hostingStartupExcludeAssemblies";
	public static readonly string DetailedErrorsKey = "detailedErrors";
	public static readonly string EnvironmentKey = "environment";
	public static readonly string WebRootKey = "webroot";
	public static readonly string CaptureStartupErrorsKey = "captureStartupErrors";
	public static readonly string ServerUrlsKey = "urls";
	public static readonly string ContentRootKey = "contentRoot";
	public static readonly string PreferHostingUrlsKey = "preferHostingUrls";
	public static readonly string PreventHostingStartupKey = "preventHostingStartup";
	public static readonly string SuppressStatusMessagesKey = "suppressStatusMessages";
	public static readonly string ShutdownTimeoutKey = "shutdownTimeoutSeconds";
}
```

下面对常用的Host配置值进行讲述。在每个配置值中，列出的环境变量来自于”ASPNETCORE_配置键“的形式（习惯全大写），下述列出的设置形式只是对常用的形式进行了表述，并不仅仅局限于代码中指定的这种形式。可以结合上述的WebHostDefaults中的成员进行理解。

#### 应用程序名称

配置键：applicationName

环境变量：ASPNETCORE_APPLICATIONNAME

调用方法设置：调用UseSetting方法，传入WebHostDefaults.ApplicationKey，等同于applicationName。

设置说明：设置IHostingEnvironment.ApplicationName属性。在Host构造期间调用UseStartup或Configure时，会自动默认将该属性的值设置为，包含应用入口点的程序集的名称。可以使用下述方法，显式设置该属性值：

```c#
WebHost.CreateDefaultBuilder(args)
//设置IHostingEnvironment.ApplicationName属性值
.UseSetting(WebHostDefaults.ApplicationKey,"MyAppName")
```

#### 捕获启动错误

配置键：captureStartupErrors

环境变量：ASPNETCORE_CAPTURESTARTUPERRORS

调用方法设置：调用CaptureStartupErrors方法

设置说明：此设置控制启动错误的捕获。默认为false，除非应用使用 Kestrel 在 IIS 后方运行，其中默认值是 `true`。当 值为false 时，启动期间出错将会导致主机退出。 当 值为true 时，主机在启动期间捕获异常，但是会尝试启动服务器。

```c#
WebHost.CreateDefaultBuilder(args)
    .CaptureStartupErrors(true)
```

#### 内容根

配置键：contentRoot

环境变量：ASPNETCORE_CONTENTROOT

调用方法形式：调用UseContentRoot方法

设置说明：设置ASP.NET Core 内容文件的根路径，默认为应用程序集所在的文件夹。内容根也是Web根的基路径（webroot包含在内容根内），如果内容根不存在，主机将无法启动。

```c#
WebHost.CreateDefaultBuilder(args)
    .UseContentRoot("c:\\<content-root>")
```

#### 详细错误

配置键：detailedErrors

环境变量：ASPNETCORE_DETAILEDERRORS

调用方法形式：调用UseSetting方法

设置说明：用于设置是否应捕获详细错误，默认值为false。当环境设置为Development或启用时，将会捕获详细的异常。

```c#
WebHost.CreateDefaultBuilder(args)
    .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
```

#### 环境

配置键：environment

环境变量：ASPNETCORE_ENVIRONMENT

调用方法方式：调用UseEnvironment方法

设置说明：用于设置应用的环境。环境可以设置为任何值，框架定义的值包括Development和Production，值不区分大小写，默认情况下，从ASPNETCORE_ENVIRONMENT 环境变量读取环境。 使用 Visual Studio 时，可能会在 launchSettings.json 文件中设置环境变量。 

```c#
WebHost.CreateDefaultBuilder(args)
    .UseEnvironment(EnvironmentName.Development)
```

#### 承载启动程序集

配置键：hostingStartupAssemblies

环境变量：ASPNETCORE_HOSTINGSTARTUPASSEMBLIES

调用方法方式：调用UseSetting方法

设置说明：设置应用的承载启动程序集，值为以分号分割的程序集字符串，对应的承载启动程序集将在应用启动时被加载。默认值为空字符串，虽然没有显式的指定，但是承载启动程序集会始终包含应用的程序集。提供承载启动程序集时，当应用在启动过程中生成其公用服务时，将加载它们添加到应用的程序集。

```
WebHost.CreateDefaultBuilder(args)
    .UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "assembly1;assembly2")
```

#### HTTPS端口

配置键：https_port

环境变量：ASPNETCORE_HTTPS_PORT

调用方法方式：使用UseSetting方法

设置说明：该配置键未在WebHostDefaults中指定，实际使用时，是一个字符串具体值。它用于设置HTTPS重定向的端口，用于强制实施HTTPS。

```c#
WebHost.CreateDefaultBuilder(args)
    .UseSetting("https_port", "8080")
```

#### 承载启动排除程序集

配置键：hostingStartupExcludeAssemblies

环境变量：ASPNETCORE_HOSTINGSTARTUPEXCLUDEASSEMBLIES

调用方法方式：UseSetting方法

设置说明：值是以分号分隔的承载启动程序集字符串，这些指定的程序集将在启动时排除。

```c#
WebHost.CreateDefaultBuilder(args)
    .UseSetting(WebHostDefaults.HostingStartupExcludeAssembliesKey, "assembly1;assembly2")
```

#### 首选承载URL

配置键：preferHostingUrls

环境变量：ASPNETCORE_PREFERHOSTINGURLS

调用方法方式：调用PreferHostingUrls方法进行设置

设置说明：该设置指示Host是否应该侦听使用WebHostBuilder配置的URL，而不是使用IServer实现配置的URL。默认值为true。

```c#
WebHost.CreateDefaultBuilder(args)
    .PreferHostingUrls(false)
```

#### 阻止承载启动

配置值：preventHostingStartup

环境变量：ASPNETCORE_PREVENTHOSTINGSTARTUP

调用方法方式：使用UseSetting

设置说明：是否阻止承载启动程序集自动加载，包括应用的程序集所配置的承载启动程序集，默认值为false。

```c#
WebHost.CreateDefaultBuilder(args)
    .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")
```

#### 服务器URL

配置值：urls

环境变量：ASPNETCORE_URLS

调用方式：调用UseUrls方法进行设置

设置说明：设置服务器应响应的以分号分隔（;）的URL前缀列表，例如：http://localhost:123。设置了该值后，必须通过访问设置的URL才能请求服务。使用“`*`”指示服务器应针对请求侦听的使用特定端口和协议（例如 http://*:5000）的 IP 地址或主机名。 协议（http:// 或 https://）必须包含每个 URL。 不同的服务器支持的格式有所不同。

```c#
WebHost.CreateDefaultBuilder(args)
    .UseUrls("http://*:5000;http://localhost:5001;https://hostname:5002")
```

#### 关闭超时

配置值：shutdownTimeoutSeconds

环境变量：ASPNETCORE_SHUTDOWNTIMEOUTSECONDS

调用方式：调用方法UseShutdownTimeout进行设置

设置说明：设置等待Web Host关闭的时长。也可以使用UseSetting方法进行设置，例如：

```c#
WebHost.CreateDefaultBuilder(args)
.UseSetting(WebHostDefaults.ShutdownTimeoutKey, "10")
```

UseSetting方法接受int字符串值，而UseShutdownTimeout扩展方法接收TimeSpan，如下：

```c#
WebHost.CreateDefaultBuilder(args)
    .UseShutdownTimeout(TimeSpan.FromSeconds(10))
```

在超时时间段中，Host将会触发 IApplicationLifetime.ApplicationStopping，并尝试停止Host服务，对服务停止失败的任何错误进行日志记录。

如果在所有Host服务停止之前就达到了超时时间，则会在应用关闭时会终止剩余的所有活动的服务。 即使没有完成处理工作，服务也会停止。 如果停止服务需要额外的时间，那么就需要增加超时时间。

#### 启动程序集

配置值：startupAssembly

环境变量：ASPNETCORE_STARTUPASSEMBLY

调用方式：调用UseStartup方法

设置说明：用于设置要在应用中搜索Startup类的程序集，可以引用按名称（string）或类型（TStartup）的程序集。如果调用多个UseStartup方法，优先选择最后一个方法。

```
WebHost.CreateDefaultBuilder(args)
    .UseStartup("StartupAssemblyName")
```

或者：

```
WebHost.CreateDefaultBuilder(args)
    .UseStartup<TStartup>()
```

#### Web 根路径

配置值：webroot

环境变量：ASPNETCORE_WEBROOT

调用方式：调用UseWebRoot方法

设置说明：设置应用的静态资源的相对路径，如果未指定，默认值是“(Content Root)/wwwroot”（如果该路径存在）。 如果该路径不存在，则使用无操作文件提供程序。

```c#
WebHost.CreateDefaultBuilder(args)
    .UseWebRoot("public")
```

### 重写配置

UseConfiguration可用于配置Web Host，注意和上述中的ConfigureAppConfiguration之间的不同。

#### UseConfiguration和ConfigureAppConfiguration的区别

- 配置的对象不同：UseConfiguration针对Web Host进行配置（框架配置），而ConfigureAppConfiguration针对的是应用程序配置，换句话说，如果只是应用在应用程序上的配置，应该使用ConfigureAppConfiguration，ConfigureAppConfiguration可以多次被调用；如果是应用在框架上的Host配置，比如服务器URL、环境等配置，这些应该使用UseConfiguration。（一般来说如果名称上带有“App”的，都是基于应用程序的，相关的配置就需要使用ConfigureAppConfiguration方法，例如：appsettings.json。）
- UseConfiguration添加的配置会影响ConfigureAppConfiguration添加的配置，这是因为IWebHostBuilder配置会添加到应用配置中，而使用ConfigureAppConfiguration添加的配置，并不会影响IWebHostBuilder 配置，也就是说基于框架的配置，优先级别更高，不会被ConfigureAppConfiguration影响。

#### 使用UseConfiguration重写配置

在下面的示例中，先使用UseUrls指定Url后，再使用hostsettings.json重写UseUrls提供的配置，重写时调用了UseConfiguration方法，在重写配置的过程中，主机配置是根据需要在 hostsettings.json 文件中指定。 命令行参数可能会重写从 hostsettings.json 文件加载的任何配置。 生成的配置（在 config 中）用于通过 UseConfiguration 配置主机。

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("hostsettings.json", optional: true)
            .AddCommandLine(args)
            .Build();

        return WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://*:5000")
            .UseConfiguration(config)
            .Configure(app =>
            {
                app.Run(context => 
                    context.Response.WriteAsync("Hello, World!"));
            });
    }
}
```

hostsettings.json：

```
{
    urls: "http://*:5005"
}
```

注意：UseConfiguration 只将所提供的 IConfiguration 中的键复制到主机生成器配置中。 因此，JSON、INI 和 XML 设置文件的设置 reloadOnChange: true 没有任何影响。

UseConfiguration 方法需要键来匹配 WebHostBuilder 键（例如 urls、environment）。若要指定在特定的 URL 上运行的主机，所需的值可以在执行 dotnet 运行时从命令提示符传入。 命令行参数重写 hostsettings.json 文件中的 urls 值，且服务器侦听端口 8080：

```
dotnet run --urls "http://*:8080"
```

### 管理主机

#### Run()

Run 方法启动 Web 应用并阻塞调用线程，直到关闭主机。

```
host.Run();
```

#### Start()

通过调用 Start 方法以非阻塞方式运行主机：

```c#
using (host)
{
    host.Start();
    Console.ReadLine();
}
```

如果将URL的列表传递给Start方法，那么将侦听该列表指定的URL：

```c#
var urls = new List<string>()
{
    "http://*:5000",
    "http://localhost:5001"
};

var host = new WebHostBuilder()
    .UseKestrel()
    .UseStartup<Startup>()
    .Start(urls.ToArray());

using (host)
{
    Console.ReadLine();
}
```

在使用WebHost.CreateDefaultBuilder方法时，应用通过该方法的预配置的默认值初始化并启动新的主机，这些方法在没有控制台输出的情况下启动服务器，并使用 WaitForShutdown 等待中断（Ctrl-C/SIGINT 或 SIGTERM）。

#### Start(RequestDelegate app)和Start(string url,RequestDelegate app)

这两个方法执行的相同的结果，不同的是Start(string url,RequestDelegate app)用于在指定的URL上进行响应，第二个参数RequestDelegate在两个方法中的用法相同：

```c#
using (var host = WebHost.Start("http://localhost:8080", app => app.Response.WriteAsync("Hello, World!")))
{
    Console.WriteLine("Use Ctrl-C to shutdown the host...");
    host.WaitForShutdown();
}
```

运行上述代码后，在浏览器中向http://localhost:5000 发出请求，接收响应“Hello World!” WaitForShutdown 受到阻止，直到发出中断（Ctrl-C/SIGINT 或 SIGTERM）。 应用显示 Console.WriteLine 消息并等待 keypress 退出。

#### `Start(Action<IRouteBuilder> routeBuilder)`和`Start(string url, Action<IRouteBuilder> routeBuilder)`

这两个方法执行的结果相同，使用 IRouteBuilder 的实例 (Microsoft.AspNetCore.Routing) 用于路由中间件，可以指定URL进行响应：

```c#
using (var host = WebHost.Start("http://localhost:8080", router => router
    .MapGet("hello/{name}", (req, res, data) => 
        res.WriteAsync($"Hello, {data.Values["name"]}!"))
    .MapGet("buenosdias/{name}", (req, res, data) => 
        res.WriteAsync($"Buenos dias, {data.Values["name"]}!"))
    .MapGet("throw/{message?}", (req, res, data) => 
        throw new Exception((string)data.Values["message"] ?? "Uh oh!"))
    .MapGet("{greeting}/{name}", (req, res, data) => 
        res.WriteAsync($"{data.Values["greeting"]}, {data.Values["name"]}!"))
    .MapGet("", (req, res, data) => res.WriteAsync("Hello, World!"))))
{
    Console.WriteLine("Use Ctrl-C to shut down the host...");
    host.WaitForShutdown();
}
```

WaitForShutdown 受到阻塞，直到发出中断（Ctrl-C/SIGINT 或 SIGTERM）。 应用显示 Console.WriteLine 消息并等待 keypress 退出。

#### `StartWith(Action<IApplicationBuilder> app)`和`StartWith(string url, Action<IApplicationBuilder> app)`

这两个方法执行的结果相同，都提供委托以配置 IApplicationBuilder，第二个方法提供了响应的URL：

```c#
using (var host = WebHost.StartWith("http://localhost:8080", app => 
    app.Use(next => 
    {
        return async context => 
        {
            await context.Response.WriteAsync("Hello World!");
        };
    })))
{
    Console.WriteLine("Use Ctrl-C to shut down the host...");
    host.WaitForShutdown();
}
```

备注：上述的这些方法都提供了URL参数版本，除此之外，带URL和不带URL的方法的其他参数的使用都相同。

### IHostingEnvironment 接口

IHostingEnvironment 接口提供有关应用的 Web Hosting环境的信息。 可以使用构造函数注入的方式获取 IHostingEnvironment，以使用其属性和扩展方法。

```c#
public class CustomFileReader
{
    private readonly IHostingEnvironment _env;

    public CustomFileReader(IHostingEnvironment env)
    {
        _env = env;
    }

    public string ReadFile(string filePath)
    {
        var fileProvider = _env.WebRootFileProvider;
        // Process the file here
    }
}
```

基于环境的Startup类和方法可以用于在启动时基于环境配置应用，或者，将IHostingEnvironment 注入到 Startup 构造函数用于 ConfigureServices：

```c#
public class Startup
{
    public Startup(IHostingEnvironment env)
    {
        HostingEnvironment = env;
    }

    public IHostingEnvironment HostingEnvironment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        if (HostingEnvironment.IsDevelopment())
        {
            // Development configuration
        }
        else
        {
            // Staging/Production configuration
        }

        var contentRootPath = HostingEnvironment.ContentRootPath;
    }
}
```

IHostingEnvironment 服务还可以直接注入到 Configure 方法以设置处理管道：

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        // In Development, use the developer exception page
        app.UseDeveloperExceptionPage();
    }
    else
    {
        // In Staging/Production, route exceptions to /error
        app.UseExceptionHandler("/error");
    }

    var contentRootPath = env.ContentRootPath;
}
```

创建自定义中间件时可以将 IHostingEnvironment 注入 Invoke 方法：

```c#
public async Task Invoke(HttpContext context, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        // Configure middleware for Development
    }
    else
    {
        // Configure middleware for Staging/Production
    }

    var contentRootPath = env.ContentRootPath;
}
```

### IApplicationLifetime 接口

IApplicationLifetime允许启动后和关闭活动，该接口的三个属性是用于注册Action方法的取消标记。

| 取消标记            | 触发条件                                                     |
| ------------------- | ------------------------------------------------------------ |
| ApplicationStarted  | 主机已完全启动。                                             |
| ApplicationStopped  | 主机正在完成正常关闭。 应处理所有请求。 关闭受到阻止，直到完成此事件。 |
| ApplicationStopping | 主机正在执行正常关闭。 仍在处理请求。 关闭受到阻止，直到完成此事件。 |

```c#
public class Startup
{
    public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime)
    {
        appLifetime.ApplicationStarted.Register(OnStarted);
        appLifetime.ApplicationStopping.Register(OnStopping);
        appLifetime.ApplicationStopped.Register(OnStopped);

        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            appLifetime.StopApplication();
            // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
            eventArgs.Cancel = true;
        };
    }

    private void OnStarted()
    {
        // Perform post-startup activities here
    }

    private void OnStopping()
    {
        // Perform on-stopping activities here
    }

    private void OnStopped()
    {
        // Perform post-stopped activities here
    }
}
```

StopApplication 请求应用终止。 以下类在调用类的 Shutdown 方法时使用 StopApplication 正常关闭应用：

```c#
public class MyClass
{
    private readonly IApplicationLifetime _appLifetime;

    public MyClass(IApplicationLifetime appLifetime)
    {
        _appLifetime = appLifetime;
    }

    public void Shutdown()
    {
        _appLifetime.StopApplication();
    }
}
```

### 作用域验证

如果应用程序的环境是“Development”，当使用CreateDefaultBuilder方法时，该方法将会把属性 ServiceProviderOptions.ValidateScopes的值设为 true。一旦将ValidateScopes 设为 true，默认服务提供程序会执行检查来验证以下内容：

- 没有从根服务提供程序直接或间接解析到有作用域的服务。
- 未将有作用域的服务直接或间接注入到单一实例。

调用 BuildServiceProvider 时，会创建根服务提供程序。 在启动提供程序和应用时，根服务提供程序的生存期对应于应用/服务的生存期，并在关闭应用时释放。
有作用域的服务由创建它们的容器释放。 如果作用域创建于根容器，则该服务的生存期会有效地提升至单一实例，因为根容器只会在应用/服务关闭时将其释放。 验证服务作用域，将在调用 BuildServiceProvider 时收集这类情况。
若要始终验证作用域（包括在生存环境中验证），请使用主机生成器上的 UseDefaultServiceProvider 配置 ServiceProviderOptions：

 ```c#
WebHost.CreateDefaultBuilder(args)
    .UseDefaultServiceProvider((context, options) => {
        options.ValidateScopes = true;
    })
 ```



## .NET Core 通用主机（HostBuilder）

【约定：Web主机在前文中统称为Web Host，为保持汉语的连贯性，此处约定将通用Host统一称为通用主机】



对于托管不处理HTTP请求的应用推荐使用通用主机，主要基于HostBuilder进行构建和配置。通用主机的目标是将HTTP管道从Web Host API中分离出来，从而启动更多的主机方案。基于通用主机的消息、后台任务和其他非 HTTP 工作负载，可从配置、依赖关系注入 [DI] 和日志记录等功能中受益。

备注：通用主机正处于开发阶段，用于在未来版本中替换 Web 主机，并在 HTTP 和非 HTTP 方案中充当主要的主机 API。

通用主机库位于 Microsoft.Extensions.Hosting 命名空间中，IHostedService 是执行代码的入口点。 每个 IHostedService 实现都按照 ConfigureServices 中服务注册的顺序执行。 主机启动时，每个 IHostedService 上都会调用 StartAsync，主机正常关闭时，以反向注册顺序调用 StopAsync。

### 设置主机

IHostBuilder 是供库和应用初始化、生成和运行主机的主要组件：

```c#
public static async Task Main(string[] args)
{
    var host = new HostBuilder()
        .Build();

    await host.RunAsync();
}
```

### 默认注册的服务

在主机初始化期间默认注册以下服务：

- 环境 (IHostingEnvironment)
- HostBuilderContext
- 配置 (IConfiguration)
- IApplicationLifetime (ApplicationLifetime)
- IHostLifetime (ConsoleLifetime)
- IHost
- 选项 (AddOptions)
- 日志记录 (AddLogging)

### 主机配置

主机配置主要有以下两种方式：

- 调用 IHostBuilder 上的扩展方法以设置“内容根”和“环境”。
- 通过ConfigureHostConfiguration中的配置提供程序来读取配置。

#### 使用扩展方法对Host进行配置

##### 内容根

配置键：contentRoot

环境变量：`<PREFIX_>CONTENTROOT`（`<PREFIX_>` 是用户定义的可选前缀）

设置使用：调用UseContentRoot方法

设置说明：此设置确定主机从哪里开始搜索内容文件，默认为应用程序集所在的文件夹。如果路径不存在，主机将无法启动。

```c#
var host = new HostBuilder()
    .UseContentRoot("c:\\<content-root>")
```

##### 环境

配置键：environment

环境变量：`<PREFIX_>ENVIRONMENT`（`<PREFIX_`> 是用户定义的可选前缀）

设置使用：调用UseEnvironment方法

设置说明：用于设置应用的环境，默认值为Production。环境可以设置为任何值，框架定义的值包括“Development"、”Staging“、和“Production”，值不区分大小写。

```c#
var host = new HostBuilder()
    .UseEnvironment(EnvironmentName.Development)
```

##### 应用程序键（名称）

配置键：applicationName

环境变量：`<PREFIX_>APPLICATIONNAME`（`<PREFIX_`> 是用户定义的可选前缀）

设置使用：HostBuilderContext.HostingEnvironment.ApplicationName

设置说明：IHostingEnvironment.ApplicationName 属性是在主机构造期间通过主机配置设定的。 要显式设置值，请使用 HostDefaults.ApplicationKey。该属性的默认值是包含应用入口点的程序集的名称。

#### ConfigureHostConfiguration

ConfigureHostConfiguration 使用 IConfigurationBuilder 来为主机创建 IConfiguration。 主机配置用于初始化 IHostingEnvironment，以供在应用的构建过程中使用。

可多次调用 ConfigureHostConfiguration，并得到累计结果。 主机使用上一次在一个给定键上设置值的选项。

主机配置自动流向应用配置（ConfigureAppConfiguration 和应用的其余部分），也就是说ConfigureHostConfiguration 会影响ConfigureAppConfiguration 配置的内容。

默认情况下不包括以下提供程序，因此必须在 ConfigureHostConfiguration 中显式指定应用所需的任何配置提供程序，包括：

- 文件配置（例如，来自 hostsettings.json 文件）。
- 环境变量配置。
- 命令行参数配置。
- 任何其他所需的配置提供程序。

通过使用 SetBasePath 指定应用的基本路径，然后调用其中一个文件配置提供程序，可以启用主机的文件配置。示例应用使用 JSON 文件 hostsettings.json，并调用 AddJsonFile 来使用文件的主机配置设置。

```c#
var host = new HostBuilder()
    .ConfigureHostConfiguration(configHost =>
    {
    configHost.SetBasePath(Directory.GetCurrentDirectory());
    configHost.AddJsonFile("hostsettings.json", optional: true);
    }).Build();
```

要添加主机的环境变量配置，请在主机生成器上调用 AddEnvironmentVariables。 AddEnvironmentVariables 接受用户定义的前缀（可选）。 示例应用使用前缀 PREFIX_。 当系统读取环境变量时，便会删除前缀。 配置示例应用的主机后，PREFIX_ENVIRONMENT 的环境变量值就变成 environment 键的主机配置值。

通过调用 AddCommandLine 可添加命令行配置。 最后添加命令行配置以允许命令行参数替代之前配置提供程序提供的配置。

```c#
var host = new HostBuilder()
    .ConfigureHostConfiguration(configHost =>
    {
        configHost.SetBasePath(Directory.GetCurrentDirectory());
        configHost.AddJsonFile("hostsettings.json", optional: true);
        configHost.AddEnvironmentVariables(prefix: "PREFIX_");
        configHost.AddCommandLine(args);
    })
```

hostsettings.json：

```json
{
  "environment": "Development"
}
```

可以通过 applicationName 和 contentRoot 键提供其他配置。

### ConfigureAppConfiguration

通过在 IHostBuilder 实现上调用 ConfigureAppConfiguration 创建应用配置。 ConfigureAppConfiguration 使用 IConfigurationBuilder 来为应用创建 IConfiguration。 可多次调用 ConfigureAppConfiguration，并得到累计结果。 应用使用上一次在一个给定键上设置值的选项。 HostBuilderContext.Configuration 中提供 ConfigureAppConfiguration 创建的配置，以供进行后续操作和在 Services 中使用。

应用配置会自动接收 ConfigureHostConfiguration 提供的主机配置。

```c#
var host = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, configApp) =>
    {
        configApp.SetBasePath(Directory.GetCurrentDirectory());
        configApp.AddJsonFile("appsettings.json", optional: true);
        configApp.AddJsonFile(
            $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", 
            optional: true);
        configApp.AddEnvironmentVariables(prefix: "PREFIX_");
        configApp.AddCommandLine(args);
    })
```

### ConfigureServices

ConfigureServices 将服务添加到应用的依赖关系注入容器。 可多次调用 ConfigureServices，并得到累计结果。

托管服务是一个类，具有实现 IHostedService 接口的后台任务逻辑。

下述代码使用 AddHostedService 扩展方法向应用添加生存期事件 LifetimeEventsHostedService 和定时后台任务 TimedHostedService 服务：

```c#
var host = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        if (hostContext.HostingEnvironment.IsDevelopment())
        {
            // Development service configuration
        }
        else
        {
            // Non-development service configuration
        }

        services.AddHostedService<LifetimeEventsHostedService>();
        services.AddHostedService<TimedHostedService>();
    })
```

### ConfigureLogging

ConfigureLogging 添加了一个委托来配置提供的 ILoggingBuilder。 可以利用相加结果多次调用 ConfigureLogging。

```c#
var host = new HostBuilder()
    .ConfigureLogging((hostContext, configLogging) =>
    {
        configLogging.AddConsole();
        configLogging.AddDebug();
    })
```

### UseConsoleLifetime

UseConsoleLifetime 侦听 Ctrl+C/SIGINT 或 SIGTERM 并调用 StopApplication 来启动关闭进程。 UseConsoleLifetime 解除阻止 RunAsync 和 WaitForShutdownAsync 等扩展。 ConsoleLifetime 预注册为默认生存期实现。 使用注册的最后一个生存期。

```c#
var host = new HostBuilder()
    .UseConsoleLifetime()
```

### Container（容器）配置

为支持插入其他容器中，主机可以接受 IServiceProviderFactory<TContainerBuilder>。 提供工厂不属于 DI 容器注册，而是用于创建具体 DI 容器的主机内部函数。

UseServiceProviderFactory(IServiceProviderFactory<TContainerBuilder>) 重写用于创建应用的服务提供程序的默认工厂。

ConfigureContainer 方法托管自定义容器配置。 ConfigureContainer 提供在基础主机 API 的基础之上配置容器的强类型体验。 可以利用相加结果多次调用 ConfigureContainer。

为应用创建服务容器：

```c#
public class ServiceContainer
{
}
```

提供服务容器工厂：

```c#
public class ServiceContainerFactory : IServiceProviderFactory<ServiceContainer>
{
    public ServiceContainer CreateBuilder(IServiceCollection services)
    {
        return new ServiceContainer(); 
    }

    public IServiceProvider CreateServiceProvider(ServiceContainer containerBuilder)
    {
        throw new NotImplementedException();
    }
}
```

使用该工厂并为应用配置自定义服务容器：

```c#
var host = new HostBuilder()
    .UseServiceProviderFactory<ServiceContainer>(new ServiceContainerFactory())
    .ConfigureContainer<ServiceContainer>((hostContext, container) =>
    {
    })
```

### 主机扩展性

在 IHostBuilder 上使用扩展方法实现主机扩展性。

```c#
var host = new HostBuilder()
    .UseHostedService<TimedHostedService>()
    .Build();

await host.StartAsync();
```

应用建立 UseHostedService 扩展方法，以注册在 T 中传递的托管服务：

```c#
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IHostBuilder UseHostedService<T>(this IHostBuilder hostBuilder)
        where T : class, IHostedService, IDisposable
    {
        return hostBuilder.ConfigureServices(services =>
            services.AddHostedService<T>());
    }
}
```

### 管理主机

IHost 实现负责启动和停止服务容器中注册的 IHostedService 实现。

#### Run()

Run 运行应用并阻止调用线程，直到关闭主机：

```c#
public class Program
{
    public void Main(string[] args)
    {
        var host = new HostBuilder()
            .Build();

        host.Run();
    }
}
```

#### RunAsync()

RunAsync 运行应用并返回在触发取消令牌或关闭时完成的 Task：

```c#
public class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .Build();

        await host.RunAsync();
    }
}
```

#### RunConsoleAsync()

RunConsoleAsync 启用控制台支持、生成和启动主机，以及等待 Ctrl+C/SIGINT 或 SIGTERM 关闭。

```c#
public class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = new HostBuilder();

        await hostBuilder.RunConsoleAsync();
    }
}
```

#### Start 和 StopAsync

Start 同步启动主机。

StopAsync 尝试在提供的超时时间内停止主机。

```c#
public class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .Build();

        using (host)
        {
            host.Start();

            await host.StopAsync(TimeSpan.FromSeconds(5));
        }
    }
}
```

#### StartAsync 和 StopAsync

StartAsync 启动应用。

StopAsync 停止应用。

```c#
public class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .Build();

        using (host)
        {
            await host.StartAsync();

            await host.StopAsync();
        }
    }
}
```

#### WaitForShutdown

WaitForShutdown 通过 IHostLifetime 触发，例如 ConsoleLifetime（侦听 Ctrl+C/SIGINT 或 SIGTERM）。 WaitForShutdown 调用 StopAsync。

```c#
public class Program
{
    public void Main(string[] args)
    {
        var host = new HostBuilder()
            .Build();

        using (host)
        {
            host.Start();

            host.WaitForShutdown();
        }
    }
}
```

#### WaitForShutdownAsync

WaitForShutdownAsync 返回在通过给定的令牌和调用 StopAsync 来触发关闭时完成的 Task。

```c#
public class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .Build();

        using (host)
        {
            await host.StartAsync();

            await host.WaitForShutdownAsync();
        }

    }
}
```

#### 主机外部控件

使用可从外部调用的方法，能够实现主机的外部控件：

```c#
public class Program
{
    private IHost _host;

    public Program()
    {
        _host = new HostBuilder()
            .Build();
    }

    public async Task StartAsync()
    {
        _host.StartAsync();
    }

    public async Task StopAsync()
    {
        using (_host)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
        }
    }
}
```

在 StartAsync 开始时调用 WaitForStartAsync，在继续之前，会一直等待该操作完成。 它可用于延迟启动，直到外部事件发出信号。

### IHostingEnvironment 接口

IHostingEnvironment 提供有关应用托管环境的信息。 使用构造函数注入获取 IHostingEnvironment 以使用其属性和扩展方法：

```c#
public class MyClass
{
    private readonly IHostingEnvironment _env;

    public MyClass(IHostingEnvironment env)
    {
        _env = env;
    }

    public void DoSomething()
    {
        var environmentName = _env.EnvironmentName;
    }
}
```

### IApplicationLifetime 接口

IApplicationLifetime 允许启动后和关闭活动，包括正常关闭请求。 接口上的三个属性是用于注册 Action 方法（用于定义启动和关闭事件）的取消标记。

| 取消标记            | 触发条件                                                     |
| :------------------ | :----------------------------------------------------------- |
| ApplicationStarted  | 主机已完全启动。                                             |
| ApplicationStopped  | 主机正在完成正常关闭。 应处理所有请求。 关闭受到阻止，直到完成此事件。 |
| ApplicationStopping | 主机正在执行正常关闭。 仍在处理请求。 关闭受到阻止，直到完成此事件 |

构造函数将 IApplicationLifetime 服务注入到任何类中。 示例应用将构造函数注入到 LifetimeEventsHostedService 类（一个 IHostedService 实现）中，用于注册事件。

LifetimeEventsHostedService.cs：

```c#
internal class LifetimeEventsHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IApplicationLifetime _appLifetime;

    public LifetimeEventsHostedService(
        ILogger<LifetimeEventsHostedService> logger, IApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        _appLifetime.ApplicationStopped.Register(OnStopped);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        _logger.LogInformation("OnStarted has been called.");

        // Perform post-startup activities here
    }

    private void OnStopping()
    {
        _logger.LogInformation("OnStopping has been called.");

        // Perform on-stopping activities here
    }

    private void OnStopped()
    {
        _logger.LogInformation("OnStopped has been called.");

        // Perform post-stopped activities here
    }
}
```

StopApplication 请求终止应用。 以下类在调用类的 Shutdown 方法时使用 StopApplication 正常关闭应用：

```c#
public class MyClass
{
    private readonly IApplicationLifetime _appLifetime;

    public MyClass(IApplicationLifetime appLifetime)
    {
        _appLifetime = appLifetime;
    }

    public void Shutdown()
    {
        _appLifetime.StopApplication();
    }
}
```



