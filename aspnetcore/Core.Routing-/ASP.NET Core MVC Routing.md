# ASP.NET Core MVC 路由

**本文适用范围**

本文所介绍的内容只是基于ASP.NET Core的MVC框架的应用，和ASP.NET Core中的Razor Pages路由有一定的差异。

**本文中的术语说明**

- 路由：本文中的路由特指的是ASP.NET Core MVC中的路由，也可以理解为控制器路由。
- 路由模板：类似于"{controller=Home}/{action=Index}/{id?}"形式的字符串，对于需要指定模板（template）参数的方法，会使用路由模板。
- 路由参数/路由参数名称：即路由模板中的“controller”、“action”、“id”等，除了系统保留的路由参数名称，也可以自定义路由参数名称（如上述中的“id”）。
- 路由名称：即路由模板中的“default”，通常作为方法的name参数进行传入，具体见本文介绍。
- 路由集合/路由表：
- 路由标记：路由模板中使用中括号（[，]）括起来的部分，一般为[action]、[area] 和 [controller]，实际匹配时，这些标记被替换为操作方法名称值、区域名称值和控制器名称值。路由标记的主要作用是替换，而路由参数的主要作用是定义参数，以便在操作方法中获取对应的参数的值。



## MVC路由

ASP.NET Core MVC使用路由中间件来匹配传入请求的 URL 并将它们映射到操作。可以在启动代码或属性中定义路由，用来描述如何将URL路径与控制器的操作方法相匹配。

控制器操作方法既支持传统路由，也支持属性路由，同时支持这两种形式的混合路由。



## 设置路由中间件

在Startup类中的ConfigureServices()方法中，添加MVC服务，并在Configure()方法中设置路由中间件，代码如下：

```c#
app.UseMvc(routes =>
{
   routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
});
```

上述代码中，routes.MapRoute()方法用于创建单个路由（default路由），"{controller=Home}/{action=Index}/{id?}"被称为路由模板，可以匹配诸如 /Products/Details/5 之类的 URL 路径，并通过对路径进行标记来提取路由值 { controller = Products, action = Details, id = 5 }。 MVC 将尝试查找名为 ProductsController 的控制器并运行 Details 操作方法。

除了使用上述代码设置路由中间件外，还可以直接使用UseMvcWithDefaultRoute()方法替换上述代码，如下所示：

```c#
app.UseMvcWithDefaultRoute();
```

UseMvc方法 和 UseMvcWithDefaultRoute方法都可以向中间件管道添加 RouterMiddleware 的实例。MVC 不直接与中间件交互，而是使用路由来处理请求，MVC 通过 MvcRouteHandler 实例连接到路由。

UseMvc()方法无论怎样配置，都和UseMvcWithDefaultRoute()一样，始终都支持属性路由。



## 传统路由

传统路由也被称为默认路由，它的定义如下：

```c#
routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
```

使用传统路由时，URL路径映射仅仅基于控制器和相关的操作名称，而不能基于命名空间、源文件位置或方法参数。



## 添加多个路由

通过添加对 MapRoute 的多次调用，可以在 UseMvc 内添加多个路由。 这样做可以定义多个约定，或添加专用于特定操作的传统路由，例如：

```c#
app.UseMvc(routes =>
{
   routes.MapRoute("blog", "blog/{*article}",
            defaults: new { controller = "Blog", action = "Article" });
   routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
});
```

对于第一个blog路由模板，由于controller和action不会在路由模板中作为路由参数显示，它们只能有默认值，因此该路由将始终映射到BlogController.Article操作方法。

路由集合中的路由会进行排序，并按添加顺序进行处理。 因此，在此示例中，将先尝试 blog 路由，再尝试default 路由。

### 路由名称

在上述代码中，routes.MapRoute()重载方法会传入一个name参数，即代码中的“blog”字符串和“default”字符串，这些都是路由名称。

路由名称为路由提供一个逻辑名称，以便使用命名路由来生成 URL。 路由排序会使 URL 生成复杂化，而这极大地简化了 URL 创建。路由名称必须在应用程序范围内是唯一的。

路由名称不影响请求的 URL 匹配或处理；它们仅用于 URL 生成。



## 属性路由——Route

属性路由使用一组Route属性将控制器操作方法直接映射到路由模板。 

