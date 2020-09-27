using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp2.Areas.Identity.Data;
using WebApp2.Models;

[assembly: HostingStartup(typeof(WebApp2.Areas.Identity.IdentityHostingStartup))]
namespace WebApp2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebApp2Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebApp2ContextConnection")));

                services.AddDefaultIdentity<WebApp2User>()
                    .AddEntityFrameworkStores<WebApp2Context>();
            });
        }
    }
}