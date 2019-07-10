using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ay.Host.Frame.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();


            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            //重写Host配置
            var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("hostsettings.json", optional: true)
            .AddCommandLine(args);


            var hostConfig = configBuilder.Build();






            return WebHost.CreateDefaultBuilder(args)

             //设置应用程序名称，默认值为包含应用入口点的程序集的名称。不常用
             .UseSetting(WebHostDefaults.ApplicationKey, "MyApplicationName")

             //设置是否捕获启动错误，默认为false
             .CaptureStartupErrors(true)

             //设置内容根文件夹，默认为应用程序集所在的文件夹,内容根也用作 Web 根设置的基路径。 如果路径不存在，主机将无法启动。
             .UseContentRoot("c:\\<content-root>")
             //设置应用的静态资产的相对路径，如果未指定，默认值是“(Content Root)/wwwroot”（如果该路径存在）。
             .UseWebRoot("public")

             //确定是否应捕获详细错误，默认值为false
             .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")

             //设置应用的环境，默认为Production
             .UseEnvironment(EnvironmentName.Development)

             //设置应用的承载启动程序集,承载启动程序集的以分号分隔的字符串在启动时加载。
             //.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "assembly1;assembly2")

             //设置应用在启动时，应排除的承载启动程序集
             //.UseSetting(WebHostDefaults.HostingStartupExcludeAssembliesKey, "assembly1;assembly2")

             //设置是否阻止承载启动程序集的自动加载
             .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")

             //设置为服务器应响应的以分号分隔 (;) 的 URL 前缀列表。
             .UseUrls("http://*:5000;http://localhost:5001;https://hostname:5002")

             //设置 HTTPS 重定向端口。
             .UseSetting("https_port", "8080")

              //设置Host是否应该侦听使用 WebHostBuilder 配置的 URL，默认值为true
              .PreferHostingUrls(false)

              //指定等待 Web 主机关闭的时长
              .UseShutdownTimeout(TimeSpan.FromSeconds(10))

             .ConfigureKestrel((context, options) =>
             {

             })
             //重写Host配置，针对的是Host，但该配置同样会作用于应用配置中，应用配置使用ConfigureAppConfiguration方法
             .UseConfiguration(hostConfig)
             .ConfigureAppConfiguration((hostingContext, appconfigbuilder) => {
                //基于应用的配置   
             })

            .UseStartup<Startup>();

        }
    }
}
