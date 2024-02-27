using OpenIddict.Abstractions;
using OpenIddictServer.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpenIddictServer;

public class OAuthClientInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public OAuthClientInitializer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var consoleClientId = "console";
        var webApiClientId = "api";

        var webApiScopeName = "api";

        await CreateApplicationsAsync();
        await CreateScopesAsync();

        // 初始化客户端信息
        async Task CreateApplicationsAsync()
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync(consoleClientId) is null)
            {
                // 备案控制台客户端
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    // 基本信息
                    ClientId = consoleClientId,
                    ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                    DisplayName = "Console Application",

                    // 权限信息
                    Permissions =
                    {
                        // 允许访问令牌端点
                        Permissions.Endpoints.Token,
                        // 允许使用客户端证书授权模式
                        Permissions.GrantTypes.ClientCredentials,
                        // 允许访问自定义的api作用域
                        Permissions.Prefixes.Scope + webApiScopeName
                    }
                });
            }

            // 备案表示API服务的客户端
            if (await manager.FindByClientIdAsync(webApiClientId) is null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    // 基本信息
                    ClientId = webApiClientId,
                    ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",

                    // 权限信息
                    Permissions =
                    {
                        // 允许访问内省端点
                        Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(descriptor);
            }
        }

        // 初始化自定义作用域
        async Task CreateScopesAsync()
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync(webApiScopeName) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    // 作用域名称
                    Name = webApiScopeName,

                    // 允许访问的资源
                    Resources =
                    {
                        // 允许访问api客户端
                        // 令牌中会包含受理人为api的声明，表示令牌是颁发给api用的
                        webApiClientId
                    }
                };

                await manager.CreateAsync(descriptor);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

