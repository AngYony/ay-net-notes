# ASP.NET Core 标记帮助程序



## 标记帮助程序概述

标记帮助程序使用 C# 创建，基于元素名称、属性名称或父标记以 HTML 元素为目标，它使服务器端代码可以在Razor文件中参与创建和呈现HTML元素。

### 标记帮助程序和HTML帮助程序的比较

标记帮助程序有利于减少 Razor 视图中 HTML 和 C# 之间的显式转换。 通常情况下，如果能够使用标记帮助程序进行HTML标签的呈现，推荐优先使用标记帮助程序；如果使用标记帮助程序不能够满足需要，可以使用HTML帮助程序，它是标记帮助程序的替换方法，即：能够使用标记帮助程序的，一定可以使用HTML帮助程序进行替换。

注意：能够使用HTML帮助程序的，不一定可以使用标记帮助程序替换，因为，并非每个 HTML 帮助程序都有对应的标记帮助程序。

> 在很多情况下，HTML 帮助程序为特定标记帮助程序提供了一种替代方法，但标记帮助程序不会替代 HTML 帮助程序，且并非每个 HTML 帮助程序都有对应的标记帮助程序，认识到这点也很重要。



## 标记帮助程序作用域

控制标记帮助程序的作用域主要有以下几种方式：

- @addTagHelper
- @removeTagHelper
- 使用“`!`”操作符
- @tagHelperPrefix

### @addTagHelper

@addTagHelper指令指示视图可以使用标记帮助程序，一般会将该指令内容添加到`_ViewImports.cshtml`文件中，与`_ViewImports.cshtml`同级的文件和子文件夹都会应用其指定的指令，常见的指令内容如下：

```c#
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

@addTagHelper指令需要指定两个参数，第一个参数指定要加载的标记帮助程序，如果是单一的标记帮助程序，必须指定该标记帮助程序的完整限定名，一般更常用的是使用`*`表示加载该程序集（由第二个参数指定）中包含的所有标记程序；第二个参数指定包含标记帮助程序（由第一个参数指定）的程序集。

例如：`@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`表示加载Microsoft.AspNetCore.Mvc.TagHelpers程序集中包含的所有标记帮助程序。

Microsoft.AspNetCore.Mvc.TagHelpers 是ASP.NET Core 内置的标记帮助程序的程序集。

“`*`”在这里使用的是通配符语法，可以直接插入通配符“`*`”作为后缀，例如：

```
@addTagHelper AuthoringTagHelpers.TagHelpers.E*, AuthoringTagHelpers
@addTagHelper AuthoringTagHelpers.TagHelpers.Email*, AuthoringTagHelpers
```

### @removeTagHelper

@removeTagHelper 与 @addTagHelper 具有相同的两个参数，它会删除之前添加的标记帮助程序。 例如，应用于特定视图的 @removeTagHelper 会删除该视图中的指定标记帮助程序。 在 Views/Folder/_ViewImports.cshtml 文件中使用 @removeTagHelper，将从 Folder 中的所有视图删除指定的标记帮助程序。

### 选择退出字符“`!`”

使用标记帮助程序选择退出字符（“!”），可在元素级别禁用标记帮助程序。例如：

```
<!span asp-validation-for="Email" class="text-danger"></!span>
```

须将标记帮助程序选择退出字符应用于开始和结束标记。 （将选择退出字符添加到开始标记时，Visual Studio 编辑器会自动为结束标记添加相应字符）。 添加选择退出字符后，元素和标记帮助程序属性不再以独特字体显示。

### @tagHelperPrefix

@tagHelperPrefix 指令可指定一个标记前缀字符串，只有使用了该前缀的元素才支持标记帮助程序。

例如：

```
@tagHelperPrefix th:
```

上述代码表示，只有使用了前缀th:的元素才支持标记帮助程序。

```html
<th:label asp-for="wy"></th:label>
<label asp-for="wy2">women</label>
```

上述代码只有`<th:label>`中的asp-for属性能够被标记帮助程序解析，`<label>`不能被解析。

*提示：可以使用标记帮助程序的元素及属性使用独特的字体进行显示，可以通过该特性查看哪些元素能够被解析。*

**==特别注意：使用上述指令时，一定不要在末尾处添加分号（";"），否则将无法解析指令==**



## 创建标记帮助程序

标记帮助程序是实现ITagHelper接口的任何类。但是通常通过派生自TagHelper类来创建自己的标记帮助程序，TagHelper提供了供子类重写的Process()方法。

TagHelper类的定义如下：

```c#
public abstract class TagHelper : ITagHelper, ITagHelperComponent
{
	protected TagHelper();
	public virtual int Order { get; }
	public virtual void Init(TagHelperContext context);
	public virtual void Process(TagHelperContext context, TagHelperOutput output);
	public virtual Task ProcessAsync(TagHelperContext context, TagHelperOutput output);
}
```

### 创建简单的标记帮助程序

创建一个派生自TagHelper的类，推荐类名以TagHelper结尾，并重写父类的Process()方法，如下：

```c#
namespace My.TagHelpers.Study.CusTagHelpers
{
    public class EmailTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
        }
    }
}
```

上述代码将标记名称为email的标记，转换为a标签。

标记帮助程序使用不包含“TagHelper”后缀的类名的小写形式作为标记的目标名称，此处为`<email>`。重写的Process()方法中，可以使用包含与执行当前HTML标记相关的信息的下文参数TagHelperContext，和输出参数TagHelperOutput（它包含监控状态的HTML元素，代表用于生成 HTML 标记和内容的原始源）。

要让标记帮助程序用于Razor视图，还需要在Razor视图中（通常是Views/_ViewImports.cshtml 文件）使用@addTagHelper指令。

例如：

```html
@addTagHelper My.TagHelpers.Study.CusTagHelpers.EmailTagHelper,My.TagHelpers.Study
<email>Support</email>
```

执行程序，上述标记将被转换为：

```
<a>Support</a>
```

注意：转换后的结果`<a>`标签并不包含href属性。

### 创建包含属性的标记帮助程序

如果想要标记支持属性，只需要在对应的标记帮助程序中，定义该类的属性即可。对上述的EmailTagHelper类进行重构，代码如下：

```c#
public class EmailTagHelper:TagHelper
{
    private const string EmailDomain = "163.com";
    public string MailTo { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        var address = $"{MailTo}@{EmailDomain}";
        output.Attributes.SetAttribute("href", "mailto:" + address);
        output.Content.SetContent(address);
    }
}
```

该标记帮助程序定义了MailTo属性，在页面使用<email>标记时，不能直接使用MailTo作为标记的属性，而是使用由短横线分割各个单词的格式的小写形式作为属性，即：`<email mail-to="value"></email>`。

使用上述定义的标记帮助程序，Razor页面的代码如下：

```html
<email mail-to="wy"></email>
```

执行程序生成的页面元素如下：

```html
<a href="mailto:wy@163.com">wy@163.com</a>
```

### 创建标记自结束的标记帮助程序

如果想使用标记自结束的标记，例如，上述中直接使用`<email mail-to="wy" />`，可以在标记帮助程序中，指定HtmlTargetElement特性，并为该特性的TagStructure属性设置为TagStructure.WithoutEndTag。

代码如下：

```c#
[HtmlTargetElement("email",TagStructure =TagStructure.WithoutEndTag)]
public class EmailTagHelper:TagHelper
{
	...
}
```

上述代码可以在Razor页面中直接使用`<email mail-to="wy" />`自结束形式的标记，需要注意的是，虽然可以使用自结束形式的标记，但是在实际页面输出时，并不能正确的显示结果：

```
<a href="mailto:wy@163.com"></a>
```

可以看到，上述的<a>标记中，并不包含内容部分，这是因为自结束形式的标记，最终输出的结果也将是自结束的，也就是会输出`<a href="mailto:wy@163.com" />`，它并不是一个有效的HTML，被浏览器解析时，就变成了内容为空的超链接。

解决方案是，在Process()方法中，设置output.TagMode的值为TagMode.StartTagAndEndTag，代码如下：

```c#
public override void Process(TagHelperContext context, TagHelperOutput output)
{
    ...
    output.TagMode = TagMode.StartTagAndEndTag;
}
```

此时，虽然在Razor页面中使用的是自结束形式的标记，但是在最终输出时，会添加HTML结束标记。

Razor页面代码如下：

```html
<email mail-to="wy" />
```

最终输出结果为：

```html
<a href="mailto:wy@163.com">wy@163.com</a>
```

注意：一旦使用HtmlTargetElement特性指定了TagStructure属性值为TagStructure.WithoutEndTag，那么在Razor页面，只能使用自结束的形式，否则运行会报错。

### 重写ProcessAsync异步方法创建标记帮助程序

和重写Process()方法类似，不同的是通过异步 GetChildContentAsync方法 返回包含 TagHelperContent 的 Task，并通过output参数获取HTML元素的内容。

```c#
public class OneTagHelper:TagHelper
{
    private const string EmailDomain = "163.com";
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        var content = await output.GetChildContentAsync();
        var target = content.GetContent() + "@" + EmailDomain;

