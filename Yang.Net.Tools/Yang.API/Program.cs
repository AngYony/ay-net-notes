var builder = WebApplication.CreateBuilder(args);

// �����������
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
app.UseCors("myany"); // Ӧ��ȫ�ֿ������

app.MapControllers();

app.Run();
