using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ay.Startup.Frame.Sample
{
    public class Startup2
    {
        private readonly ILogger logger;

        public IConfiguration Configuration { get; }




        //重点：此处必须写作ILogger<Startup>
        public Startup2(IConfiguration _configuration, ILogger<Startup2> _logger)
        {
            this.Configuration = _configuration;
            this.logger = _logger;

        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            logger.LogDebug("测试日志组件是否可用");

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //添加数据库上下文
            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("myMemoryDb"));

            //添加会话支持
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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
                app.UseExceptionHandler("/Home/Error");
                //2、HTTP 严格传输安全协议
                app.UseHsts();
            }

            #region 此处添加自定义中间件

            #endregion



            //3、HTTPS 重定向
            app.UseHttpsRedirection();
            //4、静态文件服务器
            app.UseStaticFiles();
            //5、Cookie 策略实施
            app.UseCookiePolicy();
            //6、身份验证
            app.UseAuthentication();
            //7、会话
            app.UseSession(); //必须在UseMvc之前添加，否则会发生InvalidOperationException异常
            //8、MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Use(async (context, next) => {
            //    //如果此处不调用next参数，将会使管道短路
            //    await next.Invoke();
            //});



            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
