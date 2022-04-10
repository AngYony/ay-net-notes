using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Middleware.Sample.Middlewares;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//�м�����룬��ʽһ
//�쳣�����м�������������м��֮ǰ����Use
app.UseExceptionHandler(configure =>
{
    configure.Run(async context =>
    {
        var exHandler = context.Features.Get<IExceptionHandlerPathFeature>();
        var ex = exHandler.Error;
        if (ex != null)
        {
            context.Response.ContentType = "text/plain;charset=utf-8"; //MediaTypeNames.Text.Plain;
            await context.Response.WriteAsync(ex.ToString());

        }
    });

});


//�쳣�����м�����룬��ʽ����ͨ��ExceptionHandlerOptions��ExceptionHandler
app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = async (context) =>
    {
        var exHandler = context.Features.Get<IExceptionHandlerPathFeature>();
        var ex = exHandler.Error;
        if (ex != null)
        {
            context.Response.ContentType = "text/plain;charset=utf-8"; //MediaTypeNames.Text.Plain;
            await context.Response.WriteAsync(ex.ToString());

        }
    }
});


//�쳣�����м�����룬��ʽ��,ͨ��ExceptionHandlerOptions��ExceptionHandlingPath
app.UseExceptionHandler(new ExceptionHandlerOptions
{
    //ָ���쳣����ҳ
    ExceptionHandlingPath = new PathString("/exception")
});
app.MapGet("/exception", () => "�쳣����");

//�����м�����Զ����м����ͨ��UseMiddleware�����м��
app.UseMiddleware<CusExceptionHandlerMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


////ͨ���Զ�����չ��������app.Use���з�װ
app.UseTest();



app.MapControllers();




app.Map("/run1", Run1);
app.Map("/run2", Run2);

////
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("�����");
//});

app.Run();//todo��Ū����Run()����������



static void Run1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hi ��ɮ");
    });
}

static void Run2(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hi ��˽�");
    });
}