using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace S9_WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            

            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    //���������쳣���������������
                    SourceCodeLineCount = 5
                };
                
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }

            
            //������ʾ�û��ѺõĴ���ҳ��
            else if(env.IsStaging()|| env.IsProduction() || env.IsEnvironment("UAT") )
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseFileServer();

            app.Run(async (context) =>
            {
                throw new Exception("����һ�����Ե��쳣ҳ��");
                await context.Response.WriteAsync("Hello world");
            });
        }
    }
}