        output.Attributes.SetAttribute("href", "mailto:" + target);
        output.Content.SetContent(target);
         
    }
}
```

### 创建以属性形式呈现的标记帮助程序

通过为标记帮助程序指定HtmlTargetElement特性，并为属性Attributes指定属性值，可以将其作为属性进行呈现。

例如，`<p bold>测试字符串</p>`，bold是一个自定义的标记帮助程序，在这里作为标记的属性进行解析，详细代码如下：

```c#
[HtmlTargetElement(Attributes = "bold")]
public class BoldTagHelper:TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll("bold");
        output.PreContent.SetHtmlContent("<strong>");
        output.PostContent.SetHtmlContent("</strong>");
    }
}
```

实际应用时，BoldTagHelper对应bold只能作为标签的属性被解析，Razor页面内容如下：

```html
<p bold>nnnnnnnnnn</p>
```

解析后，生成的HTML片段如下：

```html
<p><strong>nnnnnnnnnn</strong></p>
```

如果想要正确解析的解析下述标记内容：

```html
<bold>无效的bold标签</bold>
```

需要在BoldTagHelper中，使用[HtmlTargetElement("bold")]特性进行标注。

```c#
[HtmlTargetElement("bold")]
[HtmlTargetElement(Attributes = "bold")]
public class BoldTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll("bold");
        output.PreContent.SetHtmlContent("<strong>");
        output.PostContent.SetHtmlContent("</strong>");
    }
}
```

使用多个[HtmlTargetElement]特性修饰标记帮助程序类时，彼此之间是逻辑OR的联系；如果将多个属性添加到同一个语句中，运行时会将其视为逻辑AND。

```c#
[HtmlTargetElement("bold", Attributes = "bold")]
```

上述代码，HTML 元素必须命名为“bold”并具有名为“bold”的属性 (`<bold bold />)` 才能匹配。

也可以使用[HtmlTargetElement] 更改目标元素的名称，该名称是最终在Razor页面引用标记的名称，例如：

```c#
[HtmlTargetElement("MyBold")]
```

在Razor页面时，就可以使用`<mybold>`标记。

### 在标记帮助程序中使用模型（Model类）

这里的模型实质上就是一个自定义的C#实体类，例如：

```c#
public class WebsiteContext
{
    public Version Version { get; set; }
    public int CopyrightYear { get; set; }
    public bool Approved { get; set; }
    public int TagsToShow { get; set; }
}
```

如果想要将模型传递给标记帮助程序，只需要将模型作为该标记帮助程序的一个属性进行使用即可。

```c#
public class MyModelTagHelper:TagHelper
{
	public WebsiteContext MyInfo { get; set; }
	public override void Process(TagHelperContext context, TagHelperOutput output)
    {
    	output.TagName = "section";
        output.Content.SetHtmlContent($@"
<ul>
    <li>Version：{MyInfo.Version}</li>
    <li>Approved：{MyInfo.Approved}</li>
    <li>CopyrightYear：{MyInfo.CopyrightYear}</li>
</ul>");
		output.TagMode = TagMode.StartTagAndEndTag;
	}
}
```

该标记帮助程序在不使用[HtmlTargetElement]特性的情况下， 在Razor页面将以<my-model>作为标记的目标名称，同样属性MyInfo在Razor页面进行应用时，也以my-info作为标记属性的目标名称。

Razor页面内容如下：

```html
@{ 
    WebsiteContext mycontext = new WebsiteContext()
    {
        Version = new Version(1, 0, 0),
        CopyrightYear = 2019,
        Approved = true,
        TagsToShow = 111
    };

}
<my-model my-info="mycontext" />
```

上述在@{}代码块中，定义了一个实体对象，在应用标记时，将其作为my-info的属性值被指定。

由于在标记帮助程序中，指定了标记模式为TagMode.StartTagAndEndTag，因此最终会生成一个被<section>j标签包裹的片段，运行程序最终呈现的HTML片段如下：

```html
<section>
<ul>
    <li>Version：1.0.0</li>
    <li>Approved：True</li>
    <li>CopyrightYear：2019</li>
</ul></section>
```

### 创建条件标记帮助程序

条件标记帮助程序的实质是，在标记帮助程序类中添加了一组用来控制该标记是否可用的布尔类型的属性。

```c#
[HtmlTargetElement(Attributes =nameof(Where))]
public class WhereTagHelper:TagHelper
{
    public bool Where { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!Where)
        {
            output.SuppressOutput();
        }
    }
}
```

上述代码中的Where属性，只有属性值为true时，才会呈现输出。

由于使用了[HtmlTargetElement(Attributes =nameof(Where))]，因此where只能作为标记的属性才能被解析。

注意：在Razor页面中，所有应用标记的属性，都是解析的是C#代码，因此属性值可以指定能够被C#理解的代码。

例如，可以直接指定值：

```c#
<div where="false">test</div>
```

(值为false，页面将看不到该div，只要为true才能看到)

也可以指定为对象的属性：

```c#
<div where="mycontext.Approved">test</div>
```

无论哪种形式，只要最终解析的结果为true，就能够显示该`<div>`内容。

通过这种方式，可以很好的控制页面元素的动态显示，也是条件标记帮助程序最常见的用途。

下面是一个很常见的使用场景的代码。

在控制器中，需要捕捉是否显示元素的业务数据，可以使用查询字符串的形式进行获取：

```c#
public IActionResult Index(bool approved = false)
{
    return View(new WebsiteContext
    {
        Approved = approved,
        CopyrightYear = 2015,
        Version = new Version(1, 3, 3, 7),
        TagsToShow = 20
    });
}
```

当查询字符串使用“?approved=true”形式的URL时（例如，`http://localhost:1235/Home/Index?approved=true`），将会捕捉到approved的参数值，在Razor页面时，就可以通过Model进行获取和使用。

```html
@model WebsiteContext
<div where="Model.Approved">
        <p>
            This website has <strong surround="em">@Model.Approved</strong> been approved yet.
            Visit www.contoso.com for more information.
        </p>
    </div>
```

### 标记帮助程序冲突

如果定义了可以同时作用于同一个标签的多个标记帮助程序，在应用多个标记帮助程序时，产生了冲突，需要对TagHelper的属性Order进行重写，该值越小，对应的优先级越高，默认为0。

```c#
public override int Order
{
	get  {  return int.MinValue;   }
}
```

另外，**特别需要注意的是**，如果在标记帮助程序中，需要对最终呈现的内容进行重绘，一定要使用output.Content.IsModified属性监测内容是否已被修改，如果已修改，则从输出缓冲区获取内容。

```c#
var childContent = output.Content.IsModified ? output.Content.GetContent() : 
    (await output.GetChildContentAsync()).GetContent();
```

这段代码应该作为所有自定义标记帮助程序中，必不可少的代码片段。

### 检索和处理标记内容

标记帮助程序的主要作用就是对标记中的内容，按照设定的方式或格式进行展示和处理。重写的ProcessAsync()方法中，提供了多种检索标记内容的方式。

- 可将 GetChildContentAsync 的结果追加到 output.Content。

- 可使用 GetContent() 获取GetChildContentAsync 的结果。

