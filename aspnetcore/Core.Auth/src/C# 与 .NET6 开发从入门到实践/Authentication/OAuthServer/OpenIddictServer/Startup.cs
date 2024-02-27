using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIddictServer.Data;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpenIddictServer;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // 注册EF Core上下文
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"));

            // 向EF Core上下文注册OpenIddict实体，也可以通过ModelBuilder的重载在EF Core上下文类中注册
            options.UseOpenIddict();
        });

        // 开发用数据库异常页面过滤器
        services.AddDatabaseDeveloperPageExceptionFilter();

        // 注册Identity服务
        services.AddDefaultIdentity<IdentityUser>(options =>
        {
            // 把Identity系统的核心声明类型更改为OpenIddict使用的类型，方便集成
            options.ClaimsIdentity.UserNameClaimType = Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = Claims.Role;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        // 注册Quartz.NET
        services.AddQuartz(options =>
        {
            // 注册基于MSDI的Job工厂
            options.UseMicrosoftDependencyInjectionJobFactory();
            // 注册简单类加载器
            options.UseSimpleTypeLoader();
            // 注册数据存储，实际项目中推荐更换为外部存储以支持分布式部署
            options.UseInMemoryStore();
        });

        // 注册Quartz.NET托管服务，Quartz.NET在独立的托管服务中运行
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        // 注册API和网页服务
        services.AddControllersWithViews();
        services.AddRazorPages();

        // 注册OpenIddict，在后续调用中进行详细配置
        services.AddOpenIddict()
            // 注册OpenIddict核心服务
            .AddCore(options =>
            {
                // 配置为使用EF Core进行数据持久化
                options.UseEntityFrameworkCore()
                    // 配置要使用的上下文类型
                    .UseDbContext<ApplicationDbContext>();

                // 配置Quartz.NET集成.
                // 用于定期清理作废的令牌缓存
                options.UseQuartz();
            })

            // 注册授权功能所需的服务
            .AddServer(options =>
            {
                // 设置服务端点
                // 4.0的端点url配置参数不需要以斜杠开头
                // 部分端点需要自行开发，控制器和页面代码较多，可以到示例项目仓库查看示例代码：https://github.com/openiddict/openiddict-samples/
                options
                   // 设置授权端点，允许功能服务请求授权
                   // 该端点需要开发者亲自编写
                   .SetAuthorizationEndpointUris("connect/authorize")
                   // 设置结束会话端点，允许用户和功能服务退出登录和作废令牌
                   // 该端点需要开发者亲自编写
                   .SetLogoutEndpointUris("connect/logout")
                   // 设置令牌端点，允许功能服务请求令牌
                   // 通常会返回JWT格式的access_token、refresh_token和id_token
                   // 该端点需要开发者亲自编写
                   .SetTokenEndpointUris("connect/token")
                   // 设置用户信息端点，允许功能服务请求用户信息
                   .SetUserinfoEndpointUris("connect/userinfo")
                   // 设置内省端点，允许存储服务向授权服务请求验证令牌状态
                   // 存储服务需要拥有内省作用域权限
                   .SetIntrospectionEndpointUris("connect/introspect")
                   // 设置撤销端点，允许用户和功能服务要求授权服务作废令牌
                   // 如果存储服务不使用内省端点远程验证令牌，则只能等待令牌自动过期
                   // 如果作废refresh_token，能阻止功能服务自动续期授权
                   .SetRevocationEndpointUris("connect/revoke");

                // 注册要展示在元数据端点中的作用域，通常是基础的通用作用域
                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                // 配置要启用的授权流程
                options
                    // 启用隐式授权，用于纯客户端应用，例如静态部署的SPA应用
                    .AllowImplicitFlow()
                    // 启用授权码授权
                    .AllowAuthorizationCodeFlow()
                    // 启用客户端证书授权
                    .AllowClientCredentialsFlow()
                    // 启用刷新令牌授权，客户端需要拥有offline_access作用域权限
                    .AllowRefreshTokenFlow();

                options
                    // 设置加密令牌用的对称密钥
                    .AddEncryptionKey(new SymmetricSecurityKey(
                        Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")))
                    // 设置令牌的RSA签名证书
                    // 当前为开发用临时证书，发布时请更换为实际的证书
                    .AddDevelopmentSigningCertificate();

                options
                   // 注册适用于ASP.NET Core的授权功能的服务
                   .UseAspNetCore()
                   // 启用授权端点直通，由自定义端点实现授权功能
                   .EnableAuthorizationEndpointPassthrough()
                   // 启用结束会话端点直通，由自定义端点实现授权功能
                   .EnableLogoutEndpointPassthrough()
                   // 启用令牌端点直通，由自定义端点实现授权功能
                   .EnableTokenEndpointPassthrough()
                   // 启用状态码页面集成，由ASP.NET Core状态码页面中间件托管
                   .EnableStatusCodePagesIntegration();
            })
            // 注册令牌验证服务，使应用能同时当作客户端使用
            .AddValidation(options =>
            {
                // 注册适用于ASP.NET Core的验证服务
                options.UseAspNetCore();
                // 使用当前授权服务器的配置进行验证
                options.UseLocalServer();
            });

        // 注册开发用数据初始化的托管服务，为OpenIddict设置初始数据
        services.AddHostedService<OAuthClientInitializer>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // 注册开发用错误处理页面
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            // 注册错误处理页面，任选其一即可
            app.UseStatusCodePagesWithReExecute("/Error");
            app.UseExceptionHandler("/Error");
        }
        // 注册HTTPS强制跳转，使用HTTPS更安全
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        // 配置身份认证和授权中间件
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // 注册API和网页端点
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
    }
}
