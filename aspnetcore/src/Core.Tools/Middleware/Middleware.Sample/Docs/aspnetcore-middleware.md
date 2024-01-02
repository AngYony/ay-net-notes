# ASP.NET Core 中间件



## 中间件概述

每个访问ASP.NET Core应用程序的请求在到达实际处理它并生成响应的代码部分之前，会受到所配置的中间件的操作的影响。术语中间件（middleware）一词指的是在某种链条中组装的软件组件，这种链条被称为应用程序管道(application pipeline)。中间件是一种组装到应用程序管道中的软件，用于处理请求和响应。每个组件可以选择是否将请求传递到管道中的下一个组件，可以在调用管道中的下一个组件之前和之后执行工作。

### 管道架构

管道中的每个组件都可以在处理请求之前和之后进行工作以生成响应，并且可以完全自由地决定是否将请求传递给管道中的下一个组件。

![pipeline](assets/pipeline.jpg)

如图所示，管道来自中间件组件的组合。组件链以一个称为终止中间件的特殊组件结束。终止中间件是触发请求的实际处理和循环转折点的组件。中间件组件是按照注册的顺序调用的，以便对请求进行预处理。在循环结束时，终止的中间件运行，之后，相同的中间件组件有机会以相反的顺序对请求进行后处理。 

### 请求委托（RequestDelegate）

中间件组件是一段完全由请求委托表示的代码，请求委托采用如下形式：

```c#
public delegate Task RequestDelegate(HttpContext context);
```

换句话说，它是一个接收`HttpContext`对象并执行一些工作的函数。根据中间件组件向应用程序管道注册的方式，它可以处理所有传入请求或仅处理选定的请求。注册中间件组件的默认方式如下：

```c#
app.Use(async (context, next) =>
{
    // 第一次处理请求的机会。还没有为请求生成响应。
    <Perform pre-processing of the request>

    // 让位于管道中的下一个组件
    await next();

    // 第二次处理请求的机会。在这里，已经生成了请求的响应。
    <Perform post-processing of the request>
});
```

你可以在正向传递到管道中下一个组件之前和之后运行的代码块中使用流控制语句（如条件语句）。中间件组件可以采用多种形式，前面讨论的请求委托只是最简单的。

### 下一个中间件的重要性

虽然调用下一个委托是可选的，但是需要注意，如果任何中间件组件忽略了调用下一个委托，那么该请求的整个管道都会短路，并且可能根本不会调用默认的终止中间件。

每当中间件组件返回而不向下一个中间件让步时，响应生成过程就在此结束。

两个说明中间件组件短路请求的例子是`UseMvc`和`UseStaticFiles`。前者解析当前URL，如果它可以与其中一个支持的路由匹配，它会将该控件传递给相应的控制器以生成并返回响应。如果URL对应于位于已配置的Web路径中的物理文件，则后者将执行相同的操作。

请求委托用于构建请求管道，请求委托处理每个HTTP请求。

请求委托使用`RunMap()`和`Use()`扩展方法进行配置。单个请求委托可以作为匿名方法内联指定（这种称为内联中间件或并行中间件)，也可以在可重用的类中定义。这些可重用的类和内联匿名方法即为中间件，也称为中间件组件。请求管道中的每个中间件组件负责调用管道中的下一个组件或短路管道。



## 使用 IApplicationBuilder 创建中间件管道

ASP.NET Core请求管道由一系列请求委托组成，这些委托依次调用。如下图所示，沿黑色箭头执行：

![request-delegate-pipeline](assets/request-delegate-pipeline.png)

每个委托都可以在下一个委托之前和之后执行操作。委托还可以决定不将请求传递给下一个委托，这称为短路请求管道。短路通常是可取的，因为它避免了不必要的工作。

`Use()`和`Run()`是`IApplicationBuilder`的扩展方法。

`Run()`方法中的委托终止管道。

