var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// Ĭ�Ͼʹ���minimal api

app.MapGet("/mini", (string name) => $"Hello {name}!");
app.MapPost("/mini", () => "post����");

Func<string> a = () => { return "ss"; };

app.Run();
