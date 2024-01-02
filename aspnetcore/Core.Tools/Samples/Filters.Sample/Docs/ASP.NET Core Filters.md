# ASP.NET Core 筛选器



筛选器应用方式或注册方式：

- 以特性的形式作用在具体方法上

  ```csharp
  [CusActionFilterOnMethod]
  public void DoHomework(){ }
  ```

- 以特性的形式作用在类上

  ```c#
  [CusActionFilterOnClass]
  public class DoHomeworkController : ControllerBase{ }
  ```

- 以接口的形式进行全局注册

  ```csharp
  builder.Services.AddControllers(opt=> { opt.Filters.Add(typeof(CusActionFilterOnGlobalAttribute)); });
  ```

  

自定义过滤器的两种方式：

- 继承内置的过滤器特性
- 实现过滤器接口类





过滤器之间的通信，可以使用：HttpContext.Item



-----------------------------------------



说明：本文所说的筛选器，只针对ASP.NET Core MVC应用，而不是Razor Pages应用。

在ASP.NET Core MVC中，通过使用筛选器，可在请求处理的管道中的特定阶段之前或之后运行代码，筛选器可以避免跨操作的重复代码。



## 筛选器的工作原理

当MVC选择了要执行的操作之后，将会运行筛选器管道，所有的筛选器都在筛选器管道内运行，筛选器管道中的不同阶段会执行不同类型的筛选器。

### 筛选器类型

- 授权筛选器：最先运行，用于确定当前请求是否已针对当前用户授权，如果请求未获授权，可以让管道短路。
- 资源筛选器：是授权后最先处理请求的筛选器。可以在筛选器管道的其余阶段运行之前，以及管道的其余阶段完成之后运行代码。它们在模型绑定之前运行，所以可以影响模型绑定。可以通过资源筛选器来实现缓存，或以其他方式让筛选器管道短路，从而提高程序性能。
- 操作筛选器：可以在调用单个控制器操作方法之前和之后立即运行代码。可以通过操作筛选器来处理传入某个操作方法的参数，以及从该操作方法返回的结果。（注：不可以在Razor Pages应用中使用操作筛选器）
- 异常筛选器：在向响应正文写入任何内容之前，通过异常筛选器可以对未经处理的异常应用全局策略。
- 结果筛选器：可以在单个控制器操作方法中，执行的结果之前和之后立即运行代码。仅当操作方法成功执行时，结果筛选器才会运行。对于必须围绕视图或格式化的逻辑执行的程序，结果筛选器会很有用。

下图展示了这些筛选器类型在筛选器管道中的交互方式：

![mvc-filter01](assets/mvc-filter01.png)

## 筛选器的实现

实现一个筛选器时，通常有两种方式，方式一是实现内置的接口，这些接口一般都支持同步或异步实现。方式二是继承内置的筛选器特性，这些内置的筛选器特性，本质上也是实现了方式一中提到的内置的接口，所以，一般情况下，推荐使用方式二来创建自己的筛选器，只有在方式二不能解决的情况下，才使用方式一创建筛选器。

不同类型筛选器的实现大致一样，这里只以ActionFilter为例。

### IActionFilter和IAsyncActionFilter

这两个接口用于实现操作筛选器，只不过实现的一个是同步筛选器，另一个是异步筛选器。

#### IActionFilter

IActionFilter定义了OnStageExecuting 方法和 OnStageExecuted 方法，可以在其管道阶段之前和之后以同步的方式运行代码。

例如，在调用每个操作方法之前调用OnActionExecuting方法，在每个操作方法返回之后调用OnActionExecuted方法：

```c#
public class SampleActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //在每个操作方法返回之后调用
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //在调用每个操作方法之前调用
    }
}
```

为了应用筛选器，需要将其添加到选项中：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options=> {
        options.Filters.Add(typeof(SampleActionFilter));
    })
    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}
