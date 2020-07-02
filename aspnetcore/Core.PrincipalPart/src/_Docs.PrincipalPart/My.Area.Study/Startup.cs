using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace My.Area.Study
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
             

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "WyAreaRoute",
                    areaName: "Wy", //必须是真实存在的Area名称
                    template: "MyWy/{controller=Home}/{action=Index}/{id?}"
                );

                //        routes.MapRoute("blog_route", "Manage/{controller}/{action}/{id?}",
                //defaults: new { area = "Wy" }, constraints: new { area = "Wy" });

                //routes.MapRoute(
                //    name: "WyAreas",
                //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                //);

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            


        }
    }
}