- 如果直接修改output.Content，则不会执行或呈现 TagHelper 主体，除非调用GetChildContentAsync方法。

  ```c#
  public class AutoLinkerHttpTagHelper : TagHelper
  {
      public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
      {
          var childContent = output.Content.IsModified ? output.Content.GetContent() :
              (await output.GetChildContentAsync()).GetContent();
  
          // Find Urls in the content and replace them with their anchor tag equivalent.
          output.Content.SetHtmlContent(Regex.Replace(
               childContent,
               @"\b(?:https?://)(\S+)\b",
                "<a target=\"_blank\" href=\"$0\">$0</a>"));  // http link version}
      }
  }
  ```

- 对 GetChildContentAsync 的多次调用返回相同的值，且不重新执行 TagHelper 主体，除非传入一个指示不使用缓存结果的 false 参数。

### 补充：标记帮助程序的默认约定概述

- 创建标记帮助程序的类，推荐类名以TagHelper结尾。
- 在未使用HtmlTargetElement特性指定标记目标名称的情况下，约定使用不包含后缀“TagHelper”的类名的小写形式作为标记的目标名称。如果类名由多个首字母大写的单词组成，那么使用短横线分割各个单词的形式，作为标记的目标名称。例如：MyEmailTagHelper，就使用`<my-email>`作为标记的目标名称。同时，该规则也同样适用于标记帮助程序中的属性。
- 所有的标记帮助程序（包括内置的标记帮助程序）的属性，均解析的是C#代码。



## ASP.NET Core中内置的标记帮助程序

首先需要说明的是，标记帮助程序实质上是一个C#自定义的程序类，它可以以标签的形式使用，也可以作为标签属性被使用，不同标记帮助程序可以应用的HTML标签各不相同。由于内置的标记帮助程序和HTML元素标签大多数相同，因此应该首先采用HTML元素标签作为呈现方式。

### `<a>`

该标记最终会生成一个超链接标签（`<a href="..."></a>`）。

为了说明该标记帮助程序的详细用法，首先创建一个控制器：

```c#
public class Speaker
{
    public int SpeakerId { get; set; }
}

public class SpeakerController : Controller
{
    private List<Speaker> Speakers = new List<Speaker>
    {
        new Speaker{ SpeakerId=1 },
        new Speaker{ SpeakerId=2 },
        new Speaker{ SpeakerId=3 }
    };

    public IActionResult Index()
    {
        return View(Speakers);
    }

    [Route("/Speaker/Evaluations", Name = "speakerevals")]
    public IActionResult Evaluations()
    {
        return View();
    }

    [Route("/Speaker/EvaluationsCurrent", Name = "speakervalscurrent")]
    public IActionResult Evaluations(int speakerId, bool currentYear)
    {
        return View();
    }

    [Route("Speaker/{id:int}")]
    public IActionResult Detail(int id)
    {
        return View(Speakers.FirstOrDefault(a => a.SpeakerId == id));
    }

    [Route("Speaker")]
    public IActionResult Detail2(int speakerid)
    {
        return View(Speakers.FirstOrDefault(a => a.SpeakerId == speakerid));
    }
    
    public IActionResult Tag()
    {
        return View();
    }

}
```

系统提供的<a>标记帮助程序的可用属性均以asp-开头，其中以asp-page开头的属性，均只能适用于Razor Pages页面，而不是Razor视图。

**说明：`<a>`标记帮助程序的使用示例，都是作用在Home/Tag视图页面，对应的操作方法为Home控制器下的Tag方法。**

Home控制器的内容如下：

```c#
public class HomeController : Controller
{
    public IActionResult Tag()
    {
        return View();
    }

    public IActionResult Test()
    {
        return View();
    }
}
```

#### asp-controller

asp-controller属性指定用于生成URL的控制器。

```html
<a asp-controller="Speaker">只使用asp-controller示例</a>
```

上述代码没有指定asp-action属性，默认以当前视图对应的控制器操作方法为主，由于是在Home/Tag下访问的Razor视图页面，因此asp-action默认就以Home控制器下的Tag操作方法为主，最终生成的HTML片段内容如下：

```html
<a href="/Speaker/Tag">只使用asp-controller示例</a>
```

注意：Speaker控制器中一定要存在Tag操作方法，否则仍然无法得到上述结果。

#### asp-action

asp-action属性指定用于生成URL的控制器中的操作方法名称，该操作方法必须是真实存在的。

```html
<a asp-controller="Speaker" asp-action="Evaluations">asp-action的使用</a>
```

生成的HTML：

```html
<a href="/Speaker/Evaluations">asp-action的使用</a>
```

如果没有指定asp-controller属性，则默认以当前呈现视图的控制器为主。

```html
<a asp-action="Test">只使用asp-action的示例</a>
```

上述代码当前呈现的视图是Home/Tag，因此在不指定asp-controller属性的情况下，默认也以Home为主，最终生成的HTML如下：

```html
<a href="/Home/Test">只使用asp-action的示例</a>
```

**注意：最终应用的控制器操作方法一定要在控制器中真实的存在，否则不能正确解析。**

#### asp-route-{value}

该属性用于指定路由参数，如果在路由模板中找不到该指定的参数，就将该请求参数和值追加到生成的href属性，否则，将在路由模板中替换该值。

在Speaker控制器中存在下述的操作方法：

```c#
[Route("Speaker/{id:int}")]
public IActionResult Detail(int id)
{
    return View(Speakers.FirstOrDefault(a => a.SpeakerId == id));
}
```

若要正确的解析出路由模板中的id参数，需要为其设置asp-route-id属性值：

```html
<a asp-controller="Speaker" asp-action="Detail" asp-route-id="2">
    asp-route-{value}的使用</a>
```

生成的HTML也将依据指定的模板对href属性进行呈现：

```html
<a href="/Speaker/2">asp-route-{value}的使用</a>
```

如果路由模板中没有指定参数，或者没有使用Route特性指定路由模板，如果需要在控制器中接受Razor页面中指定的asp-route-{value}属性对应的参数值，例如：

```html
<a asp-controller="Speaker" asp-action="Detail2" asp-route-speakerid="2">
	asp-route-{value}的使用子自定义参数
</a>
```

要想在控制器中解析并获取speakerid参数的值，需要在控制器的操作方法中，定义对应的参数：

```c#
[Route("Speaker")]
public IActionResult Detail2(int speakerid)
{
    return View(Speakers.FirstOrDefault(a => a.SpeakerId == speakerid));
}
```

由于没有在路由模板中指定参数，因此最终生成的HTML如下：

```html
<a href="/Speaker?speakerid=2">asp-route-{value}的使用子自定义参数</a>
```

指定的asp-route-speakerid属性中的参数speakerid将以查询字符串的形式追加到url的末尾。

#### asp-route

asp-route用于创建直接链接到命名路由的URL。所谓命名路由指的是使用了Route特性并指定Name属性的路由。

```c#
[Route("/Speaker/Evaluations", Name = "speakerevals")]
public IActionResult Evaluations()
{
    return View();
}
```

在Razor页面，使用asp-route属性示例如下：

```html
<a asp-route="speakerevals">asp-route的使用</a>
```

上述的asp-route属性指定路面名称，生成的HTML如下，其中href属性值为路由模板对应的url：

```html
<a href="/Speaker/Evaluations">asp-route的使用</a>
```

需要注意的是，由于asp-controller或asp-action属性，都可以生成相应的路由，**因此为了避免路由冲突，不应该将asp-route与asp-controller或asp-action属性组合使用**，否则将不会生成预期的路由。

#### asp-all-route-data

asp-all-route-data属性支持创建键值对字典，键是参数名称，值时参数值，并将其以查询字符串的形式进行展示。

```html
@{
    var parms = new Dictionary<string, string>
    {
        { "speakerId", "11" },
        { "currentYear", "true" }
    };
}
<a asp-route="speakervalscurrent" asp-all-route-data="parms">asp-all-route-data的使用</a>
```

生成的HTML片段：

```html
<a href="/Speaker/EvaluationsCurrent?speakerId=11&amp;currentYear=true">
asp-all-route-data的使用
</a>
```

如果想要控制器正确的解析传入的参数，对应的操作方法定义如下：

```c#
[Route("/Speaker/EvaluationsCurrent", Name = "speakervalscurrent")]
public IActionResult Evaluations(int speakerId, bool currentYear)
{
    return View();
}
```

