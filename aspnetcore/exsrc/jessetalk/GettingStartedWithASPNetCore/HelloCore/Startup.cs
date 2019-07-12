using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IConfiguration configuration,IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            RequestDelegate handler = context => context.Response.WriteAsync("this is a aciton");
            var route = new Route(
            new RouteHandler(handler),
            "action",
            app.ApplicationServices.GetRequiredService<IInlineConstraintResolver>());

            //方式一
            app.UseRouter(route);
            //方式二
            app.UseRouter(builder =>
            {
                builder.MapGet("action2", async context =>
                {
                    await context.Response.WriteAsync("this is a aciton2");
                });
            });


            applicationLifetime.ApplicationStarted.Register(()=> {
                Console.WriteLine("Started");
            
            });

            applicationLifetime.ApplicationStopped.Register(()=> {
                Console.WriteLine("Stopped");

            });

            applicationLifetime.ApplicationStopping.Register(() => {
                Console.WriteLine("Stopping");

            });


            //不建议复杂的应用使用Map方法，该方法的第二个委托参数taskapp（IApplicationBuilder类型）和这里的app不是同一个对象
            app.Map("/task", taskapp => {

                taskapp.Run(async context => {

                   await context.Response.WriteAsync("this is a task"); 
                });
            });


            app.Use( async (context, next) =>
            {
                await context.Response.WriteAsync("1: before start...");

                await next.Invoke();
            
            });

            app.Use(next=> {
                return (context) => {
                    context.Response.WriteAsync("2: in the middle of start...");

                    return next(context);
                };
                
            });



            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("3:start....");
                await context.Response.WriteAsync($"ContentRootPath:{env.ContentRootPath}");
                await context.Response.WriteAsync($"WebRootPath:{env.WebRootPath}");

                await context.Response.WriteAsync(configuration["ay"]);
            });
        }
    }
}
