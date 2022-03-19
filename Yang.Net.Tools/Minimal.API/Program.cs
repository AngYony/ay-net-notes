var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// Ĭ�Ͼʹ���minimal api
string Hi(string name) => $"Hi��{name}";

//http://localhost:5068/mini?name=wy
app.MapGet("/mini", (string name) => $"Hello {name}!");
//����ͬ��
app.MapGet("/hi",Hi);
//��name��Ϊ·���е�һ�֣�http://localhost:5068/hi/wy
app.MapGet("/hi/{name}", Hi);
//���в�����
app.MapGet("/par/{username}/{age}", (string username, int age) => { return $"���ƣ�{username}�����䣺{age}"; });

app.Run();