注意：实际开发中，应该以控制器定义的操作方法或路由的配置情况，去指定标记帮助程序的属性值，因为只有在满足指定的属性值对应的操作方法或路由真实存在时，才能够被正确的解析。

#### asp-fragment

asp-fragment用于指定目标Url的锚点项。

```html
<a asp-controller="Speaker" asp-action="Evaluations" asp-fragment="wy">
asp-fragment锚点的使用</a>
```

生成的HTML内容如下：

```html
<a href="/Speaker/Evaluations#wy">asp-fragment锚点的使用</a>
```

#### asp-area

asp-area属性用于设置路由的区域名称。一旦设置了asp-area属性值，那么在为asp-controller、asp-action、asp-page指定值时，都是基于area下的文件进行指定的。

注意：asp-area同时适用于Razor视图和Razor Pages页面，其中Razor Pages应用如果要支持区域，需要 将RazorPagesOptions.AllowAreas 属性设置为 true。代码如下：

```c#
services.AddMvc()
        .AddRazorPagesOptions(options => options.AllowAreas = true);
```

应用该属性的Razor Pages代码：

```html
<a asp-area="Sessions"
   asp-page="/Index">View Sessions</a>
```

同样，如果需要在Razor视图中（MVC应用）支持区域，需要对路由模板添加对该区域（如果存在该区域）的引用。在Startup.Configure()方法中进行配置：

```c#
app.UseMvc(routes =>
{
    // need route and attribute on controller: [Area("Blogs")]
    routes.MapRoute(name: "mvcAreaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}");

    // default route for non-areas
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});
```

Razor视图中的代码：

```html
<a asp-area="Blogs"
   asp-controller="Home"
   asp-action="AboutBlog">About Blog</a>
```

综上所述，一旦需要使用区域，就必须在Startup中进行相关的配置和设定。

#### asp-protocol

asp-protocol属性用于指定生成的URL的协议，比如https。

```html
<a asp-protocol="https" asp-controller="Home" asp-action="Index">asp-protocol的使用</a>
```

生成的HTML：

```html
<a href="https://localhost:1799/">asp-protocol的使用</a>
```

#### asp-host

asp-host用于指定生成的URL的主机名。

```html
<a asp-protocol="https"
       asp-host="microsoft.com"
       asp-controller="Home"
       asp-action="Test">asp-host的使用</a>
```

生成的HTML：

```html
<a href="https://microsoft.com/Home/Test">asp-host的使用</a>
```

注意：虽然可以使用asp-host指定主机名，但是仍然要确保asp-action指定的操作方法必须在当前环境真实存在，否则不能得到想要的结果。

#### asp-page

asp-page只适用于Razor Pages应用，不适用于Razor视图的MVC应用，Razor Pages页面一般都位于Pages文件夹下，因此在指定asp-page值时，都以正斜杠（”/"）作为前缀，表示从Pages文件夹下开始查找。

```html
<a asp-page="/Attendee">All Attendees</a>
```

可以在指定asp-page属性的同时，指定asp-route-{value}属性，用于传递路由参数。

```html
<a asp-page="/Attendee"
   asp-route-attendeeid="10">View Attendee</a>
```

生成的HTML结果：

```html
<a href="/Attendee?attendeeid=10">View Attendee</a>
```

#### asp-page-handler

asp-page-handler只能用于Razor Pages应用，用于指定特定的Page页面处理程序。常用于多个按钮对同一个页面进行后端程序处理时，需要以该属性来确定不同的处理程序类型。

例如，在Razor Pages页面中，存在下述方法（`On<Verb>`类型的方法，此处是OnGet）：

```c#
public void OnGetProfile(int attendeeId)
{
 	...
}
```

若要使链接能够触发上述方法，Razor页面代码如下：

```html
<a asp-page="/Attendee"
   asp-page-handler="Profile"
   asp-route-attendeeid="12">Attendee Profile</a>
```

注意：asp-page-handler属性设置的是处理程序类型，它的值要和`On<Verb>`类型的方法名一致（不包含`On<Verb>`前缀，如果方法是异步的，也不包括Async后缀）。

生成的HTML：

```html
<a href="/Attendee?attendeeid=12&handler=Profile">Attendee Profile</a>
```



### `<cache>`

缓存标记帮助程序通过将其内容缓存到内部 ASP.NET Core 缓存提供程序中，极大地提高了 ASP.NET Core 应用的性能。

cache标记帮助程序的简单应用如下：

```html
<cache>缓存时间：@DateTime.Now</cache>
```

对页面的第一次请求将会显示正确的时间，其他请求将对标记内的所有内容都将被缓存，直到缓存过期（默认20分钟）或因内存压力而逐出。

#### enabled

enabled属性用于指定`<cache>`标记包含的内容是否缓存。默认为true，如果设置为false，将不会缓存标记内的内容。

```html
<cache enabled="false">
    缓存无效: @DateTime.Now
</cache>
```

#### expires-on

expires-on属性用于为缓存项设置一个绝对到期日期。

```html
<p>
    <cache enabled="true" expires-on="@new DateTime(2019,4,10,13,13,0)">
        缓存到期时间：@(new DateTime(2019, 4, 10, 13, 13, 0)) ， 缓存时间: @DateTime.Now
    </cache>
</p>
```

一旦设置了expires-on属性值，缓存项只在指定的绝对日期之前缓存内容。

#### expires-after

expires-after属性用于指定每次缓存的时长，默认为20分钟。

```html
<p>
    <cache expires-after="@TimeSpan.FromSeconds(10)">
        每次缓存时长：10秒，@DateTime.Now
    </cache>
</p>
```

上述代码中，每次日期时间都会缓存10秒，即，频繁的刷新页面时，每隔10秒才能得到正确的时间值。

#### expires-sliding

expires-sliding属性用于设置缓存项在多长时间未被访问时自动被逐出。

```html
<cache expires-sliding="@TimeSpan.FromSeconds(5)">
    缓存逐出时长的使用，@DateTime.Now
</cache>
```

#### vary-by-header

vary-by-header属性可以设置在指定的标头值发生改变时，触发缓存的刷新。该属性值接受由逗号分隔的标头值列表对应的名称。

```html
<cache vary-by-header="User-Agent,content-encoding">
    vary-by-header示例：  @DateTime.Now
</cache>
```

关于标头值列表，可以通过浏览器F12中的Network下的Headers进行查看，上述代码指定了标头值属性为User-Agent，该属性在不同的浏览器中值不相同，一旦更换了浏览器，上述代码中的缓存项就会被刷新。

#### vary-by-query

vary-by-query属性可以根据查询字符串参数值的变化触发缓存的刷新，该属性接受查询字符串中的参数以逗号隔开的形式的字符串。

```html
<cache vary-by-query="wy,smallz">
    vary-by-query示例：@DateTime.Now
</cache>
```

当第一次使用`http://localhost:1799/Tag?wy=2`进行访问时，得到正确的时间。在不改变wy参数的值的情况下，刷新页面并不能触发时间的刷新，如果将wy的值改为其他值，就可以刷新时间了。

#### vary-by-route

vary-by-route和vary-by-query类似，只不过是基于路由参数的变化触发缓存的刷新。该属性值支持由逗号隔开的多个路由参数的字符串。

在控制器中，定义一个包含路由参数的控制器操作方法：

```c#
[Route("Tag/{wy?}/{smallz?}")]
public IActionResult Tag()
{
    return View();
}
```

Razor页面使用：

```html
<cache vary-by-route="wy,smallz">
    vary-by-route示例：@DateTime.Now
</cache>
```

#### vary-by-cookie

vary-by-cookie属性可以根据指定的Cookie值发生变化时触发缓存的刷新，该属性值支持由逗号隔开的多个Cookie名称的字符串。

```html
<cache vary-by-cookie=".AspNetCore.Identity.Application,HairColor">
    Current Time Inside Cache Tag Helper: @DateTime.Now
</cache>
```

上述代码中，.AspNetCore.Identity.Application是Cookie的名称，该Cookie用于身份验证，一旦标识发生更改就会刷新缓存。

#### vary-by-user

