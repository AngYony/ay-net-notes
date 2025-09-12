using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Debugging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AY.LearningTag.ToolKitShared
{
    public static class ServicesExtensions
    {
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

        public static IServiceCollection AddConfigureService(this IServiceCollection services, IConfiguration configuration)
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


        public static IServiceCollection AddLog(this IServiceCollection services)
        {
            //在不使用主机的情况下使用 DI 时，请在 LoggingServiceCollectionExtensions.AddLogging 中进行配置。
            var serilog = new LoggerConfiguration()
                .MinimumLevel.Debug() //设置日志级别
                                      //.WriteTo.Console() //输出到控制台，需要安装Serilog.Sinks.Console包
                .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
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
            #endregion



            return services;
        }
    }
}
