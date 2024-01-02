# ASP.NET Core WebAPI响应数据的格式设置

原文参考链接：<https://docs.microsoft.com/zh-cn/aspnet/core/web-api/advanced/formatting?view=aspnetcore-2.2>

当Web API控制器操作返回结果时，可以对结果进行格式设置。



## 特定于格式的操作结果

这类操作结果的类型特定于特殊格式，例如JsonResult和ContentResult。

> 返回非 IActionResult 类型对象的操作结果将使用相应的 IOutputFormatter 实现来进行序列化。
>
> 对于有多个返回类型或选项的重要操作（例如基于所执行操作的结果的不同 HTTP 状态代码），请首选 IActionResult 作为返回类型。



## 内容协商（Content Negotiation）

当客户端指定 [Accept 标头](https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html)时，会发生内容协商（简称 conneg）。

ASP.NET Core MVC 使用的默认格式是 JSON。 

内容协商由 ObjectResult 实现，并且还内置于从帮助程序方法（全部基于 ObjectResult）返回的特定于状态代码的操作结果中。 还可以返回一个模型类型（已定义为数据传输类型的类），框架将自动将其打包在 ObjectResult 中。

例如，下述代码中使用了OK()和NotFound()帮助程序方法：

```c#
[HttpGet("Search")]
public IActionResult Search(string namelike)
{
    if (_context.TodoItems.Any(a => a.Name == namelike))
    {
        return Ok();
    }
    return NotFound(namelike);
}
```

这两个方法都将返回JSON格式的响应。

 默认情况下，ASP.NET Core MVC 仅支持 JSON。所以，即使指定另一种格式，返回的结果仍然是 JSON 格式。（可以通过配置格式化程序改变这一行为，但是大多数情况下，不需要进行格式化配置）

即使控制器操作返回的是一个普通的类对象（例如Student），在这种情况下，ASP.NET Core MVC 将自动创建打包对象的 ObjectResult。 客户端将获取设有格式的序列化对象（默认为 JSON 格式，可以配置 XML 或其他格式）。 如果返回的对象为 null，那么框架将返回 204 No Content 响应。

例如：

```c#
public Student Get(string stuNo)
{
    return GetByStudent(stuNo);
}
```

上述代码中，如果获取到有效的Student，将会返回“200 正常”响应，如果获取的是null，将会返回“204 无内容”响应。

注意：该示例更加说明了不同的HTTP状态码特定于不同的场景，实际中应该结合相应的场景，使用特定于此场景的具有语义的HTTP状态码对应的返回IActionResult的方法。

### 内容协商过程

> 内容协商仅在 `Accept` 标头出现在请求中时发生。 请求包含 accept 标头时，框架会以最佳顺序枚举 accept 标头中的媒体类型，并且尝试查找可以生成一种由 accept 标头指定格式的响应的格式化程序。 如果未找到可以满足客户端请求的格式化程序，框架将尝试找到第一个可以生成响应的格式化程序（除非开发人员配置 `MvcOptions` 上的选项以返回“406 不可接受”）。 如果请求指定 XML，但是未配置 XML 格式化程序，那么将使用 JSON 格式化程序。 一般来说，如果没有配置可以提供所请求格式的格式化程序，那么使用第一个可以设置对象格式的格式化程序。 如果不提供任何标头，则将使用第一个可以处理要返回的对象的格式化程序来序列化响应。 在此情况下，没有任何协商发生 - 服务器确定将使用的格式。
>
> 说明：
>
> 如果 Accept 标头包含 `*/*`，则将忽略该标头，除非 `RespectBrowserAcceptHeader` 在 `MvcOptions` 上设置为 true。

### 浏览器和内容协商

>不同于传统 API 客户端，Web 浏览器倾向于提供包括各种格式（含通配符）的 `Accept` 标头。 默认情况下，当框架检测到请求来自浏览器时，它将忽略 `Accept` 标头转而以应用程序的配置默认格式（JSON，除非有其他配置）返回内容。 这在使用不同浏览器使用 API 时提供更一致的体验。
>
>如果首选应用程序服从浏览器 accept 标头，则可以将此配置为 MVC 配置的一部分，方法是在 Startup.cs 中以 `ConfigureServices`方法将 `RespectBrowserAcceptHeader` 设置为 `true`。
>
>```c#
>services.AddMvc(options =>
>{
>    options.RespectBrowserAcceptHeader = true; // 默认为false
>});
>```



