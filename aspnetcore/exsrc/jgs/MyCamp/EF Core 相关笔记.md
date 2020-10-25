# EF Core 相关笔记

[TOC]

## Code-First 模式开发

### 需要安装的工具和Nuget包

```
Pomelo.EntityFrameworkCore.MySql
Microsoft.EntityFrameworkCore.Design
```

### 安装 dotnet tool ef 工具

进入到解决方案目录，安装dotnet tool ef工具

```
dotnet tool install --global dotnet-ef
```

### 添加迁移文件

进入到具体的项目目录，添加迁移文件，“Init”是迁移脚本的名字：

```
dotnet ef migrations add Init
```

如果后期对数据库进行了更改，可以继续使用该命令添加新的迁移文件，例如：

```
dotnet ef migrations add ProjectIdAutoGenerate
```

如果要删除上一次迁移 (回滚对迁移) 所做的代码更改，可以执行下述命令：

```
dotnet ef migrations remove
```

### 更新数据库

然后执行更新数据库：

```
dotnet ef database update
```



## EF Core 编程建议

### CancellationToken 

建议在 Controller 中，每个异步方法都采用带有 CancellationToken 参数的方法，这样在前台中断请求时，后端可以接收到前台中断的消息，从而取消后端的程序执行，以此来提高性能。

### 单独设置 EF Core 的日志级别

单独设置 EF Core 的日志级别：

"Microsoft.EntityFrameworkCore.Database.Command":"Debug"，一旦设置之后，可以在控制台中查看EF Core 执行的生成脚本。

### AddDbContext 和 AddDbContextPool

AddDbContext 和 AddDbContextPool：

参考EF Core官方文档说明，https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/context-pooling#limitations。并不是AddDbContextPool一定会起到很明显的优化作用。



## EF Core中的关系

### 一对一

### 一对多

一是主体实体（主表），多是依赖实体（从表）。

### 多对多

A表和B表时多对多的关系，在此基础上，存在中间表C表，将A表和B表进行串联，C表中含有两个外键，分别指向A表和B表。同时包含两个引用属性，指向A实体和B实体。

