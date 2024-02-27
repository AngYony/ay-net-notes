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
            // ע��ָ���������˻�
            // �����ָ��������ʹ��Ĭ��ע��������û����������˵�Ĭ�Ϸ�����Ҳû������Ĭ�Ϸ����������쳣
            // �˴�ָ���ķ���������Startup��ע��
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(redirectUri);
        }
        else
        {
            return Page();
        }
    }
}
