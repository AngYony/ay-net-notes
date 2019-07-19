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

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private void AddErrors(IdentityResult identityResult){
            foreach (var err in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
                }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {

                ViewData["ReturnUrl"] = returnUrl;

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
                    return RedirectToLocal(returnUrl);
                }
                else{
                    AddErrors(identityResult);
                }
            }
            return View();
        }


        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;

                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user == null)
                {
                    return View();
                }

                await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = true });


                return RedirectToLocal(returnUrl);
            }
            return View();


            //return RedirectToAction("Index", "Home");
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