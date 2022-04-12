using Auth.Sample.CusAuthentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSession();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ע���Ȩ�ܹ�
//#region ��ʽһ��ʹ��cookie��Ĭ�Ϸ�ʽ��
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//.AddCookie(opt=> {
//    //����δ��¼����תҳ��
//    opt.LoginPath = "/api/Login/NoLogin";
//});
//#endregion

#region ��ʽ�����Զ���Token��֤
builder.Services.AddAuthentication(opt =>
{
    //���Զ���ļ�Ȩ������ӵ���Ȩ�ܹ���
    opt.AddScheme<TokenAuthenticationHandler>("token", "cusToken");
    //����Ĭ�ϼ�Ȩ����ΪToken��Ȩ
    opt.DefaultAuthenticateScheme = "token";
    opt.DefaultChallengeScheme = "token";
    opt.DefaultForbidScheme = "token";
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
