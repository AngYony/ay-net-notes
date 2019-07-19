using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample2.Data
{
    public static class WebHostMigrationExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> sedder) where TContext : DbContext
        {
            using(var scope=host.Services.CreateScope()){
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    context.Database.Migrate();

                    sedder(context, services);

                    logger.LogInformation($"执行DBContext { typeof(TContext).Name} Seed方法成功");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"执行DBContext{typeof(TContext).Name} Seed方法失败");
                }
            }

            return host;
        }
    }

}
