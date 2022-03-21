using Autofac;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Yang.API.Controllers;
using Yang.API.Models;

namespace Yang.API.AutofacCus.Modules
{
    public class APIModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // 默认情况下，不能在控制器中使用属性注入，需要显示设置builder.Services.AddControllers().AddControllersAsServices()后才可以.
            builder.RegisterType<UserController>().PropertiesAutowired();

            // 也可以在方法中引入其他module
            //builder.RegisterModule<APIModule>();

            // 反射注入
          var libs=  DependencyContext.Default.CompileLibraries
            .Where(x => !x.Serviceable && x.Type == "project").ToList();

            List<Assembly> assemblies = new();
            foreach (var lib in libs)
            {
                assemblies.Add(AssemblyLoadContext.Default
                .LoadFromAssemblyName(new AssemblyName(lib.Name)));
            }
            builder.RegisterAssemblyTypes(assemblies.ToArray())
            .Where(t => t.IsAssignableTo<IocTag>() && !t.IsAbstract)
            .AsSelf().AsImplementedInterfaces()
            .InstancePerLifetimeScope()  //scope
            //.SingleInstance()//单例，什么都不指定表示Transient

            .PropertiesAutowired();


        }
    }
}