```

可以在筛选器中添加断点，运行程序，会发现对于每一个操作方法，都会在执行之前和执行之后调用筛选器中对应的方法。

#### IAsyncActionFilter

异步筛选器定义单一的 OnStageExecutionAsync 方法。 此方法采用 {FilterType}ExecutionDelegate 委托来执行筛选器的管道阶段。 

 例如，ActionExecutionDelegate 调用该操作方法或下一个操作筛选器，用户可以在调用它之前和之后执行代码：

```c#
public class SampleAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
    ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //调用之前执行
        var resultContext = await next();
        //调用之后执行
    }
}
```

可以在await next()语句之前和之后添加断点，来观察执行的先后顺序。

若要使用该筛选器，也需要将其添加到选项中：

```c#
 options.Filters.Add(typeof(SampleAsyncActionFilter));
```

> 可以在单个类中为多个筛选器阶段实现接口。 例如，ActionFilterAttribute 类实现 IActionFilter、IResultFilter 及其异步等效接口。

备注：只需要实现同步筛选器或异步筛选器中的任意一个，而不是同时实现。异步筛选器会被优先调用，因此实际应用中，推荐使用一部筛选器进行实现。

### IFilterFactory

IFilterFactory可以在应用启动时，无需显示的设置精确的筛选器管道，提供了一种更灵活的设计。

IFilterFactory的定义如下：

```c#
public interface IFilterFactory : IFilterMetadata
{
	bool IsReusable { get; }
	IFilterMetadata CreateInstance(IServiceProvider serviceProvider);
}
```



IFilterFactory实现了IFilterMetadata接口，因此，IFilterFactory 实例可在筛选器管道中的任意位置用作 IFilterMetadata 实例。 当该框架准备调用筛选器时，它会尝试将其转换为 IFilterFactory。 如果强制转换成功，则调用 CreateInstance 方法来创建将调用的 IFilterMetadata 实例。

可以基于特性实现IFilterFactory接口来创建筛选器：

```c#
public class AddHeaderWithFactoryAttribute : Attribute, IFilterFactory
{
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        return new InternalAddHeaderFilter();
    }

    //定义一个实现指定类型的筛选器
    private class InternalAddHeaderFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Internal", new string[] { "Header Added" });
        }
    }
}
```

应用该筛选器：

```c#
[AddHeaderWithFactory]
public IActionResult HeaderWithFactory(){
    return Content("调用AddHeaderWithFactory");
}
```

### 内置筛选器特性

创建筛选器的另一种方式是使用内置筛选器特性，这些特性有以下几种：

- ActionFilterAttribute，用于操作筛选器
- ExceptionFilterAttribute，用于异常筛选器
- ResultFilterAttribute，用于结果筛选器
- FormatFilterAttribute，用于格式筛选器（不常用）
- ServiceFilterAttribute，用于从DI访问的筛选器
- TypeFilterAttribute，与 ServiceFilterAttribute 类似，但不会直接从 DI 容器解析其类型。



## ActionFilterAttribute和操作筛选器

操作筛选器可以通过实现IActionFilter或IAsyncActionFilter 接口来定义，也可以继承自ActionFilterAttribute特性类。

### 实现 IActionFilter 或 IAsyncActionFilter 接口

实现了IActionFilter接口示例：

```c#
public class SampleActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //在每个操作方法返回之后调用
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //在调用每个操作方法之前调用
    }
}
```

该接口提供的OnActionExecuting()方法中，包含ActionExecutingContext参数，ActionExecutingContext具有以下属性：

- `ActionArguments`：用于处理对操作的输入。
- `Controller`：用于处理控制器实例。
- `Result`：设置此属性会使操作方法和后续操作筛选器的执行短路。 引发异常也会阻止操作方法和后续筛选器的执行，但会被视为失败，而不是一个成功的结果。

而OnActionExecuted()方法中的ActionExecutedContext参数，在提供了上述中的Controller和Result属性之外，还提供了下述属性：

- `Canceled`：如果操作执行已被另一个筛选器设置短路，则为 true。
- `Exception`：如果操作或后续操作筛选器引发了异常，则为非 NULL 值。 将此属性设置为 NULL 可有效地“处理”异常，并且将执行 `Result`，就像它是从操作方法正常返回的一样。

对于IAsyncActionFilter接口：

```c#
public class SampleAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
    ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //调用之前
        var resultContext = await next();
        //调用之后
    }
}
```

一个向 `ActionExecutionDelegate` 的调用可以达到以下目的：

- 执行所有后续操作筛选器和操作方法。
- 返回 `ActionExecutedContext`。

若要设置短路，可将 ActionExecutingContext.Result 分配到某个结果实例，并且不调用ActionExecutionDelegate。

### 继承自ActionFilterAttribute

操作筛选器可用于验证模型状态，并在状态为无效时返回任何错误：

```c#
public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
```

`OnActionExecuted` 方法在操作方法之后运行，可通过 `ActionExecutedContext.Result` 属性查看和处理操作结果。 如果操作执行已被另一个筛选器设置短路，则 `ActionExecutedContext.Canceled` 设置为 true。 如果操作或后续操作筛选器引发了异常，则 `ActionExecutedContext.Exception` 设置为非 NULL 值。 将 `ActionExecutedContext.Exception` 设置为 null：

- 有效地“处理”异常。
- 执行 `ActionExecutedContext.Result`，从操作方法中将它正常返回。



## ExceptionFilterAttribute和异常筛选器

异常筛选器可以通过实现IExceptionFilter 或 IAsyncExceptionFilter 接口来定义，也可以通过派生自ExceptionFilterAttribute来定义。

下面的异常筛选器示例，使用自定义错误视图显示在开发环境下发生异常的详细信息：

```c#
public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly IModelMetadataProvider _modelMetadataProvider;

    public CustomExceptionFilterAttribute(
        IHostingEnvironment hostingEnvironment,
        IModelMetadataProvider modelMetadataProvider)
    {
        _hostingEnvironment = hostingEnvironment;
        _modelMetadataProvider = modelMetadataProvider;
    }

    public override void OnException(ExceptionContext context)
    {
        if (!_hostingEnvironment.IsDevelopment())
        {
            // do nothing
            return;
        }
        var result = new ViewResult { ViewName = "CustomError" };
        result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
        result.ViewData.Add("Exception", context.Exception);
        // TODO: 通过ViewData传递其他详细数据
        context.Result = result;
    }
}
```

异常筛选器：

- 没有之前和之后的事件。
- 实现 `OnException` 或 `OnExceptionAsync`。
- 处理控制器创建、模型绑定、操作筛选器或操作方法中发生的未经处理的异常。
- 不能捕获资源筛选器、结果筛选器或 MVC 结果执行中发生的异常。
- 非常适合捕获发生在 MVC 操作中的异常。

> 若要处理异常，请将 ExceptionContext.ExceptionHandled 属性设置为 true，或编写响应。 这将停止传播异常。 异常筛选器无法将异常转变为“成功”。 只有操作筛选器才能执行该转变。

注意：只有在需要根据所选 MVC 操作以不同方式执行错误处理时，才使用异常筛选器。其他情况下，强烈建议使用中间件处理异常，因为中间件更加灵活。



## ResultFilterAttribute和结果筛选器

可以通过实现IResultFilter 和 IAsyncResultFilter接口来定义结果筛选器，也可以继承自ResultFilterAttribute抽象类来定义。除此之外，还可以实现IAlwaysRunResultFilter 和 IAsyncAlwaysRunResultFilter接口。

### 实现IResultFilter 和 IAsyncResultFilter接口

下述示例用于定义一个添加HTTP标头的结果筛选器：

```c#
public class AddHeaderFilterWithDi : IResultFilter
{
    private ILogger _logger;

