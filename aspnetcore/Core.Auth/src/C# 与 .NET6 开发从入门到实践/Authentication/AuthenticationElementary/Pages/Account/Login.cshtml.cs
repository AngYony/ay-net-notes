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

        // ��������б�
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, LoginViewModel.UserName),
            new Claim("FullName", LoginViewModel.UserName),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

        // ʹ��ָ���������б����֤����ʵ�������
        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // �����֤����չ����
        var authProperties = new AuthenticationProperties
        {
            // �Ƿ�����ˢ�������֤�Ự
            //AllowRefresh = <bool>,

            // �����֤Ʊ�ݵľ��Թ���ʱ��
            // ��Cookies��ָCookie��Ч��
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),

            // �Ƿ�����Ч
            // ��Cookies��ָ�Ƿ�Ӧ�ó־û�Cookie
            IsPersistent = rememberMe,

            // ��ʱ�䷢�������֤Ʊ��
            IssuedUtc = DateTimeOffset.UtcNow,

            // ��½�����תUrl
            RedirectUri = redirectUri,
        };

        // ʹ��ָ�������ʵ�����������
        var identityPrincipal = new ClaimsPrincipal(claimsIdentity);

        // ʹ��ָ���ķ���������������չ���Ե�¼�û���������RedirectUriʱ���Զ���ת��ָ����ַ
        // �����ָ����¼������ʹ��Ĭ�ϵ�¼���������Ҳû������Ĭ�ϵ�¼��������˵�Ĭ�Ϸ�������û������Ĭ�Ϸ����������쳣
        // �˴�ָ���ķ���������Startup��ע��
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            identityPrincipal,
            authProperties);
    }
}

