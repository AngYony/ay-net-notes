using Autofac;
using Autofac.Extensions.DependencyInjection;
using Yang.API.AutofacCus.Extensions;
using Yang.API.AutofacCus.Modules;
using Yang.API.Controllers;
using Yang.API.Models;
 

var builder = WebApplication.CreateBuilder(args);

// 设置允许跨域
builder.Services.AddCors(c => c.AddPolicy("myany", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//默认控制器是route+反射生成的，这里改成通过容器生成.
//builder.Services.AddControllers();
builder.Services.AddControllers().AddControllersAsServices(); //改为通过容器生成，这样可以使用Autofac属性注入
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//注册服务时，必须在Build()之前注入
builder.Services.AddTransient<IStudentService, Student>();

//添加Autofac，Autofac建议在自带的容器注册之后引入
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    // 自定义扩展方法，将需要注入的服务通过该方法进行注入
    builder.RegisterTypeExtensions();
    //或者模块化注册，模块化注册可以在每个类库中对该类库的成员进行注入
    builder.RegisterModule<APIModule>();


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseRouting();

app.UseStaticFiles();
app.UseAuthorization();
app.UseCors("myany"); // 应用全局跨域规则

app.MapControllers();

app.Run();







