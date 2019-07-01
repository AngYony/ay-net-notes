using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docs.Options.Sample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Docs.Options.Sample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MyOptions>(Configuration);
            services.Configure<MyOptionsWithDelegateConfig>(myconfig =>
            {
                myconfig.Option1 = "value1_configured_by_delegate";
                myconfig.Option2 = 500;
            });

            services.Configure<MySubOptions>(Configuration.GetSection("subsection"));


            services.Configure<MyOptions>("named_options_1", Configuration);

            services.Configure<MyOptions>("named_options_2", myOptions =>
            {
                myOptions.Option1 = "named_options_2_value1_from_action";
            });

            services.ConfigureAll<MyOptions>(myOptions =>
            {
                myOptions.Option1 = "ConfigureAll replacement value";
            });

            services.AddOptions<MyOptions>().Configure(o => o.Option1 = "addOptions1");

            services.AddOptions<MyOptions>("wyOptions").Configure(o => o.Option2 = 333);


            //        services.AddOptions<MyOptions>("optionalName")
            //.Configure<Service1, Service2, Service3, Service4, Service5>(
            //    (o, s, s2, s3, s4, s5) =>
            //        o.Property = DoSomethingWith(s, s2, s3, s4, s5));




            services.AddOptions<MyOptions>("optionalOptionsName")
            .Configure(o => { })
            .Validate(o => {
                //return YourValidationShouldReturnTrueIfValid(o);
                return true; 
            },"custom error");




            //获取服务
            var monitor = services.BuildServiceProvider().GetService<IOptionsMonitor<MyOptions>>();

            try
            {
                var options = monitor.Get("optionalOptionsName");
            }
            catch (OptionsValidationException e)
            {
                // e.OptionsName returns "optionalOptionsName"
                // e.OptionsType returns typeof(MyOptions)
                // e.Failures returns a list of errors, which would contain 
                //     "custom error"
            }


            services.PostConfigure<MyOptions>(myOptions =>
            {
                myOptions.Option1 = "post2ww_configured_option1_value";
            });

            services.PostConfigure<MyOptions>("named_options_1", myOptions =>
            {
                myOptions.Option1 = "postqq11_configured_option1_value";
            });

            services.PostConfigureAll<MyOptions>(myOptions =>
            {
                myOptions.Option1 = "6666666post_configured_option1_value";
            });



            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptionsMonitor<MyOptions> optionsAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();

            var option1 = optionsAccessor.CurrentValue.Option1;
        }
    }
}
