using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ay.Host.Frame.Sample
{
    public class Program2
    {
         
        public static void Main2(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseConfiguration(config)
                .Configure(app =>
                {
                    app.Run(context =>
                        context.Response.WriteAsync("Hello, world!"));
                });
        }


        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

        //    WebHost.CreateDefaultBuilder(args)
        //    //设置IHostingEnvironment.ApplicationName属性值
        //    .UseSetting(WebHostDefaults.ApplicationKey, "MyAppName")

        //    .ConfigureAppConfiguration((hostingContext, config) =>
        //    {
        //        config.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);
        //    })
        //    .ConfigureLogging(logging =>
        //    {
        //        logging.SetMinimumLevel(LogLevel.Warning);
        //    })
        //    .ConfigureKestrel((context, options) =>
        //    {
        //        options.Limits.MaxRequestBodySize = 20_000_000;
        //    })
        //    .UseUrls("http://localhost:50003")


        //    .UseStartup<Startup>();
    }
}

