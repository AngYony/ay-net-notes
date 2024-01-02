# ASP.NET Core Razor页面路由和约定

**本文适用范围**

只适用于Razor Pages应用，不适用于MVC应用。

**本文中的术语说明**

- 路由：本文中的路由均指的是在Razor页面应用下的路由，非MVC控制器路由，当然中间有与MVC控制器路由相关联或相同的地方。在ASP.NET Core的内部，Razor Pages 路由和 MVC 控制器路由共享一个实现。
- 应用程序模型：用于表示Web 应用程序的各个组件的抽象接口和具体实现类，通过使用应用程序模型，可以修改应用以遵循与默认行为不同的约定。
- 约定：英文名Convention，默认情况下，Web应用（例如MVC应用程序）遵循特定的约定，以确定将哪些类（模型）视为控制器，这些类上的哪些方法是操作，以及参数和路由的行为方式。可以创建自己的约定来满足应用的需要，将它们应用于全局或作为属性应用。

**快速理解技巧**

- 如果类名、接口名、方法名、属性名等，只要名称中出现“`Convention`”，都和“约定”有关。
- 如果名称中出现“`ModelConvention`”，都和“模型约定”相关。
- 如果名称中出现“`Page`”，一般都用于Razor Pages，而不是MVC。

**本文关联的成员**

本文主要讲解的成员有：

- `PageConventionCollection`和其扩展类`PageConventionCollectionExtensions`
- `PageConventionCollection`的内部方法及其所有扩展方法

成员的代码来源：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
        .AddRazorPagesOptions(options =>
        {
            //重点RazorPagesOptions.Conventions
            PageConventionCollection pam = options.Conventions;
        })
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}
```

从代码层面上讲，本文主要是对上述代码中的`options.Conventions`获得到的`PageConventionCollection`类型，及其用法进行讲解。



## PageConventionCollection和PageConventionCollectionExtensions

`PageConventionCollection`可以由`RazorPagesOptions.Conventions`获取，它的定义如下（F12可查看）：

```c#
public class PageConventionCollection : Collection<IPageConvention>
{
	public PageConventionCollection();
    public PageConventionCollection(IList<IPageConvention> conventions);
    
    // 与Area相关的
    public IPageApplicationModelConvention AddAreaFolderApplicationModelConvention(
    string areaName, string folderPath, Action<PageApplicationModel> action);
    
    public IPageRouteModelConvention AddAreaFolderRouteModelConvention(
    string areaName, string folderPath, Action<PageRouteModel> action);
    
    public IPageApplicationModelConvention AddAreaPageApplicationModelConvention(
    string areaName, string pageName, Action<PageApplicationModel> action);

	public IPageRouteModelConvention AddAreaPageRouteModelConvention(
	string areaName, string pageName, Action<PageRouteModel> action);
	
	//更常用的
	public IPageApplicationModelConvention AddFolderApplicationModelConvention(
	string folderPath, Action<PageApplicationModel> action);
	
	public IPageRouteModelConvention AddFolderRouteModelConvention(
	string folderPath, Action<PageRouteModel> action);
	
	public IPageApplicationModelConvention AddPageApplicationModelConvention(
	string pageName, Action<PageApplicationModel> action);
	
	public IPageRouteModelConvention AddPageRouteModelConvention(
	string pageName, Action<PageRouteModel> action);
	
	//很少用到的
	public void RemoveType<TPageConvention>() where TPageConvention : IPageConvention;
	public void RemoveType(Type pageConventionType);
}
```

本文主要对上述中常用的4个方法进行讲解（Area相关的方法与其类似），除此之外，还包括`PageConventionCollectionExtensions`类型的扩展方法。

`PageConventionCollectionExtensions`的定义如下（可F12查看源码）：

```c#
public static class PageConventionCollectionExtensions
{
    //常用
    public static PageConventionCollection Add(
    this PageConventionCollection conventions, 
    IParameterModelBaseConvention convention);
    
	public static PageConventionCollection AddAreaPageRoute(
    this PageConventionCollection conventions, 
        string areaName, string pageName, string route);
    //常用
	public static PageConventionCollection AddPageRoute(
        this PageConventionCollection conventions, string pageName, string route);
    
	public static PageConventionCollection AllowAnonymousToAreaFolder(
        this PageConventionCollection conventions, 
        string areaName, string folderPath);
    
	public static PageConventionCollection AllowAnonymousToAreaPage(
        this PageConventionCollection conventions, 
        string areaName, string pageName);
	
    public static PageConventionCollection AllowAnonymousToFolder(
        this PageConventionCollection conventions, string folderPath);
	
    public static PageConventionCollection AllowAnonymousToPage(
        this PageConventionCollection conventions, string pageName);
    
