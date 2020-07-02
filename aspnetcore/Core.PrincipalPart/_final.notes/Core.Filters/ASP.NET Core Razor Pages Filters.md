# ASP.NET Core Razor页面筛选器

**本文术语描述**

Filter：筛选器，过滤器，本文统一称为筛选器。

Razor Page Handler：Razor页面处理程序，从底层上说，它是ASP.NET Core中用来处理Razor页面的系统组件程序，但更多的时候，它是指Razor页面的后台代码程序，即PageModel相关的代码处理程序，本文也是如此。

## Razor页面筛选器

Razor页面筛选器主要用于 在处理Razor页面之前或之后时执行特定的代码，更具体的说，它是：

- 在选择处理程序方法后（如`OnPost`方法），但在模型绑定发生前运行代码。
- 在模型绑定完成后，执行处理程序方法（如`OnPost`方法）前运行代码。
- 在执行处理程序方法（如`OnPost`方法）后运行代码。

简单来说就是在模型绑定前、模型绑定后、执行完处理程序方法后都可以执行特定的代码。

### Razor页面筛选器作用范围

当在全局范围内实现时，它可以作用于整个Web程序。

当在Razor页面内实现时，它仅作用于单个Razor页面。

**注意**：Razor页面筛选器无法应用于Razor页面处理程序的单个特定方法，它是作用于整个Razor页面的。

### 实现方式

实现Razor页面筛选器主要有以下几种方式：

- 使用`IpageFilter`接口
- 使用`IAsyncPageFilter`接口
- 重写`PageModel`的virtual方法
- 自定义筛选器属性

虽然也可以在页面的构造函数中或使用中间件（执行处理程序方法之前），运行特定的代码实现筛选器同样的功能，**但是只有Razor页面筛选器可以访问`HttpContext`**，筛选器具有`FilterContext`派生的参数，它提供了对`HttpContext`的访问。在下文的示例中，通过筛选器属性可以将Header表头添加到响应中，而使用构造函数或中间件无法做到这一点。



## IPageFilter和IAsyncPageFilter

当创建一个Razor页面时，默认会派生自`PageModel`，而`PageModel`又派生自接口`IAsyncPageFilter`和`IPageFilter`，如下：

```c#
[PageModel]
public abstract class PageModel : IAsyncPageFilter, IFilterMetadata, IPageFilter
{
    ...
    public virtual void OnPageHandlerExecuted(PageHandlerExecutedContext context);
    public virtual void OnPageHandlerExecuting(PageHandlerExecutingContext context);
    public virtual Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next);
    public virtual void OnPageHandlerSelected(PageHandlerSelectedContext context);
    public virtual Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context);
    ...
}
```

因此`IAsyncPageFilter`和`IPageFilter`的方法都可以在Razor页面中进行重写。

`IPageFilter`和`IAsyncPageFilter`作用类似，只不过一个是同步版本另一个是异步版本，实际使用的过程中，只需要实现其中的一个版本即可，不需要同时实现。

框架会先查看筛选器是否实现了异步版本的接口，如果是，则调用实现了`IAsyncPageFilter`接口的方法；如果不是，则调用实现了`IPageFilter`接口的方法；如果两个接口都实现了，则只会调用异步版本的方法，即实现了`IAsyncPageFilter`接口的异步方法。对Razor页面中的重写应用也遵循相同的规则，即要么重写上述中的异步版本的方法，要么重写同步版本的方法，不可二者皆选。没有特殊要求下，推荐实现异步版本的接口`IAsyncPageFilter`。

### IPageFilter

`IPageFilter`包含了同步版本的要被实现或重写的方法：

```c#
public interface IPageFilter : IFilterMetadata
{
    void OnPageHandlerExecuted(PageHandlerExecutedContext context);
	void OnPageHandlerExecuting(PageHandlerExecutingContext context);
	void OnPageHandlerSelected(PageHandlerSelectedContext context);
}
```

#### OnPageHandlerSelected

在选择处理程序方法后（如`OnPost`方法），但在模型绑定发生前调用。

#### OnPageHandlerExecuting

在执行处理器方法前（如`OnPost`方法），模型绑定完成后调用。

#### OnPageHandlerExecuted

在执行处理器方法后（如`OnPost`方法），生成操作结果前调用。

### IAsyncPageFilter

`IAsyncPageFilter`包含了异步版本的要被实现或重写的方法：

```c#
public interface IAsyncPageFilter : IFilterMetadata
{
	Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next);
	Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context);
}
```

#### OnPageHandlerSelectionAsync

在选择处理程序方法后（如`OnPost`方法），但在模型绑定发生前，进行异步调用。

#### OnPageHandlerExecutionAsync

在调用处理程序方法前（如`OnPost`方法），但在模型绑定结束后，进行异步调用。



## 实现Razor页面筛选器

在上文中已经提到了实现Razor页面筛选器的几种方式，下面对这些方式的实现进行具体说明。

### 通过全局实现Razor页面筛选器

主要分为两步：

