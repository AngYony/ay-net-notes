# ASP.NET Core MVC 控制器

**本文中的术语说明**

操作方法：控制器中使用public访问符修饰的方法（除了那些使用 `[NonAction]` 属性标注的方法）均被称为操作方法，或简称控制器操作。



## 控制器发现

在ASP.NET Core MVC应用程序中，任何请求都通过URL路由映射到控制器类上的操作方法。可以选择基于惯例的路由（默认约定的路由）或属性路由，或两者都选择，来填充路由表。最后，根据系统路由表中注册的路由将URL映射到控制器。

### 基于约定的路由发现

如果传入的URL和预定义的常规路由相匹配，那么控制器的名称将从路由解析中得到。默认路由定义如下：

```c#
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});
```

从URL模板参数中推断出控制器名称，作为URL的第一个段，该段位于服务器名称之后。常规路由通过显式或隐式路由参数设置控制器参数的值。显式路由参数是定义为URL模板一部分的参数，如上所示。隐式路由参数是一个不会出现在URL模板中的参数，它被视为一个常量。

在下面的示例中，URL模板是"`today`"，并且控制器参数的值是通过路由的defaults属性静态设置的。

```c#
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "route-today",
        template: "today",
        defaults: new { controller = "date", action = "day", offset = 0 });
});
```

注意，从路由推断出的控制器值可能不是要使用的控制器类的确切名称。更多的时候是一种别称。因此,可能需要一些额外的工作将控制器的值转化为一个实际的类名。

### 基于属性的路由发现

属性路由允许使用特殊属性来修饰控制器类或方法，这些属性指示URL模板最终将调用方法。

属性路由的主要好处是路由定义与他们的对应行为密切相关，将路由定义放置在它们对应的操作附近，通过这种方式，任何阅读代码的人都清楚地知道何时以及如何调用该方法。此外，选择属性路由可使URL模板独立于控制器和用于提供请求的操作。

```c#
[Route("Day")]
public class DateController : Controller
{
	[Route("{offset}")]
	public ActionResult Details(int offset)
	{
    	return Content(DateTime.Now.AddDays(offset).ToShortDateString());
	}
}
```

通过属性指定的路由仍将流入应用程序的全局路由表，在使用基于约定的路由时，将以编程方式显式填充相同的路由表。

### 混合策略路由发现

基于约定的路由和属性路由不是互斥的。两者都可以在同一应用程序的上下文中使用。属性路由和基于约定的路由都填充用于解析URL的相同路由表。传统路由必须显式启用，因为基于传统的路由必须始终以编程方式添加。属性路由总是打开的，不需要显式启用。请注意，Web API和早期版本的ASP.NET MVC中的属性路由不是这种情况。

因为属性路由始终是打开的，所以通过属性定义的路由优先于基于约定的路由。



## 控制器类

控制器实质上是一个C#自定义类，根据惯例，控制器类需要：

- 包含在项目根目录下的“Controllers”文件夹内
- 继承自Microsoft.AspNetCore.Mvc.Controller类

判断一个类是否是控制器类，需要满足以下几个条件中的任何一个即可：

- 类名称带有“Controller”后缀
- 该类继承自带有“Controller”后缀的类
- 该类使用了[Controller]属性进行修饰

注意：控制器类不可含有关联的 [NonController] 属性。

编写控制器类可以分为两个步骤：实现一个可发现的控制器类，并添加一组可发现的公共方法作为运行时的操作。

### 继承自Controller类的控制器

控制器类通常是直接或间接从给定基类`Microsoft.AspNetCore.Mvc.Controller`类继承的类。

注意，在ASP.NET Core之前发布的所有ASP.NET MVC版本中，继承自基类`Controller`是一项严格的要求。相反，在ASP.NET Core中，可以拥有没有继承功能的普通C#类的控制器类（具体见下面的介绍）。

一旦系统成功解析路由后，它就拥有一个控制器名称。该名称是一个普通的字符串，类似于别名（例如，`Home`或`Date`），必须与项目中包含或引用的实际类匹配。

### 带有`Controller`后缀的类名