	public static PageConventionCollection AuthorizeAreaFolder(
        this PageConventionCollection conventions, 
        string areaName, string folderPath, string policy);
    
	public static PageConventionCollection AuthorizeAreaFolder(
        this PageConventionCollection conventions, 
        string areaName, string folderPath);
	
    public static PageConventionCollection AuthorizeAreaPage(
        this PageConventionCollection conventions, 
        string areaName, string pageName);
	
    public static PageConventionCollection AuthorizeAreaPage(
        this PageConventionCollection conventions, 
        string areaName, string pageName, string policy);
    
	public static PageConventionCollection AuthorizeFolder(
        this PageConventionCollection conventions, 
        string folderPath, string policy);
    
	public static PageConventionCollection AuthorizeFolder(
        this PageConventionCollection conventions, string folderPath);
    
	public static PageConventionCollection AuthorizePage(
        this PageConventionCollection conventions, string pageName);
    
	public static PageConventionCollection AuthorizePage(
        this PageConventionCollection conventions, string pageName, string policy);
    //常用
	public static PageConventionCollection ConfigureFilter(
        this PageConventionCollection conventions, IFilterMetadata filter);
    //常用
	public static IPageApplicationModelConvention ConfigureFilter(
        this PageConventionCollection conventions, 
        Func<PageApplicationModel, IFilterMetadata> factory);

}
```

通过`PageConventionCollection`和`PageConventionCollectionExtensions`的源码，可以看出`PageConventionCollection`继承自`IPageConvention`集合，因此可以使用`Add()`方法添加实现了接口`IPageConvention`的成员。

而在`PageConventionCollectionExtensions`的扩展方法中，同样存在`Add()`方法：

```c#
public static PageConventionCollection Add(
    this PageConventionCollection conventions, 
    IParameterModelBaseConvention convention);
```

因此如果引入了扩展方法，在使用`PageConventionCollection`的`Add()`方法时，该方法的参数可以接受实现了接口`IParameterModelBaseConvention`或`IPageConvention`的成员。

**重要说明：**

本文主要是对`PageConventionCollection`的内部方法和扩展方法的使用进行讲解，这些方法主要有：

- `Add()`，返回类型：`PageConventionCollection`或`void`，取决于调用的是否是扩展方法。
- `AddFolderRouteModelConvention()`，返回类型：`IPageRouteModelConvention`
- `AddPageRouteModelConvention()`，返回类型：`IPageRouteModelConvention`
- `AddFolderApplicationModelConvention()`，返回类型：`IPageApplicationModelConvention`
- `AddPageApplicationModelConvention()`，返回类型：`IPageApplicationModelConvention`
- `ConfigureFilter()`，返回类型：`PageConventionCollection`或`IPageApplicationModelConvention`
- `AddPageRoute()`，返回类型：`PageConventionCollection`

下面对这些方法的使用进行分类讲解。



## 使用Add()方法添加应用于Razor页面的模型约定

在上文中已经提到过，`PageConventionCollection`的`Add()`方法接受的参数可以是实现了`IPageConvention`接口或`IParameterModelBaseConvention`接口（来自于扩展方法）的成员对象。

由于`IPageConvention`和`IParameterModelBaseConvention`都是原始接口（这两个接口都没有实现任何基接口，只被其他类或接口实现），实现这两个接口的后代成员有很多，这里只对常用的进行讲述。

更具体的说，主要有以下几个：

```c#
public interface IPageRouteModelConvention : IPageConvention
```

 ```c#
public interface IPageApplicationModelConvention : IPageConvention
 ```

```c#
public interface IPageHandlerModelConvention : IPageConvention
```

```c#
public class PageRouteTransformerConvention : IPageRouteModelConvention, IPageConvention
```

上述只是列出的一部分，只要是`IPageConvention`或`IParameterModelBaseConvention`的后代，在使用`Add()`方法时都可以作为参数传入，包括实现了后代接口的后代类。

下面对上述中的成员的使用加以说明。

### IPageRouteModelConvention——路由模型约定

可以创建实现了`IPageRouteModelConvention`接口的实体类，并将其实例作为参数传递给`PageConventionCollection`的`Add()`方法，通过这种方式，这些实例将在页面路由模板构造过程中应用，可以将自己的路由模板添加到应用中的所有页面。

创建一个实现了`IPageRouteModelConvention`接口的实体类：

```c#
public class GlobalTemplatePageRouteModelConvention : IPageRouteModelConvention
{
    ILogger _logger;
    public GlobalTemplatePageRouteModelConvention(ILogger logger)
    {
        _logger = logger;
    }
    public void Apply(PageRouteModel model)
    {
        StringBuilder log = new StringBuilder();
        log.AppendLine("==================================");
        log.AppendLine($"Count：{model.Selectors.Count}  ViewEnginePath:{model.ViewEnginePath}    RelativePath:{model.RelativePath}");

        var selectorCount = model.Selectors.Count;
        for (var i = 0; i < selectorCount; i++)
        {
            var selector = model.Selectors[i];
            log.AppendLine("未添加前");
            log.AppendLine($"Order：{selector.AttributeRouteModel.Order} ， Template：{selector.AttributeRouteModel.Template}");

            //在现有的基础上添加新的路由模板
            model.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel
                {
                    Order = 1,//设置路由匹配顺序
                    //有当前的模板和自定义模板合并为一个新的模板
                    Template = AttributeRouteModel.CombineTemplates(
                        //获取当前的模板
                        selector.AttributeRouteModel.Template,
                        "{globalTemplate?}"),
                }

            });

