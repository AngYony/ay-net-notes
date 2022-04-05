using Filters.Sample.CusFilters;
using Filters.Sample.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//添加全局过滤器
builder.Services.AddControllers(opt => { opt.Filters.Add(typeof(CusActionFilterOnGlobalAttribute)); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
