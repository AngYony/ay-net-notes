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
            //    //��������쳣���һ�����Development�����м���ᱻ���ã���ʾ�����쳣ҳ�档
            //    app.UseDeveloperExceptionPage();
            //}

            app.Use(async (context, next)=> {

                logger.LogInformation("MW1:��������");
                
                //next������ܵ�����һ���м����ͨ��ί��
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("abcd");
                await next();

                logger.LogInformation("MW1:������Ӧ");
            });

            app.Use(async (context, next) => {
                logger.LogInformation("MW2:��������");

                //next������ܵ�����һ���м����ͨ��ί��
                //context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("efg");
                await next();

                logger.LogInformation("MW2:������Ӧ");

            });



            //Run()����ֻ�ܽ�һ���ն��м����ӵ�����ܵ����ն��м����ʹ�ܵ���·�������������һ���м����
            app.Run(async (context) =>
            {
                //��ֹ����
                //context.Response.ContentType = "text/plain;charset=utf-8";
                //wy�������û����ܣ��Ҽ���Ŀ�������û����ܣ�
                await context.Response.WriteAsync(_configuration["wy"]);
                logger.LogInformation("MW3:��������������Ӧ");

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
