using Autofac.Sample.IocServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;

namespace Autofac.Sample.Modules
{
    public class ApiModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IocService>().As<IIocService>();

            // 获取所有创建的项目Lib
            var libs = DependencyContext.Default
            .CompileLibraries
            .Where(x => !x.Serviceable && x.Type == "project").ToList();
            // 将lib转成Assembly
            List<Assembly> assemblies = new();
            foreach (var lib in libs)
            {
                assemblies.Add(AssemblyLoadContext.Default
                .LoadFromAssemblyName(new AssemblyName(lib.Name)));
            }
            // 反射获取其中所有的被接口修饰的类型，并区分生命周期
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(t => t.IsAssignableTo<IocTagScope>() && !t.IsAbstract)
                .AsSelf().AsImplementedInterfaces()
                .InstancePerLifetimeScope();
                //.PropertiesAutowired(); 允许属性注入
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(t => t.IsAssignableTo<IocTagSington>() && !t.IsAbstract)
                .AsSelf().AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired();
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(t => t.IsAssignableTo<IocTagTransient>() && !t.IsAbstract)
                .AsSelf().AsImplementedInterfaces()
                .PropertiesAutowired();
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(t => t.IsAssignableTo<ControllerBase>() && !t.IsAbstract)
                .AsSelf()
                .PropertiesAutowired();


        }
        
    }
}
