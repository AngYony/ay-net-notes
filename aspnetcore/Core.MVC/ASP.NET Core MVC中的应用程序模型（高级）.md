# ASP.NET Core MVC中的应用程序模型（高级）

本文是ASP.NET Core MVC应用中的高级部分，主要用于架构的底层设计。

ASP.NET Core MVC应用的各个组件都可以通过一个应用程序模型来表示，通过读取和处理此模型可修改 MVC 元素的行为方式。



## 应用程序模型和提供程序

ASP.NET Core MVC应用程序模型包括用于描述 MVC应用程序的抽象接口和具体实现类。通过使用应用程序模型，可以修改应用以遵循与MVC默认行为不同的约定。 

ASP.NET Core MVC应用程序模型及结构如下：

- ApplicationModel（应用程序模型）
  - ControllerModel（控制器模型）
    - ActionModel（操作方法模型）
      - ParameterModel（参数模型）

上述模型层次结构中的较低级别可以访问和覆盖由较高级别设置的属性值。

上述中的各个模型都实现了IPropertyModel接口，该接口包含一个Properties属性，因此每个级别的模型都有权访问Properties属性集合，如果每项操作都要对筛选器、模型绑定器进行配置，使用该属性是一个很好的解决办法（具体见下述示例）。需要注意的是，应用一旦完成启动，ActionDescriptor.Properties 集合就不再是线程安全的（针对写入），因此要让数据安全的添加到此集合，最佳方式是使用约定（见下文）。

### IApplicationModelProvider

在ASP.NET Core MVC中，加载应用程序模型是由IApplicationModelProvider接口定义的提供程序模式完成的。

> IApplicationModelProvider 是一种高级概念，框架创建者可对其进行扩展。 一般情况下，应用应使用约定，而框架应使用提供程序。 主要不同之处在于提供程序始终先于约定运行。

关于这部分的详细介绍，请参阅：

https://docs.microsoft.com/zh-cn/aspnet/core/mvc/controllers/application-model?view=aspnetcore-2.2#iapplicationmodelprovider

备注：应用程序模型定义了约定抽象，通过约定抽象来自定义模型行为比重写整个模型或提供程序更简单。 建议使用这些约定抽象来修改应用的行为。



## 约定

默认情况下，MVC 遵循特定的约定，以确定将哪些类视作控制器，这些类上的哪些方法是操作，以及参数和路由的行为方式。可以自定义此行为以满足应用的需要，即创建自己的约定，并将它们应用于全局或作为属性应用。

通过使用约定，可以编写能动态应用自定义项的代码。 使用筛选器可以修改框架的行为，而利用自定义项可以控制整个应用连接在一起的方式。

可用约定如下：

- IApplicationModelConvention
- IControllerModelConvention
- IActionModelConvention
- IParameterModelConvention

可通过以下方式应用约定：将它们添加到 MVC 选项，或实现 Attribute 并将它们应用于控制器、操作或操作参数（类似于 Filters）。 与筛选器不同的是，约定仅在应用启动时执行，而不作为每个请求的一部分执行。

### IApplicationModelConvention

应用一：通过定义实现了IApplicationModelConvention接口的约定，用于向应用程序模型添加属性。

```c#
namespace My.MvcApplicationModel.Study.Conventions
{
    public class ApplicationDescription : IApplicationModelConvention
    {
        private readonly string _desc;

        public ApplicationDescription(string description)
        {
            _desc = description;
        }

        public void Apply(ApplicationModel application)
        {
            application.Properties["Desc"] = _desc;
        }
    }
}
```

在Startup的ConfigureServices()方法中，添加MVC时，将应用程序模型约定作为选项进行配置：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options=> {
        options.Conventions.Add(new ApplicationDescription("My Desc"));
    })
    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
}
```

在控制器的操作方法中，通过ActionDescriptor.Properties进行访问：

```c#
public string Index()
{
    return "Desc:" + ControllerContext.ActionDescriptor.Properties["Desc"];
}
```

应用二：使用 IApplicationModelConvention 来自定义路由约定。

例如，以下约定会将控制器的命名空间合并到其路由中，并将命名空间中的 . 替换为路由中的 /：

```c#
public class NamespaceRoutingConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var hasAttributeRouteModels = controller.Selectors.Any(a => a.AttributeRouteModel != null);
            if (!hasAttributeRouteModels
            && controller.ControllerName.Contains("Namespace"))
            {
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = controller.ControllerType.Namespace.Replace('.', '/') 
                    + "/[controller]/[action]/{id?}"
                };
            }
        }
    }
}
```

将该约定作为选项添加到Startup中：

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options=> {
        options.Conventions.Add(new ApplicationDescription("My Desc"));
        options.Conventions.Add(new NamespaceRoutingConvention());
    })
    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}
```

