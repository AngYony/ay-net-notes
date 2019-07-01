using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Docs.Options.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //通过 CreateDefaultBuilder 从设置文件加载选项配置时，不需要显式设置基路径。
            //.ConfigureAppConfiguration(config=> {
            //    config.SetBasePath(Directory.GetCurrentDirectory())
            //      .AddJsonFile("appsettings.json", optional: true);

            //})
                .UseStartup<Startup>();
    }
}
