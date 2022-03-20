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



## 依赖注入

依赖注入是实现控制反转的一种手段或者方式。

- 谁依赖于谁：当然是应用程序依赖于IOC容器。
- 为什么需要依赖：应用程序需要IOC容器来提供对象需要的外部资源。
- 谁注入谁：很明显是IOC容器注入应用程序某个对象，应用程序依赖的对象。
- 注入了什么：就是注入某个对象所需要的外部资源（包括对象、资源、常量数据）

ASP.NET Core 6 中的IOC容器就是 ServiceCollection。

### AddTransient

- AddTransient()：可以用于注册泛型类型

  ```csharp
  builder.Services.AddTransient(typeof(Student<>));
  ```

- AddTransient<>()：写法更简单，但不能注册泛型类型

  - 注册单个类型，这里的服务类型是Stuent：

    ```csharp
    builder.Services.AddTransient<Stuent>();
    ```

    在控制器构造方法中，也必须是单个类型：

    ```csharp
    public HomeController(Student stu);
    ```

  - 注册接口和对应实现类，这里的服务类型是IUserService，实现类型是UserService：

    ```csharp
    builder.Services.AddTransient<IUserService,UserService>();
    ```

    在控制器构造方法中，也必须是接口类型，即使用时必须匹配曾经注入的服务类型：

    ```
    public HomeController(IUserService service);
    ```

### AddScoped

```csharp
services.AddScoped<IUser,User>();
services.AddScoped<typeof(IUser),typeof(User));
```

## AddSingleton

```csharp
services.AddSingleton<IUser,User>();
services.AddSingleton(typeof(IUser),typeof(User))
//单例模式下，也可以直接注入一个实体对象，全局使用
services.AddSingleton<IUser>(new User(){Name="wy"});
```