            log.AppendLine($"添加完之后，Count:{model.Selectors.Count}");
            foreach (var s in model.Selectors)
            {
                log.AppendLine($"Order：{s.AttributeRouteModel.Order} ， Template：{s.AttributeRouteModel.Template}");
            }

            _logger.LogDebug(log.ToString());
        }
    }
}
```

`Apply()`方法会在构造每个页面对应的路由模型过程中被依次调用。

`AttributeRouteModel`的`Order`属性，用于设置[路由匹配顺序](https://docs.microsoft.com/zh-cn/aspnet/core/razor-pages/razor-pages-conventions?view=aspnetcore-2.2#route-order)，这里作简要说明。

路由顺序说明：

- 路由按照`Order`值进行顺序处理的（ `-1`、`0`、`1`、`2`...`n`）。
- 顺序值为`-1`时，将在处理其他路由之前处理该路由。
- 未指定顺序值时，或`Order=null`，将按照顺序值为`0`进行处理。
- 除上述之外，可以设置其他顺序值（`1`、`2`...`n`）。
- 当路由具有相同的`Order顺序值时，优先匹配更具体的路由。例如`/a/b/c`和`/a/b`，优先匹配`/a/b/c`。

**注意：实际应用中，应该避免设置`Order`的值，不应该显示设置路由顺序，因为这样会容易让人困惑。**本文只是为了说明路由的匹配顺序而显示进行的设置，通常不建议这么做。

创建完实体类后，在`Startup`的`ConfigureServices`方法中，调用`RazorPagesOptions.Conventions.Add()`方法，将其添加到`IPageConvention`实例集合中，代码如下：

```c#
private readonly ILoggerFactory loggerFacotry;
public IConfiguration Configuration { get; }

public Startup(ILoggerFactory _loggerFactory, IConfiguration configuration)
{
    this.loggerFacotry = _loggerFactory;
    this.Configuration = configuration;
}

public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
        .AddRazorPagesOptions(options =>
        {
            //添加路由模型约定
            options.Conventions.Add(
                new GlobalTemplatePageRouteModelConvention(
 loggerFacotry.CreateLogger<GlobalTemplatePageRouteModelConvention>()
                ));
        })       .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
}
```

上述中的路由模型约定会把自定义的路由模板（`{globalTemplate?}`）添加到应用中的所有页面，直接运行将会看到如下日志输出：

![convention](assets/convention.png)

通过日志，可以很清楚的看到每个页面的路由模型在构建过程中，添加了新的路由模板到路由模型中。

以Index页面为例：

```c#
public class IndexModel : PageModel
{
    public string RouteDataGlobalTemplateValue { get; private set; }

    public void OnGet()
    {
        if(RouteData.Values["globalTemplate"]!=null)
        {
            RouteDataGlobalTemplateValue = $"globalTemplate提供了路由数据：{RouteData.Values["globalTemplate"]}";
        }
    }
}
```

Index.cshtml：

```html
<body>
    <h1>Razor页面路由和约定</h1>
    <div>@Model.RouteDataGlobalTemplateValue</div>
</body>
```

运行程序，直接访问Index页面，不会得到路由模板中`globalTemplate`的值：

![rc_01](assets/rc_01.png)

这是因为，模板为“`Index`”的`Order`值为`null`，而“`Index/{globalTemplate?}`”的`Order`值为`1`，如下：

```
dbug: My.RazorRoute.Study.Conventions.GlobalTemplatePageRouteModelConvention[0]
      ==================================
      Count：2  ViewEnginePath:/Index    RelativePath:/Pages/Index.cshtml
      未添加前
      Order： ， Template：Index
      添加完之后，Count:3
      Order： ， Template：Index
      Order： ， Template：
      Order：1 ， Template：Index/{globalTemplate?}
```

在地址栏中，输入：`/Index/wy`，显示效果如下：

![rc_02](assets/rc_02.png)

此时可以得到路由模板中`globalTemplate`的值。

