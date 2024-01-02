# ASP.NET Core中的会话和应用状态

**术语说明**

- 会话：Session，为了便于理解，本文约定将文字中出现的所有“会话”，统一使用“Session”代替。



## HTTP状态管理

HTTP是一个无状态的协议，每一个HTTP请求在不采取其他方法进行干预的情况下，是无法保留用户值或应用状态的。

若要保留请求间的用户数据或应用状态，可以使用以下方法：

- Cookie
- Session
- TempData
- 查询字符串
- 表单隐藏域
- HttpContext.Items
- Cache（缓存）
- DI（依赖关系注入）

本文将对这些方法的使用进行详细概述。



## Cookie

- Cookie可以跨请求存储数据。
- 由于每次请求都会发送Cookie，因此应将其大小保持在最低限度。大多数浏览器将cookie大小限制为4096字节。
- Cookie通常是客户端最持久的数据暂存方式，但是客户端上的Cookie可能被用户删除或过期。
- 建议Cookie只存储简单的信息，可以是用户名、账户名或唯一的用户ID，但不要存储密码等敏感信息，由于Cookie易被篡改，对于重要信息，建议必须由服务器进行验证。



## Session

Session是用户在浏览Web应用时，用来存储用户数据的常见方案。Session通常存储的是临时数据，由缓存支持。

ASP.NET Core通过向客户端提供包含Session ID的Cookie来维护Session状态，Session ID会随每个请求一起发送到应用程序，应用程序使用Session ID来获取Session数据。

Session具有以下行为：

- 不会跨浏览器共享Session。
- 当浏览器会话结束（关闭浏览器）时，会删除Session的Cookie。
- 如果收到过期的Session的Cookie，将会创建一个使用相同Session的Cookie的新Session。
- 空的Session不会被保留，因此Session必须要设置至少一个值才能保存所有请求的Session。如果未设置值，则会为每个新的请求生成新的Session ID。
- 默认Session超时时间为20分钟，从上次请求结束时开始算起。
-  当Session到期时，或者调用了ISession.Clear()方法，Session数据会被删除。
- 在调用ISession.Clear实现或会话到期时，会删除会话数据。

### 配置Session

  在应用程序的Startup类中，添加Session中间件，必须包含以下部分：

- 添加任意一个IDistributedCache 内存缓存，IDistributedCache实现用作会话的后备存储。
- 在ConfigureServices()方法中，调用AddSession()方法。
- 在Configure()方法中，调用UseSession()方法。

```c#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        //必须在UseMvc之前添加
        app.UseSession();
        app.UseHttpContextItemsMiddleware();
        app.UseMvc();
    }
}
```

上述需要注意以下几点：

- UseSession()必须在UseMvc()之前被调用，否则，在 UseMvc 之后调用 UseSession 时会发生 InvalidOperationException 异常。
- 在调用UseSession 之前，是无法访问 HttpContext.Session的。

### 异步加载Session

> 只有在 TryGetValue、Set 或 Remove 方法之前显式调用 ISession.LoadAsync 方法，ASP.NET Core 中的默认会话提供程序才会从基础 IDistributedCache 后备存储以异步方式加载会话记录。 如果未先调用 LoadAsync，则会同步加载基础会话记录，这可能对性能产生大规模影响。
>
> 该段引用来自于：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/app-state?view=aspnetcore-2.2#load-session-state-asynchronously

### Session选项

可以使用SessionOptions替换Session的默认值（默认设置）。

Session使用Cookie跟踪和标识来自单个浏览器的请求。

默认情况下，此 Cookie 名为 .AspNetCore.Session ，并使用路径 /。 由于 Cookie 默认值不指定域，因此它不提供页面上的客户端脚本（因为 HttpOnly 默认为 true）。

```c#
services.AddSession(options=> {
    //Session过期时间设置
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.Name = ".AdventureWorks.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
```

常见的选项说明如下：

#### Cookie

- 用于创建Cookie时的Cookie设置。它有以下几个常用的属性：
- Name：默认为 SessionDefaults.CookieName的值（即：“.AspNetCore.Session“）；
- Path：默认为SessionDefaults.CookiePath的值（即：“/”）；
- SameSite：默认为 SameSiteMode.Lax枚举值（即：1）；
- HttpOnly：默认为true，指示客户端脚本是否可以访问cookie。
- IsEssential：默认为false，指示此cookie是否对于应用程序正常运行至关重要。如果为true，则可以绕过同意政策检查。默认值为false。

#### IdleTimeout 

IdleTimeout用于指示会话在其内容被放弃之前可以空闲多长时间。每个会话访问都会重置超时。请注意，这仅适用于会话的内容，即只适用于Session过期时间，而不是Cookie过期时间。默认为20分钟。

#### IOTimeout 

允许从存储加载会话或将其提交回存储的最长时间。注意，该设置可能仅适用于异步操作。可以使用InfiniteTimeSpan禁用超时，默认值为1分钟。

### 设置和获取Session值

 可以通过HttpContext.Session 从 Razor Pages 的PageModel 类或 MVC 控制器类访问会话状态。 此属性是 ISession 实现。