## 配置格式化程序

如果应用程序需要支持默认 JSON 格式以外的其他格式，那么可以添加 NuGet 包并配置 MVC 来支持它们。

 输入和输出的格式化程序不同：输入格式化程序由模型绑定使用；输出格式化程序用来设置响应格式。 

### 添加 XML 格式支持

若要添加对 XML 格式的支持，请安装 Microsoft.AspNetCore.Mvc.Formatters.Xml NuGet 包。

将 XmlSerializerFormatters 添加到 Startup.cs 中 MVC 的配置：

```c#
services.AddMvc()
    .AddXmlSerializerFormatters();
```

或者，可以仅添加输出格式化程序：

```c#
services.AddMvc(options =>
{
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
```

这两个方法将使用 System.Xml.Serialization.XmlSerializer 来序列化结果。 如果愿意，可以通过添加相关联的格式化程序使用 System.Runtime.Serialization.DataContractSerializer：

```c#
services.AddMvc(options =>
{
    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
});
```

### 强制执行特定格式

如果需要限制特定操作的响应格式，那么可以应用 [Produces] 筛选器。 [Produces] 筛选器指定特定操作（或控制器）的响应格式。 如同大多筛选器，这可以在操作层面、控制器层面或全局范围内应用。

```c#
[Produces("application/json")]
public class AuthorsController
```

[Produces] 筛选器将强制 AuthorsController 中的所有操作返回 JSON 格式的响应，即使已经为应用程序配置其他格式化程序且客户端提供了请求其他可用格式的 Accept 标头也是如此。 

### 特例格式化程序

一些特例是使用内置格式化程序实现的。 默认情况下，`string` 返回类型的格式将设为 text/plain（如果通过 `Accept` 标头请求则为 text/html）。可以通过删除 TextOutputFormatter 删除此行为。 有模型对象返回类型的操作将在返回 `null` 时返回“204 无内容”响应。 可以通过删除 `HttpNoContentOutputFormatter` 删除此行为。（具体见下述示例）

在 Startup.cs 中通过 Configure 方法删除格式化程序，以下代码删除 TextOutputFormatter 和 HttpNoContentOutputFormatter：

```c#
services.AddMvc(options =>
{
    options.OutputFormatters.RemoveType<TextOutputFormatter>();
    options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
});
```

没有 `TextOutputFormatter` 时，`string` 返回类型将返回“406 不可接受”等内容。 请注意，如果 XML 格式化程序存在，当删除 `TextOutputFormatter` 时，它将设置 `string` 返回类型的格式。

没有 `HttpNoContentOutputFormatter`，null 对象将使用配置的格式化程序来进行格式设置。 例如，JSON 格式化程序仅返回正文为 `null` 的响应，而 XML 格式化程序将返回属性设为 `xsi:nil="true"` 的空 XML 元素。



## 响应格式URL文件扩展名映射

客户端可要求特定格式作为 URL 一部分，例如在查询字符串中或在路径的一部分中，或者通过使用特定格式的文件扩展名（例如 .xml 或 .json）。 请求路径的映射必须在 API 使用的路由中指定。 例如:

```c#
[FormatFilter]
public class ProductsController
{
    [Route("[controller]/[action]/{id}.{format?}")]
    public Product GetById(int id)
```

此路由将允许所所请求格式指定为可选文件扩展名。 `[FormatFilter]` 属性检查 `RouteData` 中格式值是否存在，并在响应创建时将响应格式映射到相应格式化程序。

| 路由                       | 格式化程序                |
| :------------------------- | :------------------------ |
| `/products/GetById/5`      | 默认输出格式化程序        |
| `/products/GetById/5.json` | JSON 格式化程序（如配置） |
| `/products/GetById/5.xml`  | XML 格式化程序（如配置）  |



## 自定义格式化程序

默认情况下，ASP.NET Core MVC 使用 JSON 或 XML，为 Web API 中的数据交换提供内置支持。通过创建自定义格式化程序，可以添加对其他格式的支持。例如上文中提到的内容协商，如果希望在内容协商的过程中支持内置格式化程序（JSON 和 XML）所不支持的其他内容类型，可使用自定义格式化程序。

