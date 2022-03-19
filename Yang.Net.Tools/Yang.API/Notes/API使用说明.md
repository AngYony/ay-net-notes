# Web API 应用笔记


跨域：
可以配置到单个控制器上面，也可以全局配置到program中。

Web API的方法一共有三种返回类型：
- 特定类型（自定义对象类型） 
- IActionResult，同时返回多个ActionResult时，适合使用。
- `ActionResult<T>`，既能返回普通类型的返回值，又能返回指定状态码。

wwwroot目录：
```csharp
<ItemGroup>
        <Content Update="wwwroot">
            <CopyToOutputDirectory>Always</CopyToOutputDirectory>
        </Content>
    </ItemGroup>
```



Minimal API