### IPageApplicationModelConvention——应用模型约定

可以创建实现了`IPageApplicationModelConvention`接口的实体类，并将其实例作为参数传递给`PageConventionCollection`的`Add()`方法。这些实例将在页面应用模型构造过程中应用。

创建一个实现了`IPageApplicationModelConvention`接口的实体类：

```c#
public class GlobalHeaderPageApplicationModelConvention : IPageApplicationModelConvention
{
    public void Apply(PageApplicationModel model)
    {
        model.Filters.Add(new AddHeaderAttribute("GlobalHeader", 
            new string[] { "Global Header Value" }));
    }
}
```

上述中的`AddHeaderAttribute`是一个基于`ResultFilterAttribute`特性类的自定义属性，`ResultFilterAttribute`是一个内置筛选器，[可以通过它进行扩展](https://docs.microsoft.com/zh-cn/aspnet/core/razor-pages/filter?view=aspnetcore-2.2#implement-a-filter-attribute)：

```c#
public class AddHeaderAttribute: ResultFilterAttribute
{
    private readonly string _name;
    private readonly string[] _values;

    public AddHeaderAttribute(string name,string [] values)
    {
        _name = name;
        _values = values;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add(_name, _values);
        base.OnResultExecuting(context);
    }
}
```

在`Startup`类的`ConfigureServices`方法中进行添加：

```c#
public void ConfigureServices(IServiceCollection services)
{  
    services.AddMvc()
        .AddRazorPagesOptions(options =>
        {       
            options.Conventions.Add(
            new GlobalHeaderPageApplicationModelConvention());
        })
		.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);     
}
```

运行项目，访问任意页面， 均可在响应的标头信息中，看到上述中添加的`GlobalHeader`值：

![rc_03](assets/rc_03.png)

实现`IPageApplicationModelConvention`接口的实例的`Apply()`方法，会在每个页面的应用模型构造过程中被调用。

### IPageHandlerModelConvention——处理程序模型约定

可以创建实现了`IPageHandlerModelConvention`接口的实体类，并将其实例作为参数传递给`PageConventionCollection`的`Add()`方法，这些实例将在页面处理程序模型构造过程中应用。

创建实现了`IPageHandlerModelConvention`接口的实体类：

```c#
public class GlobalPageHandlerModelConvention
    : IPageHandlerModelConvention
{
    public void Apply(PageHandlerModel model)
    {
        ...
    }
}
```

在`Startup`类的`ConfigureServices()`方法中进行添加：

```c#
services.AddMvc()
    .AddRazorPagesOptions(options =>
        {
            options.Conventions.Add(new GlobalPageHandlerModelConvention());
        });
```

实现了`IPageHandlerModelConvention`接口的`Apply()`方法，将在任意页面的处理程序模型构造过程中被调用。

### PageRouteTransformerConvention——页面路由参数转换约定

`PageRouteTransformerConvention`是一个实现了`IPageConvention`接口和`IPageRouteModelConvention`接口的类，它的定义如下：

```c#
public class PageRouteTransformerConvention : IPageRouteModelConvention, IPageConvention
{
    public PageRouteTransformerConvention(IOutboundParameterTransformer parameterTransformer);
    public void Apply(PageRouteModel model);
	protected virtual bool ShouldApply(PageRouteModel action);
}
```

因此它的实例也可以作为参数，传递给`PageConventionCollection`的`Add()`方法。

`PageRouteTransformerConvention`包含一个参数为`IOutboundParameterTransformer`的构造函数，`IOutboundParameterTransformer`接口用于参数值转换，可以自定义一个实现该接口的实体类，这样在调用`PageRouteTransformerConvention`类的构造函数时，就可以将实体类的实例作为参数进行传入。这个实体类被称为参数转换程序，它可以将输入的路由值更改为另一个值输出。

`PageRouteTransformerConvention`仅将来自Razor中的Pages文件夹和文件名称自动生成的段对应的页面路由进行转换，例如，假如一个Razor页面在`/Pages/My/Abc.cshtml`下，那么`PageRouteTransformerConvention`仅对自动生成的段`/My/Abc.cshtml`进行转换，如果页面使用了`@page`指令，指定了路由模板，`PageRouteTransformerConvention`将不会转换`@page`指令设置的路由，同时也不会转换通过其他方法（例如`AddPageRoute`）添加的路由。一句话概括就是，`PageRouteTransformerConvention`只可以转换基于文件目录自动生成的页面路由中的段。

上文中已经提到过，使用`PageRouteTransformerConvention`时，必须有实现`IOutboundParameterTransformer`接口的实体类，这样才能调用它的构造函数。

首先定义一个实现了`IOutboundParameterTransformer`接口的实体类：

```c#
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        if (value == null) { return null; }
        string str= Regex.Replace(value.ToString(),"([a-z])([A-Z])", "$1-$2").ToLower();
        return str;
    }
}
```

上述自定义的`SlugifyParameterTransformer`类即为一个转换程序，它用于将默认的段转换为另一个形式，例如，如果段为`OtherPages`，执行上述方法之后，将会返回`other-pages`，如下图所示：

![rc_04](assets/rc_04.png)

在`Startup`类的`ConfigureServices()`方法中进行调用：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
        .AddRazorPagesOptions(options =>
            {
                options.Conventions.Add(
                    new PageRouteTransformerConvention(
                        new SlugifyParameterTransformer()));
            });
}
```

运行程序，通过上图中的断点执行情况来看，自定义的转换程序会在自动生成页面路由的段时执行（不会在每个页面访问前运行），有多少个段就执行多少次，例如`/Pages/OtherPages/Page_1.cshtml`页面，自动生成的路由段有`OtherPages`和`Page_1`，因此针对这个页面会执行两次（具体可以加断点进行验证），一旦执行了转换程序，就不能直接使用`/OtherPages/Page_1`路由访问页面，而只能使用`/Other-Pages/page_1`进行访问。

### Add()方法的参数如何选择与对比

上文中分别讲述了可以为`Add()`方法传入的参数可选类型，下面对这些类型进行概括和比较：

#### 触发时机

`IPageApplicationModelConvention`、`IPageHandlerModelConvention`会在页面路由第一次被请求时执行，例如`/About`页面，当第一次访问该页面时，这两个方法接口对应的方法都会执行，如果再次访问就不会触发。

`IPageRouteModelConvention`会在所有的页面路由模型生成时执行（包括超链接生成），访问具体的页面时不会被触发。

`PageRouteTransformerConvention`会在自动生成的路由段时被执行，访问具体的页面不会被触发。

#### Apply()方法参数和适用场景

`IPageRouteModelConvention`和`PageRouteTransformerConvention`类的成员方法`Apply()`的参数都是`PageRouteModel`类型，`PageRouteModel`类可以对路由的模板和顺序值进行设置，因此如果是针对路由模板的操作，优先使用这两个类型的成员，其中`PageRouteTransformerConvention`主要用于路由值的转换，除此之外，优先使用`IPageRouteModelConvention`成员。

`IPageApplicationModelConvention`的`Apply()`方法的参数是`PageApplicationModel`类型，`PageApplicationModel`类的`Filters`属性可以执行筛选操作。

`IPageHandlerModelConvention`的`Apply()`方法的参数是`PageHandlerModel`类型，该类型可以获取处理的类型（GET、Post）、参数等信息，`PageHandlerModel`的`Page`属性也可以获取到`PageApplicationModel`类型对象，用法和`IPageApplicationModelConvention.Apply()`的参数`PageApplicationModel`用法类似。



## AddFolderRouteModelConvention()和AddPageRouteModelConvention()

除了`Add()`方法之外，`PageConventionCollection`还提供了`AddFolderRouteModelConvention()`和`AddPageRouteModelConvention()`方法。这两个方法都返回`IPageRouteModelConvention`，并且传入的委托都是`PageRouteModel`类型。

### AddFolderRouteModelConvention()

该方法的声明如下：

```c#
public Microsoft.AspNetCore.Mvc.ApplicationModels.IPageRouteModelConvention AddFolderRouteModelConvention (string folderPath, Action<Microsoft.AspNetCore.Mvc.ApplicationModels.PageRouteModel> action);
```

需要注意的是该方法返回的类型是`IPageRouteModelConvention`，它与上文介绍的`Add()`方法传入的参数是`IPageRouteModelConvention`类型的一样，因此调用`AddFolderRouteModelConvention()`方法，与直接为`Add()`方法传入`IPageRouteModelConvention`类型的参数的用法类似，只不过是一个需要显示的在`Apply()`方法中进行处理，而`AddFolderRouteModelConvention()`方法，只需要在第二个参数对应的委托中进行处理即可。

另外需要注意的是，`AddFolderRouteModelConvention()`方法可以为指定文件夹下的所有页面调用`PageRouteModel`上的操作，而`Add()`方法（参数为`IPageRouteModelConvention`的版本）针对的是所有页面， 除此之外二者没有区别。

下述代码使用`AddFolderRouteModelConvention()`方法为`OtherPages`文件夹下的所有页面的路由模板追加"`{otherPagesTemplate?}`"：

```c#
options.Conventions.AddFolderRouteModelConvention("/OtherPages", model =>
{
    var selectorCount = model.Selectors.Count;
    for (var i = 0; i < selectorCount; i++)
    {
        var selector = model.Selectors[i];
        model.Selectors.Add(new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel
            {
                Order = 2,
                Template = AttributeRouteModel.CombineTemplates(
                    selector.AttributeRouteModel.Template, 
                    "{otherPagesTemplate?}"),
            }
        });
    }
});
```

Page_1.cshtml.cs：

```c#
public class Page_1 : PageModel
{
    public string RouteDataGlobalTemplateValue { get; private set; }