`Use()`将多个请求委托链接在一起，它的`next`参数表示管道中的下一个委托，可以通过不调用`next`参数使管道短路。通常可以在下一个委托之前和之后执行操作，例如：

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.Use( async (context,next)=> {
        //如果此处不调用next参数，将会使管道短路
        await next.Invoke();
    });

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
}
```

上述代码中，如果在`app.Use()`方法的委托中，没有调用`next.Invoke()`，将会使管道短路，页面将不会输出任何内容。

注意：在向客户端发送响应后，不能再调用`next.Invoke`，因为调用`next`后写入响应正文将会引发异常。

#### 补充：处理HTTP响应

由于HTTP协议的基本规则，中间件组件是一段很微妙的代码。对输出流的写入是一个顺序操作。因此，一旦写入了响应主体(或刚刚开始写入)，就不能添加HTTP响应头。这是因为，在HTTP响应中，标题出现在正文前面。

只要所有中间件代码都是在团队的完全控制下由内联函数组成的，这就不一定是一个大问题，并且响应头的任何问题都可以很容易地修复。相反，如果你正在编写一个其他人可以使用的第三方中间件组件呢?在这种情况下，组件必须能够在不同的运行时环境中运行。如果组件的业务逻辑需要更改响应主体，该怎么办?

当你的代码开始向输出流写入内容时，它就会阻止后面的其他组件添加HTTP响应头。同时，如果你需要添加HTTP头，那么其他组件可能会偶尔阻塞你。为了解决这个问题，ASP.NET Core中的`Response`对象公开了`OnStarting`事件。事件在第一个组件尝试写入输出流之前触发。因此，如果你的中间件需要编写响应标头，那么你要做的就是为`OnStarting`事件注册一个处理程序，并从那里附加这个标头。

```c#
app.Use(async (context, nextMiddleware) =>
{
    context.Response.OnStarting(() =>
    {
        context.Response.Headers.Add("courtesy", "Programming ASP.NET Core");
        return Task.CompletedTask;
    });

    await nextMiddleware();
});
```

注意，在`OnStarting`处理程序中添加响应头在大多数情况下都可以工作，但需要提及一些边缘情况。特别是，有时您可能需要等待生成整个响应，然后才能确定要添加的标头及其内容。在这种情况下，你可以考虑围绕`Response.Body`属性创建一种内存缓冲区，该属性接收所有写入而不会实际填充响应输出流。当所有中间件组件都完成后，它会将所有内容复制回来。这个想法在这里得到了很好的说明：https：//stackoverflow.com/questions/43403941。



## 添加中间件组件的顺序

向 `Startup.Configure `方法添加中间件组件的顺序，决定了针对请求调用这些组件的顺序，以及响应的相反顺序。 此排序对于安全性、性能和功能至关重要。

常见应用在`Startup.Configure()`方法中添加中间件组件顺序如下：

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
    }
    else
    {
        //1、异常/错误处理
        app.UseExceptionHandler("/Error");
        //2、HTTP 严格传输安全协议
        app.UseHsts();
    }
    //3、HTTPS 重定向
    app.UseHttpsRedirection();
    //4、静态文件服务器
    app.UseStaticFiles();
    //5、Cookie 策略实施
    app.UseCookiePolicy();
    //6、身份验证
    app.UseAuthentication();
    //7、会话
    app.UseSession();
    //8、MVC
    app.UseMvc();
}

```

上述中的中间件扩展方法都基于`IApplicationBuilder`，下面对这些组件的顺序进行说明

