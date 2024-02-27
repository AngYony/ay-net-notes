using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntegrationThirdAuthService.Pages.Account;


public class SigninModel : PageModel
{
    public void OnGet() { }

    public IActionResult OnPost(string provider) => Challenge(provider);
}
