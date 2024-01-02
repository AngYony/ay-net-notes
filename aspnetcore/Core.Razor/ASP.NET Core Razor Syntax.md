# ASP.NET Core Razor语法

Razor 是一种标记语法，用于将基于服务器的代码嵌入网页中。 Razor 语法由 Razor 标记、C# 和 HTML 组成。 包含 Razor 的文件通常具有 .cshtml 文件扩展名。



## 隐式Razor表达式

隐式Razor表达式以@开头，后面直接跟C#代码。例如：

```html
<p>@DateTime.Now</p>
```

隐式表达式不能包含空格和C#泛型。

不能包含空格并不是绝对的，如果该 C# 语句具有明确的结束标记，则可以混用空格，例如：使用C# await 关键字时可以包含空格：

```html
<p>@await DoSomething("hello", "world")</p>
```

而C#泛型在使用时会出现一对“<>”，会被解析为HTML标记，因此不能通过编译。

*注：泛型方法调用必须包装在显式 Razor 表达式或 Razor 代码块中。*

### @符号或关键字的转义

若要对 Razor 标记中的 @ 符号或关键字进行转义，需要使用两个@ 符号：

```htnml
<p>@@Username</p>
@(@case)
```

*备注：包含电子邮件地址的 HTML 属性和内容中的@符号，不会被视为转换字符，因此不需要使用两个@符号。*



## 显式Razor表达式

显式Razor表达式由@符号和平衡圆括号组成。例如：

```c#
<p>Last week this time: @(DateTime.Now - TimeSpan.FromDays(7))</p>
<p>@(GenericMethod<int>())</p>
```

显式Razor表达式将计算 @() 括号中的所有内容，并将其呈现到输出中。

### 表达式编码

如果C#表达式返回的结果是字符串，将会自动对字符串进行HTML编码。例如：

```c#
@("<span>Hello World</span>")
```

将生成以下HTML：

```html
&lt;span&gt;Hello World&lt;/span&gt;
```

如果想生成可以被浏览器解析的HTML内容，可以使用HtmlHelper.Raw()方法。例如：

```c#
@Html.Raw("<span>Hello World</span>")
```

最终将呈现为HTML标记：

```html
<span>Hello World</span>
```



## Razor代码块

使用@{}包裹的C#代码被称为代码块。代码块内的C#代码只用来处理数据，而不进行呈现，这点与表达式不同。

一个视图中的代码块和表达式共享相同的作用域并按顺序进行定义。

```C#
@{
    var quote = "The future depends on what you do today. - Mahatma Gandhi";
}
<p>@quote</p>
```

### 隐式转换

Razor代码块中可以包含HTML，将会被隐式转换。

```c#
@{
    var inCSharp = true;
    <p>Now in HTML, was in C# @inCSharp</p>
}
```

### 显示转换

若要显示转换，一般有两种方式。

一种方式是借助HTML元素标签，将要显示为HTML的内容使用标签括起来：

```c#
@for (var i = 0; i < people.Length; i++)
{
    var person = people[i];
    <text>Name: @person.Name</text>
}
```

推荐使用`<text>`元素而不是`<span>`，因为`<text>` 标记可用于在呈现内容时控制空格（`<text>` 标记之前或之后的空格不会显示在 HTML 输出中）。

另一种方式是使用`@:`语法，将以 HTML 的形式呈现整个行的其余内容。

```c#
@for (var i = 0; i < people.Length; i++)
{
    var person = people[i];
    @:Name: @person.Name
}
```

实际应用中，推荐使用方式一。



## Razor指令

Razor 指令由隐式表达式表示：@ 符号后跟保留关键字。 指令通常用于更改视图分析方式或启用不同的功能。

常用的Razor指令有：

- @using：用于向生成的视图添加 C# using 指令。

- @model：用于指定传递到视图的模型类型。

- @inherits：用于对视图继承的类提供完全控制。

- @inject：用于允许 Razor 页面将服务从服务容器注入到视图。

- @functions：用于允许 Razor 页面将 C# 代码块添加到视图中，例如：

  ```c#
  @functions {
      public string GetHello()
      {
          return "Hello";
      }
  }
  <div>From method: @GetHello()</div> 
  ```

