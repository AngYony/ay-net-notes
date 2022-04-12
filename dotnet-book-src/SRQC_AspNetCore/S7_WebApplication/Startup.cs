using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace S7_WebApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
         
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //if (env.IsDevelopment())
            //{
            //    //如果存在异常并且环境是Development，此中间件会被调用，显示开发异常页面。
            //    app.UseDeveloperExceptionPage();
            //}

            app.Use(async (context, next)=> {

                logger.LogInformation("MW1:传入请求");
                
                //next：代表管道中下一个中间件的通用委托
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("abcd");
                await next();

                logger.LogInformation("MW1:传出响应");
            });

            app.Use(async (context, next) => {
                logger.LogInformation("MW2:传入请求");

                //next：代表管道中下一个中间件的通用委托
                //context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("efg");
                await next();

                logger.LogInformation("MW2:传出响应");

            });



            //Run()方法只能将一个终端中间件添加到请求管道，终端中间件会使管道短路，而不会调用下一个中间件。
            app.Run(async (context) =>
            {
                //防止乱码
                //context.Response.ContentType = "text/plain;charset=utf-8";
                //wy来自于用户机密（右键项目，管理用户机密）
                await context.Response.WriteAsync(_configuration["wy"]);
                logger.LogInformation("MW3:处理请求并生成响应");

            });



            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
