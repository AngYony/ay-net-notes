using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSession();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ע���Ȩ�ܹ�
#region cookie Ĭ�Ϸ�ʽ
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(opt=> {
    //����δ��¼����תҳ��
    opt.LoginPath = "/api/Login/NoLogin";
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
