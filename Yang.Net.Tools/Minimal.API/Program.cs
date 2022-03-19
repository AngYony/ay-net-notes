var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// 默认就带有minimal api

app.MapGet("/mini", (string name) => $"Hello {name}!");
app.MapPost("/mini", () => "post方法");

Func<string> a = () => { return "ss"; };

app.Run();
