var builder = WebApplication.CreateBuilder(args);

// 设置允许跨域
builder.Services.AddCors(c => c.AddPolicy("myany", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mnimal API
app.MapGet("mini", () => { return "wyang"; });


app.UseRouting();

app.UseStaticFiles();
app.UseAuthorization();
app.UseCors("myany"); // 应用全局跨域规则

app.MapControllers();

app.Run();
