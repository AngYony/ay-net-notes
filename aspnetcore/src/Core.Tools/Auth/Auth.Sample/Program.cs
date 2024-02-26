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

//注册认证架构
#region 方式一：使用cookie（默认方式）
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(opt =>
{
    //设置未登录的跳转页面
    opt.LoginPath = "/api/Login/NoLogin";
});
#endregion

#region 方式二：自定义Token验证(实际使用中直接引入JwtBearear包的验证方式)，这里同时启用两种鉴权方式,token一般用于web api，Cookie更多用于MVC程序，需要在控制器的方法上进行标注
builder.Services.AddAuthentication(opt =>
{
    //把自定义的鉴权方案添加到鉴权架构中
    //当前scheme名称设为token（将自定义的类作为策略添加到鉴权中，并起名为token）此时token，就代表着自定义的鉴权类型
    opt.AddScheme<TokenAuthenticationHandler>("wytoken", "cusToken");
    //设置默认鉴权方案为wytoken鉴权
    opt.DefaultAuthenticateScheme = "wytoken"; //也可以直接将具体授权方案和鉴权进行绑定[Authorize]
    //设置没有登录时的默认鉴权方案也用wytoken
    opt.DefaultChallengeScheme = "wytoken";
    //设置没有权限访问时的鉴权方案也为wytoken
    opt.DefaultForbidScheme = "wytoken";
});
#endregion

//自定义授权
#region 授权
//一：应用普通授权
//builder.Services.AddAuthorization(opt =>
//{
//    方式一：简单规则，使用RequireClaim
//    opt.AddPolicy(AuthorizationConts.MYPOLICY,
//    p => p.RequireClaim(ClaimTypes.NameIdentifier, "6"));

//方式二：复杂规则，使用RequireAssertion
//opt.AddPolicy(AuthorizationConts.MYPOLICY, p=> {
//    p.RequireAssertion(context => context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier)
//    && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value == "6");
//});
//});

//二：使用自定义授权方案
builder.Services.AddAuthorization(opt =>
{
    //在使用双授权方案时，这两个策略必须同时通过才可以，否则授权失败
    //添加2个policy，并在控制器的Action上，指定两个Authorize特性标记
    opt.AddPolicy(AuthorizationConts.MYPOLICY,
    p => p.AddAuthenticationSchemes("wytoken") //授权与鉴权的绑定，一个授权对应一个鉴权方案
    .Requirements.Add(new MyAuthorizationHandler("6")));

    //当使用policy2时，就不会去policy1中授权
    opt.AddPolicy(AuthorizationConts.MYPOLICY2,
    p => p.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
    .Requirements.Add(new MyAuthorizationHandler2("孙悟空")));
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
