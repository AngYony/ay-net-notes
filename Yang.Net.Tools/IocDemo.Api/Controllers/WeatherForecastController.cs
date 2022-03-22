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
        //����ȫ�־�̬�����������ڿ���д���һ��ȫ������
        static MyServiceCollection services = new();
        //����һ����̬�������ṩ�ߣ������ڿ���л���һ��һ�з����ṩ�ߵ�Դͷ
        static MyServiceProvider provider;


        public WeatherForecastController()
        {
            services.AddScoped<IUser, User>();
            services.AddSingleton<IRole, Role>();

            //�������ṩ������Ϊ����
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