1. 异常错误处理：该组件必须是添加管道的第一个中间件，用于捕获稍后调用中发生的任何异常。
2. HTTP 严格传输安全协议：在发送响应之前，修改请求的组件之后。
3. HTTPS 重定向：在使用 URL 的组件之前添加。
4. 静态文件服务器：尽早在管道中调用静态文件中间件，以便它可以处理请求并使其短路，而无需通过剩余组件。 静态文件中间件不提供授权检查。 可公开访问由静态文件中间件服务的任何文件，包括 wwwroot 下的文件。如果静态文件中间件未处理请求，则请求将被传递给执行身份验证的身份验证中间件 (`UseAuthentication`)。
5. Cookie 策略实施：在发出Cookie的中间件之前添加，应该早于身份验证、会话、MVC（`TempData`）。
6. 身份验证：在需要 `HttpContext.User` 之前添加，身份验证不使未经身份验证的请求短路。 虽然身份验证中间件对请求进行身份验证，但仅在 MVC 选择特定 Razor 页或 MVC 控制器和操作后，才发生授权（和拒绝）。
7. 会话
8. MVC：一般在最后添加。

实际应用中，可以根据上述顺序进行添加中间件，注意：添加中间件的先后顺序在一定程度上会影响程序的性能，因此需要特别注意。

其他内置中间件请参阅：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1#built-in-middleware

### Use、Run和Map

`Use`、`Run`和`Map`主要用于配置HTTP管道，除此之外三者的作用如下：

- `Use`：如果不调用next请求委托，可以实现管道的短路。
- `Run`：它是一种约定，某些中间件组件可以公开在管道末尾运行的方法（`Run[Middleware]`）。
- `Map`：用作创建管道分支的约定，Map*基于给定请求路径的匹配项来创建请求管道分支。如果请求路径以给定路径开头，则执行分支。

需要特别注意的是：

可以多次调用`Run`方法，但只处理第一个方法。这是因为`Run`方法是请求处理结束的地方，也是管道链流被反转的地方。在第一次找到正在运行的中间件时，就会发生反转。在第一个中间件之后定义的任何正在运行的中间件永远不会到达。

例如：

```c#
public void Configure(IApplicationBuilder app)
{
    // Terminating middleware
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Courtesy of 'Programming ASP.NET Core'");
    });

    // No errors, but never reached
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Courtesy of 'Programming ASP.NET Core' repeated");
    });
}
```

中间件组件在`Startup`类的`Configure`方法中注册。

#### Map()、MapWhen()方法的使用

首先在Startup中创建如下几个方法：

```c#
private static void HandleTestMap1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        string p= context.Request.Path;
        string b = context.Request.PathBase;
        Console.WriteLine("Request.Path:"+p);
        Console.WriteLine("Request.PathBase:" + b);
        await context.Response.WriteAsync("Test Map 1, "+context.Request.PathBase);
    });
}

private static void HandleTestMap2(IApplicationBuilder app)
{
    app.Run(async context=>await context.Response.WriteAsync("Test Map 2, "+ context.Request.PathBase));
}

private static void HandleTestLevelMap(IApplicationBuilder app)
{
    app.Run(async context => await context.Response.WriteAsync("Test Map level , " + context.Request.PathBase));
}

private static void HandleTestLevelMap2(IApplicationBuilder app)
{
    app.Run(async context => await context.Response.WriteAsync("Test Map level 2, "+context.Request.PathBase));
}

private static void HandleTestLevelMap3(IApplicationBuilder app)
{
    app.Run(async context => await context.Response.WriteAsync("Test Map level 3, " + context.Request.PathBase));
}


private static void HandleTestMapWhen(IApplicationBuilder app)
{
    app.Run(async context=> {
        var branchVer= context.Request.Query["branch"];
        await context.Response.WriteAsync($"Test MapWhen, Branch used={branchVer}");
    });
}
```

使用`Map()`方法，如果请求路径以给定路径开头，则执行分支。`MapWhen`基于给定谓词的结果创建请求管道分支。`Func<HttpContext, bool>` 类型的任何谓词均可用于将请求映射到管道的新分支。

