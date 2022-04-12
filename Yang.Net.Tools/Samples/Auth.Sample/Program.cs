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

//注册鉴权架构
//#region 方式一：使用cookie（默认方式）
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//.AddCookie(opt=> {
//    //设置未登录的跳转页面
//    opt.LoginPath = "/api/Login/NoLogin";
//});
//#endregion

#region 方式二：自定义Token验证
builder.Services.AddAuthentication(opt =>
{
    //把自定义的鉴权方案添加到鉴权架构中
    opt.AddScheme<TokenAuthenticationHandler>("token", "cusToken");
    //设置默认鉴权方案为Token鉴权
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
//引入鉴权
app.UseAuthentication();
//引入授权
app.UseAuthorization();

app.MapControllers();

app.Run();