```c#
public class HomeController : Controller
{
   [Route("")]
   [Route("Home")]
   [Route("Home/Index")]
   public IActionResult Index()
   {
      return View();
   }
   [Route("Home/About")]
   public IActionResult About()
   {
      return View();
   }
   [Route("Home/Contact")]
   public IActionResult Contact()
   {
      return View();
   }
}
```

与传统路由相比，属性路由需要精确控制应用于每项操作方法的路由模板。使用属性路由时，控制器名称和操作方法名称对于匹配的选择没有影响。



## 属性路由——Http[Verb]（Http谓词属性）

常见的Http谓词属性有HttpPost、HttpGet等：

```c#
[HttpGet("/products")]
public IActionResult ListProducts()
{
   // ...
}

[HttpPost("/products")]
public IActionResult CreateProduct(...)
{
   // ...
}
```

上述代码中，对于诸如 /products 之类的 URL 路径，当 Http 谓词为 GET 时将执行 ProductsApi.ListProducts 操作，当 Http 谓词为 POST 时将执行 ProductsApi.CreateProduct。 

**提示**：与[Route(...)]相比，应该优先使用HTTP谓词属性路由，尤其是对于REST API应用，更是如此。

下述示例演示了如何在路由模板中定义路由参数id，并在操作方法中，获取id参数的值：

```c#
public class ProductsApiController : Controller
{
   [HttpGet("/products/{id}", Name = "Products_List")]
   public IActionResult GetProduct(int id) { ... }
}
```

### HTTP谓词属性路由中的路由名称

```c#
[HttpGet("/products/{id}", Name = "Products_List")]
```

第二个参数Name即为路由名称，这里的路由名称为”Products_List“，和上文介绍的路由名称一样，这里的路由名称也不影响路由的URL匹配行为，仅用于生成URL，路由名称必须在应用程序范围内唯一。



## 合并路由

即在控制器上使用路由属性将各个操作上的属性路由进行合并，从而使属性路由减少重复。

控制器上定义的所有路由模板，将会作为操作方法上的路由模板的前缀一同被映射使用（以/或~/开头的路由模板除外）

在控制器上使用的路由属性，会使该控制器中的所有操作方法都使用属性路由。

```c#
[Route("products")]
public class ProductsApiController : Controller
{
   [HttpGet]
   public IActionResult ListProducts() { ... }

   [HttpGet("{id}")] //可以匹配的url：/products/5
   public ActionResult GetProduct(int id) { ... }
}
```

**注意：操作方法上的以/或~/开头的路由模板，不会和应用于控制器上的路由模板合并，而是作为独立的路由模板被使用。**



## 路由排序

默认情况下，更具体特定的路由比一般的路由先执行，例如：blog/search/{topic} 这样的路由比像 blog/{*article} 这样的路由更具体更特定，因此它具有更高的优先级先运行。

属性路由也可以使用框架提供的所有路由都具有的属性Order来配置顺序，路由按 Order 属性的升序进行处理。 默认顺序为 0。 使用 Order = -1 设置的路由比未设置顺序的路由先运行。 使用 Order = 1 设置的路由在默认路由排序后运行。

注意：虽然可以通过设置Order属性改变路由的匹配顺序，但是通常不建议这么做，如果用于 URL 生成的默认顺序不起作用，使用路由名称作为替代项通常比应用 Order 属性更简单。



## 路由模板中的标记替换（[controller]、[action]、[area]）

路由模板中，使用中括号（[和]）括起来的部分被称为路由标记，常见的标记有[controller]、[action]、[area]，这些标记在路由映射时，分别被替换为路由的操作方法名称值、区域名称值和控制器名称值。

路由标记常被应用在属性路由中，标记替换发生在属性路由生成的最后一步。

控制器属性路由中使用标记替换：

```c#
[Route("[controller]/[action]")]
public class ProductsController : Controller
{
    [HttpGet] // 匹配： '/Products/List'
    public IActionResult List() {
        // ...
    }

    [HttpGet("{id}")] // 匹配： '/Products/Edit/{id}'
    public IActionResult Edit(int id) {
        // ...
    }
}
```

操作方法的属性路由中使用标记替换：

```c#
public class ProductsController : Controller
{
    [HttpGet("[controller]/[action]")] // Matches '/Products/List'
    public IActionResult List() {
        // ...
    }

    [HttpGet("[controller]/[action]/{id}")] // Matches '/Products/Edit/{id}'
    public IActionResult Edit(int id) {
        // ...
    }
}
```