- @section：与布局结合使用，允许视图将内容呈现在 HTML 页面的不同部分。



## 使用委托（匿名函数）对Razor模板化

模板化的实质是在Razor页面中定义匿名函数，在匿名函数中设置要输出的HTML内容，并返回。常用的方式是通过Func委托进行处理，Func委托最终要返回的TResult类型为IHtmlContent，也可以直接返回object。

首先定义一个非常简单的类：

 ```c#
public class Student
{
    public string StudentName{ get; set; }
}
 ```

然后在Razor页面中进行呈现：

```c#
@using Microsoft.AspNetCore.Html
@{
    //定义一个Student集合
    var students = new List<Student>{
        new Student{ StudentName="曹操"},
        new Student{ StudentName="刘备"},
        new Student{ StudentName="孙权"}
    };

    //定义一个Func委托，item是默认参数，因此可以直接使用
    Func<Student, IHtmlContent> stuTemplate = @<p>姓名：<strong>@item.StudentName</strong></p>;

    //呈现内容
    @foreach (var stu in students)
    {
        //开头的@符号必不可少
        @stuTemplate(stu);
    }
}
```

上述代码中，需要特别注意的有以下几点。

```c#
//定义一个Func委托，item是默认参数，因此可以直接使用
Func<Student, IHtmlContent> stuTemplate = 
	@<p>姓名：<strong>@item.StudentName</strong></p>;
```

该委托输入参数的类型为Student，但是这里并没有显示的指定参数，而是使用了默认参数item，关于item产生的原因目前暂不清楚。另外，委托返回的TResult类型为IHtmlContent，以下语句：

```c#
@<p>姓名：<strong>@item.StudentName</strong></p>;
```

不能脱离委托独立存在，必须在委托中使用，并且只能以上述赋值的形式出现（使用=），不能以lambda表达式（以=>形式）的形式使用。（上述代码更像是Razor模板）。

使用内联 Razor 模板作为方法的参数：

```c#
@functions{
    public static IHtmlContent Repeat(IEnumerable<dynamic> items, int count, Func<dynamic, IHtmlContent> template)
    {
        var html = new HtmlContentBuilder();
        foreach (var item in items)
        {
            for (int i = 0; i < count; i++)
            {
                html.AppendHtml(template(item));
            }
        }
        return html;
    }
}
```

调用时：

```c#
<ul>
    @Repeat(students, 3, @<li>@item.StudentName</li>)
</ul>
```

Repeat方法的第三个参数为Func<dynamic, IHtmlContent>类型，这里直接传入了`@<li>@item.StudentName</li>`模板。

本部分的完整代码如下：

```c#
@page
@model My.RazorSyntax.Study.Pages.IndexModel
@{
    Layout = null;
}
@namespace My.RazorSyntax.Study
@using Microsoft.AspNetCore.Html
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    @functions{
        public static IHtmlContent Repeat(IEnumerable<dynamic> items, int count, Func<dynamic, IHtmlContent> template)
        {
            var html = new HtmlContentBuilder();
            foreach (var item in items)
            {
                for (int i = 0; i < count; i++)
                {
                    html.AppendHtml(template(item));
                }
            }
            return html;
        }
    }

    @{
        //定义一个Student集合
        var students = new List<Student>{
            new Student{ StudentName="曹操"},
            new Student{ StudentName="刘备"},
            new Student{ StudentName="孙权"}
        };

        //定义一个Func委托，item是默认参数，因此可以直接使用
        Func<Student, IHtmlContent> stuTemplate =@<p>姓名：<strong>@item.StudentName</strong></p>;

        //呈现内容
        @foreach (var stu in students)
        {
            @stuTemplate(stu);
        }
    }

    <ul>
        @Repeat(students, 3, @<li>@item.StudentName</li>)
    </ul>

    <a></a>
</body>
</html>
```



## 标记帮助程序相关的指令

- @addTagHelper：向视图提供标记帮助程序。
- @removeTagHelper：从视图中删除以前添加的标记帮助程序。
- @tagHelperPrefix：指定标记前缀，以启用标记帮助程序支持并阐明标记帮助程序的用法。



