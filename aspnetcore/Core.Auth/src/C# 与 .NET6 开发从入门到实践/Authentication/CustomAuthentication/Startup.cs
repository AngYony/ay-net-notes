namespace CustomAuthentication;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = MyHandler.SchemeName;
        })
            .AddScheme<MyOption, MyHandler>(MyHandler.SchemeName, options =>
            {
                options.Text = "text";
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        // 配置身份认证中间件
        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync(context.User.Identity.Name);
            });
        });
    }
}