**属性路由还可以与继承结合使用，尤其是与标记替换结合使用尤为强大。**

```c#
[Route("api/[controller]")]
public abstract class MyBaseController : Controller { ... }

public class ProductsController : MyBaseController
{
   [HttpGet] // Matches '/api/Products'
   public IActionResult List() { ... }

   [HttpPut("{id}")] // Matches '/api/Products/{id}'
   public IActionResult Edit(int id) { ... }
}
```

### 路由名称中的标记替换

标记替换也适用于属性路由定义的路由名称。

```c#
[Route("[controller]/[action]", Name="[controller]_[action]")]
```

上述代码为每项操作生成一个唯一的路由名称。

**注意：若要匹配文本标记替换分隔符 [ 或 ]，可通过重复该字符（[[ 或 ]]）对其进行转义。**

### 使用参数转换程序自定义属性路由标记替换

属性路由支持使用参数转换程序自定义标记替换方式。

参数转换程序是一个实现了IOutboundParameterTransformer接口的类，用于转换参数值。

```c#
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        if (value == null) return null;

        return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
```

上述代码中的转换程序可以将“MyTest“路由值转换为”my-test“。

如果想要将参数转换程序应用到属性路由中，需要使用RouteTokenTransformerConvention（应用程序模型约定），RouteTokenTransformerConvention可以将参数转换程序应用到程序中的所有属性路由上。它在 ConfigureServices 中注册为选项：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options => {
    	//添加路由转换程序
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;
}
```

控制器示例：

```c#
public class SubscriptionManagementController : Controller
{
    [HttpGet("[controller]/[action]")] // 匹配： '/subscription-management/list-all'
    public IActionResult ListAll() { ... }
}
```

**注意：参数转换程序只能作用于属性路由，默认路由不受影响。**例如，下述操作方法没有使用属性路由，即使配置的转换程序，依然只能使用默认路由进行匹配：

```c#
//[Route("[action]")] 
public IActionResult MyTest() //属性路由被注释，只能匹配：/SubscriptionManagement/mytest
{
    return View();
}
```



## 作用于同一个操作方法或控制器上的多个路由

可以在同一个操作方法或控制器上定义多个路由。例如：

```c#
[Route("[controller]")]
public class ProductsController : Controller
{
   [Route("")]     // Matches 'Products'
   [Route("Index")] // Matches 'Products/Index'
   public IActionResult Index()
}
```

在控制器上放置多个路由属性意味着，每个路由属性将与操作方法上的每个路由属性合并。

```c#
[Route("Store")]
[Route("[controller]")]
public class ProductsController : Controller
{
   [HttpPost("Buy")]     // Matches 'Products/Buy' and 'Store/Buy'
   [HttpPost("Checkout")] // Matches 'Products/Checkout' and 'Store/Checkout'
   public IActionResult Buy()
}
[Route("api/[controller]")]
public class ProductsController : Controller
{
   [HttpPut("Buy")]      // Matches PUT 'api/Products/Buy'
   [HttpPost("Checkout")] // Matches POST 'api/Products/Checkout'
   public IActionResult Buy()
}
```



## 自定义路由属性

### 实现IRouteTemplateProvider接口自定义路由属性

框架提供的所有路由属性，如[Route(...)]、[HttpGet(...)] 等，都实现了IRouteTemplateProvider接口。当应用启动时，MVC 会查找控制器类和操作方法上的属性，并使用可实现 IRouteTemplateProvider接口的属性生成一组初始路由。

同样，可实现IRouteTemplateProvider接口来自定义自己的路由属性。

```c#
public class MyCusRouteAttribute : Attribute, IRouteTemplateProvider
{
    //自定义路由模板
    public string Template => "smallz/[controller]";

    //路由顺序
    public int? Order{get;set;}

    //路由名称
    public string Name { get; set; }
}
```

每个实现IRouteTemplateProvider接口的路由，都包含自定义路由模板、顺序和名称属性。

应用自定义路由属性：

```c#
[MyCusRoute]
public IActionResult MyRoute()
{
    return View();
}
```

上述的操作方法（控制器名为Home）使用了自定义路由属性MyCusRoute，当使用url为/smallz/Home进行访问时，就可以匹配到该操作上。

### 使用应用程序模型自定义属性路由

> 应用程序模型是一个在启动时创建的对象模型，MVC 可使用其中的所有元数据来路由和执行操作。 应用程序模型包含从路由属性收集（通过 IRouteTemplateProvider）的所有数据。 可通过编写约定在启动时修改应用程序模型，以便自定义路由的行为方式。 

```c#
public class NamespaceRoutingConvention : IControllerModelConvention
{
    private readonly string _baseNamespace;

