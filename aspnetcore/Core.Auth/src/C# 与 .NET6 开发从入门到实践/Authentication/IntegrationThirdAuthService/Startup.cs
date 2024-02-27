using AspNet.Security.OAuth.Gitee;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace IntegrationThirdAuthService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // 注册身份认证服务
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GiteeAuthenticationDefaults.AuthenticationScheme;
        })
            // 使用指定的方案名注册Cookie方案
            // 此方案在上面被配置为默认方案
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/signin";
                options.LogoutPath = "/signout";
            })
            .AddGitee(options =>
            {
                // 创建应用时自动生成
                options.ClientId = "<你申请的client id>";
                // 在应用设置中创建或重置后生成
                options.ClientSecret = "<服务商提供的secret>";

                // 是否要把令牌（access_token、refresh_token）保存到扩展验证属性。使用Cookie方案登录时令牌会序列化到Cookie中
                // ASP.NET Core的Cookie已使用数据保护服务加密，但通常情况下令牌不应该以任何方式传输到前端
                // 默认在完成登陆后会被丢弃，无法重新读取和使用，除非再次向OAuth服务器请求令牌
                options.SaveTokens = true;

                // 用于生成符合OAuth服务器安全要求的请求Url（回调Url在创建应用时填写）和判断请求是否为OAuth服务器的回调以决定是要直接处理请求还是向后传递请求
                // 不需要编写回调端点，回调由验证处理器直接完成，内置的基类已经实现了回调的功能（用code兑换令牌）
                options.CallbackPath = "/gitee-oauth";

                // 创建身份票据时会触发此事件
                options.Events.OnCreatingTicket = static async context =>
                {
                    // 可以通过事件参数获取令牌
                    // 如果不存储令牌，这是唯一一次读取并处理令牌的机会
                    // 例如把令牌存储到缓存中，这样就可以在不把令牌传输到前端的情况下保留并重复使用令牌
                    //context.Properties.GetTokens();
                    //context.AccessToken;
                    //context.RefreshToken;
                };
            });

        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        //app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}
