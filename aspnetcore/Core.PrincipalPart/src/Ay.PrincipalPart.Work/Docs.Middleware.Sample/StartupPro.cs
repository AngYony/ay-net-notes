using Docs.Middleware.Sample.Data;
using Docs.Middleware.Sample.Pro;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Middleware.Sample
{
    public class StartupPro
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            //添加数据库上下文
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("myMemoryDb"));

            //将工厂激活的中间件添加到内置容器中
            services.AddTransient<FactoryActivatedMiddleware>();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();

            //在请求处理管道中注册中间件
            app.UseConventionalMiddleware();
            app.UseFactoryActivatedMiddleware();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();

        }
    }
}