    public AddHeaderFilterWithDi(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<AddHeaderFilterWithDi>();
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var headerName = "WyOnResultExecuting";
        context.HttpContext.Response.Headers.Add(headerName, new string[] { "AAA", "BBB" });
        _logger.LogInformation($"Header added:{headerName}");
    }
}
```

要执行的结果类型取决于所执行的操作。 返回视图的 MVC 操作会将所有 Razor 处理作为要执行的 ViewResult 的一部分。 API 方法可能会将某些序列化操作作为结果执行的一部分。

当操作或操作筛选器生成操作结果时，仅针对成功的结果执行结果筛选器。 当异常筛选器处理异常时，不执行结果筛选器。

`OnResultExecuting` 方法可以将 `ResultExecutingContext.Cancel` 设置为 true，使操作结果和后续结果筛选器的执行短路。 设置短路时，通常应写入响应对象，以免生成空响应。 如果引发异常，则会导致：

- 阻止操作结果和后续筛选器的执行。
- 结果被视为失败而不是成功。

当 `OnResultExecuted` 方法运行时，响应可能已发送到客户端，而且不能再更改（除非引发了异常）。 如果操作结果执行已被另一个筛选器设置短路，则 `ResultExecutedContext.Canceled` 设置为 true。

如果操作结果或后续结果筛选器引发了异常，则 `ResultExecutedContext.Exception` 设置为非 NULL 值。 将 `Exception` 设置为 NULL 可有效地“处理”异常，并防止 MVC 在管道的后续阶段重新引发该异常。 在处理结果筛选器中的异常时，可能无法向响应写入任何数据。 如果操作结果在其执行过程中引发异常，并且标头已刷新到客户端，则没有任何可靠的机制可用于发送失败代码。

对于 `IAsyncResultFilter`，通过调用 `ResultExecutionDelegate` 上的 `await next` 可执行所有后续结果筛选器和操作结果。 若要设置短路，可将 `ResultExecutingContext.Cancel` 设置为 true，并且不调用 `ResultExecutionDelegate`。

### AddHeaderAttribute

例如，通过结果筛选器向响应添加标头。

```c#
public class AddHeaderAttribute : ResultFilterAttribute
{
    private readonly string _name;
    private readonly string _value;

