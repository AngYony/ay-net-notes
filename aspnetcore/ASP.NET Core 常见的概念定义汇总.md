# ASP.NET Core 常见的概念定义汇总

本文对ASP.NET Core中的基础知识点，以及应用程序中常见的名词定义、概念描述进行汇总，方便进一步理解ASP.NET Core应用程序的开发。



### Web 根 （webroot）

即ASP.NET Core应用程序项目中wwwroot目录（默认情况下），其中包含公共资源、CSS 等静态资源、JavaScript 和图形文件等。

注意：在Razor（.cshtml）文件中，使用波浪号斜杠`~/`指向该目录，常用于静态资源的引用，以 `~/` 开头的路径称为虚拟路径。



### 内容根（Content Root）

内容根不能简单的理解为项目的根目录，它应该是包含程序中所使用的所有内容的根目录，比如Razor pages、MVC视图和静态资源、DLL文件等。默认情况下，内容根位置与用于托管应用的可执行文件的应用基路径相同。



### 中间件（Middleware）

在Startup.cs的Configure 方法中，常常见到调用类似UserXYZ形式的扩展方法，它表示的就是向管道添加名为“XYZ”的中间件组件。简单来说中间件就是一种装配到应用管道以处理请求和响应的软件。从代码层面上讲，中间件就是由一些可重用的类和方法组成，中间件也叫中间件组件或简称组件。

关于中间件的详细介绍请参考官方文档：https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/index



### 管道

TODO:



### 环境（Environments）

环境（如“开发”环境和“生产”环境）主要是用来区分应用程序根据不同的使用场景来实现不同的操作的。ASP.NET Core在应用启动时会读取环境变量，运行时环境根据设置的环境变量值不同，而使ASP.NET Core配置不同的应用行为。内置的环境变量值有Development、Staging 和 Production。



### 宿主（Hosting）

宿主（有时也叫主机或托管）由ASP.NET Core应用配置和启动，负责应用的启动和生存期管理。



### 服务器（Servers）

注意和主机（宿主或托管）的不同，ASP.NET Core 托管模型不直接侦听请求，而是依赖 HTTP 服务器实现将请求转发到应用。ASP.NET Core包括一个名为Kestrel的托管的跨平台web服务器。 Kestrel 通常在生产 Web 服务器（如反向代理配置中的 IIS 或 Nginx）后台运行。



### 日志记录（Logging）

从字面上讲，就是记录程序中以特定规则生成的日志，日志的内容完全由自己内定。ASP.NET Core 支持适用于各种内置和第三方日志记录提供程序的日志记录 API。内置提供程序支持向一个或多个目标发送日志。  



















------



#### 参考资源

- [ASP.NET Core基础知识](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/?view=aspnetcore-2.1)
- [ASP.NET Core中间件](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/index)
- [在 ASP.NET Core 中使用多个环境](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/environments)
- [在 ASP.NET Core 中托管](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/index)
- [ASP.NET Core 中的日志记录](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/logging)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。

最后一次更新时间：2018-10-19

------



