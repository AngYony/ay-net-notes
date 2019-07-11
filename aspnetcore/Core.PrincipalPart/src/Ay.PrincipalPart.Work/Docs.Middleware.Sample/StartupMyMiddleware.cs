using Docs.Middleware.Sample.Dev;
using Docs.Middleware.Sample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Middleware.Sample
{
    public class StartupMyMiddleware
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();


            app.Use((context, next) =>
            {
                var stuQuery = context.Request.Query["s"];
                if (!string.IsNullOrWhiteSpace(stuQuery))
                {
                    //获取值
                    var student = new Student(stuQuery);
                    MyClass.StudentA = student;
                    MyClass.StudentB = student;
                }
                return next();
            });

            app.UseOctOceanMiddleware();


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    $"Hello {MyClass.StudentB.Name}");
            });
        }
    }
}
