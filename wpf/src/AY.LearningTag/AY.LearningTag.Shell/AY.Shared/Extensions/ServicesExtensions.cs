using AY.LearningTag.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;
using System.Windows;

namespace AY.Shared.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// 添加配置文件支持
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigureEx(this IServiceCollection services, IConfiguration configuration)
        {
            //https://learn.microsoft.com/zh-cn/dotnet/core/extensions/configuration-providers
            //方式一：绑定配置并直接添加到服务容器中（推荐），可以直接通过注入的方式读取到配置项，最推荐方式
            services.Configure<MySettings>(configuration.GetSection("Settings"));

            ////方式二：直接读取配置文件到实体中
            //configuration.GetRequiredSection("settings").Get<Settings>();

            ////方式三：读取配置绑定到实体
            //Settings options = new();
            //configuration.GetSection(nameof(Settings)).Bind(options);

            ////方式四：
            //configuration.GetSection(nameof(Settings)).Get<Settings>();

            return services;
        }

        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPooledDbContextFactoryEx(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //从配置文件中获取迁移程序集信息，好处是：可以不需要显式添加程序集的引用就可以注册服务
            var migrationsAssembly = configuration.GetValue("MigrationsAssembly", string.Empty);
            services.AddPooledDbContextFactory<LearningTagDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlite(connectionString, sqlOptions =>
                {
                    if (!string.IsNullOrEmpty(migrationsAssembly))
                    {
                        //指定迁移程序集
                        sqlOptions.MigrationsAssembly(migrationsAssembly);
                    }
                });
            }, poolSize: 32); // 设置连接池大小为32

            //注册 DbContext 实例
            services.AddScoped(provider => provider.GetRequiredService<IDbContextFactory<LearningTagDbContext>>().CreateDbContext());
            return services;
        }

        public static IServiceCollection AddLogEx(this IServiceCollection services)
        {
            //在不使用主机的情况下使用 DI 时，请在 LoggingServiceCollectionExtensions.AddLogging 中进行配置。
            var serilog = new LoggerConfiguration()
                .MinimumLevel.Debug() //设置日志级别
                                      //.WriteTo.Console() //输出到控制台，需要安装Serilog.Sinks.Console包
                .WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            services.AddLogging(builder => builder.AddSerilog(serilog));

            #region 在不使用主机和DI的情况下

            //ILoggerFactory logger = LoggerFactory.Create(logging =>
            //{
            //    logging.AddSerilog(serilog);

            //});
            //ILogger logger = loggerFactory.CreateLogger<Program>();
            //services.AddSingleton < Microsoft.Extensions.Logging.ILogger<>(_ =>
            //    new LoggerFactory()
            //        .AddSerilog(serilog)
            //        .CreateLogger("Logger")
            //);

            #endregion 在不使用主机和DI的情况下

            return services;
        }

        /// <summary>
        /// 注册所有数据仓储（开放泛型接口及其实现类）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            //这里由于添加了程序集的引用，因此可以直接通过 typeof 来获取程序集
            Assembly assembly = typeof(EFCoreRepository<,>).Assembly;

            var implementationTypes = assembly.GetTypes()
                .Where(t => t.IsClass
                    && !t.IsAbstract
                    && t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == typeof(EFCoreRepository<,>)
                );


            //foreach (var impl in implementationTypes)
            //{
            //    var interfaces = impl.GetInterfaces();

            //    foreach (var itf in interfaces)
            //    {
            //        // 匹配规则：
            //        // 1. 以 "IxxxRepository" 结尾
            //        // 2. 是泛型接口（你的情况是 1 个泛型参数）
            //        if (itf.Name.EndsWith("DataRepository`1"))
            //        {
            //            // 自动绑定：
            //            // ISectionRepository<LearningTagDbContext> -> SectionRepository
            //            services.AddTransient(itf, impl);
            //        }
            //    }
            //}

            foreach (var impl in implementationTypes)
            {
                var interfaces = impl.GetInterfaces()
                                .Where(x => x.IsGenericType &&
                                x.GetGenericTypeDefinition().Name.EndsWith("DataRepository`1"));

                foreach (var itf in interfaces)
                {
                    //开放泛型的注册方式
                    services.AddTransient(
                        itf.GetGenericTypeDefinition(),   // ISectionDataRepository<>
                        impl.GetGenericTypeDefinition()   // SectionDataRepository<>
                    );
                }
            }

            return services;
        }



        public static IServiceCollection AddApplicationServices(this IServiceCollection services, Type transientServiceBaseType)
        {
            Assembly serviceAssembly = transientServiceBaseType.Assembly;
            var implementationTypes = serviceAssembly.GetTypes()
            .Where(t => t.IsClass
                     && !t.IsAbstract
                     && t.IsAssignableTo(transientServiceBaseType));

            foreach (var impl in implementationTypes)
            {
                var interfaces = impl.GetInterfaces();

                foreach (var itf in interfaces)
                {
                    if (itf.Name.EndsWith("Service`1"))
                    {
                        services.AddTransient(itf.GetGenericTypeDefinition(), impl.GetGenericTypeDefinition());
                    }
                }
            }
            return services;
        }




        public static IServiceCollection AddViewModel<TView, TViewModel>(this IServiceCollection services)
            where TView : class
            where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient<TView>(provider =>
            {
                var viewModel = provider.GetRequiredService<TViewModel>();
                var view = ActivatorUtilities.CreateInstance<TView>(provider);
                if (view is FrameworkElement fe)
                {
                    fe.DataContext = viewModel;
                }
                return view;
            });
            return services;
        }
    }
}