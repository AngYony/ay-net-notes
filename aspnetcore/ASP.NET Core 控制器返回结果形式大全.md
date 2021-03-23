# ASP.NET Core 控制器返回结果形式大全



## 返回 JSON

### 形式一：将 JSON 字符串直接输出成 JSON

```csharp
public  IActionResult Index()
{
	string msg = "{\"a\":\"b\"}"; //msg为JSON字符串
    return Content(msg, "application/json");
    //或return new ContentResult { Content = msg, ContentType = "application/json" };
}
```

### 形式二：将 Object 对象输出成 JSON

```csharp
public  IActionResult Index()
{
	var obj = new { a = "AA", b = "BB" };
    return Json(obj);
}
```





