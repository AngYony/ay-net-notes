using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MvcCookieAuthSample3.Data;

namespace MvcCookieAuthSample3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
            //.MigrateDbContext<ApplicationDbContext>((context,services)=> 
            //{
            //    new ApplicationDBContextSeed().SeedAsync(context, services).Wait();

            //})
            .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://localhost:5000")
                .UseStartup<Startup>();
    }
}