拥有系统可以轻松发现的有效控制器类的最常见方案是为类名提供后缀“`Controller`”，并从上述中的`Controller`基类继承它。这意味着控制器名称Home的对应类将是`HomeController`类。如果存在这样的类，系统就会运行，并且能够成功地处理请求。（这是之前传统的ASP.NET MVC工作方式）

在ASP.NET Core中，控制器类的命名空间并不重要，尽管社区中的工具和许多例子都倾向于将控制器类放在一个名为`Controllers`的文件夹下。实际上，您可以将控制器类放在任何文件夹和任何名称空间中。只要该类具有“`Controller`”后缀并从`Controller`继承，它就会一直被发现。

### 没有Controller后缀的类名

在ASP.NET Core中，如果控制器类缺少“`Controller`”后缀，也会成功被发现，但有几点需要注意。

- 只有当类继承自基类`Controller`时，发现过程才有效。
- 类的名称必须与路由分析中的控制器名称匹配。

如果从路由中提取的控制器名称是`Home`，那么可以使用一个名为`Home`的类继承自基类`Controller`。任何其他名称都不起作用。换句话说，您不能只使用自定义后缀，名称的根部分必须始终与路由中的名称匹配。

通常，控制器类直接从类`Controller`继承，它从`Controller`类获取环境属性和功能。最值得注意的是，控制器从其基类继承HTTP上下文。您可以拥有从`Controller`继承的中间自定义类，绑定到URL的实际控制器类从该类继承。

### POCO控制器（不继承自基类）

动作调用者将HTTP上下文注入控制器的实例中，控制器类中运行的代码可以通过`HttpContext`属性访问它。从系统提供的基类继承`Controller`类可以免费获得所有必需的管道。但是，在ASP.NET Core中，不再需要从公共基类继承任何控制器。在ASP.NET Core中，控制器类可以是普通的C＃对象（POCO），其定义如下：

```c#
public class PocoController
{
   // Write your action methods here
}
```

要使系统成功发现POCO控制器，要么类名具有“`Controller`”后缀，要么使用`Controller`属性修饰类。

```c#
[Controller]
public class PocoController
{
   // Write your action methods here
}
```

拥有POCO控制器是一种优化形式，优化通常来自于删除一些特性以减少开销或内存占用。因此，不从已知基类继承可能会排除一些常见的操作，或者使它们的实现更加冗长。下面是几个应用场景。

#### 返回纯数据

POCO控制器是一个完全可测试的普通C＃类，它不依赖于周围的ASP.NET Core环境。需要注意的是，POCO控制器只有在不需要依赖于周围环境的情况下才能很好地工作。如果您的任务是创建一个超简单的Web服务，几乎不代表返回数据的固定端点，那么POCO控制器可能是一个不错的选择。 （请参阅以下代码。）

```c#
public class PocoController
{
    public IActionResult Today()
    {
        return new ContentResult() { Content = DateTime.Now.ToShortDateString() };
    }
}
```

如果必须返回文件的内容(无论文件是存在的还是动态创建的)，此代码也能很好地工作。

#### 返回HTML内容

您可以通过`ContentResult`的服务将纯HTML内容发送回浏览器。与上面的示例不同的是，将`ContentType`属性设置为适当的MIME类型，并根据自己的喜好构建HTML字符串。

```c#
[Controller]
public class Poco2
{
    public IActionResult Html()
    {
        return new ContentResult()
        {
            Content = "<h1>Hello</h1>",
            ContentType = "text/html",
            StatusCode = 200
        };
    }
}
```

以这种方式构建的任何HTML内容都是通过算法创建的。

#### 返回HTML视图

访问处理HTML视图的ASP.NET基础架构不是即时的。在控制器方法中，您必须返回一个适当的`IActionResult`对象，但是所有用于快速有效地执行该操作的可用助手方法都属于基类，在POCO控制器中不可用。下面是一个基于视图返回HTML的变通方法。

下面代码片段的主要目的是显示POCO控制器占用的内存更少，但是缺少一些内置的功能。

```c#
[Controller]
public class Poco2
{
    public IActionResult Index([FromServices] IModelMetadataProvider provider)
    {
        var viewdata = new ViewDataDictionary<MyViewModel>(provider
    , new ModelStateDictionary());
        viewdata.Model = new MyViewModel() { Title = "Hi!" };
        return new ViewResult()
        {
            ViewData = viewdata,
            ViewName = "index"
        };
    }
}
```

