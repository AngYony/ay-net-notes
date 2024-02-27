using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CustomAuthentication;

public class MyHandler : AuthenticationHandler<MyOption>
{
    public const string SchemeName = "myScheme";

    public MyHandler(IOptionsMonitor<MyOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "MyUser"),
            new Claim("FullName", "MyUser"),
            new Claim(ClaimTypes.Role, "Administrator"),
            new Claim("Text", Options.Text)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, SchemeName);

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal,Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