在`Configure`方法中，定义`Map`匹配规则，如下：

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    #region  Map的简单使用
    // 匹配规则：/map1
    app.Map("/map1", HandleTestMap1);
    // 匹配规则：/map2
    app.Map("/map2", HandleTestMap2);
    #endregion


    #region Map的嵌套
    app.Map("/level", levelApp =>
    {
        levelApp.Map("/l2", levelApp2 =>
        {
            //匹配规则：/level/l2/l3
            levelApp2.Map("/l3", HandleTestLevelMap3);
        });
    });
    #endregion

    #region Map匹配多段
    app.Map("/level", levelApp =>
    {
        //匹配规则：/level/l2/l3
        levelApp.Map("/l2/l3", HandleTestLevelMap3);
        //匹配规则：/level/l2
        levelApp.Map("/l2", HandleTestLevelMap2);

    });
    #endregion

    //MapWhen的使用，如果查询字符串中存在变量branch，就执行该分支
    //匹配：?branch=
    app.MapWhen(
       context => context.Request.Query.ContainsKey("branch"),
       HandleTestMapWhen);


    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
}
```

上述中的代码可以正确的匹配到相关的路径规则，实际使用时，有以下几点需要特别注意：

- 如果有多层路径，优先使用多段匹配而不是嵌套匹配，原因是嵌套没有多段直观，而且对于同时匹配父级和子级的路径需求，嵌套不能够很好的实现。例如在上述的嵌套方法中，如果想要同时满足`/level/l2`和`/level/l2/l3`，表面上可以这样实现：

  ```c#
  app.Map("/level", levelApp =>
  {
  
      //代码一，一旦加入该行代码，下述的l3将失效
      //levelApp.Map("/l2", HandleTestLevelMap2); //想要匹配到/level/l2/
      
      //代码二
      levelApp.Map("/l2", levelApp2 =>
      {
          //匹配规则：/level/l2/l3
          levelApp2.Map("/l3", HandleTestLevelMap3);
      });
  });
  ```

  实际上一旦这样编码，当输入`/level/l2/l3`时，并不会执行`HandleTestLevelMap3`方法，而是会执行`HandleTestLevelMap2`方法，因为`/l2`优先级更高。同样，如果将`levelApp.Map("/l2", HandleTestLevelMap2)`写在代码二的后面，此时代码二对应的优先级更高，只能解析`/level/l2/l3`，不能解析`/level/l2`。综上所述，多个`Map()`方法基于同一个路径定义了多种规则，将以第一次定义的为主。即代码一和代码二，谁先指定，就以谁进行匹配。解决此类问题的办法就是使用多段匹配。

- 使用多段匹配时，应该先列出层级多的路径，再列出层级少的路径，例如上述代码中的：

  ```c#
   #region Map匹配多段
   app.Map("/level", levelApp =>
   {
       //代码一：匹配规则：/level/l2/l3
       levelApp.Map("/l2/l3", HandleTestLevelMap3);
       
       //代码二：匹配规则：/level/l2
       levelApp.Map("/l2", HandleTestLevelMap2);
   });
   #endregion
  ```

  如果将代码二放在了代码一之前，那么在请求`/level/l2/l3`时，将会匹配到代码二中指定的规则，并执行`HandleTestLevelMap2`方法，`HandleTestLevelMap3`不会被执行。

最后，一定要注意，无论使用哪种形式，定义匹配规则的顺序非常重要，直接影响到是否能够匹配到定义的规则。

> 通常，Map调用放在管道中较早的位置。中间件组件是经典ASP.NET中HTTP模块的概念对等物。但是，Map方法与HTTP模块有一个关键区别。实际上，HTTP模块无法过滤URL。在编写HTTP模块时，您必须自己检查URL并决定是处理还是忽略该请求。没有办法只为某些URL注册模块。
>



## 编写中间件

通常，中间件封装在类中，并且通过扩展方法公开。

在编写中间件之前，先考虑如何在`Startup.Configure`中添加一个中间件，代码如下：

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseDeveloperExceptionPage();
	
	//添加自定义中间件
    app.Use((context, next) =>
    {
        var stuQuery = context.Request.Query["s"];
        if (!string.IsNullOrWhiteSpace(stuQuery))
        {
            //获取值
            var student = new Student(stuQuery);
            //使用获取的值
            MyClass.StudentA = student;
            MyClass.StudentB = student;
        }
        //必不可少的一句
        return next();
    });

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync(
            $"Hello {MyClass.StudentB.Name}");
    });
}
```

