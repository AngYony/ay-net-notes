using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AuthenticationElementary.Pages.Account;

public class LoginViewModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginViewModel LoginViewModel { get; set; }

    public void OnGet() { }

    public async Task OnPostAsync(bool rememberMe = false, string redirectUri = "/")
    {
        if (HttpContext.User.Identity.IsAuthenticated || string.IsNullOrEmpty(LoginViewModel.UserName) || LoginViewModel.Password != "123123")
        {
            return;
        }

        // 身份声明列表
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, LoginViewModel.UserName),
            new Claim("FullName", LoginViewModel.UserName),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

        // 使用指定的声明列表和认证方案实例化身份
        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // 身份认证的扩展属性
        var authProperties = new AuthenticationProperties
        {
            // 是否允许刷新身份认证会话
            //AllowRefresh = <bool>,

            // 身份认证票据的绝对过期时间
            // 在Cookies中指Cookie有效期
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),

            // 是否长期有效
            // 在Cookies中指是否应该持久化Cookie
            IsPersistent = rememberMe,

            // 何时颁发的身份认证票据
            IssuedUtc = DateTimeOffset.UtcNow,

            // 登陆后的跳转Url
            RedirectUri = redirectUri,
        };

        // 使用指定的身份实例化身份主体
        var identityPrincipal = new ClaimsPrincipal(claimsIdentity);

        // 使用指定的方案、身份主体和扩展属性登录用户，设置了RedirectUri时会自动跳转到指定地址
        // 如果不指定登录方案则使用默认登录方案，如果也没有设置默认登录方案则回退到默认方案，还没有设置默认方案则引发异常
        // 此处指定的方案必须在Startup中注册
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            identityPrincipal,
            authProperties);
    }
}

