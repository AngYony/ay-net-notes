using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ay.Startup.Frame.Sample
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly ILoggerFactory loggerFactory;
        private readonly IHostingEnvironment hostingEnvironment; 



        public Startup(IHostingEnvironment _hostingEnvironment,IConfiguration _configuration, ILoggerFactory _loggerFactory){
            configuration = _configuration;
            loggerFactory = _loggerFactory;
            hostingEnvironment = _hostingEnvironment;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = loggerFactory.CreateLogger<Startup>();

            if (hostingEnvironment.IsDevelopment())
            {
                logger.LogInformation("Development environment");
            }
            else
            {
                logger.LogInformation($"Environment: {hostingEnvironment.EnvironmentName}");
            }


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseMvcWithDefaultRoute();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();

            





        }
    }
}
