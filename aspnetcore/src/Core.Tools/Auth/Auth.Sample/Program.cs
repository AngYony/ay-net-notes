using Auth.Sample.Conts;
using Auth.Sample.CusAuthentication;
using Auth.Sample.CusAuthorizations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSession();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ע����֤�ܹ�
#region ��ʽһ��ʹ��cookie��Ĭ�Ϸ�ʽ��
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(opt =>
{
    //����δ��¼����תҳ��
    opt.LoginPath = "/api/Login/NoLogin";
});
#endregion

#region ��ʽ�����Զ���Token��֤(ʵ��ʹ����ֱ������JwtBearear������֤��ʽ)������ͬʱ�������ּ�Ȩ��ʽ,tokenһ������web api��Cookie��������MVC������Ҫ�ڿ������ķ����Ͻ��б�ע
builder.Services.AddAuthentication(opt =>
{
    //���Զ���ļ�Ȩ������ӵ���Ȩ�ܹ���
    //��ǰscheme������Ϊtoken�����Զ��������Ϊ������ӵ���Ȩ�У�������Ϊtoken����ʱtoken���ʹ������Զ���ļ�Ȩ����
    opt.AddScheme<TokenAuthenticationHandler>("wytoken", "cusToken");
    //����Ĭ�ϼ�Ȩ����Ϊwytoken��Ȩ
    opt.DefaultAuthenticateScheme = "wytoken"; //Ҳ����ֱ�ӽ�������Ȩ�����ͼ�Ȩ���а�[Authorize]
    //����û�е�¼ʱ��Ĭ�ϼ�Ȩ����Ҳ��wytoken
    opt.DefaultChallengeScheme = "wytoken";
    //����û��Ȩ�޷���ʱ�ļ�Ȩ����ҲΪwytoken
    opt.DefaultForbidScheme = "wytoken";
});
#endregion

//�Զ�����Ȩ
#region ��Ȩ
//һ��Ӧ����ͨ��Ȩ
//builder.Services.AddAuthorization(opt =>
//{
//    ��ʽһ���򵥹���ʹ��RequireClaim
//    opt.AddPolicy(AuthorizationConts.MYPOLICY,
//    p => p.RequireClaim(ClaimTypes.NameIdentifier, "6"));

//��ʽ�������ӹ���ʹ��RequireAssertion
//opt.AddPolicy(AuthorizationConts.MYPOLICY, p=> {
//    p.RequireAssertion(context => context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier)
//    && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value == "6");
//});
//});

//����ʹ���Զ�����Ȩ����
builder.Services.AddAuthorization(opt =>
{
    //��ʹ��˫��Ȩ����ʱ�����������Ա���ͬʱͨ���ſ��ԣ�������Ȩʧ��
    //���2��policy�����ڿ�������Action�ϣ�ָ������Authorize���Ա��
    opt.AddPolicy(AuthorizationConts.MYPOLICY,
    p => p.AddAuthenticationSchemes("wytoken") //��Ȩ���Ȩ�İ󶨣�һ����Ȩ��Ӧһ����Ȩ����
    .Requirements.Add(new MyAuthorizationHandler("6")));

    //��ʹ��policy2ʱ���Ͳ���ȥpolicy1����Ȩ
    opt.AddPolicy(AuthorizationConts.MYPOLICY2,
    p => p.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
    .Requirements.Add(new MyAuthorizationHandler2("�����")));
});





#endregion

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSession();
//�����Ȩ
app.UseAuthentication();
//������Ȩ
app.UseAuthorization();

app.MapControllers();

app.Run();
