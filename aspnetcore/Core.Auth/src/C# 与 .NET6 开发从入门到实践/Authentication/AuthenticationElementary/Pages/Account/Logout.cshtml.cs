using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationElementary.Pages.Account;

public class LogoutModel : PageModel
{
    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync(string redirectUri = "/")
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            // 注销指定方案的账户
            // 如果不指定方案则使用默认注销方案，没有配置则回退到默认方案，也没有配置默认方案则引发异常
            // 此处指定的方案必须在Startup中注册
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(redirectUri);
        }
        else
        {
            return Page();
        }
    }
}
