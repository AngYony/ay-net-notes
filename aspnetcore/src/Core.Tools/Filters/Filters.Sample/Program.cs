using Filters.Sample.CusFilters;
using Filters.Sample.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//���ȫ�ֹ�����
builder.Services.AddControllers(opt => { opt.Filters.Add(typeof(CusActionFilterOnGlobalAttribute)); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//���û��壬����ʹ�õ���ģʽ�����򽫲��ᱻ������
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
//���Զ������������Ҫʹ�õ�����ע������ݣ������ʹ��TypeFilter������ʹ��ServiceFilter����ô������������������ע�롣�Ƽ�ʹ��[TypeFilter(typeof(CusResourceFilterAttribute))] 
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
