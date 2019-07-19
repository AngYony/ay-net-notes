using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample2.Models;
using MvcCookieAuthSample2.ViewModels;

namespace MvcCookieAuthSample2.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new ApplicationUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                NormalizedUserName = registerViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
            if (identityResult.Succeeded)
            {
                //登录，该方法的本质也是调用的HTTPContext.SingnInAsync方法
               await _signInManager.SignInAsync(identityUser, new AuthenticationProperties { IsPersistent = true });


                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegisterViewModel loginViewModel){
           var user=await _userManager.FindByEmailAsync(loginViewModel.Email);
           if(user==null){
                
           }

            await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }





        //public IActionResult MakeLogin()
        //{
        //    var claims = new List<Claim>{
        //        new Claim(ClaimTypes.Name,"jesse"),
        //        new Claim(ClaimTypes.Role,"admin")
        //    };

        //    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //    new ClaimsPrincipal(claimIdentity));

        //    return Ok();
        //}

        //public IActionResult MakeLogout()
        //{
        //    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return Ok();
        //}

       
    }
}