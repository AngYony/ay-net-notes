using DearlerPlatform.Core.Repository;
using DearlerPlatform.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Extensions
{
    public static class RepositoryRegisterExtensions
    {
        public static IServiceCollection RepositoryRegister1(this IServiceCollection services)
        {
            var asmCore = Assembly.Load("DearlerPlatform.Core");
            var implementationType = asmCore.GetTypes().FirstOrDefault(a => a.Name == "Repository`1");
            var interfaceType = implementationType?.GetInterface("IRepository`1"); //这句无法获取到具体类型
            interfaceType = asmCore.GetTypes().FirstOrDefault(a => a.Name == "IRepository`1");
            if (interfaceType != null && implementationType != null)
            {
                services.AddTransient(interfaceType, implementationType);
            }
            return services;
        }



        public static IServiceCollection RepositoryRegister(this IServiceCollection services)
        {
            var asmCore = Assembly.Load("DearlerPlatform.Core");
            var implementationTypes = asmCore.GetTypes().Where(a => a.Name == "Repository`1");
            //var implementationTypes = asmCore.GetTypes().Where(t =>
            //(!t.IsInterface) && (!t.IsAbstract) && t.IsAssignableTo(typeof(IRepository)));

            foreach(var implementationType in implementationTypes)
            {
                services.AddTransient(typeof(IRepository<>), implementationType);
            } 
            return services;
        }

        public static IServiceCollection ServicesRegister(this IServiceCollection services)
        {
            var asmCore = Assembly.Load("DearlerPlatform.Service");
            var implementationTypes = asmCore.GetTypes().Where(t =>
            (!t.IsInterface) && (!t.IsAbstract) && t.IsAssignableTo(typeof(IocTag)));

            foreach (var implementationType in implementationTypes)
            {
                var interfaceType= implementationType.GetInterfaces().Where(a => a.Name != nameof(IocTag)).FirstOrDefault();
                services.AddTransient(interfaceType, implementationType);
            }
            return services;
        }


    }
}