    public AddHeaderAttribute(string name, string value)
    {
        _name = name;
        _value = value;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add(
        _name, new string[] { _value });

        base.OnResultExecuting(context);
    }
}
```

可以将创建的筛选器特性添加到控制器或操作方法上，并指定HTTP标头的名称和值：

```c#
[AddHeader("Author","Smallz")]
public class HomeController:Controller
{
	...
}
```



### IAlwaysRunResultFilter 和 IAsyncAlwaysRunResultFilter

IAlwaysRunResultFilter 和 IAsyncAlwaysRunResultFilter 接口声明了一个针对操作结果运行的 IResultFilter 实现。 除非应用 IExceptionFilter 或 IAuthorizationFilter 并使响应短路，否则会将筛选器应用于操作结果。

也就是说，这些“始终运行”的筛选器始终运行，除非异常或授权筛选器使它们短路。 除 IExceptionFilter 和 IAuthorizationFilter 之外的筛选器不会使它们短路。

例如，以下筛选器始终运行，并在内容协商失败时设置422状态代码：

```c#
public class UnprocessableResultFilter : Attribute, IAlwaysRunResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is StatusCodeResult statusCodeResult &&
            statusCodeResult.StatusCode == 415)
        {
            context.Result = new ObjectResult("Can't process this!")
            {
                StatusCode = 422,
            };
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
```



## ServiceFilterAttribute/TypeFilterAttribute和筛选器的依赖关系注入

当创建好一个筛选器时，如果筛选器实现的是接口（而不是内置的筛选器特性），要应用筛选器，需要在Startup.ConfigureServices()方法中进行添加，可以按照类型或实例添加筛选器，取决于传递给Add()方法的参数：

```c#
//按实例添加
options.Filters.Add(new AddHeaderAttribute("GlobalAddHeader", "Global Smalzz"));
//按类型添加
options.Filters.Add(typeof(SampleActionFilter));
```

如果添加实例，该实例将用于每个请求。 如果添加类型，则将激活该类型，这意味着将为每个请求创建一个实例，并且依赖关系注入 (DI) 将填充所有构造函数依赖项。 按类型添加筛选器等效于`filters.Add(new TypeFilterAttribute(typeof(MyFilter)))`。

**如果将筛选器作为特性实现，并直接添加到控制器类或操作方法中，则该筛选器不能由依赖关系注入 (DI) 提供构造函数依赖项**。 这是因为特性在应用时必须提供自己的构造函数参数。 这是特性工作原理上的限制。如果基于特性实现的筛选器具有一些需要从DI访问的依赖项，只能使用以下几种受支持的方式：

- ServiceFilterAttribute
- TypeFilterAttribute
- 在特性上实现的 IFilterFactory

只有上述几种方式可以实现筛选器对DI依赖项的访问。

>记录器就是一种可能需要从 DI 获取的依赖项。 但是，应避免单纯为进行日志记录而创建和使用筛选器，因为内置的框架日志记录功能可能已经提供用户所需。 如果要将日志记录功能添加到筛选器，它应重点关注业务领域问题或特定于筛选器的行为，而非 MVC 操作或其他框架事件。

### ServiceFilterAttribute

ServiceFilterAttribute实现了IFilterFactory接口，并且还提供了IsReusable属性。

在DI中注册实现筛选器的类型，ServiceFilterAttribute可以从DI中检索筛选器实例。

下述示例将筛选器的类型添加到DI中，并通过ServiceFilterAttribute特性引用它。

创建一个结果筛选器：

```c#
public class AddHeaderFilterWithDi : IResultFilter
{
    private ILogger _logger;

    public AddHeaderFilterWithDi(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<AddHeaderFilterWithDi>();
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var headerName = "WyOnResultExecuting";
        context.HttpContext.Response.Headers.Add(headerName, new string[] { "AAA", "BBB" });
        _logger.LogInformation($"Header added:{headerName}");
    }
}
```

可以看到筛选器中有对DI的依赖项ILogger的引用，若要使用该筛选器，需要通过ServiceFilterAttribute特性应用到控制器操作方法上：

```c#
[ServiceFilter(typeof(AddHeaderFilterWithDi))]
public IActionResult ServiceFilterTest(){
    return Content("ServiceFilter应用");
}
```

同时，还需要将筛选器注册到DI中：

```c#
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddScoped<AddHeaderFilterWithDi>(); 
}
```

上文中提到的ServiceFilterAttribute的IsReusable属性，在设置值时，需要特别注意：

> `IsReusable` 设置会提示：筛选器实例可能在其创建的请求范围之外被重用。 该框架不保证在稍后的某个时刻将创建筛选器的单个实例，或不会从 DI 容器重新请求筛选器。 如果使用的筛选器依赖于具有除单一实例以外的生命周期的服务，请避免使用 `IsReusable`。

### TypeFilterAttribute

TypeFilterAttribute 与 ServiceFilterAttribute 类似，但不会直接从 DI 容器解析其类型。使用 TypeFilterAttribute 引用的类型不需要先注册在容器中。 它们具备由容器实现的依赖项。TypeFilterAttribute 可以选择为类型接受构造函数参数。

例如，定义如下实现了IActionFilter接口的控制器筛选器：

```c#
public class LogConstantFilter : IActionFilter
{
    private readonly string _value;
    private readonly ILogger<LogConstantFilter> _logger;