### 创建和使用自定义格式化程序的步骤

- 如果想对服务器端发送的数据进行序列化，则创建输出格式化程序类
- 如果想对服务器端接收的数据进行反序列化，则创建输入格式化程序类
- 将格式化程序的实例添加到 MvcOptions 中的 InputFormatters 和 OutputFormatters 集合

### 创建自定义格式化程序类

若要创建格式化程序，需要执行以下操作：

- 创建派生自相应基类的程序类
- 在构造函数中指定有效的媒体类型和编码
- 重写 `CanReadType`/`CanWriteType(或CanWriteResult)` 方法
- 重写 `ReadRequestBodyAsync`/`WriteResponseBodyAsync` 方法

#### 第一步：创建派生自相应基类的程序类

- 文本类型：从[TextInputFormatter](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.mvc.formatters.textinputformatter) 或 [TextOutputFormatter](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.mvc.formatters.textoutputformatter) 基类派生。例如：

  ```c#
  public class VcardOutputFormatter : TextOutputFormatter
  或
  public class VcardInputFormatter : TextInputFormatter
  ```

- 二进制类型：从 [InputFormatter](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.mvc.formatters.inputformatter) 或 [OutputFormatter](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.mvc.formatters.outputformatter) 基类派生。

#### 第二步：在构造函数中，指定有效的媒体类型和编码

在构造函数中，通过添加到 SupportedMediaTypes 和 SupportedEncodings 集合来指定有效的媒体类型和编码。

VcardOutputFormatter.cs：

```c#
public class VcardOutputFormatter : TextOutputFormatter
{
    public VcardOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }
}
```

VcardInputFormatter.cs：

```c#
public class VcardInputFormatter : TextInputFormatter
{
    public VcardInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }
}
```

**注意：**不能在格式化程序类中执行构造函数依赖关系注入。 例如，不能通过向构造函数添加记录器参数来获取记录器。 若要访问服务，必须使用传递到方法的上下文对象。 下文中的代码示例（见第四步）展示了如何执行此操作。

#### 第三步：重写 CanReadType/CanWriteType（或CanWriteResult）方法

通过重写 CanReadType 或 CanWriteType 方法，指定可反序列化为的类型，或从其序列化的类型。

下面示例指定只能从Contact类型创建vCard文本，反之亦然：

VcardOutputFormatter.cs：

```c#
public class VcardOutputFormatter : TextOutputFormatter
{
    public VcardOutputFormatter()
    {
       ...//见上文代码
    }

    protected override bool CanWriteType(Type type)
    {
        if (typeof(Contact).IsAssignableFrom(type) 
            || typeof(IEnumerable<Contact>).IsAssignableFrom(type))
        {
            return base.CanWriteType(type);
        }
        return false;
    }
}
```

VcardInputFormatter.cs：

```c#
public class VcardInputFormatter : TextInputFormatter
{
    public VcardInputFormatter()
    {
        ...//见上文
    }

    protected override bool CanReadType(Type type)
    {
        if (type == typeof(Contact))
        {
            return base.CanReadType(type);
        }
        return false;
    }
}
```

##### CanWriteResult 方法

在某些情况下，必须重写 `CanWriteResult`，而不是 `CanWriteType`。 如果同时满足以下条件，则使用 `CanWriteResult`：

- 操作方法返回模型类。
- 具有可能在运行时返回的派生类。
- 需要知道操作在运行时返回了哪个派生类。

