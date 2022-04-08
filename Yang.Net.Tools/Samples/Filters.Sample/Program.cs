using Filters.Sample.CusFilters;
using Filters.Sample.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//添加全局过滤器
builder.Services.AddControllers(opt => { opt.Filters.Add(typeof(CusActionFilterOnGlobalAttribute)); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//启用缓冲，必须使用单例模式，否则将不会被缓存上
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
//当自定义过滤器中需要使用到依赖注入的内容，如果不使用TypeFilter，而是使用ServiceFilter，那么必须像下面这样进行注入。推荐使用[TypeFilter(typeof(CusResourceFilterAttribute))] 
builder.Services.AddTransient<CusResourceFilterAttribute>();
//builder.Services.AddScoped<IDistributedCache, MemoryDistributedCache>();

builder.Services.AddTransient<IUser, User>();

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