    public LogConstantFilter(string value, ILogger<LogConstantFilter> logger)
    {
        _logger = logger;
        _value = value;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation(_value);
    }
}
```

该筛选器提供了构造函数，下面代码演示了如何应用该筛选器，并为其传入参数：

```c#
[TypeFilter(typeof(LogConstantFilter),
        Arguments = new object[] { "Method 'Hi' called" })]
public IActionResult Hi(string name)
{
    return Content("Hi," + name);
}
```

由于使用TypeFilterAttribute应用的类型不需要先在容器中注册，因此上述筛选器类型不用在Startup中进行注册。并且可以通过Arguments属性，为筛选器的构造函数传入参数值，同时TypeFilterAttribute也具有IsReusable属性，使用该属性时，也需要注意：

> 使用 `TypeFilterAttribute` 时，`IsReusable` 设置会提示：筛选器实例可能在其创建的请求范围之外被重用。 该框架不保证将创建筛选器的单一实例。 如果使用的筛选器依赖于具有除单一实例以外的生命周期的服务，请避免使用 `IsReusable`。

### 在特性上实现IFilterFactory

如果筛选器的构造函数只需要具有DI填充的依赖项，而不需要其他任何参数，那么在控制器类和操作方法上，可以不使用`[TypeFilter(typeof(FilterType))]`，而改用自己定义的特性。自己定义的特性，需要继承自实现了IFilterFactory接口的类（特性）：

```c#
public class SampleActionFilterAttribute : TypeFilterAttribute
{
    public SampleActionFilterAttribute() : base(typeof(SampleActionFilterImpl))
    {
    }

