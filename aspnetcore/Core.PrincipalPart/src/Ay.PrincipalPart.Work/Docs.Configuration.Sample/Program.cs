using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Docs.Configuration.Sample.AyConfigurationProvider;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Docs.Configuration.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        static Dictionary<string, string> arrayDict = new Dictionary<string, string>
        {
            {"array:entries:0", "value0"},
            {"array:entries:1", "value1"},
            {"array:entries:2", "value2"},
            {"array:entries:4", "value4"},
            {"array:entries:5", "value5"}
        };

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

            WebHost.CreateDefaultBuilder(args)

            .ConfigureAppConfiguration((hostingContext, config) =>
            {

                config.SetBasePath(Directory.GetCurrentDirectory());

                config.AddInMemoryCollection(arrayDict);

                config.AddJsonFile("json_array.json", optional: false, reloadOnChange: false);
                config.AddJsonFile("starship.json", optional: false, reloadOnChange: false);
                config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);


                //自定义配置提供程序
                config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));

                config.AddCommandLine(args);
            })
            .UseStartup<Startup>();
    }
}