- 定义实现了接口`IAsyncPageFilter`或`IPageFilter`的具体类
- 在`Startup`的`ConfigureServices`方法中添加筛选器

#### 使用IAsyncPageFilter实现

定义一个实现了`IAsyncPageFilter`接口的类：

```c#
public class SampleAsyncPageFilter : IAsyncPageFilter
{
    private readonly ILogger _logger;

    public SampleAsyncPageFilter(ILogger logger)
    {
        this._logger = logger;
    }
    public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        _logger.LogDebug("全局过滤器方法：OnPageHandlerSelectionAsync，被调用 【WY01】");
        await Task.CompletedTask;
    }


    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
        PageHandlerExecutionDelegate next)
    {
        _logger.LogDebug("全局过滤器方法：OnPageHandlerExecutionAsync ，被调用 【WY02】");
        await next.Invoke();
    }
}
```

在`Startup`类中的`ConfigureServices`方法中添加筛选器：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options =>
    {
        options.Filters.Add(new SampleAsyncPageFilter(_logger));
    });
}
```

可以设置只在特定文件夹下的Razor页面应用筛选器：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AddFolderApplicationModelConvention(
            "/mycode",
            model => model.Filters.Add(new SampleAsyncPageFilter(_logger)));
    });
}
```

上述代码调用`AddFolderApplicationModelConvention` 将 `SampleAsyncPageFilter` 仅应用于`/mycode`中的页面，当请求不是mycode文件夹内的页面时，不会应用上述筛选器。

#### 使用IPageFilter实现

定义一个实现`IPageFilter`接口的类：

```c#
public class SamplePageFilter : IPageFilter
{
    private readonly ILogger _logger;

    public SamplePageFilter(ILogger logger)
    {
        _logger = logger;
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        _logger.LogDebug("全局过滤器方法：OnPageHandlerSelected，被调用 【WY03】");
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        _logger.LogDebug("全局过滤器方法：OnPageHandlerExecuting，被调用 【WY04】");
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        _logger.LogDebug("全局过滤器方法：OnPageHandlerExecuted，被调用 【WY05】");
    }
}
```

在`Startup`类中的`ConfigureServices`方法中添加筛选器：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options =>
    {
        options.Filters.Add(new SamplePageFilter(_logger));
    });
}
```



### 通过重写筛选器方法实现 Razor 页面筛选器

在上文中已经提到过，由于`PageModel`实现了接口`IAsyncPageFilter`和`IPageFilter`，这些接口的方法可以在Razor页面的处理程序中进行重写，但是需要注意的是只能重写一个接口对应的方法，要么重写的是同步版本的`IPageFilter`接口中方法，要么重写异步版本的`IAsyncPageFilter`接口中的方法。

下述代码实现了同步版本（`IPageFilter`）中的三个方法：

```c#
public class IndexModel : PageModel
{
    private readonly ILogger _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        var r = Request;
        var s = Response;
        var h = HttpContext;
        _logger = logger;
    }

    public string Message { get; set; }


    public void OnGet()
    {
        _logger.LogInformation("Index Get ................");
    }


    public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        base.OnPageHandlerSelected(context);
        _logger.LogDebug("IndexModel/OnPageHandlerSelected...........");
    }

    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        Message = "Message set in handler executing";
        base.OnPageHandlerExecuting(context);
        _logger.LogDebug("IndexModel/OnPageHandlerExecuting...........");
    }

    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        base.OnPageHandlerExecuted(context);
        _logger.LogDebug("IndexModel/OnPageHandlerExecuted...........");
    }
}
```



### 基于ResultFilterAttribute实现筛选器

`ResultFilterAttribute`是一个抽象的过滤器，以异步的方式执行，可以自定义一个继承自该类的特性类，下述示例使用筛选器向响应添加标头：

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
        context.HttpContext.Response.Headers.Add(_name, new string[] { _value });
    }
}
```

在Razor页面中进行添加和使用：

```c#
[AddHeader("Author", "smallz")]
public class PageOneModel : PageModel
{
    private readonly ILogger logger;

    public PageOneModel(ILogger<PageOneModel> logger)
    {
        this.logger = logger;
    }

    public string Message { get; set; }

    public async Task OnGet()
    {
        Message = "Your PageOne page.";
        logger.LogDebug("PageOne/OnGet");
        await Task.CompletedTask;
    }
}
```



## Razor页面筛选器与MVC Razor视图

Razor页面筛选器的实质是`PageModel`实现的接口`IAsyncPageFilter`和`IPageFilter`，因此直接访问MVC的视图时，并不会触发定义的Razor页面筛选器。

属性筛选器可以应用在MVC的控制器上：

```C#
[AddHeader("Author", "test")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

上述同样可以向响应中添加标头。



## 补充内容



### 授权筛选器属性

`Authorize` 属性可应用于 `PageModel`：

```c#
[Authorize]
public class ModelWithAuthFilterModel : PageModel
{
	public IActionResult OnGet() => Page();
}
```



