using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Sample.IocServices;
using Autofac.Sample.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    //ͨ���������ʽ��ӿ�������������ͨ���������ʽ�������Ϳ����ڿ������н�������ע��
    .AddControllersAsServices();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Autofac֮ǰ��bug�������Autofac���������������������ע������֮��builder.Build()���֮ǰ
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(builder =>
//{
//    // Ĭ����Transine
//    builder.RegisterType<IocService>().As<IIocService>()
//    //ע��ΪScope
//    .InstancePerLifetimeScope();
//    // ����֧������ע�룬�����鿪����һ���������е����ж�����ʹ���������룬�����Ӵ���ĸ����ԣ��������Services.AddControllers().AddControllersAsServices()һ��ʹ��
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
