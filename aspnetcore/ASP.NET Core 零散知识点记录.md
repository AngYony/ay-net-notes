# ASP.NET Core 零散知识点记录



## Web API

### ControllerBase 类的使用说明

Web API 中的控制器派生自 ControllerBase 类，而其他 Web 应用（MVC）派生自 Controller 类。

Controller 派生自 ControllerBase，并添加对视图的支持，因此它用于处理 Web 页面，而不是 Web API 请求。

因此，不要通过从 Controller 类派生来创建 Web API 控制器。但此规则有一个例外：如果打算为视图和 Web API 使用相同的控制器，则从 Controller 派生控制器。【[参见链接](https://docs.microsoft.com/zh-cn/aspnet/core/web-api/?view=aspnetcore-3.1#controllerbase-class)】



## .NET

### 类库（Library）和框架（Framework）的不同

类库（Library）和框架（Framework）的不同之处在于：前者往往只是提供实现某种单一功能的 API；而后者则针对一个目标任务对这些单一功能进行编排，以形成一个完整的流程，并利用一个引擎来驱动这个流程自动执行