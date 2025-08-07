using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AY.LearningTag.Shared
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


        public static IServiceCollection AddLog(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(_ =>
                new LoggerConfiguration()
                .MinimumLevel.Debug() //设置日志级别
                .WriteTo.Console() //输出到控制台
                .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger());
            return services;
        }
    }
}
