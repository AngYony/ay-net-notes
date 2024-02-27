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
        // ע�������֤����
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GiteeAuthenticationDefaults.AuthenticationScheme;
        })
            // ʹ��ָ���ķ�����ע��Cookie����
            // �˷��������汻����ΪĬ�Ϸ���
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/signin";
                options.LogoutPath = "/signout";
            })
            .AddGitee(options =>
            {
                // ����Ӧ��ʱ�Զ�����
                options.ClientId = "<�������client id>";
                // ��Ӧ�������д��������ú�����
                options.ClientSecret = "<�������ṩ��secret>";

                // �Ƿ�Ҫ�����ƣ�access_token��refresh_token�����浽��չ��֤���ԡ�ʹ��Cookie������¼ʱ���ƻ����л���Cookie��
                // ASP.NET Core��Cookie��ʹ�����ݱ���������ܣ���ͨ����������Ʋ�Ӧ�����κη�ʽ���䵽ǰ��
                // Ĭ������ɵ�½��ᱻ�������޷����¶�ȡ��ʹ�ã������ٴ���OAuth��������������
                options.SaveTokens = true;

                // �������ɷ���OAuth��������ȫҪ�������Url���ص�Url�ڴ���Ӧ��ʱ��д�����ж������Ƿ�ΪOAuth�������Ļص��Ծ�����Ҫֱ�Ӵ�����������󴫵�����
                // ����Ҫ��д�ص��˵㣬�ص�����֤������ֱ����ɣ����õĻ����Ѿ�ʵ���˻ص��Ĺ��ܣ���code�һ����ƣ�
                options.CallbackPath = "/gitee-oauth";

                // �������Ʊ��ʱ�ᴥ�����¼�
                options.Events.OnCreatingTicket = static async context =>
                {
                    // ����ͨ���¼�������ȡ����
                    // ������洢���ƣ�����Ψһһ�ζ�ȡ���������ƵĻ���
                    // ��������ƴ洢�������У������Ϳ����ڲ������ƴ��䵽ǰ�˵�����±������ظ�ʹ������
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
