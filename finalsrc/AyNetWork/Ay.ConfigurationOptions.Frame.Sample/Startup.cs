using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ay.ConfigurationOptions.Frame.Sample.OptionsModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ay.ConfigurationOptions.Frame.Sample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AyOption>(Configuration);

            services.Configure<AyOption>(ay =>
            {
                ay.Option1 = "value1_from_delegate";
                ay.Option2 = 444;
            });


            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(config["AAA"]);
            //});

            app.UseMvcWithDefaultRoute();
        }
    }
}
