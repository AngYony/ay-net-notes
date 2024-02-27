using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntegrationThirdAuthService.Pages.Account;


public class Signout : PageModel
{
    public void OnGet() { }

    public async Task OnPostAsync() => await HttpContext.SignOutAsync();
}
