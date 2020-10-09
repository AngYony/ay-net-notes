# ʹ��EFCoreʱ������߱��Ĵ���Ƭ������˵��



## ����ʵ��ģ�����DbContext

ʵ��ģ���࣬���磺Student

������DbContext������ݿ��������࣬���磺AyDbContext����Ҫע����ǹ��캯����ʹ�ã�

```c#
public class AyDbContext : DbContext
{
    public AyDbContext(DbContextOptions<AyDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}

```



## �����������ݿ��ʼ�����ࣨ�Ǳ��裩

�������ݿ��ʼ������һ���Ǿ�̬�࣬���磺

```c#
public static class DbInitializer
{
    public static void Initialize(AyDbContext context){

    }
}
```



## ͨ��������ϵע��DbContext

```
 services.AddDbContext<AyDbContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("MyConnectonString"));
});
```



## ��������ϵע�������л�ȡע���DbContext

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
            //���Ե������ݿ��ʼ������
            //DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "��ȡ�쳣��Ϣʾ��");
        }
    }

    host.Run();
}
```



## ��������������ռ�

```c#
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
```





