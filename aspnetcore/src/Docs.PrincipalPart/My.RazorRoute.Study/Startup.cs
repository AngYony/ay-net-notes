using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using My.RazorRoute.Study.Conventions;
using My.RazorRoute.Study.Factories;
using My.RazorRoute.Study.Filters;
using My.RazorRoute.Study.Transformer;

namespace My.RazorRoute.Study
{

    public class Startup
    {
        private readonly ILoggerFactory loggerFacotry;

        public IConfiguration Configuration { get; }

        public Startup(ILoggerFactory _loggerFactory, IConfiguration configuration)
        {
            this.loggerFacotry = _loggerFactory;
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
             
            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    //options.Conventions.Add(
                    //    new GlobalTemplatePageRouteModelConvention(
                    //        loggerFacotry.CreateLogger<GlobalTemplatePageRouteModelConvention>()
                    //    ));

                    //options.Conventions.Add(new GlobalHeaderPageApplicationModelConvention());
                    //options.Conventions.Add(new GlobalPageHandlerModelConvention());

                    //options.Conventions.AddFolderRouteModelConvention("/OtherPages", model =>
                    //{
                    //    var selectorCount = model.Selectors.Count;
                    //    for (var i = 0; i < selectorCount; i++)
                    //    {
                    //        var selector = model.Selectors[i];
                    //        model.Selectors.Add(new SelectorModel
                    //        {
                    //            AttributeRouteModel = new AttributeRouteModel
                    //            {
                    //                Order = 2,
                    //                Template = AttributeRouteModel.CombineTemplates(
                    //                    selector.AttributeRouteModel.Template,
                    //                    "{otherPagesTemplate?}"
                    //                    )
                    //            }
                    //        });

                    //    }

                    //});



                    //options.Conventions.AddPageRouteModelConvention("/About", model =>
                    //{
                    //    var selectorCount = model.Selectors.Count;
                    //    for (int i = 0; i < selectorCount; i++)
                    //    {
                    //        var selector = model.Selectors[i];
                    //        model.Selectors.Add(new SelectorModel
                    //        {
                    //            AttributeRouteModel = new AttributeRouteModel
                    //            {
                    //                Order = 2,
                    //                Template = AttributeRouteModel.CombineTemplates(
                    //                      selector.AttributeRouteModel.Template,
                    //                      "{aboutTemplate?}"
                    //                      )
                    //            }
                    //        });

                    //    }

                    //});


                    //options.Conventions.Add(
                    //    new PageRouteTransformerConvention(
                    //        new SlugifyParameterTransformer()
                    //        ));


                    //options.Conventions.AddPageRoute("/Contact", "ThecontactPage/{text?}");


                    //options.Conventions.AddFolderApplicationModelConvention("/OtherPages", model =>
                    //{

                    //    model.Filters.Add(new AddHeaderAttribute(
                    //        "OtherPagesHeader", new string[] { "OtherPages Header Value" }));
                    //});


                    //options.Conventions.AddPageApplicationModelConvention("/About", model =>
                    //{
                    //    model.Filters.Add(new AddHeaderAttribute("AboutHeader", new string[] { "About Header Value" }));

                    //});


                    //options.Conventions.ConfigureFilter(model =>
                    //{
                    //    if (model.RelativePath.Contains("OtherPages/Page2"))
                    //    {
                    //        return new AddHeaderAttribute("OtherPagesPage2Header"
                    //            , new string[] { "OtherPages/Page2 Header Value" });
                    //    }
                    //    return new EmptyFilter();
                    //});


                    options.Conventions.ConfigureFilter(new AddHeaderWithFactory());


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
            app.UseMvc();

            
        }
    }
}