ISession 提供用于设置和检索整数和字符串值的若干扩展方法。

ISession 扩展方法：

- Get(ISession, String)
- GetInt32(ISession, String)
- GetString(ISession, String)
- SetInt32(ISession, String, Int32)
- SetString(ISession, String, String)

```c#
@page
@using Microsoft.AspNetCore.Http
@model IndexModel

...

Name: @HttpContext.Session.GetString(IndexModel.SessionKeyName)
```

设置和获取值：

```c#
public const string SessionKeyName = "_Name";
public const string SessionKeyAge = "_Age";

HttpContext.Session.SetString(SessionKeyName, "The Doctor");
HttpContext.Session.SetInt32(SessionKeyAge, 773);

var name = HttpContext.Session.GetString(SessionKeyName);
var age = HttpContext.Session.GetInt32(SessionKeyAge);
```

对于复杂类型（如普通类）的数据，存储到Session中时，需要进行序列化，将复杂类型序列化为JSON字符串，下面提供了一个可扩展的方案。

添加以下扩展方法以设置和获取可序列化的对象：

```c#
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);

        return value == null ? default(T) : 
            JsonConvert.DeserializeObject<T>(value);
    }
}
```

如何使用扩展方法设置和获取可序列化的对象：

```c#
if (HttpContext.Session.Get<DateTime>(SessionKeyTime) == default(DateTime))
{
    HttpContext.Session.Set<DateTime>(SessionKeyTime, currentTime);
}
```



## TempData

TempData属性用于存储未读取的数据，其Keep 和 Peek 方法可用于检查数据，而不执行删除。可以基于Cookie或Session通过TempData提供程序实现TempData。

### TempData提供程序

TempData提供程序，可以基于Cookie或Session实现TempData。

基于 cookie 的 TempData 提供程序默认用于存储 cookie 中的 TempData。默认情况下，已经启用的是基于 Cookie 的 TempData 提供程序。

若要启用基于Session的 TempData 提供程序，需要使用 AddSessionStateTempDataProvider 扩展方法：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
        .AddSessionStateTempDataProvider();

    services.AddSession();
}
```

如果应用已经启用了Session，使用基于Session的TempData提供程序，并不会对应用增加额外的成本。

对于较小的数据量使用TempData时，可以使用基于Cookie的TempData提供程序，否则推荐使用基于Session的TempData提供程序。



## 查询字符串

即访问页面的URL带有参数的字符串，由于 URL 查询字符串是公共的，因此请勿对敏感数据使用查询字符串。



## 表单隐藏域

> 数据可以保存在隐藏的表单域中，并在下一个请求上回发。 这在多页窗体中很常见。 由于客户端可能篡改数据，因此应用必须始终重新验证存储在隐藏字段中的数据。



## HttpContext.Items

可以使用HttpContext.Items存储或读取数据，通常使用 Items 集合允许组件或中间件在请求期间在不同时间点操作且没有直接传递参数的方法时进行通信。

下述代码中，中间件将 isVerified 添加到 Items 集合：

```c#
app.Use(async (context, next) =>
{
    // perform some verification
    context.Items["isVerified"] = true;
    await next.Invoke();
});
```

然后，在管道中，另一个中间件可以访问 isVerified 的值：

```c#
app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Verified: {context.Items["isVerified"]}");
});
```

对于只供单个应用使用的中间件，string 键是可以接受的。 应用实例间共享的中间件应使用唯一的对象键以避免键冲突。 以下示例演示如何使用中间件类中定义的唯一对象键：

```c#
public class HttpContextItemsMiddleware
{
    private readonly RequestDelegate _next;
    public static readonly object HttpContextItemsMiddlewareKey = new Object();

    public HttpContextItemsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Items[HttpContextItemsMiddlewareKey] = "K-9";

        await _next(httpContext);
    }
}

public static class HttpContextItemsMiddlewareExtensions
{
    public static IApplicationBuilder 
        UseHttpContextItemsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpContextItemsMiddleware>();
    }
}
```

其他代码可以使用通过中间件类公开的键访问存储在 HttpContext.Items 中的值：

```c#
HttpContext.Items
    .TryGetValue(HttpContextItemsMiddleware.HttpContextItemsMiddlewareKey, 
        out var middlewareSetValue);
SessionInfo_MiddlewareValue = 
    middlewareSetValue?.ToString() ?? "Middleware value not set!";
```



## Cache（缓存）

> 缓存是存储和检索数据的有效方法。 应用可以控制缓存项的生存期。
> 缓存数据未与特定请求、用户或会话相关联。 请注意不要缓存可能由其他用户请求检索的特定于用户的数据。



## DI（依赖关系注入）

使用依赖关系注入可向所有用户提供数据。

定义一项包含数据的服务。 例如，定义一个名为 MyAppData 的类：

```c#
public class MyAppData
{
    // Declare properties and methods
}
```

将服务类添加到 Startup.ConfigureServices：

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<MyAppData>();
}
```

使用数据服务类：

```c#
public class IndexModel : PageModel
{
    public IndexModel(MyAppData myService)
    {
        // Do something with the service
        //    Examples: Read data, store in a field or property
    }
}
```