vary-by-user属性接受的是Boolean类型的值，要么为tue，要么为false，默认为true。用于指定当已登录用户（或上下文主体）发生更改时是否应重置缓存。 当前用户也称为请求上下文主体，可通过引用 @User.Identity.Name 在 Razor 视图中查看。

```c#
<cache vary-by-user="true">
    vary-by-user示例：@DateTime.Now
</cache>
```

> 通过登录和注销周期，使用此属性将内容维护在缓存中。 当值设置为 true 时，身份验证周期会使已经过身份验证的用户的缓存失效。 缓存无效是因为用户进行身份验证时生成了一个新的唯一 cookie 值。 如果 cookie 不存在或已过期，则会维持缓存以呈现匿名状态。 如果用户未经过身份验证，则会维持缓存。

#### vary-by

vary-by用于指定的值发生改变时触发缓存的刷新。通常会将要追踪的值作为Model的属性进行关联，最终分配给vary-by属性。

例如，Razor页面需要根据控制器返回的结算结果刷新缓存，在控制器方法中：

```c#
public IActionResult Index(string myParam1, string myParam2, string myParam3)
{
    int num1;
    int num2;
    int.TryParse(myParam1, out num1);
    int.TryParse(myParam2, out num2);
    return View(viewName, num1 + num2);
}
```

Razor视图：

```html
<cache vary-by="@Model">
       vary-by示例：@DateTime.Now
</cache>
```

上述代码，只要后台计算的和值发生了改变，就会刷新缓存的内容。

#### priority

priority属性用于设置缓存被逐出时的优先等级。在内存压力下，会首先逐出Low缓存项。该属性值来自于CacheItemPriority，分别为High, Low, NeverRemove, Normal。

注意：priority属性并不能保证特定级别的缓存保留，即使值设置为CacheItemPriority.NeverRemove，也不能保证缓存项将始终保留。 

```html
<cache priority="@Microsoft.Extensions.Caching.Memory.CacheItemPriority.High">
    Current Time Inside Cache Tag Helper: @DateTime.Now
</cache>
```



### `<distributed-cache>`

分布式缓存标记帮助程序这里不做过多的介绍，请求请参阅：https://docs.microsoft.com/zh-cn/aspnet/core/mvc/views/tag-helpers/built-in/distributed-cache-tag-helper



### `<environment>`

环境标记帮助程序根据当前宿主环境，有条件的显示其包含的内容。

#### names

names属性用于指定环境名称，只有在指定的名称下的环境，才显示该标记包含的内容。值可以是单个宿主环境名称或由逗号隔开的多个环境名称的字符串。

系统会将设置的环境名称值和IHostingEnvironment.EnvironmentName 返回的当前值进行比较（不区分大小写）。

```html
<environment names="Staging,Production">
    <strong>HostingEnvironment.EnvironmentName is Staging or Production</strong>
</environment>
```

上述代码如果宿主环境是Staging或Production，就显示标记中的内容。

#### include

include属性表现出和names属性相似的行为。include属性值中列出的环境**必须包括**应用程序的托管环境(IHostingEnvironment.EnvironmentName) 才能呈现 `<environment> `标记的内容。

```html
<environment include="Staging,Production">
    <strong>HostingEnvironment.EnvironmentName is Staging or Production</strong>
</environment>
```

#### exclude

exclude与include相反，当exclude属性值中列中的环境**不包括**托管环境时，才呈现 `<environment> `标记的内容。



### `<form>`

表单标记帮助程序可以为MVC控制器操作或命名路由生成对应的HTML元素`<form>`标签的action属性值。同时，ASP.NET Core中的表单标记程序还会为最终的表单生成隐藏的请求验证令牌（Token），可以在HTTP Post操作方法中与[ValidateAntiForgeryToken] 属性配合使用，防止跨站点请求伪造。

表单标记帮助程序的常用属性有：

- asp-controller：用于生成action（此处的action指的是HTML元素`<form>`的action属性）值的控制器名称。
- asp-action：用于生成action值的控制器操作方法名。
- asp-route：通过命名路由生成action的值。
- asp-route-{Parameter Name}：其中的参数名称会添加到路由生成的URL中。
- 其他

```html
<form asp-controller="Home" asp-action="Save" asp-route-wy="sdf" method="post">
    <input type="submit" value="Save" />
</form>
```

上述代码生成的HTML内容如下：

```html
<form method="post" action="/Home/Save?wy=sdf">
        <input type="submit" value="Save">
    <input name="__RequestVerificationToken" type="hidden" value="CfDJ8Obm5Kf2GbNHqG2AFpMzfTfvMUyfMEwujfOOCkjVefVRrgPauENhyA-QRNGGCXNolpj9AynlT5-jx5ispJm8mtTHmxNcDqV9GdvZTVB6idRKv9tfOw7-rDnUROzMJX_7BPGdvaq9Zq4b6kJ7EdUP22c">
</form>
```

也可以通过asp-route属性指定命名路由，为action生成标记。

注：可以代替表单标记帮助程序的HTML帮助程序项为Html.BeginForm和Html.BeginRouteForm，它的参数提供了类似asp-route-{Parameter Name}属性的功能。



### 表单操作标记帮助程序（`<button>`或 `<input type="image">`）

这里的表单操作标记帮助程序指的是，最终在HTML标签元素上生成formaction属性的标记，formaction属性可以控制表单提交数据的目标URL，并且HTML的formaction 属性能够覆盖 `<form> `元素的 action 属性。

表单操作标记帮助程序主要有`<button>`和`<input type="image">`标记。

注意：`<input type="button">`不属于表单标记帮助程序。例如：`<input type="button" value="ceshi" asp-action="Index" asp-controller="Home"/>`，生成的最终HTML内容为：`<input type="button" value="ceshi" asp-action="Index" asp-controller="Home">`，这里指定的asp-action没有转化为最终要生成的formaction属性。

用于控制HTML的formaction属性值的标记帮助程序属性有：

- asp-controller：控制器的名称。
- asp-action：操作方法的名称。
- asp-area：区域名称。
- asp-page：Razor Page 的名称。
- asp-page-handler：Razor Page 处理程序的名称。
- asp-route：路由的名称。
- asp-route-{value}：单个 URL 路由值。 例如 asp-route-id="1234"。
- asp-all-route-data：所有路由值。
- asp-fragment：URL 片段。

示例一，提交到控制器：

```html
<button asp-controller="Home" asp-action="Test">Click Me</button>
<input type="image" src="" alt="Or Click Me" asp-controller="Home" asp-action="Test" />
```

示例一生成的HTML：

```html
<button formaction="/Home/Test">Click Me</button>
<input type="image" src="" alt="Or Click Me" formaction="/Home/Test">
```

示例二，提交到路由：

```c#
[Route("/Home/Test2",Name ="Custom")]
public string Test2(){
    return "Test2";
}
<button asp-route="Custom">Click Me</button>
<input type="image" src="" alt="Or Click me" asp-route="Custom" />
```

示例二生成的HTML：

```html
<button formaction="/Home/Test2">Click Me</button>
<input type="image" src="" alt="Or Click me" formaction="/Home/Test2">
```



### `<img>`

图像标记帮助程序为静态图像文件提供缓存破坏行为。触发破坏行为的是一组附加到图像资源URL的哈希值，它是一个唯一的字符串，会提示客户端（和某些代理）从主机 Web 服务器重新加载图像，而不是从客户端的缓存重新加载。

#### src

src属性指向图像文件对应的服务器上的物理静态文件。如果 src 是一个远程 URI，则不会生成缓存破坏查询字符串参数。

#### asp-append-version

如果asp-append-version值设为true，则会调用图像标记帮助程序，前提是也指定了src值。

```html
<img src="~/images/asplogo.png" asp-append-version="true">
```

如果目录 /wwwroot/images/ 中存在静态文件，则生成的 html 与下面类似（哈希有所不同）：

```html
<img src="/images/asplogo.png?v=Kl_dqr9NVtnMdsM2MUg4qthUnWZm5T1fCEimBPWDNgM">
```

分配给参数 v 的值是磁盘上的 asplogo.png 文件的哈希值。 如果 Web 服务器无法获取对静态文件的读取访问权限，则不会向呈现在标记中的 src 属性添加 v 参数。