    public string RouteDataOtherPagesTemplateValue { get; private set; }

    public void OnGet()
    {
        if(RouteData.Values["globalTemplate"]!=null)
        {
            RouteDataGlobalTemplateValue = $"globalTemplate提供了路由数据：{RouteData.Values["globalTemplate"]}";
        }

        if (RouteData.Values["otherPagesTemplate"] != null)
        {
            RouteDataOtherPagesTemplateValue =
                 $"otherPagesTemplate：{RouteData.Values["otherPagesTemplate"]}"; 
        }
    }
}
```

Page_1.cshtml：

```html
<body>
    <h1>Page_1</h1>
    <div>@Model.RouteDataGlobalTemplateValue</div>

    <p>@Model.RouteDataOtherPagesTemplateValue</p>
</body>
```

运行程序，如果只含有上述配置项时，访问`/OtherPages/page_1/a`，显示效果如下：

![rc_05](assets/rc_05.png)

如果同时启用本文前面的`Add()`方法，传入实现了`IPageRouteModelConvention`接口的实体：

```c#
options.Conventions.Add(
    new GlobalTemplatePageRouteModelConvention(
        loggerFacotry.CreateLogger<GlobalTemplatePageRouteModelConvention>()
    ));
```

仍然访问`/OtherPages/Page_1/a`，此时显示效果如下：

![rc_06](assets/rc_06.png)

这是因为在`GlobalTemplatePageRouteModelConvention`实现`IPageRouteModelConvention`的`Apply()`方法时，将`AttributeRouteModel`的`Order`属性值设置为了`1`，因此它有更高的优先级匹配路由数据值。这正是`Order`属性的用处。

若在同时启用了`Add()`和`AddFolderRouteModelConvention()`方法的情况下，同时显示这两个路由值，可以访问`/OtherPages/Page_1/a/b`，显示效果如下：

![rc_07](assets/rc_07.png)

目前对这种显示现象没有特别官方的说明。

### AddPageRouteModelConvention()

与`AddFolderRouteModelConvention()`方法都返回相同类型（`IPageRouteModelConvention`）的值，并且调用方法的方式和传入的委托参数的类型（`PageRouteModel`）都相同，唯一不同的是，`AddFolderRouteModelConvention()`方法中的`PageRouteModel`上的操作，针对的是指定名称的文件夹下的页面，而`AddPageRouteModelConvention()`针对的是指定名称的页面调用`PageRouteModel`上的操作。除此之外，其他的方法调用和`PageRouteModel`操作都一样。

下述代码使用`AddPageRouteModelConvention()`方法将"{`aboutTemplate?`}"路由模板添加到`About`页面（About.cshtml未在OtherPages文件夹下）：

```c#
options.Conventions.AddPageRouteModelConvention("/About", model =>
{
    var selectorCount = model.Selectors.Count;
    for (var i = 0; i < selectorCount; i++)
    {
        var selector = model.Selectors[i];
        model.Selectors.Add(new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel
            {
                Order = 2,
                Template = AttributeRouteModel.CombineTemplates(
                    selector.AttributeRouteModel.Template, 
                    "{aboutTemplate?}"),
            }
        });
    }
});
```

它的显示效果和说明与调用`AddFolderRouteModelConvention()`类似，此处不再累述。



## AddFolderApplicationModelConvention()和AddPageApplicationModelConvention()

`AddFolderApplicationModelConvention()`和`AddPageApplicationModelConvention()`是`PageConventionCollection`类的另外两个成员方法，这两个方法都返回`IPageApplicationModelConvention`，并且传入的委托参数类型都是`PageApplicationModel`。

和上文中的`Add()`方法传入参数类型为`IPageApplicationModelConvention`的类似，一个是在实现接口的`Apply()`方法中对`PageApplicationModel`进行操作，另一个是直接在委托方法中操作`PageApplicationModel`。

在讲述着两个方法的使用之前，先定义一个继承自系统自带的`ResultFilterAttribute`过滤器的特性类：

```c#
public class AddHeaderAttribute : ResultFilterAttribute
{
    private readonly string _name;
    private readonly string[] _values;

