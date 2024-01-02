using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Docs.Middleware.Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //1、异常/错误处理
                app.UseExceptionHandler("/Error");
                //2、HTTP 严格传输安全协议
                app.UseHsts();
            }
            //3、HTTPS 重定向
            app.UseHttpsRedirection();
            //4、静态文件服务器
            app.UseStaticFiles();
            //5、Cookie 策略实施
            app.UseCookiePolicy();
            //6、身份验证
            app.UseAuthentication();
            //7、会话
            app.UseSession();
            //8、MVC
            app.UseMvc();




            app.Use(async (context, next) => {
                //如果此处不调用next参数，将会使管道短路
                await next.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