#### 哈希缓存行为

> 图像标记帮助程序使用本地 Web 服务器上的缓存提供程序来存储给定文件的已计算 Sha512 哈希。 如果多次请求文件，则不重新计算哈希值。 当计算该文件的 Sha512 哈希时，附加到该文件的文件观察程序会让 Cache 失效。 当磁盘上的的文件发生更改时，将会计算和缓存新的哈希。

### `<input>`

输入标记帮助程序用于生成HTML中的`<input>`标签。

HTML中的<input>标签通常有id、name、type等属性。

#### HTML元素的id和name属性生成规则

其中id和name属性值可以通过输入标记帮助程序的asp-for属性进行指定，最终将生成指定表达式名称的id和name属性值，asp-for属性通常指定的是Model属性，Model属性可以是其它类的引用，这样asp-for解析的就是子属性，子属性生成的结果是由下划线隔开的字符串。见下文示例。

#### HTML元素的type属性生成规则

而type属性，由asp-for指定的Model数据类型和应用于Model属性的数据注解特性来生成type属性值。如果直接指定了type属性值，将以指定的值为主。

下面是常见的Model数据类型生成的HTML的type属性类型：

| .NET 类型                 | input类型             |
| ------------------------- | --------------------- |
| Bool                      | type="checkbox"       |
| String                    | type="text"           |
| DateTime                  | type="datetime-local" |
| Byte、Int、Single、Double | type="number"         |

下表是常见的数据注解特性生成的HTML的type属性类型：

| 特性                          | input类型       |
| ----------------------------- | --------------- |
| [EmailAddress]                | type="email"    |
| [Url]                         | type="url"      |
| [HiddenInput]                 | type="hidden"   |
| [Phone]                       | type="tel"      |
| [DataType(DataType.Password)] | type="password" |
| [DataType(DataType.Date)]     | type="date"     |
| [DataType(DataType.Time)]     | type="time"     |

除了id、name、type属性外，指定的Model属性的数据注解特性还会生成HTML验证相关的属性。

假如存在下述数据模型：

```c#
public class TagViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name ="Email Address")]
    public string Email{ get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password{ get; set; }
    
    //其它类的引用
    public AddressViewModel Address{ get; set; }
}

public class AddressViewModel
{
    public string AddressLine{ get; set; }
}
```

在Razor页面中，使用上述模型和输入标记帮助程序：

```html
<form asp-controller="Home" asp-action="Save" method="post">
    Email:<input asp-for="Email"/>
    Password:<input asp-for="Password" />
    Address:<input asp-for="Address.AddressLine"/>
    <button type="submit">Register</button>
</form>
```

运行程序生成的HTML内容如下：

```html
<form method="post" action="/Home/Save">
            Email:<input type="email" data-val="true" data-val-email="The Email Address field is not a valid e-mail address." data-val-required="The Email Address field is required." id="Email" name="Email" value="">
            Password:<input type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password">
            Address:<input type="text" id="Address_AddressLine" name="Address.AddressLine" value="">
            <button type="submit">Register</button>
        <input name="__RequestVerificationToken" type="hidden" value="CfDJ8Obm5Kf2GbNHqG2AFpMzfTcLQTClOXt1ranBrrZSnPNhEQKgR-P8Hn63CRVS9FC8O892xzfBLSKOBmYLuVEB9Wl0Y5j6uhcf3rtN79QrCFJv1nW9IZCipThaPbdlvKbiOONXZS2Dhf45dn9hvt9v4yk">
</form>
```

通过生成的HTML内容可以看到，除了id和name属性之外，其他的大多数属性都是根据模型属性的注解特性生成的。其中data-val-{par}属性是一种非介入式的HTML5属性（均以data-开头），属性中的{par}对应模型属性的注解特性验证规则名称，如`data-val-required`、`data-val-email`、`data-val-maxlength` 等。

> 如果在属性中提供错误消息，则该错误消息会作为 data-val-rule 属性的值显示。 还有表单 data-val-ruleName-argumentName="argumentValue" 的属性，这些属性提供有关规则的其他详细信息，例如，data-val-maxlength-max="1024"。

另外一点需要注意的是Address引用的是子属性，解析后的id和name属性值并不相同，id值为Address_AddressLine，而name值为Address.AddressLine。

#### 替代输入标记帮助程序的HTML帮助程序项

可以替代输入标记帮助程序的HTML帮助程序项有Html.TextBox、Html.TextBoxFor、Html.Editor和Html.EditorFor。它们与标记帮助程序的主要区别有以下几点：

- 输入标记帮助程序会自动设置 type 属性；而 Html.TextBox 和 Html.TextBoxFor 不会。
- Html.Editor 和 Html.EditorFor 可以处理集合、复杂对象和模板；而输入标记帮助程序不会。 
- 输入标记帮助程序、Html.EditorFor 和 Html.TextBoxFor 是强类型（使用 lambda 表达式）；而 Html.TextBox 和 Html.Editor 不是（使用表达式名称）。

##### HTML帮助程序的HtmlAttributes

> @Html.Editor() 和 @Html.EditorFor() 在执行其默认模板时使用名为 htmlAttributes 的特殊 ViewDataDictionary 条目。 此行为可选择使用 additionalViewData 参数增强。 键“htmlAttributes”区分大小写。 键“htmlAttributes”的处理方式与传递到输入帮助程序的 htmlAttributes 对象（例如 @Html.TextBox()）的处理方式类似。

```c#
@Html.EditorFor(model => model.YourProperty, 
  new { htmlAttributes = new { @class="myCssClass", style="Width:100px" } })
```

#### asp-for属性指定复杂表达式

asp-for属性值是模型表达式，一般是lambda表达式的右边部分。例如，asp-for="wy"在生成的代码中会变成m=>m.wy，所以asp-for属性值不需要指定Model前缀。如果需要使用Model前缀才能应用属性值，需要使用“@”字符作为内联表达式的开头。

```c#
@{ 
    var wy = "smallz";
}
<input asp-for="@wy"/>
```

生成的HTML：

```html
<input type="text" id="wy" name="wy" value="smallz">
```

对于集合的显示，假如在TagViewModel中存在List<string>集合属性Colors，下面会产生相同的输出结果：

```html
@model TagViewModel

@Html.EditorFor(m=>m.Colors[1])
<input asp-for="@Model.Colors[1]"/>
<input asp-for="Colors[1]"/>
```

上述三种形式生成的HTML如下：

```html
<input class="text-box single-line" id="Colors_1_" name="Colors[1]" type="text" value="Blue">
<input type="text" id="Colors_1_" name="Colors[1]" value="Blue">
<input type="text" id="Colors_1_" name="Colors[1]" value="Blue">
```

可以看到，生成的结果都一样。



### `<label>`

label标记帮助程序主要用来生成HTML的label标签描述和for属性。

它最大的特点是：可以根据模型属性的Display特性，生成对应的描述信息。

```c#
[Required]
[EmailAddress]
[Display(Name ="Email Address")]
public string Email{ get; set; }
```

上述属性使用了Display特性，并且名称指定为“Email Address”，在Razor页面中，使用下述代码：

```php+HTML
<label asp-for="Email"></label>
<input asp-for="Email" />
```

生成的HTML：

```html
<label for="Email">Email Address</label>
<input type="email" data-val="true" data-val-email="The Email Address field is not a valid e-mail address." data-val-required="The Email Address field is required." id="Email" name="Email" value="wy@163.com">
```

HTML 帮助程序替代项：Html.LabelFor。

### `<partial>`

Partial 标记帮助程序用于在 Razor 页面和 MVC 应用中呈现分部视图。

用于呈现分部视图的 HTML 帮助程序选项包括：

- @await Html.PartialAsync
- @await Html.RenderPartialAsync
- @Html.Partial
- @Html.RenderPartial

#### name

name属性指定要呈现的分部视图的名称或路径。 提供分部视图名称时，会启动视图发现进程。 提供显式路径时，将绕过该进程。

以下标记使用显式路径，指示要从共享文件夹加载 _ProductPartial.cshtml。 使用 for 属性，将模型传递给分部视图进行绑定。

```html
<partial name="Shared/_ProductPartial.cshtml" for="Product" />
```

