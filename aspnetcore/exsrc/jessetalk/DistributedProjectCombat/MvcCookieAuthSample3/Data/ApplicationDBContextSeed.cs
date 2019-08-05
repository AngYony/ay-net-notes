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
        private RoleManager<ApplicationUserRole> _roleManager;

        public async Task SeedAsync(ApplicationDbContext context,IServiceProvider services){

            if (!context.Roles.Any())
            {
                _roleManager = services.GetRequiredService < RoleManager<ApplicationUserRole>>();
                var role = new ApplicationUserRole() { Name = "Administrators", NormalizedName = "Administrators" };
                await _roleManager.CreateAsync(role);

            }
            if (!context.Users.Any()){
                _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser = new ApplicationUser
                {
                    UserName = "Administrator",
                    Email = "smallz@163.com",
                    NormalizedUserName = "admin",
                    Avatar="",
                    SecurityStamp="admin"
                };
                await _userManager.AddToRoleAsync(defaultUser, "Administrators");


                var result = await _userManager.CreateAsync(defaultUser,"Abcd-1234"); //设置的密码要满足要求，否则不能初始化成功
                if(!result.Succeeded){
                    throw new Exception("初始默认角色失败：" + result.Errors.SelectMany(e => e.Description));
                }
          
                
                if(!result.Succeeded)
                {
                    throw new Exception("初始默认用户失败！");
                }
            }

             
        }
    }
}