上述代码中的中间件，主要用于获取查询字符串中的参数s的值，其中必须要调用`next()`方法，以便使请求通过该中间件之后，继续向下执行。下面将上述的中间件进行封装，然后使用扩展方法的形式进行公开。

#### 第一步：编写包含请求委托的中间件类：

```c#
public class OctOceanMiddleware
{

    private readonly RequestDelegate _next;

    public OctOceanMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    //定义处理任务
    public async Task InvokeAsync(HttpContext context)
    {
        //此处进行一些操作

        //例如获取查询字符串参数
        var query = context.Request.Query["q"];
        if(!string.IsNullOrWhiteSpace(query))
        {
            MyClass.StudentB= new Student(query);
            //其他的一些操作
        }

        //最后必不可少的代码
        //继续下一个中间件过程
        await _next(context);
    }
}
```

构造函数接收`RequestDelegate`指针，该指针指向已配置链中的下一个中间件组件，并将其保存到内部成员。相反，`Invoke`方法只包含您将传递给`Use`方法的代码，您可以在其中注册内联中间件。` Invoke`方法的签名必须与`RequestDelegate`类型的签名匹配。

上述代码的格式基本固定，注意最后要调用`next(context)`方法，否则将会短路。



#### 第二步：通过IApplicationBuilder扩展方法公共中间件：

```c#
public static  class OctOceanMiddlewareExtensions
{
    public static IApplicationBuilder UseOctOceanMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OctOceanMiddleware>();
    }
}
```

`UseMiddleware <T>`方法将指定的类型注册为中间件组件。

#### 第三步：在Startup.Configure调用中间件：

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseDeveloperExceptionPage();
    
	//调用中间件
    app.UseOctOceanMiddleware();

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync(
            $"Hello {MyClass.StudentB.Name}");
    });
}
```

在调用扩展方法时，注意导入方法所在的命名空间。

注意：中间件是在应用启动时构造的，而不是按请求构造的，因此在每个请求过程中，中间件构造函数使用的范围内，生存期服务不与其他依赖关系注入类型共享。

如果必须在中间件和其他类型之间共享范围内服务，请将这些服务添加到 `Invoke` 方法的签名。 `Invoke` 方法可接受由 DI 填充的其他参数。具体参阅：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1#per-request-dependencies



## 基于工厂（MiddlewareFactory）激活中间件

前面介绍的中间件都是使用约定（常规）激活的，实际使用中，更多的是基于`MiddlewareFactory`类激活，

`UseMiddleware` 扩展方法会检查中间件的已注册类型是否实现 `IMiddleware`。 如果是，则使用在容器中注册的 `IMiddlewareFactory` 实例来解析 `IMiddleware` 实现，而不使用基于约定的中间件激活逻辑。中间件在应用的服务容器中注册为`Scoped`或`Transient`服务。

`IMiddlewareFactory`/`IMiddleware `是中间件激活的扩展点。它按请求（Scoped服务的注入）激活，可以让中间件强类型化。因此Scoped服务可以注入到中间件的构造函数中。

下面使用一个示例进行详细说明，该示例主要用于记录由查询字符串参数 (key) 提供的值。 中间件使用插入的数据库上下文（作用域服务）将查询字符串值记录在内存中数据库。

其中数据库上下文类如下：

```c#
public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions options):base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}
```



### 第一步：定义一个实现IMiddleware的中间件

`IMiddleware` 定义应用的请求管道的中间件。` InvokeAsync(HttpContext, RequestDelegate) `方法处理请求，并返回代表中间件执行的 Task。

下面分别定义两种不同方式激活的中间件

#### 使用约定（常规）激活的中间件

```c#
//常规中间件
public class ConventionalMiddleware
{
    private readonly RequestDelegate _next;

