using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;

namespace AY.SmartEngine.Shared.Extensions
{
    public static class ServicesExtensions
    {
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="transientServiceBaseType"></param>
        ///// <returns></returns>

        //public static IServiceCollection AutoRegisterDataRepositoriesAndApplicationServices(
        //    this IServiceCollection services, Type serviceType)
        //{
        //    ////开放泛型接口其实现类也是开放泛型的注册方式（实现类不需要指定具体泛型参数的，但必须保证泛型参数数量和接口定义的一直才可以使用这种形式注册）
        //    //services.AddTransient(typeof(IEFCoreRepository<,>), typeof(EFCoreRepository<,>));

        //    ////扫描并注册所有具体的仓储类
        //    //Assembly repositoryAssembly = typeof(EFCoreRepository<,>).Assembly;
        //    //services.Scan(scan => scan.FromAssemblies(repositoryAssembly)
        //    //    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("DataRepository")))
        //    //    .AsImplementedInterfaces()
        //    //    .WithTransientLifetime());

        //    ////扫描并注册所有具体的应用服务类
        //    Assembly serviceAssembly = serviceType.Assembly;
        //    services.Scan(scan => scan
        //        .FromAssemblies(serviceAssembly)
        //        //.AddClasses(classes => classes.AssignableTo(transientServiceBaseType))
        //        .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
        //        .AsImplementedInterfaces()
        //        .WithTransientLifetime());

        //    return services;
        //}


        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceAssembly"></param>
        /// <param name="endsWithServiceName"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, Assembly serviceAssembly, string endsWithServiceName)
        {
            //扫描并注册所有具体的应用服务类
            services.Scan(scan => scan
                .FromAssemblies(serviceAssembly)
                //.AddClasses(classes => classes.AssignableTo(transientServiceBaseType))
                .AddClasses(c => c.Where(t => t.Name.EndsWith(endsWithServiceName)))
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
