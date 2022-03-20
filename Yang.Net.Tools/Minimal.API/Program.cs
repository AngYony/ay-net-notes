using Minimal.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// 默认就带有minimal api
string Hi(string name) => $"Hi，{name}";

//http://localhost:5068/mini?name=wy
app.MapGet("/mini", (string name) => $"Hello {name}!");
//访问同上
app.MapGet("/hi", Hi);
//将name作为路径中的一种：http://localhost:5068/hi/wy
app.MapGet("/hi/{name}", Hi);
//带有参数的
app.MapGet("/par/{username}/{age}", (string username, int age) => { return $"名称：{username}，年龄：{age}"; });
// 带有路由约束
app.MapGet("/set/{age:regex(^[0-9]+$)}", (string age) => $"{age}");
//依赖注入Logger组件
app.MapGet("/", (ILogger<Student> logger) => { logger.LogWarning("输出日志"); return "首页"; });
app.Run();
