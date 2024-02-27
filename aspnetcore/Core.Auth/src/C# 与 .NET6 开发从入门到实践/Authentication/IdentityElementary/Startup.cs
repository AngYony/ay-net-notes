using OpenIddictServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace OpenIddictServer;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        WebHostEnvironment = webHostEnvironment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment WebHostEnvironment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // 注册Razor Pages服务以呈现身份系统的页面，需要端点路由配合
        services.AddRazorPages();

        // 注册用于存储身份信息的EF Core上下文，通过配置系统从appsettings.json读取数据库连接字符串
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

        // 注册Identity服务，类型参数确定用户账户和角色的实体类型，必须和数据存储用上下文中的类型一致
        // 还有其他能自定义更多细节的扩展方法
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // 是否必须先激活账号才能登录，可用于确保电子邮箱地址真实存在且受账号注册人管理，密码找回等功能依赖电子邮箱
            // 默认使用E-mail发送激活链接，邮件发送功能需要自行实现和注册IEmailSender服务，可以使用SendGrid或其他Nuget包简化开发
            options.SignIn.RequireConfirmedAccount = false;

            // 配置合格的密码强度要求
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            // 是否启用隐私数据保护
            // 稍后会详细介绍
            options.Stores.ProtectPersonalData = true;

            // 锁定账号前允许的最大连续登录失败次数
            // 如果账号不幸被锁定，需要等待一段时间（默认锁定5分钟）或者使用找回密码功能重置密码才能解锁
            options.Lockout.MaxFailedAccessAttempts = 3;

            // 注册用户的电子邮箱是否允许重复
            // 模板代码中电子邮箱同时作为用户名使用，此时电子邮箱必须不重复，否则登录账号时无法确定特定记录，会引起异常
            options.User.RequireUniqueEmail = true;
        })
            // 配置EF Core存储使用的上下文类型
            .AddEntityFrameworkStores<ApplicationDbContext>()
            // 配置默认令牌提供者
            // 用于生成账号激活、密码找回等功能需要的令牌
            .AddDefaultTokenProviders()
            // 注册隐私数据保护服务
            .AddPersonalDataProtection<AesLookupProtector, AesProtectorKeyRing>()
            // 启用默认UI
            .AddDefaultUI();

        // 注册选项（选项内容简单，仅为展示之用，没有使用 .NET选项系统）
        services.AddSingleton(new ProtectorOptions { KeyPath = $@"{WebHostEnvironment.ContentRootPath}\App_Data\AesDataProtectionKey" });

        // 注册邮件发送服务，如果使用默认Identity，会覆盖默认的开发用服务
        services.AddScoped<IEmailSender, DesktopFileEmailSender>();

    }

    public void Configure(IApplicationBuilder app)
    {
        // 有网页内容，需要提供必要的静态资源
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        // 账号管理部分使用到授权相关功能，需要配置授权中间件
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // Identity 相关页面使用Razor Pages技术实现，需要注册相关端点
            endpoints.MapRazorPages();
        });
    }
}
