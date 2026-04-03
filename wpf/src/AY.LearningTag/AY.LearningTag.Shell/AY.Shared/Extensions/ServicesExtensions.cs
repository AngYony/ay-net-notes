using AY.LearningTag.Domain.EFCore.Repositories.Common;
using AY.LearningTag.Infrastructure.EntityFrameworkCore;
using AY.LearningTag.Infrastructure.EntityFrameworkCore.DbContexts;
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
        public static IServiceCollection AddOptionsBinding(this IServiceCollection services, IConfiguration configuration)
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
        public static IServiceCollection AddDbContextFactory(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //从配置文件中获取迁移程序集信息，好处是：可以不需要显式添加程序集的引用就可以注册服务
            var migrationsAssembly = configuration.GetValue("MigrationsAssembly", string.Empty);
            services.AddDbContextFactory<LearningTagDbContext>(options =>
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
            });

            return services;
        }

        /// <summary>
        /// 添加日志支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="transientServiceBaseType"></param>
        /// <returns></returns>

        public static IServiceCollection AutoRegisterDataRepositoriesAndApplicationServices(
            this IServiceCollection services, Type transientServiceBaseType)
        {
            ////开放泛型接口其实现类也是开放泛型的注册方式（实现类不需要指定具体泛型参数的，但必须保证泛型参数数量和接口定义的一直才可以使用这种形式注册）
            //services.AddTransient(typeof(IEFCoreRepository<,>), typeof(EFCoreRepository<,>));

            ////扫描并注册所有具体的仓储类
            Assembly repositoryAssembly = typeof(EFCoreRepository<,>).Assembly;
            services.Scan(scan => scan.FromAssemblies(repositoryAssembly)
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith("DataRepository")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            //扫描并注册所有具体的应用服务类
            Assembly serviceAssembly = transientServiceBaseType.Assembly;
            services.Scan(scan => scan
                .FromAssemblies(serviceAssembly)
                .AddClasses(classes => classes.AssignableTo(transientServiceBaseType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

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