    public AddHeaderAttribute(string name, string[] values)
    {
        _name = name;
        _values = values;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add(_name, _values);
        base.OnResultExecuting(context);
    }
}
```

该类在前文中已经有过介绍，此处不再累述。

### AddFolderApplicationModelConvention()

`AddFolderApplicationModelConvention()`方法针对指定文件夹下的所有页面调用`PageApplicationModel`实例上的操作。

下面代码中，将`OtherPages`目录下的所有页面的响应标头中添加`OtherPagesHeader`属性：

```c#
options.Conventions.AddFolderApplicationModelConvention("/OtherPages", model =>
{
    model.Filters.Add(new AddHeaderAttribute(
        "OtherPagesHeader", new string[] { "OtherPages Header Value" }));
});
```

当访问`/OtherPages/page_1`时：

![rc_08](assets/rc_08.png)



### AddPageApplicationModelConvention()

`AddPageApplicationModelConvention()`方法针对指定页面调用`PageApplicationModel`实例上的操作，除此之外，用法和`AddFolderApplicationModelConvention()`方法基本一样：

```c#
options.Conventions.AddPageApplicationModelConvention("/About", model =>
{
    model.Filters.Add(new AddHeaderAttribute(
        "AboutHeader", new string[] { "About Header Value" }));
});
```

显示效果也基本一样，此处不再累述。



## 使用AddPageRoute()方法配置页面路由

可以使用`AddPageRoute()`方法配置路由，该路由指向页面路径中的页面。`AddPageRoute()`的实质是使用`AddPageRouteModelConvention()`建立路由。

注意：`AddPageRoute()`方法会影响指定的页面路由链接生成。

例如，下述代码将指定的页面Contact的路由设置为`TheContactPage`：

```c#
options.Conventions.AddPageRoute("/Contact", "ThecontactPage/{text?}");
```

在前台.cshtml文件中，存在下述链接标记：

```html
<a asp-page="Contact">Contact</a>
```

运行页面后，将会生成如下的链接内容：

```html
<a href="/ThecontactPage">Contact</a>
```

此时访问Contact页面，可以使用默认路由`/Contact`进行访问，也可以使用新设置的路由`/ThecontactPage`进行访问。

上述代码中，为路由指定的可选参数{`text?`}，获取路由参数的值有两种方式。

方式一：在Contact.cshtml中，使用`@Page`指令包含可选段：

```html
@page "{text?}"
```

这样在Contact.csthml.cs中，就可以使用`OnGet()`参数形式获取到：

```c#
public void OnGet(string text){}
```

方式二：不使用`@Page`指令，直接在Contact.cshtml.cs中使用`RouteData`获取：

```c#
public void OnGet(string text)
{
    Message = "Your contact page.";
    if (RouteData.Values["text"] != null)
    {
        RouteDataTextTemplateValue = $"Route data for 'text' was provided: {RouteData.Values["text"]}";
    }
}
```



## 使用ConfigureFilter()方法配置筛选器

`ConfigureFilter()`方法是`PageConventionCollection`的扩展方法，它们位于`PageConventionCollectionExtensions`类中，该方法有两个版本。

版本一：用于配置要应用的指定筛选器，返回类型为`IPageApplicationModelConvention`，

```c#
public static IPageApplicationModelConvention ConfigureFilter(
	this PageConventionCollection conventions, 
	Func<PageApplicationModel, 
	IFilterMetadata> factory);
