using IocDemo.Api.Services;
using IocDemo.Api.Services.IServices;
using IocDemo.Container;
using Microsoft.AspNetCore.Mvc;

namespace IocDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //创建全局静态容器，类似在框架中创建一个全局容器
        static MyServiceCollection services = new();
        //创建一个静态根服务提供者，类似在框架中会有一个一切服务提供者的源头
        static MyServiceProvider provider;


        public WeatherForecastController()
        {
            services.AddScoped<IUser, User>();
            services.AddSingleton<IRole, Role>();

            //讲服务提供者设置为单例
            if (provider == null)
            {
                provider = services.BuildProvider();
            }
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public void Get()
        {
            var providerScoped = provider.CreateScoped();
            var a = providerScoped.GetService(typeof(IUser));
            var b=providerScoped.GetService(typeof(IRole));

        }
    }
}