using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace My.ApplicationParts.Study
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var assembly = typeof(Startup).GetTypeInfo();
            //var part = new AssemblyPart(assembly);




            //var testAssembly = Assembly.Load("My.ApplicationPart.Test.Study");



            services.AddMvc()
            .ConfigureApplicationPartManager(apm =>
            {
                //var aps = apm.ApplicationParts;

                //var dependentLibrary = apm.ApplicationParts.FirstOrDefault(part => part.Name == "SmallZ");
                //if (dependentLibrary != null)
                //{
                //    apm.ApplicationParts.Remove(dependentLibrary);
                //}


               apm.FeatureProviders.Add(new WyControllerFeatureProvider());

            })


            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();

        }
    }
}
