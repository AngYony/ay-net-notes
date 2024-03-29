using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ExceptionHandling.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             
            

            
            services.AddControllers(options =>
            {
                options.Filters.Add(new GlobalExceptionFilter());
                options.Filters.Add(new HttpResponseExceptionFilter());
            })
            //必须在AddController之后配置
            .ConfigureApiBehaviorOptions(options =>
            {
                //设置当出现模型验证失败时依然执行过滤器操作，而抑制InvalidModelStateResponseFactory的执行
                options.SuppressModelStateInvalidFilter = true; 
                //模型验证处理异常
                options.InvalidModelStateResponseFactory = context =>
                {
                    //options.SuppressConsumesConstraintForFormFileParameters = true;
                    //options.SuppressInferBindingSourcesForParameters = true;
                    
                    //options.SuppressMapClientErrors = true;
                    //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                    //    "https://httpstatuses.com/404";
                    //options.DisableImplicitFromServicesParameters = true;


                    var result = new BadRequestObjectResult(context.ModelState);

                    // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                    return result;
                };
            })

            ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExceptionHandling.Sample", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
                //app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExceptionHandling.Sample v1"));

            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseMiddleware<CusExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