    public NamespaceRoutingConvention(string baseNamespace)
    {
        _baseNamespace = baseNamespace;
    }

    public void Apply(ControllerModel controller)
    {
        var hasRouteAttributes = controller.Selectors.Any(selector => selector.AttributeRouteModel != null);

        if (hasRouteAttributes)
        {
            return;
        }

        var namespc = controller.ControllerType.Namespace;
        if (namespc == null)
        {
            return;
        }
        var template = new StringBuilder();
        template.Append(namespc, _baseNamespace.Length + 1, namespc.Length - _baseNamespace.Length - 1);
        template.Replace('.', '/');
        template.Append("/[controller]");

        foreach (var selector in controller.Selectors)
        {
            selector.AttributeRouteModel = new AttributeRouteModel()
            {
                Template = template.ToString()
            };
        }
    }
}
```



## 混合路由：属性路由与传统路由

MVC 应用程序可以混合使用传统路由与属性路由。 

传统路由：通常用于为浏览器处理HTML页面的控制器；

属性路由：通常用于处理REST API的控制器；

控制器操作方法既支持传统路由，也支持属性路由。但是不能同时存在，一旦指定了属性路由，那么传统路由将不再支持访问。

属性路由比传统路由具有更高的优先级。



## Url生成

MVC应用程序可以使用控制器的Url属性，生成指向操作方法的URL链接。它

```c#
public IUrlHelper Url { get; set; }
```

IUrlHelper 接口用于生成 URL，在控制器、视图和视图组件中，可通过 Url 属性得到IUrlHelper 的实例。

```c#
using Microsoft.AspNetCore.Mvc;

public class UrlGenerationController : Controller
{
    [HttpGet("")]
    public IActionResult Source()
    {
        var url = Url.Action("Destination"); // Generates /custom/url/to/destination
        return Content($"Go check out {url}, it's really great.");
    }

    [HttpGet("custom/url/to/destination")]
    public IActionResult Destination() {
        return View();
    }
}
```

### 根据操作名称生成URL

Url.Action()的所有相关重载方法，都是通过指定控制器名称和操作名称来生成链接的。

```c#
public class TestController : Controller
{
    public IActionResult Index()
    {
        // Generates /Products/Buy/17?color=red
        var url = Url.Action("Buy", "Products", new { id = 17, color = "red" });
        return Content(url);
    }
}
```

若要创建绝对 URL，请使用采用 protocol 的重载：

```c#
Url.Action("Buy", "Products", new { id = 17 }, protocol: Request.Scheme);
```

### 根据路由生成URL

使用Url.RouteUrl()方法可以根据路由生成URL。最常见的用法是指定一个路由名称，以使用特定路由来生成 URL，通常不指定控制器或操作名称。

```c#
public class UrlGenerationController : Controller
{
    [HttpGet("")]
    public IActionResult Source()
    {
        var url = Url.RouteUrl("Destination_Route"); // Generates /custom/url/to/destination
        return Content($"See {url}, it's really great.");
    }

    [HttpGet("custom/url/to/destination", Name = "Destination_Route")]
    public IActionResult Destination() {
        return View();
    }
}
```

### 在HTML中生成URL

IHtmlHelper 提供了 Html.BeginForm() 和 Html.ActionLink()方法，可分别生成 <form> 和 <a> 元素。这些方法使用 Url.Action())方法来生成 URL，并且采用相似的参数。 

对应的HtmlHelper 的Url.RouteUrl()为Html.BeginRouteForm()和 Html.RouteLink()，两者具有相似的功能。

### 在操作结果中生成URL

控制器中最常见的用法是将 URL 生成为操作结果的一部分。

ControllerBase 和 Controller 基类为操作结果提供简便的方法来引用另一项操作。 一种典型用法是在接受用户输入后进行重定向。

```c#
public IActionResult Edit(int id, Customer customer)
{
    if (ModelState.IsValid)
    {
        // Update DB with new details.
        return RedirectToAction("Index");
    }
    return View(customer);
}
```



## IActionConstraint



https://docs.microsoft.com/zh-cn/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.2#implementing-iactionconstraint















