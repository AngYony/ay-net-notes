using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            
            using(var scope=host.Services.CreateScope()){
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SchoolContext>();

                    //DbInitializer.Initializer(context);
                    
                    //EnsureCreated 确保存在上下文数据库。 如果存在，则不需要任何操作。 如果不存在，则会创建数据库及其所有架构。
                    //EnsureCreated 不使用迁移创建数据库。 使用 EnsureCreated 创建的数据库稍后无法使用迁移更新。
                   //var _a= context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {

                    var logger= services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "创建数据库时发生错误");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