#### for

for属性指定要绑定的视图模型对应的模型表达式，模型表达推断@Model. 语法。 例如，可使用 for="Product" 而非 for="@Model.Product"。 通过使用 @ 符号定义内联表达式来替代此默认推理行为。

以下标记加载 _ProductPartial.cshtml：

```html
<partial name="_ProductPartial" for="Product" />
```

for属性在PageModel中，关联的是一个PageModel属性，而在MVC中，关联的是ViewModel的一个属性。

#### model

model 属性分配模型实例，以传递到分部视图。 注意：model 属性不能与 for 属性一起使用。

```html
<partial name="_ProductPartial"
         model='new Product { Number = 1, Name = "Test product", Description = "This is a test" }' />
```

#### view-data

view-data 属性分配 ViewDataDictionary，以传递到分部视图。 以下标记使整个 ViewData 集合可访问分部视图：

```html
@{
    ViewData["IsNumberReadOnly"] = true;
}

<partial name="_ProductViewDataPartial"
         for="Product"
         view-data="ViewData" />
```

view-data主要用于向分部视图传递数据。

### `<select>`

select标记帮助程序主要用于生成HTML的`<select>`和`<option>`元素。

为了使用select标记帮助程序，通常需要为ViewModel类定义两个属性，一个为多个SelectListItem组成的集合，例如`List<SelectListItem>`，另一个属性为该select标签选择的值。

对上文中的TagViewModel进行扩展，添加下述两个属性：

```c#
public class TagViewModel
{
	...
	
    public string Country{ get; set; }
    public List<SelectListItem> Countries{ get; set; }
}
```

在控制器中，为上述两个属性赋值：

```c#
public IActionResult Tag()
{
    TagViewModel tagmodel = new TagViewModel
    {
         ...
         
         Countries=new List<SelectListItem>{
         new SelectListItem{ Value="1", Text="One"},
         new SelectListItem{ Value="2", Text="Two"},
         new SelectListItem{ Value="3", Text="Three"}
         },
         Country="3"
    };
    return View(tagmodel);
}
```

在Razor页面中使用该标记帮助程序：

```html
<select asp-for="Country" asp-items="Model.Countries"></select>
```

这里需要注意的是，asp-for指定的模型属性是最终能够得到选择项的值的属性，而asp-items指定的是option元素生成所依赖的SelectListItem类型的集合对应的模型属性。另外，asp-for 属性值是特殊情况，它不要求提供 Model 前缀，但其他标记帮助程序属性需要该前缀（例如 asp-items）。

最终生成的HTML如下：

```html
<select id="Country" name="Country"><option value="1">One</option>
<option value="2">Two</option>
<option selected="selected" value="3">Three</option>
</select>
```

由于asp-for指定的Country在控制器中被赋值为3，因此option第3项是选中状态。

注意：选择标记帮助程序应与ViewModel结合使用，而不是将 ViewBag 或 ViewData 与选择标记帮助程序配合使用，因为视图模型在提供 MVC 元数据方面更可靠且通常更不容易出现问题。

注：具有 HTML 帮助程序替代项 Html.DropDownListFor 和 Html.ListBoxFor。

#### 下拉框的枚举绑定

可以直接为下拉框绑定枚举类型对应的成员。

假如存在下述枚举类型：

```c#
public enum CountryEnum
{
    [Display(Name ="th1")]
    One,
    [Display(Name = "th2")]
    Two,
    Three
}
```

为了使下拉框能够绑定选择的值，需要为ViewModel指定一个枚举类型的属性，虽然也可以指定为string类型，但是推荐定义该枚举类型的属性：

```c#
public class TagViewModel
{
	...
	public CountryEnum EnumCountry { get; set; }
}
```

如果需要下拉框默认选择某一项，需要在控制器中为上述的枚举属性赋值：

```c#
public IActionResult Tag()
{
    TagViewModel tagmodel = new TagViewModel
    {
        ...
        //默认选择值为CountryEnum.Two
        EnumCountry=CountryEnum.Two
    };
    return View(tagmodel);
}
```

在Razor页面绑定该枚举类型下的所有项：

```html
<select asp-for="EnumCountry" asp-items="Html.GetEnumSelectList<CountryEnum>()"></select>
```

上述代码中的asp-for指定的是最终提交的值要绑定的属性，asp-items使用了HTML帮助程序提供的方法。枚举定义时，各个枚举项使用了Display特性，可以用来设置最终展示的内容。

上述生成的HTML如下：

```c#
<select data-val="true" data-val-required="The EnumCountry field is required." id="EnumCountry" name="EnumCountry">
	<option value="0">th1</option>
	<option selected="selected" value="1">th2</option>
	<option value="2">Three</option>
</select>
```

#### 选项组

对下拉框的选项进行分组，通常生成的是HTML的`<optgroup>`元素。

关于选项组的使用并不会牵扯到标记帮助程序，只是在实例化每个SelectListItem对象时，需要为Group属性指定SelectListGroup类型的值。

```c#
public class CountryViewModelGroup
{
    public CountryViewModelGroup()
    {
        var NorthAmericaGroup = new SelectListGroup { Name = "North America" };
        var EuropeGroup = new SelectListGroup { Name = "Europe" };

        Countries = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "MEX",
                Text = "Mexico",
                Group = NorthAmericaGroup
            },
            new SelectListItem
            {
                Value = "CAN",
                Text = "Canada",
                Group = NorthAmericaGroup
            },
            new SelectListItem
            {
                Value = "FR",
                Text = "France",
                Group = EuropeGroup
            },
            new SelectListItem
            {
                Value = "ES",
                Text = "Spain",
                Group = EuropeGroup
            }
      };
    }

    public string Country { get; set; }
    public List<SelectListItem> Countries { get; }
}
```

#### 下拉框的多项选择

如果 asp-for 属性中指定的属性为 IEnumerable，选择标记帮助程序会自动生成 multiple = "multiple" 属性。

```c#
public IEnumerable<string> CountryCodes { get; set; }
public List<SelectListItem> Countries { get; } = new List<SelectListItem>
{
	new SelectListItem { Value = "MX", Text = "Mexico" },
	new SelectListItem { Value = "CA", Text = "Canada" },
	new SelectListItem { Value = "US", Text = "USA"    }
};
```

Razor页面：

```html
<select asp-for="CountryCodes" asp-items="Model.Countries"></select> 
```

生成的HTML：

```html
<select id="CountryCodes"
    multiple="multiple"
    name="CountryCodes">
    <option value="MX">Mexico</option>
	<option value="CA">Canada</option>
	<option value="US">USA</option>
</select>
```



### `<textarea>`

文本区域标记帮助程序类似于输入标记帮助程序，模型属性的注解特性会影响最终生成的元素属性值。

```c#
[MinLength(5)]
[MaxLength(1024)]
public string Description{ get; set; }
```

Razor页面使用该属性：

```html
<textarea asp-for="Description"></textarea>
```

生成的HTML：

```html
<textarea data-val="true" data-val-maxlength="The field Description must be a string or array type with a maximum length of '1024'." data-val-maxlength-max="1024" data-val-minlength="The field Description must be a string or array type with a minimum length of '5'." data-val-minlength-min="5" id="Description" maxlength="1024" name="Description"></textarea>
```

可以替换文本区域标记程序的HTML帮助程序项为：Html.TextAreaFor。

### 验证标记帮助程序

跟上文介绍的标记不同，这类标记帮助程序最终以HTML元素的属性形式被应用。提供了两个属性：验证消息标记帮助程序（asp-validation-for）和验证摘要标记帮助程序（asp-validation-summary）。

关于这两个标记的使用，这里不做详细介绍。

#### 验证消息标记帮助程序

参阅：https://docs.microsoft.com/zh-cn/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-2.2#the-validation-message-tag-helper

#### 验证摘要标记帮助程序

参阅：https://docs.microsoft.com/zh-cn/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-2.2#the-validation-summary-tag-helper





## 标记帮助程序组件

标记帮助程序组件最主要的功能就是，对于应用了标记帮助程序的Razor页面，最终呈现的HTML元素，可以通过组件有条件地修改或添加标记帮助程序的服务器端代码。例如创建了一个标记帮助程序为MyTag，如果想要Razor页面中使用的每个<y-tag>标记，最终生成的HTML元素中，根据条件的不同而包含其他内容，就需要借助标记帮助程序组件进行实现。

