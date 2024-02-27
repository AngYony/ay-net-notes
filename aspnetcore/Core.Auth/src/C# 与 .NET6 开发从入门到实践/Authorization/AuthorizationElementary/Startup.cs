using Microsoft.AspNetCore.Authorization;

namespace AuthorizationElementary;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // 添加授权策略并为策略取名
            options.AddPolicy("MyRole01", policy =>
            {
                // 添加授权要求
                policy.AddRequirements(new TimeAuthorizationRequirement(new TimeOnly(6, 0, 0), TimeSpan.FromHours(18)));
            });

            options.AddPolicy("MyRole02", policy =>
            {
                // 对于简单授权逻辑，可以直接使用委托进行定义和配置
                // 基于策略的授权中，授权上下文的资源由授权中间件自动设置为HTTP上下文
                // 可以认为基于策略的授权等价于资源类型是HTTP上下文的基于资源的授权
                policy.RequireAssertion(context => ((HttpContext)context.Resource).Request.Query["pass"] == "天王盖地虎，宝塔镇河妖。");
            });
        });

        // 注册授权要求处理器，可以为同一种要求类型注册多个处理器
        services.AddScoped<IAuthorizationHandler, TimeAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, TimeAndNameAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, PermissionHandler>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        // 如果授权要求不需要以身份为依据，例如之前例举的深夜防沉迷授权要求，可以不配置身份认证处理器
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/authByPolicy", static async context => { await context.Response.WriteAsync("Hello!"); })
                .RequireAuthorization("MyRole01", "MyRole02");

            endpoints.MapGet("/authByCode", static async context =>
            {
                // 获取授权服务
                var authorizationService = context.RequestServices.GetRequiredService<IAuthorizationService>();
                // 使用指定的参数授权，服务会自动选择合适的处理器
                // 此处直接传递授权要求，因此可以不用为处理器专门注册策略
                var authorizationResult = 
                    await authorizationService.AuthorizeAsync(
                        context.User,
                        new MyResource { Name = context.User.Identity.Name },
                        new TimeAuthorizationRequirement(new TimeOnly(6, 0, 0), TimeSpan.FromHours(18))
                    );

                if (authorizationResult.Succeeded)
                {
                    await context.Response.WriteAsync("授权成功！");
                }
                else
                {
                    await context.Response.WriteAsync("授权失败！");
                }
            });
        });
    }
}
