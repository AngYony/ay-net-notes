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

//中间件引入，方式一
//异常处理中间件必须在所有中间件之前进行Use
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


//异常处理中间件引入，方式二，通过ExceptionHandlerOptions的ExceptionHandler
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


//异常处理中间件引入，方式三,通过ExceptionHandlerOptions的ExceptionHandlingPath
app.UseExceptionHandler(new ExceptionHandlerOptions
{
    //指定异常处理页
    ExceptionHandlingPath = new PathString("/exception")
});
app.MapGet("/exception", () => "异常错误");

//其他中间件，自定义中间件，通过UseMiddleware引入中间件
app.UseMiddleware<CusExceptionHandlerMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


////通过自定义扩展方法，对app.Use进行封装
app.UseTest();



app.MapControllers();




app.Map("/run1", Run1);
app.Map("/run2", Run2);

////
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("孙悟空");
//});

app.Run();//todo：弄明白Run()方法的作用



static void Run1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hi 唐僧");
    });
}

static void Run2(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hi 猪八戒");
    });
}