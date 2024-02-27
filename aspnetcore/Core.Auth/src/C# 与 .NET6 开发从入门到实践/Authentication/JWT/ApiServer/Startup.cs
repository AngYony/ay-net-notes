using JwtLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiServer;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        var jwtConfig = Configuration.GetSection("jwtToken");
        services.Configure<JwtTokenOptions>(jwtConfig);
        var jwtOption = jwtConfig.Get<JwtTokenOptions>();

        services.AddAuthentication(x =>
        {
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            // 注册JWT身份验证架构
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    // 这里就是关键，签名证书、颁发者名称等和颁发服务一致才能正确验证
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret)),
                    ValidIssuer = jwtOption.Issuer,
                    ValidAudience = jwtOption.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }

    public void Configure(IApplicationBuilder app)
    {
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
