using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;

namespace ApiServer;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // 注册OpenIddict身份认证方案
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });

        // 注册OpenIddict验证组件
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                // 注意：验证处理程序使用OAuth发现文档端点来检索内省端点的地址和请求验证令牌签名的RSA公钥
                options.SetIssuer("https://localhost:<OpenIddict服务端项目的端口>/");
                // 验证令牌的受理人是否包含API服务，可以确定令牌是不是为API服务颁发的
                options.AddAudiences("api");

                # region 远程验证令牌
                options
                   // 将验证处理程序配置为使用内省验证模式
                   .UseIntrospection()
                   // 注册与远程内省端点通信时使用的客户端凭据
                   .SetClientId("api")
                   .SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");
               #endregion

               # region 本地验证令牌
               // 配置解密令牌的对称密钥，需要和服务端的加密密钥保持一致
               // 如果令牌是明文的，可以不用配置
               options.AddEncryptionKey(new SymmetricSecurityKey(
                   Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));
               #endregion

                // 使用ASP.NET Core的HTTP客户端服务和OAuth服务通信
                // 本地验证模式下也要注册，用来请求令牌签名的RSA公钥
                options.UseSystemNetHttp();

                // 注册适用于ASP.NET Core的相关服务
                options.UseAspNetCore();
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();

        app.UseRouting();

        // 配置身份认证和授权中间件
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
