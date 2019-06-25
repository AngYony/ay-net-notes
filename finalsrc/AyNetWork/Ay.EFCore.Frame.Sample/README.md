# 使用EFCore时，必须具备的代码片段样例说明



## 创建实体模型类和DbContext

实体模型类，例如：Student

派生自DbContext类的数据库上下文类，例如：AyDbContext，需要注意的是构造函数的使用：

```c#
public class AyDbContext : DbContext
{
    public AyDbContext(DbContextOptions<AyDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}

```



## 创建用于数据库初始化的类（非必需）

用于数据库初始化的类一般是静态类，例如：

```c#
public static class DbInitializer
{
    public static void Initialize(AyDbContext context){

    }
}
```



## 通过依赖关系注入DbContext

```
 services.AddDbContext<AyDbContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("MyConnectonString"));
});
```



## 从依赖关系注入容器中获取注入的DbContext

```c#
public static void Main(string[] args)
{
    var host = CreateWebHostBuilder(args).Build();

    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AyDbContext>();

            //context.Database.EnsureCreated();
            //可以调用数据库初始化操作
            //DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "获取异常信息示例");
        }
    }

    host.Run();
}
```



## 必须引入的命名空间

```c#
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
```