    private class SampleActionFilterImpl : IActionFilter
    {
        private readonly ILogger _logger;

        public SampleActionFilterImpl(ILoggerFactory _loggerFactory)
        {
            _logger = _loggerFactory.CreateLogger<SampleActionFilterAttribute>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("操作完成");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("开始执行操作");
        }
    }
}
```

可以使用 [SampleActionFilter] 语法将此筛选器应用于类或方法，而不必使用 [TypeFilter] 或 [ServiceFilter]。

```c#
[SampleActionFilter]
public IActionResult HiFactory(){
    return Content("在属性上实现IFilterFactory");
}
```

上述三种形式创建的筛选器如何选择：

- 如果筛选器的构造函数有其他参数，只能选择TypeFilterAttribute。
- 如果筛选器的构造函数只有DI的依赖项参数，推荐使用继承自实现了IFilterFactory接口的类
- 除此之外，使用ServiceFilterAttribute。



## 授权筛选器

授权筛选器：

- 控制对操作方法的访问。
- 是筛选器管道中第一个要执行的筛选器。
- 具有在它之前的执行的方法，但没有之后执行的方法。

只有在编写自己的授权框架时，才应编写自定义授权筛选器。 建议配置授权策略或编写自定义授权策略，而不是编写自定义筛选器。 内置筛选器实现只负责调用授权系统。

切勿在授权筛选器内引发异常，因为没有任何能处理该异常的组件（异常筛选器不会进行处理）。 在出现异常时请小心应对。



## 资源筛选器

资源筛选器：

- 实现 `IResourceFilter` 或 `IAsyncResourceFilter` 接口，
- 它们的执行会覆盖筛选器管道的绝大部分。
- 只有授权筛选器在资源筛选器之前运行。

如果需要使某个请求正在执行的大部分工作短路，资源筛选器会很有用。 例如，如果响应在缓存中，则缓存筛选器可以绕开管道的其余阶段。（该示例见下文中的“在筛选器中取消和设置短路”）

官方推荐示例为 [DisableFormValueModelBindingAttribute](https://github.com/aspnet/Entropy/blob/rel/1.1.1/samples/Mvc.FileUpload/Filters/DisableFormValueModelBindingAttribute.cs)：

- 可以防止模型绑定访问表单数据。
- 如果要上传大型文件，同时想防止表单被读入内存，那么此筛选器会很有用。





## 筛选器作用域和执行顺序

TODO：https://docs.microsoft.com/zh-cn/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2#filter-scopes-and-order-of-execution>



## 在筛选器中取消和设置短路

通过设置提供给筛选器方法的 context 参数上的 Result 属性，可以在筛选器管道的任意位置设置短路。 例如，以下资源筛选器将阻止执行管道的其余阶段。

```c#
public class ShortCircuitingResourceFilterAttribute : Attribute, IResourceFilter
{
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.Result = new ContentResult()
        {
            Content = "资源不可用 - 不应设置标头"
        };
    }
}
```

在操作方法上应用上述筛选器：

```c#
[AddHeader("Author", "Smallz")]
public class HomeController : Controller
{
    [ShortCircuitingResourceFilter]
    public IActionResult SomeResource()
    {
        return Content("成功访问到标头资源");
    }
}
```

上述代码中，ShortCircuitingResourceFilter 和 AddHeader 筛选器都以 SomeResource 操作方法为目标。

`ShortCircuitingResourceFilter`：

- 先运行，因为它是资源筛选器且 `AddHeader` 是操作筛选器。
- 对管道的其余部分进行短路处理。

这样 `AddHeader` 筛选器就不会为 `SomeResource` 操作运行。 如果这两个筛选器都应用于操作方法级别，只要 `ShortCircuitingResourceFilter` 先运行，此行为就不会变。 先运行 `ShortCircuitingResourceFilter`（考虑到它的筛选器类型），或显式使用 `Order` 属性。



## 在筛选器管道中使用中间件

https://docs.microsoft.com/zh-cn/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2#using-middleware-in-the-filter-pipeline