    public ConventionalMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context,AppDbContext db)
    {
        var keyValue = context.Request.Query["key"];
        if(!string.IsNullOrWhiteSpace(keyValue))
        {
            db.Add(new Student("常规方式："+keyValue));
            await db.SaveChangesAsync();
        }
        //必不可少
        await _next(context);
    }
}
```

#### 使用 MiddlewareFactory 激活的中间件（推荐）

```c#
//MiddlewareFactory中间件
public class FactoryActivatedMiddleware : IMiddleware
{
    private readonly AppDbContext _db;
    public FactoryActivatedMiddleware(AppDbContext db)
    {
        _db = db;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var keyValue = context.Request.Query["key"];
        if(!string.IsNullOrWhiteSpace(keyValue))
        {
            _db.Add(new Student("工厂方式："+keyValue));
            await _db.SaveChangesAsync();
        }
        await next(context);
    }
}
```

### 第二步：添加扩展

```c#
public static class ProMiddlewareExtensions
{
    //常规
    public static IApplicationBuilder UseConventionalMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ConventionalMiddleware>();
    }
	//工厂
    public static IApplicationBuilder UseFactoryActivatedMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FactoryActivatedMiddleware>();
    }
}
```

注意：无法通过 `UseMiddleware` 将对象传递给工厂激活的中间件，例如上述代码，如果使用`builder.UseMiddleware<FactoryActivatedMiddleware>(option);`这里使用了`option`对象进行传递，此时在运行时将会引发`NotSupportedException`异常。

### 第三步：添加到Startup的内置容器中

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<CookiePolicyOptions>(options => {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
    });
    //添加数据库上下文
    services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("myMemoryDb"));
    
    //将工厂激活的中间件添加到内置容器中
    services.AddTransient< FactoryActivatedMiddleware>();

    services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseDeveloperExceptionPage();
    app.UseDatabaseErrorPage();

    //在请求处理管道中注册中间件
    app.UseConventionalMiddleware();
    app.UseFactoryActivatedMiddleware();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCookiePolicy();
    app.UseMvc();
    
}
```

上述代码呈现页面如下：

```c#
public class IndexModel : PageModel
{

    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public List<Student> Students { get; private set; }
    public async Task OnGet()
    {
        Students = await _db.Students.ToListAsync();
    }
}
```

前台展示页面：

```html
@page
@model MiddlewareCompilation.Pages.IndexModel
@{
    Layout = null;
}

<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    @if (Model.Students.Count == 0)
    {
        <p>没有Student显式</p>
    }
    else
    {
        <ul>
            @foreach (var stu in Model.Students)
            {
                <li>@stu.Name</li>
            }
        </ul>
    }
</body>
</html>
```



### IMiddlewareFactory

`IMiddlewareFactory` 提供了中间件的创建方法。 中间件工厂实现在容器中注册为Scoped服务。

`IMiddlewareFactory`包含以下两个方法：

`Create(Type)`：为每个请求创建一个中间件实例。

`Release(IMiddleware)`：在每个请求结束时释放一个`IMiddleware`实例。

你可以实现该接口，定义自己的中间件工厂，具体可以参考链接：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/extensibility-third-party-container?view=aspnetcore-2.1#imiddlewarefactory





重要补充部分：

在中间件内使用有作用域的服务时，请将该服务注入至 `Invoke` 或 `InvokeAsync` 方法。 请不要通过构造函数注入进行注入，因为它会强制服务的行为与单一实例类似。 有关更多信息，请参见[ASP.NET Core 中间件](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/index?view=aspnetcore-2.2)。

https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes



注意：在中间件内使用有作用域的服务时，请将该服务注入至 Invoke 或 InvokeAsync 方法。 请不要通过构造函数注入进行注入，因为它会强制服务的行为与单一实例类似。