假如存在/Views/Poco2/Index.cshtml文件，内容如下：

```c#
<body>
    @(ViewData.Model.Title)
</body>
```

当直接使用/poco2/index进行访问时，将会显示正确的结果出来。

上述中，方法签名中的附加参数需要更多的说明。它是在ASP.NET Core 中广泛使用并推荐的一种依赖注入形式。要创建HTML视图，至少需要从外部引用`IModelMetadataProvider`。坦率地说，如果没有外部注入的依赖项，您将无法做很多事情。下面的代码片段，它试图简化上面的代码：

```c#
//注：该模式在poco中，并不能将数据传入到视图中去
public IActionResult Simple()
{
    return new ViewResult() { ViewName = "simple" };
}
```

您可以使用名为“`simple`”的Razor模板，并且返回的HTML都来自模板。但是，**您无法将自己的数据传递给视图以使渲染逻辑足够智能**。此外，无论是通过表单还是查询字符串提交的任何数据，你都无法访问。

#### 访问HTTP上下文

POCO控制器最棘手的方面是缺少HTTP上下文。特别是，这意味着您不能检查提交的原始数据，包括查询字符串和路由参数。但是，这个上下文信息是可用的，只能在需要时附加到控制器。有两种方法可以做到这一点。

第一种方法包括为操作注入当前上下文。上下文是`ActionContext`类的一个实例，它包装了HTTP上下文和路由信息，这就是你需要的。代码如下：

```c#
public class PocoController
{
    [ActionContext]
    public ActionContext Context { get; set; }
}
```

有了上述代码，您现在可以像访问常规的非POCO控制器一样访问`Request`对象或`RouteData`对象。以下代码允许您从`RouteData`集合中读取控制器名称：

```c#
public class PocoController
{
    [ActionContext]
    public ActionContext Context { get; set; }
    public IActionResult Today()
    {
    	//读取控制器名称
        var controller = Context.RouteData.Values["controller"];
        return new ContentResult() { Content = DateTime.Now.ToShortDateString() };
    }
}
```

另一种方法使用一种称为模型绑定的特性。模型绑定可以看作是将HTTP上下文中可用的特定属性注入到控制器方法中。

例如，下述代码采用查询字符串的形式进行模型绑定，从而获取值：

```c#
public IActionResult Http([FromQuery] int p1 = 0)
{
    return new ContentResult() { Content = p1.ToString() };
}
```

当你使用`/poco/http?p1=100`进行访问时，将会得到正确的显示结果。

上述代码中，通过使用`FromQuery`属性修饰方法参数，可以指示系统尝试查找参数名称（例如，`p1`）与URL查询字符串中的一个参数相匹配的项。如果找到匹配且类型可转换，那么方法参数将自动接收传递的值。类似地，通过使用`FromRoute`或`FromForm`属性，您可以访问`RouteData`集合中的数据或通过HTML表单提交的数据。

注意：在ASP.NET Core中，全局数据的概念非常模糊。在应用程序的任何地方都可以进行全局访问的意义上，没有什么是真正的全局访问。任何想要全局访问的数据都必须显式地传递。更确切地说，它必须在任何可能使用它的上下文中导入。为了实现这一点，ASP.NET Core提供了一个内置的依赖注入（DI）框架，开发人员可以通过这个框架注册抽象类型(比如接口)和它们的具体类型，当需要引用抽象类型时，框架就需要返回具体类型的实例。



## 控制器操作方法

控制器上的公共方法（除了那些使用 [NonAction] 属性装饰的方法）都称为控制器操作方法。

操作方法上的参数会绑定到请求数据，并使用模型绑定进行验证。（所有模型绑定的内容都会执行模型验证，`ModelState.IsValid` 属性值指示模型绑定和验证是否成功）

操作方法可以返回任何内容，比较常见的是返回生成响应的 IActionResult（或异步方法的 Task<IActionResult>）的实例。 

控制器类一般都继承自Microsoft.AspNetCore.Mvc.Controller类，派生自Controller类会提供对以下几种类型的操作方法的访问：