上述约定会应用于未使用属性路由的路由，假如存在下述操作方法：

```c#
public class NamespaceRoutingController : Controller
{
    //访问路由：/my/mvcapplicationmodel/study/controllers/NamespaceRouting/Index
    public string Index()
    {
        return "Namespace";
    }
}
```

若要访问上述操作方法，需要使用路由：`/my/mvcapplicationmodel/study/controllers/NamespaceRouting/Index`进行访问。



### IControllerModelConvention

与上一个示例一样，也可以修改控制器模型，以包含自定义属性。 

注意：这些属性将覆盖应用程序模型中指定的具有相同名称的现有属性。

```c#
public class ControllerDescriptionAttribute : Attribute, IControllerModelConvention
{
    private readonly string _desc;
    public ControllerDescriptionAttribute(string description)
    {
        _desc = description;
    }
    public void Apply(ControllerModel controller)
    {
        controller.Properties["Desc"] = _desc;
    }
}
```

将上述约定作为特性应用在控制器上：

```c#
[ControllerDescription("My Desc2")]
public class HomeController : Controller
{
    public string Index()
    {
        return "Desc2:" + ControllerContext.ActionDescriptor.Properties["Desc"];
    }
}
```

### IActionModelConvention

应用一：可向各项操作应用不同的属性约定，并覆盖已在应用程序或控制器级别应用的行为。

```c#
public class ActionDescriptionAttribute : Attribute, IActionModelConvention
{
    private readonly string _desc;
    public ActionDescriptionAttribute(string description){
        _desc = description;
    }
    public void Apply(ActionModel action)
    {
        action.Properties["Desc3"] = _desc;
    }
}
```

下述代码演示了它如何覆盖控制器级别的约定：

```c#
[ControllerDescription("My Desc2")]
public class HomeController : Controller
{
    [ActionDescription("My Desc3")]
    public string Index()
    {
        return "Desc3:" + ControllerContext.ActionDescriptor.Properties["Desc3"];
    }
}
```

应用二：定义可修改ActionModel的约定，以更新其应用到的操作方法的名称。新名称以参数形式提供给该属性。 此新名称供路由使用，因此它将影响用于访问此操作方法的路由。

```c#
public class CustomActionNameAttribute : Attribute, IActionModelConvention
{
    private readonly string _actionName;

    public CustomActionNameAttribute(string actionName)
    {
        _actionName = actionName;
    }

    public void Apply(ActionModel action)
    {
        action.ActionName = _actionName;
    }
}
```

将该属性应用于控制器中的操作方法上：

```c#
[CustomActionName("NewAc")]
public string SomeName(){
    return ControllerContext.ActionDescriptor.ActionName;
}
```

即使方法名称为 SomeName，该属性也会覆盖 MVC 使用该方法名称的约定，并将操作名称替换为 NewAc。 因此，用于访问此操作的路由为 /Home/NewAc。

备注：此示例本质上与使用内置 ActionName 属性相同。

### IParameterModelConvention

可将以下约定应用于操作参数，以修改其 BindingInfo。 以下约定要求参数为路由参数；忽略其他可能的绑定源（比如查询字符串值）。

```c#
public class MustBeInRouteParameterModelConvention : Attribute, IParameterModelConvention
{
    public void Apply(ParameterModel parameter)
    {
        if (parameter.BindingInfo == null)
        {
            parameter.BindingInfo = new BindingInfo();
        }
        parameter.BindingInfo.BindingSource = BindingSource.Path;
    }
}
```

上述定义的属性可以应用于任何操作参数：

```c#
public string GetById([MustBeInRouteParameterModelConvention]int id)
{
    return $"Bound to id: {id}";
}
```

访问上述操作方法时，id参数只能作为路由的一部分才能绑定，例如：`/home/getbyid/2`，如果使用`/home/getbyid?id=2`不能得到id的正确值。







