using Autofac;
using Autofac.Extensions.DependencyInjection;
using Yang.API.Controllers;
using Yang.API.Models;

var builder = WebApplication.CreateBuilder(args);

// �����������
builder.Services.AddCors(c => c.AddPolicy("myany", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//Ĭ�Ͽ�������route+�������ɵģ�����ĳ�ͨ����������.
//builder.Services.AddControllers();
builder.Services.AddControllers().AddControllersAsServices(); //��Ϊͨ���������ɣ���������ʹ��Autofac����ע��
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//ע�����ʱ��������Build()֮ǰע��
builder.Services.AddTransient<IStudentService, Student>();

//���Autofac��Autofac�������Դ�������ע��֮������
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    //builder.RegisterType<Student>().As<IStudentService>().InstancePerLifetimeScope();
    //ע��Ϊscope��Ĭ��ΪTransient
    builder.RegisterType<User>().As<IUserService>()
    // ����Ϊscopeģʽ
    .InstancePerLifetimeScope()
    //��User����������ע�룬������User���е����Կ��Բ�ʹ�ù��캯�����и�ֵ������ֱ���ڴ�ָ��User�������Ѿ���ע����
    .PropertiesAutowired() ;

    // Ĭ������£������ڿ�������ʹ������ע�룬��Ҫ��ʾ����builder.Services.AddControllers().AddControllersAsServices()��ſ���.
    builder.RegisterType<UserController>().PropertiesAutowired();

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
app.UseCors("myany"); // Ӧ��ȫ�ֿ������

app.MapControllers();

app.Run();







