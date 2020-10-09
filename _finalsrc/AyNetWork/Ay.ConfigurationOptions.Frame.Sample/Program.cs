using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ay.ConfigurationOptions.Frame.Sample.CusConfigurationProvider;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ay.ConfigurationOptions.Frame.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) => {
                
                config.AddAyConfiguration(ayinfo=> {
                    //为委托指定操作
                    ayinfo.Value = ayinfo.Key + ayinfo.Value;
                });
            })
            .UseStartup<Startup>();
    }
}
