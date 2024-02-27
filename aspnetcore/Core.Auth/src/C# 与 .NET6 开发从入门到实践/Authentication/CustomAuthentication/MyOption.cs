using Microsoft.AspNetCore.Authentication;

namespace CustomAuthentication;

public class MyOption : AuthenticationSchemeOptions
{
    public string Text { get; set; }
}
