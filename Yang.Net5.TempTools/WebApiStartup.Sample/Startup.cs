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
                //����ȫ�ֹ�����
                options.Filters.Add(new GlobalExceptionFilter());
            })
            //������AddControllers֮������
            .ConfigureApiBehaviorOptions(options =>
            {
                //����ģ����֤ʧ��ʱ����Ȼִ�й���������
                options.SuppressModelStateInvalidFilter = true;

                //����ģ����֤ʧ��ʱ��ȫ�ִ���������һ�㲻��Ҫָ����ͨ�����ù���������ʽ����ģ�Ͱ󶨴���
                //�����Ҫ���õ�InvalidModelStateResponseFactory��Ч����Ҫ��options.SuppressModelStateInvalidFilter = true;ע������
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