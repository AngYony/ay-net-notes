using Autofac;
using Autofac.Extensions.DependencyInjection;
using Yang.API.Models;

var builder = WebApplication.CreateBuilder(args);

// 设置允许跨域
builder.Services.AddCors(c => c.AddPolicy("myany", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//注册服务时，必须在Build()之前注入
builder.Services.AddTransient<IStudentService, Student>();

//添加Autofac，Autofac建议在自带的容器注册之后引入
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    //builder.RegisterType<Student>().As<IStudentService>().InstancePerLifetimeScope();
    //注册为scope，默认为Transient
    builder.RegisterType<User>().As<IUserService>()
    // 设置为scope模式
    .InstancePerLifetimeScope()
    //在User中启用属性注入，即：在User类中的属性可以不使用构造函数进行赋值，而是直接在此指明User中属性已经被注入了
    .PropertiesAutowired() ;

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







