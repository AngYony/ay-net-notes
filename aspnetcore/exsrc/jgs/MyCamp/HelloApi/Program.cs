using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Console.WriteLine("Server started");

            using(var scope=host.Services.CreateScope()){
                var helloService1 = scope.ServiceProvider.GetRequiredService<IHelloService>();
                helloService1.Hello();


                var helloService2 = scope.ServiceProvider.GetRequiredService<IHelloService>();
                helloService2.Hello();

                
                using (var scope3 = scope.ServiceProvider.CreateScope()){
                    var helloservice4= scope3.ServiceProvider.GetRequiredService<IHelloService>();
                    helloservice4.Hello();
                }
            }

            using (var scope2 = host.Services.CreateScope())
            {
                var helloService3 = scope2.ServiceProvider.GetRequiredService<IHelloService>();
                helloService3.Hello();
            }


            //Add AddTransient
            //var helloService1 = host.Services.GetRequiredService<IHelloService>();
            //helloService1.Hello();

            //var helloService2 = host.Services.GetRequiredService<IHelloService>();
            //helloService2.Hello();



            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            .ConfigureServices((ctx, services) =>
            {
                //services.AddTransient<IHelloService, HelloService>();
                services.AddScoped<IHelloService, HelloService>();

            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
