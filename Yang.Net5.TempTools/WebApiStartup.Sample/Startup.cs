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

namespace WebApiStartup.Sample
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
                //配置全局过滤器
                options.Filters.Add(new GlobalExceptionFilter());
            })
            //必须在AddControllers之后配置
            .ConfigureApiBehaviorOptions(options =>
            {
                //配置模型验证失败时，仍然执行过滤器操作
                options.SuppressModelStateInvalidFilter = true;

                //设置模型验证失败时的全局处理操作，一般不需要指定，通常采用过滤器的形式处理模型绑定错误
                //如果想要设置的InvalidModelStateResponseFactory生效，需要将options.SuppressModelStateInvalidFilter = true;注释起来
                //options.InvalidModelStateResponseFactory = context =>
                //{
                //options.SuppressConsumesConstraintForFormFileParameters = true;
                //options.SuppressInferBindingSourcesForParameters = true;

                //options.SuppressMapClientErrors = true;
                //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                //    "https://httpstatuses.com/404";
                //options.DisableImplicitFromServicesParameters = true;

                //    var result = new BadRequestObjectResult(context.ModelState);
                //    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                //    result.ContentTypes.Add(MediaTypeNames.Application.Xml);
                //    return result;
                //};
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiStartup.Sample", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiStartup.Sample v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