- 产生空响应正文的方法
- 产生含有预定义内容类型的非空响应正文的方法
- 产生内容协商的非空响应正文的方法

### 产生空响应正文的方法

此类方法返回的HTTP响应不包含Content-Type标头，因为没有要描述的内容作为响应的正文，常见的包括重定向方法和HTTP状态代码方法。

#### HTTP状态代码方法

这类方法返回HTTP的状态代码，常见的如：BadRequest()、NotFound()、OK()等。

#### 重定向方法

这类方法返回一个指向特定操作或目标的重定向，常见的如：Redirect()、LocalRedirect()、RedirectToAction()、RedirectToRoute()等。

与HTTP状态代码方法相比，重定向方法会在HTTP响应标头中添加Location属性。

### 产生含有预定义内容类型的非空响应正文的方法

这类方法返回的HTTP响应的标头中包含Content-Type属性，用来描述响应正文，这类方法大多数都包含ContentType参数，可以用来设置响应的Content-Type 标头值。常见的包括返回视图方法和格式化响应的方法。

#### 视图操作方法

这类方法返回一个使用模型呈现HTML的视图，例如：View()等；

#### 已格式化响应的方法

这类方法返回以特定的数据交换格式呈现的数据对象，比如返回JSON或特定类型的文件，常见的方法如：Json()、File()、PhysicalFile()等；

### 产生内容协商的非空响应正文的方法

应用内容协商的方法常见的有以下几种类型：

- 返回ObjectResult类型的操作方法
- 返回没有实现IActionResult类型的其他实现的操作方法。

这类方法常见的如：BadRequest()的带参重载方法（例如return BadRequest(modelState);）、CreatedAtRoute()的带参重载方法（例如return CreatedAtRoute("routename", values, newobject);）、Ok()的带参重载方法（例如return Ok(value);），这些方法只有传递了参数时才执行内容协商，在没有传递值的情况下，它们充当 HTTP 状态码结果类型。另外，CreatedAtRoute 方法始终执行内容协商，因为它的重载均要求传递一个值。



## 控制器中的依赖关系注入

为了说明控制器中的依赖关系注入情况，首先定义一个简单的接口和对应的类：

```c#
interface IDateTime
{
    DateTime Now { get; }
}
```

实现该接口的类：

```c#
public class SystemDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;
}
```

### 构造函数注入

服务（通常使用接口来定义，如上述中的IDateTime）作为构造函数参数添加，并且运行时从服务容器中解析服务。

可以通过下述方法将服务添加到服务容器中：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IDateTime, SystemDateTime>();

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}
```

在控制器的构造函数中使用：

```c#
public class HomeController : Controller
{
    private readonly IDateTime _dateTime;

    public HomeController(IDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public IActionResult Index()
    {
        var serverTime = _dateTime.Now;
        return View();
    }
}
```

### FromServices 的操作注入

FromServicesAttribute 允许将服务直接注入到操作方法，而无需使用构造函数注入：

```c#
public IActionResult About([FromServices] IDateTime dateTime)
{
    ViewData["Message"] = $"Current server time: {dateTime.Now}";

    return View();
}
```

### 从控制器访问设置

控制器有时需要访问应用的配置设置，一般使用选项（IOptions）管理配置，通常情况下，不直接将 IConfiguration 注入到控制器。

定义一个表示选项的类：

```c#
public class SampleWebSettings
{
    public string Title { get; set; }
    public int Updates { get; set; }
}
```

将该类添加服务集合中：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IDateTime, SystemDateTime>();
    services.Configure<SampleWebSettings>(Configuration);
    
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}
```

将应用配置为从 JSON 格式文件中读取设置：

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("samplewebsettings.json", 
                                optional: false,        // File is not optional.
                                reloadOnChange: false);
        })
        .UseStartup<Startup>();
}
```

在控制器中，从服务容器请求IOptions<SampleWebSettings> 设置：

```c#
public class SettingsController : Controller
{
    private readonly SampleWebSettings _settings;

    public SettingsController(IOptions<SampleWebSettings> settingsOptions)
    {
        _settings = settingsOptions.Value;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = _settings.Title;
        ViewData["Updates"] = _settings.Updates;
        return View();
    }
}
```





