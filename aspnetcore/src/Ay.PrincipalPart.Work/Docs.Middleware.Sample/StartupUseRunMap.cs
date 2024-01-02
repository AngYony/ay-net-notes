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
    public class StartupUseRunMap
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }


        private static void HandleTestMap1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string p = context.Request.Path;
                string b = context.Request.PathBase;
                Console.WriteLine("Request.Path:" + p);
                Console.WriteLine("Request.PathBase:" + b);
                await context.Response.WriteAsync("Test Map 1, " + context.Request.PathBase);
            });
        }

        private static void HandleTestMap2(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Test Map 2, " + context.Request.PathBase));
        }

        private static void HandleTestLevelMap(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Test Map level , " + context.Request.PathBase));
        }

        private static void HandleTestLevelMap2(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Test Map level 2, " + context.Request.PathBase));
        }

        private static void HandleTestLevelMap3(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Test Map level 3, " + context.Request.PathBase));
        }


        private static void HandleTestMapWhen(IApplicationBuilder app)
        {
            app.Run(async context => {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Test MapWhen, Branch used={branchVer}");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region  Map的简单使用
            // 匹配规则：/map1
            app.Map("/map1", HandleTestMap1);
            // 匹配规则：/map2
            app.Map("/map2", HandleTestMap2);
            #endregion


            #region Map的嵌套
            //app.Map("/level", levelApp =>
            //{



            //    levelApp.Map("/l2", levelApp2 =>
            //    {
            //        //匹配规则：/level/l2/l3
            //        levelApp2.Map("/l3", HandleTestLevelMap3);
            //    });

            //    //一旦加入该行代码，下述的l3将失效
            //    //levelApp.Map("/l2", HandleTestLevelMap2);

            //});
            #endregion

            #region Map匹配多段
            app.Map("/level", levelApp =>
            {
                //匹配规则：/level/l2/l3
                levelApp.Map("/l2/l3", HandleTestLevelMap3);
                //匹配规则：/level/l2
                levelApp.Map("/l2", HandleTestLevelMap2);
            });
            #endregion

            //MapWhen的使用
            //匹配：?branch=
            app.MapWhen(
               context => context.Request.Query.ContainsKey("branch"),
               HandleTestMapWhen);


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