例如，假设操作方法签名返回 `Person` 类型，但它可能返回从 `Person` 派生的 `Student` 或 `Instructor` 子类型。 如果希望格式化程序仅处理 `Student` 对象，请检查提供给 `CanWriteResult` 方法的上下文对象中的[OutputFormatterCanWriteContext](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.mvc.formatters.outputformattercanwritecontext#Microsoft_AspNetCore_Mvc_Formatters_OutputFormatterCanWriteContext_Object)类型。 请注意，当操作方法返回 `IActionResult` 时，不必使用 `CanWriteResult`；在这种情况下，`CanWriteType` 方法可接收运行时类型。

#### 第四步：重写 `ReadRequestBodyAsync`/`WriteResponseBodyAsync` 方法

实际的反序列化或序列化工作在 ReadRequestBodyAsync 或 WriteResponseBodyAsync 中执行。

下述代码展示了如何从依赖关系注入容器中获取服务（不能从构造函数参数中获取它们）。

VcardOutputFormatter.cs：

```c#
public class VcardOutputFormatter : TextOutputFormatter
{
    public VcardOutputFormatter()
    {
    	...//见上文
    }

    protected override bool CanWriteType(Type type)
    {
    	...//见上文
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        IServiceProvider serviceProvider = context.HttpContext.RequestServices;
        var logger = serviceProvider.GetService(typeof(ILogger<VcardOutputFormatter>)) as ILogger;

        var response = context.HttpContext.Response;

        var buffer = new StringBuilder();
        if (context.Object is IEnumerable<Contact>)
        {
            foreach (Contact contact in context.Object as IEnumerable<Contact>)
            {
                FormatVcard(buffer, contact, logger);
            }
        }
        else
        {
            var contact = context.Object as Contact;
            FormatVcard(buffer, contact, logger);
        }
        await response.WriteAsync(buffer.ToString());
    }

    private static void FormatVcard(StringBuilder buffer, Contact contact, ILogger logger)
    {
        buffer.AppendLine("BEGIN:VCARD");
        buffer.AppendLine("VERSION:2.1");
        buffer.AppendFormat($"N:{contact.LastName};{contact.FirstName}\r\n");
        buffer.AppendFormat($"FN:{contact.FirstName} {contact.LastName}\r\n");
        buffer.AppendFormat($"UID:{contact.ID}\r\n");
        buffer.AppendLine("END:VCARD");
        logger.LogInformation($"Writing {contact.FirstName} {contact.LastName}");
    }
}
```

VcardInputFormatter.cs：

```c#
public class VcardInputFormatter : TextInputFormatter
{
    public VcardInputFormatter()
    {
    	...//见上文
    }

    protected override bool CanReadType(Type type)
    {
    	...//见上文
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding effectiveEncoding)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (effectiveEncoding == null)
        {
            throw new ArgumentNullException(nameof(effectiveEncoding));
        }

        var request = context.HttpContext.Request;

        using (var reader = new StreamReader(request.Body, effectiveEncoding))
        {
            try
            {
                await ReadLineAsync("BEGIN:VCARD", reader, context);
                await ReadLineAsync("VERSION:2.1", reader, context);

                var nameLine = await ReadLineAsync("N:", reader, context);
                var split = nameLine.Split(";".ToCharArray());
                var contact = new Contact() { LastName = split[0].Substring(2), FirstName = split[1] };

                await ReadLineAsync("FN:", reader, context);

                var idLine = await ReadLineAsync("UID:", reader, context);
                contact.ID = idLine.Substring(4);

                await ReadLineAsync("END:VCARD", reader, context);

                return await InputFormatterResult.SuccessAsync(contact);
            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }
        }
    }

    private async Task<string> ReadLineAsync(string expectedText, StreamReader reader, InputFormatterContext context)
    {
        var line = await reader.ReadLineAsync();
        if (!line.StartsWith(expectedText))
        {
            var errorMessage = $"Looked for '{expectedText}' and got '{line}'";
            context.ModelState.TryAddModelError(context.ModelName, errorMessage);
            throw new Exception(errorMessage);
        }
        return line;
    }
}
```

### 将 MVC 配置为使用自定义格式化程序

若要使用自定义格式化程序，需要将格式化程序类的实例添加到 InputFormatters 或 OutputFormatters 集合。

```c#
services.AddMvc(options =>
{
    options.InputFormatters.Insert(0, new VcardInputFormatter());
    options.OutputFormatters.Insert(0, new VcardOutputFormatter());
})
.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
```

**按格式化程序的插入顺序对其进行计算。 第一个优先。**

该程序运行说明：

>若要查看 vCard 输出，请运行该应用程序，并向 http://localhost:63313/api/contacts/（从 Visual Studio 运行时）或 http://localhost:5000/api/contacts/（从命令行运行时）发送具有 Accept 标头“text/vcard”的 Get 请求。
>若要将 vCard 添加到内存中联系人集合，请向相同的 URL 发送具有 Content-Type 标头“text/vcard”且正文中包含 vCard 文本的 Post 请求，格式化方式与上面的示例类似。





