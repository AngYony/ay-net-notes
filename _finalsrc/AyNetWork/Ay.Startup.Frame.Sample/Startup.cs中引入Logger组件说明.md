# Startup.cs中引入Logger组件说明

ILogger记录日志时，需要指定日志对应的实体类对象，例如：

```powershell
info: My.Filters.Study.Pages.IndexModel[0]
      Index Get ................
```

ILogger<T>中的泛型类型T即为要记录的日志的实体类。

#### 方式一

```c#
private readonly ILogger logger;

//重点：此处必须写作ILogger<Startup>
public Startup(ILogger<Startup> _logger)
{
    this.logger = _logger;
}
```



#### 方式二

```c#
private readonly ILogger _logger;

public Startup(ILoggerFactory loggerFactory)
{
    _logger = loggerFactory.CreateLogger<MyCusLogger>();
}

```

MyCusLogger.cs：

```c#
public class MyCusLogger
{
    //只需要定义这个类即可
}
```