ASP.NET Core包含两个内置标记帮助程序组件元素：head和body，这两个组件分别作用于head元素和body元素。注意：这里说的内置并不是真实存在的组件，而是在定义组件时，TagHelperContext的TagName只能获取到head和body，仍然需要创建组件才能影响这两个元素的行为。具体见下文描述。

### 注入到HTML的head元素中的组件的使用

如果想要每个页面的head元素中包含指定的内容，如<link>或<script>，就可以使用组件进行实现。

扩展标记包含的内容的类需要派生自TagHelperComponent类，TagHelperComponent类的定义如下：

```c#
public abstract class TagHelperComponent : Microsoft.AspNetCore.Razor.TagHelpers.ITagHelperComponent
{
	protected TagHelperComponent();
	public virtual int Order { get; }

	public virtual void Init(TagHelperContext context);
	public virtual void Process(TagHelperContext context, TagHelperOutput output);
	public virtual Task ProcessAsync(TagHelperContext context, TagHelperOutput output);
}
```

创建一个WyStyleTagHelperComponent类，继承TagHelperComponent类，并重写其中的Order和ProcessAsync方法：

```c#
public class WyStyleTagHelperComponent : TagHelperComponent
{
    private readonly string _style = @"<link ref=""stylesheet"" href=""/css/address.css""";
    public override int Order => 1;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
        {
            output.PostContent.AppendHtml(_style);
        }
        return Task.CompletedTask;
    }
}
```

注：关于该组件的调用，请参阅下文中的”注册组件“部分。

### 注入到HTML的body元素中的组件的使用

和基于head元素的组件使用类似，通过body标记帮助程序组件可以将<script>或其他元素添加每个<body>元素中。这种类型的也需要继承TagHelperComponent类。

如下所示：

```c#
public class WyScriptTagHelperComponent : TagHelperComponent
{
    public override int Order => 2;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
        {
            var script = await File.ReadAllTextAsync("Views/wy.html");
            output.PostContent.AppendHtml(script);
        }
    }
}
```

这里需要注意的是context.TagName默认只能获取到head和body值，这就是上文说的内置的两个标记帮助程序组件（实际并不是真正的组件）。

上述代码读取了Views/wy.html文件的内容，其内容如下：

```js
<script>
    console.log('Hello ?')
</script>
```

注：关于该组件的调用，请参阅下文中的”注册组件“部分。

### 创建基于其他标记帮助程序的组件

上文介绍的两个组件只能基于head和body，因为重写的ProcessAsync方法在执行时，如果添加断点进行追踪，可以看到context.TagName的值只能是head或body。如果想要基于其他标记名称扩展相应的内容，就需要创建基于其他标记帮助程序的组件。

首先，创建派生自TagHelperComponentTagHelper类的子类，用于定义可供支持的标记帮助程序。TagHelperComponentTagHelper类的定义如下：

```c#
public abstract class TagHelperComponentTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
```

可以看到TagHelperComponentTagHelper类派生自TagHelper，而TagHelper又实现了ITagHelperComponent接口，而上文中的TagHelperComponent也实现了ITagHelperComponent接口，因此TagHelperComponentTagHelper实质上就是标记帮助程序，它和TagHelperComponent一样，都是ITagHelperComponent接口的衍生类。

创建派生自TagHelperComponentTagHelper类的标记帮助程序类如下：

```c#
[HtmlTargetElement("address")]
[EditorBrowsable(EditorBrowsableState.Never)]
public class AddressTagHelperComponentTagHelper : TagHelperComponentTagHelper
{
    public AddressTagHelperComponentTagHelper(
        ITagHelperComponentManager componentManager,
        ILoggerFactory loggerFactory) : base(componentManager, loggerFactory)
    {
    }
}
```

上述代码中，指定标记名称为address，将 [EditorBrowsable(EditorBrowsableState.Never)] 属性应用于该类的作用是，禁止在 IntelliSense 中显示该类型的提示信息。该特性可选，如果不想在代码智能提示中显示定义的内容，都可以使用该特性。

接着，创建派生自TagHelperComponent类的子类，用于扩展标记包含的内容。

```c#
public class AddressTagHelperComponent : TagHelperComponent
{
    private readonly string _markup;

    public override int Order { get; }

    public AddressTagHelperComponent(string markup = "", int order = 1)
    {
        _markup = markup;
        Order = order;
    }

    public override async Task ProcessAsync(TagHelperContext context,
                                            TagHelperOutput output)
    {
        if (string.Equals(context.TagName, "address",
                StringComparison.OrdinalIgnoreCase) &&
            output.Attributes.ContainsName("printable"))
        {
            TagHelperContent childContent = await output.GetChildContentAsync();
            string content = childContent.GetContent();
            output.Content.SetHtmlContent(
                $"<div>{content}<br>{_markup}</div>");
        }
    }
}
```

需要注意的是，重写的ProcessAsync方法中，通过context.TagName获取的值需要和address比较，若要使其成立，必须要将自定义的address标记帮助程序使用@addTagHelper指令添加到Razor页面中。具体的使用见下文中的“注册组件”部分。

上述代码中，只有标签为address并且包含printable属性，if判断才会成立。

### 注册组件

定义好了组件之后，只有注册后才能够使用，有以下三种方式：

- 通过服务容器注册
- 通过Razor文件注册
- 通过页面模型或控制器注册

#### 通过服务容器注册

在Startup.ConfigureServices()方法中，通过服务容器注册上述中的中WyScriptTagHelperComponent和WyStyleTagHelperComponent类：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    services.AddTransient<ITagHelperComponent, WyScriptTagHelperComponent>();
    services.AddTransient<ITagHelperComponent, WyStyleTagHelperComponent>();
}
```

注册完成之后，直接运行程序，会发现所有的页面的`<head>`标签下都包含下述内容：

```html
<link ref="stylesheet" href="/css/address.css" <="" head="">
```

并且在`<body>`标签的末尾，都包含上述指定的js内容：

```html
<script>
    console.log('Hello ?')
</script>
```

#### 通过Razor文件注册

如果未向 DI 注册标记帮助程序组件，则可以从 Razor Pages 页或 MVC 视图注册。 此项技术用于控制注入的标记和 Razor 文件中的组件执行顺序。向Razor文件注册，需要使用ITagHelperComponentManager，ITagHelperComponentManager用于添加标记帮助程序组件或从应用中删除这些组件。

```html
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *,My.TagHelpers.Study

@using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
@using My.TagHelpers.Study.Component;

@inject ITagHelperComponentManager manager;

@{
    manager.Components.Add(new AddressTagHelperComponent("<h1>ccccccccc</h1>", 1));
}

<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>ASP.NET Core标记帮助程序组件</title>
</head>
<body>
    <address printable>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>
</body>
</html>
```

#### 通过PageModel或控制器注册

对于Razor Pages应用可以通过PageModel进行注册，而对于MVC应用，一般可以通过控制器进行注册。无论哪种应用，都是基于ITagHelperComponentManager实现组件的添加的。PageModel注册示例见[官方介绍](<https://docs.microsoft.com/zh-cn/aspnet/core/mvc/views/tag-helpers/th-components?view=aspnetcore-2.2#registration-via-page-model-or-controller>)。

这里以向控制器注册的方式进行介绍：

```c#
public class HomeController : Controller
{
    private readonly ITagHelperComponentManager _tagHelperComponentManager;

    public HomeController(ITagHelperComponentManager tagHelperComponentManager)
    {
        _tagHelperComponentManager = tagHelperComponentManager;
    }
    public IActionResult Component2()
    {
        _tagHelperComponentManager.Components.
           Add(new AddressTagHelperComponent("<h1>AAAAAAAAA</h1>", 1));
        return View();
    }
}
```

对应的视图页面需要使用@addTagHelper指令添加对应的标记：

```html
@addTagHelper *,My.TagHelpers.Study

@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Component2</title>
</head>
<body>
    <address printable>
        .net core
    </address>
</body>
</html>
```

运行程序访问该视图，可以看到替换后的结果。