```

版本二：用于配置筛选器工厂，将筛选器应用于所有的Razor页面，返回类型为`PageConventionCollection`，

```c#
public static PageConventionCollection ConfigureFilter(
	this PageConventionCollection conventions, IFilterMetadata filter);
```

### 配置指定的筛选器

版本一的方法的使用示例：

```c#
options.Conventions.ConfigureFilter(model =>
{
    //注意大小写
    if (model.RelativePath.Contains("OtherPages/Page2"))
    {
        return new AddHeaderAttribute(
            "OtherPagesPage2Header", 
            new string[] { "OtherPages/Page2 Header Value" });
    }
    return new EmptyFilter();
});
```

上述方法会在访问每个页面时进行筛选，如果当前页面对应的页面路径包含“`OtherPages/Page2`”，则添加标头，如果不通过，则应用`EmptyFilter`筛选器。

`EmptyFilter`是一种操作筛选器，由于Razor页面会忽略操作筛选器（操作筛选器应用在MVC中），因此，如果路径中不包含`OtherPages/Page2`，`EmptyFilter`会按预期发出空操作指令。

可以访问`/Others/Page2`来请求Page2页面，标头信息如下：

![rc_09](assets/rc_09.png)

另外需要注意的是，该方法会在每个页面访问前执行，但是刷新页面时不执行。

### 配置筛选器工厂

`ConfigureFilter()`方法的另一个版本可以配置指定的工厂，用于将筛选器应用于所有Razor页面。

首先定义一个实现了`IFilterFactory`接口的工厂：

```c#
public class AddHeaderWithFactory : IFilterFactory
{
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        return new AddHeaderFilter();
    }

    private class AddHeaderFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(
                "FilterFactoryHeader",
                new string[]
                {
                    "Filter Factory Header Value 1",
                    "Filter Factory Header Value 2"
                });
        }
    }
}
```

然后调用该工厂：

```c
options.Conventions.ConfigureFilter(new AddHeaderWithFactory());
```

运行代码，访问任意页面，均会在标头中看到如下信息：

![rc_10](assets/rc_10.png)



## 替换默认的页面应用模型提供程序(DefaultPageApplicationModelProvider，2.2版本暂不支持)

Razor页面默认的模型提供程序是`DefaultPageApplicationModelProvider` ，它是一个实现`IPageApplicationModelProvider`接口的类：

```c#
public class DefaultPageApplicationModelProvider : Microsoft.AspNetCore.Mvc.ApplicationModels.IPageApplicationModelProvider
```

可以创建自己的类继承自`DefaultPageApplicationModelProvider`类，以便为处理程序提供自己的实现逻辑。

自带的默认实现已经为未命名和已命名处理程序的命名规则建立了约定。

### 默认的未命名处理程序方法

未命名的处理程序方法也被称为Http谓词的处理程序方法：

| 未命名处理程序方法           | 操作                                      |
| ---------------------------- | ----------------------------------------- |
| `OnGet` / `OnGetAsync`       | 初始化页面状态                            |
| `OnPost` / `OnPostAsync`     | 处理POST请求                              |
| `OnDelete` / `OnDeleteAsync` | 处理DELETE请求（可用于向页面发出API调用） |
| `OnPut` / `OnPutAsync`       | 处理PUT请求（可用于向页面发出API调用）    |
| `OnPatch` / `OnPatchAsync`   | 处理PATCH请求（可用于向页面发出API调用）  |

这些方法遵循以下约定：

`On<HTTP verb>[Async]`，即`On`+`Http谓词`+`Async`（追加`Async`是可选操作，建议为异步方法执行此操作）。

### 默认的已命名处理程序方法

由开发人员提供的处理程序方法（“已命名”的处理程序方法）遵循类似的预定。

处理程序名称出现在Http谓词之后或者Http谓词与`Async`之间：

`On<Http verb><handler name>[Async]`，即：`On`+`Http谓词`+`处理程序名称`+`Async`（追加`Async`是可选操作）。

例如，用于处理消息的方法可能采用下表所示的命名：

| 已命名处理程序方法示例                     | 操作说明                                      |
| ------------------------------------------ | --------------------------------------------- |
| `OnGetMessage` / `OnGetMessageAsync`       | 以Get方式获取消息                             |
| `OnPostMessage` / `OnPostMessageAsync`     | 以Post方式处理消息                            |
| `OnDeleteMessage` / `OnDeleteMessageAsync` | 以DELETE方式处理消息，可用于向页面发出API调用 |
| `OnPutMessage` / `OnPutMessageAsync`       | 以PUT方式处理消息，可用于向页面发出API调用    |
| `OnPatchMessage` / `OnPatchMessageAsync`   | 以PATCH方式处理消息，可用于向页面发出API调用  |

### 自定义处理程序方法名称（未完善）

上文中所有的处理程序方法，均以“`On`”开头，如果想要更改上述中的处理程序方法的命名方式，例如避免让方法名称以“`On`”开头，并且使用第一个分词来确定Http谓词，并且可以进行其他更改，比如讲DELETE、PUT和PATCH的谓词转换为POST。

若要实现此需求，需要继承`DefaultPageApplicationModelProvider`类，并重写它的`CreateHandlerModel`方法，以提供自定义逻辑来解析`PageModel`处理程序名称。

**特别说明**

实际使用过程中，不需要自定义处理程序方法名称规则，没有特别大的意义。

由于2.2版本中并不支持`DefaultPageApplicationModelProvider`类，可能后期不再支持自定义名称规则，因此此处不能提供代码示例。如果想了解`DefaultPageApplicationModelProvider`的用法，具体请参阅2.2之前版本的代码示例：https://docs.microsoft.com/zh-cn/aspnet/core/razor-pages/razor-pages-conventions?view=aspnetcore-2.2#replace-the-default-page-app-model-provider



## MVC 筛选器和页面筛选器 (IPageFilter)

Razor 页面会忽略 MVC 操作筛选器，因为 Razor 页面使用处理程序方法。

可使用其他类型的 MVC 筛选器：授权筛选器、异常筛选器、资源筛选器和结果筛选器。

页面筛选器（`IPageFilter`）是应用于Razor页面的一种筛选器，不能用应用于MVC。



## 自定义路由

https://docs.microsoft.com/zh-cn/aspnet/core/razor-pages/index?view=aspnetcore-2.2&tabs=visual-studio#custom-routes

