# ASP.NET Core中的布局

本文主要介绍以下三个文件的使用：

- _Layout.cshtml：用于通用布局。
- _ViewImports.cshtml：用于多个Razor页面（或试图）共享指令。
- _ViewStart.cshtml：用于在呈现页面或试图之前运行通用代码。



## _Layout.cshtml

_Layout.cshtml用于应用中的各个页面布局，使每个页面保持一致的外观体验。

按照约定，ASP.NET Core应用的默认布局名为_Layout.cshtml，通常位于Views（或Pages）目录下的Shared文件夹下。

ASP.NEt Core应用可以定义多个布局，并且不同的视图可以指定不同的布局。

### RenderBody()

默认情况下，每个布局页面都必须调用RenderBody()方法，用于呈现应用了该布局的内容视图。

```html
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
</head>
<body>
    <h1>Layout</h1>
    <div>
        @RenderBody()
    </div>
</body>
</html>
```

### RenderSection()

RenderBody()方法是为了给整个内容视图的呈现预留位置，而针对每个视图不同的差异需要呈现的内容，需要借助RenderSection()方法，它提供了更大的灵活性。可以在布局中，指定每个视图应用该Section是必需的还是可选的。

```html
<body>
    <h1>Layout</h1>
    <div>
        @RenderBody()
    </div>

    <footer>
    @RenderSection("myfooter",required:true)
    </footer>
</body>
```

如果RenderSection()方法的required参数值为true，意味着应用该布局的所有内容视图都必须提供相应的myfooter的section，如果内容视图没有找到所需的section，就会引发异常。因此实际使用中，建议将required参数值设为false。

### 应用布局

Razor视图中，可以使用Layout属性指定要应用的布局。

指定的布局可以使用完整路径（推荐）或部分名称（如：_Layout），如果提供的是部分名称，Razor视图引擎会使用标准发现过程来搜索布局文件，首先会搜索处理程序方法（或控制器）所在的文件夹，然后搜索Shared文件夹。

```c#
@{
    ViewData["Title"] = "Index";
    Layout = "~/Views/_Layout.cshtml";
}

<p>Index</p>
@section myfooter{ 
    <b>hello section!</b>
    <partial name="_ValidationScriptsPartial" />
}
```

如果布局页中使用了RenderSection()方法，那么在内容视图中，就需要使用@section 指定相应的section内容，如上述代码所示，同时也可以指定部分视图。

### 忽略Section

>默认情况下，必须由布局页面呈现内容页中的正文和所有节。 Razor 视图引擎通过跟踪是否已呈现正文和每个节来强制执行此操作。
>要让视图引擎忽略正文或节，请调用 IgnoreBody 和 IgnoreSection 方法。
>必须呈现或忽略 Razor 页面中的正文和每个节。



## _ViewImports.cshtml

`_ViewImports.cshtml`文件可以指定多个视图共享的指令。`_ViewImports`文件支持的指令如下：

- @addTagHelper
- @removeTagHelper
- @tagHelperPrefix
- @using
- @model
- @inherits
- @inject

```js
@using WebApplication1
@using WebApplication1.Models
@using WebApplication1.Models.AccountViewModels
@using WebApplication1.Models.ManageViewModels
@using Microsoft.AspNetCore.Identity
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

默认情况下，`_ViewImports.cshtml`文件通常直接放在Views（或Pages）文件夹中。也可以将`_ViewImports.cshtml`文件放在其他文件夹中，在这种情况下，它只会应用于该文件夹及其子文件夹中的页面或视图。程序将从根级别开始处理 `_ViewImports` 文件，然后处理在页面或视图本身的位置之前的每个文件夹。 可以在文件夹级别覆盖根级别指定的 `_ViewImports` 设置，也就是与该Razor视图同级别或最接近的`_ViewImports`，具有更高的采用权（后处理的会覆盖先处理的）。

如果在文件层次结构中找到多个 _ViewImports.cshtml 文件，指令的合并行为是：

- @addTagHelper和@removeTagHelper：按顺序全部运行
- @tagHelperPrefix：最接近视图的文件会替代任何其他文件
- @model：最接近视图的文件会替代任何其他文件
- @inherits：最接近视图的文件会替代任何其他文件
- @using：全部包括在内；忽略重复项
- @inject：针对每个属性，最接近视图的属性会替代具有相同属性名的任何其他属性



## _ViewStart.cshtml

_ViewStart.cshtml文件可以指定每个视图在呈现之前都要运行的代码。

按照约定，_ViewStart.cshtml文件通常直接位于Views（或Pages）文件夹下， 在呈现每个完整视图（不是布局，也不是分部视图）之前运行 _ViewStart.cshtml 中列出的语句。

与 `_ViewImports.cshtml` 一样，`_ViewStart.cshtml` 也是分层结构。 可以指定多个`_ViewStart.cshtml`文件，与页面同级别或最接近的_ViewStart具有更高的生效权（后处理的会覆盖先处理的）。

最常见的 _ViewStart.cshtml 文件：

```c#
@{
    Layout = "_Layout";
}
```

上述文件指定所有视图都将使用 _Layout.cshtml 布局。

_ViewStart.cshtml 和 _ViewImports.cshtml 通常不置于 /Pages/Shared（或 /Views/Shared）文件夹中。 这些应用级别版本的文件应直接放置在 /Pages（或 /Views）文件夹中。













