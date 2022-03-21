using Autofac;
using Yang.API.Controllers;
using Yang.API.Models;

namespace Yang.API.AutofacCus.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterTypeExtensions(this ContainerBuilder builder)
        {
            //builder.RegisterType<Student>().As<IStudentService>().InstancePerLifetimeScope();
            //注册为scope，默认为Transient
            builder.RegisterType<User>().As<IUserService>()
            // 设置为scope模式
            .InstancePerLifetimeScope()
            //在User中启用属性注入，即：在User类中的属性可以不使用构造函数进行赋值，而是直接在此指明User中属性已经被注入了
            .PropertiesAutowired();
        }
    }
}
