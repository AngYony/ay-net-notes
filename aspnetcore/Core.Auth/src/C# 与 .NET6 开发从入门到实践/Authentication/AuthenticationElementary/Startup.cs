using Microsoft.AspNetCore.Authentication.Cookies;

namespace AuthenticationElementary;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 注册身份认证服务
        services.AddAuthentication(options =>
        {
            // 默认方案，就是个字符串标识，在没有指定更具体的方案时提供默认回退
            // 方案必须在下面注册详细配置
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // 默认身份验证方案，在验证身份时会覆盖默认方案的设置
            // 除此之外还有默认登录、注销、质询、拒绝等方案，均会在相应情况下覆盖回退用的默认方案
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            // 使用指定的方案名注册Cookie方案
            // 此方案在上面被配置为默认身份验证和回退方案
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                // 该方案的登录地址
                // 匿名访问需要授权的地址时供自动跳转功能使用，框架内置的身份认证组件会自动使用，自定义授权组件时需自行编写代码实现同样的功能
                options.LoginPath = "/Account/Login";
                // 该方案的注销地址
                options.LogoutPath = "/Account/Logout";
            });

        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        // 配置身份认证中间件
        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}
