using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MvcCookieAuthSample3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample3.Data
{
    public class ApplicationDBContextSeed
    {
        private UserManager<ApplicationUser> _userManager;


        public async Task SeedAsync(ApplicationDbContext context,IServiceProvider services){
            if(!context.Users.Any()){
                _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser = new ApplicationUser
                {
                    UserName = "Administrator",
                    Email = "smallz@163.com",
                    NormalizedUserName = "admin"
                };


                var result = await _userManager.CreateAsync(defaultUser,"Abcd-1234"); //设置的密码要满足要求，否则不能初始化成功
                if(!result.Succeeded)
                {
                    throw new Exception("初始默认用户失败！");
                }
            }

             
        }
    }
}
