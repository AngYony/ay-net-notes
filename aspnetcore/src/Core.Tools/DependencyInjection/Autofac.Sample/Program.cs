using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Sample.IocServices;
using Autofac.Sample.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    //通过服务的形式添加控制器，而不是通过反射的形式，这样就可以在控制器中进行属性注入
    .AddControllersAsServices();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Autofac之前的bug，建议把Autofac容器的引入放在所有依赖注入的语句之后，builder.Build()语句之前
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(builder =>
//{
//    // 默认是Transine
//    builder.RegisterType<IocService>().As<IIocService>()
//    //注入为Scope
//    .InstancePerLifetimeScope();
//    // 开启支持属性注入，不建议开启，一旦开启所有的类中都必须使用属性引入，会增加代码的复杂性，必须配合Services.AddControllers().AddControllersAsServices()一起使用
//    //.PropertiesAutowired();

//});

builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(builder => {
    builder.RegisterModule<ApiModule>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
