# ASP.NET Core 身份认证

**本文名词释义**

Authentication：认证/验证，本文使用“认证”作为术语。

Identity：身份/标识，本文使用“身份”作为术语（如果需要）。

**说明：本文统一使用“身份认证”作为术语进行表述，同时为了避免歧义，在表述”身份/标识“术语时，使用“Identity“本身代替”身份/标识“。**

**什么是身份认证？**

当用户提供的凭据和应用程序中的凭据（可能存储在操作系统、数据库、应用或资源中）匹配时，才能够使用应用程序中的资源，凭据匹配的过程就是身份认证。可以将身份认证理解为进入空间（例如服务器、数据库、应用或资源）的一种方式。身份认证在系统中表现的行为，对应的就是用户登录的过程。包括注册和注销用户，都需要使用Identity身份标识。



## Identity（身份/标识）

使用Identity（身份/标识）来注册、登录和注销用户。



### Identity体系结构



### 用户

### 用户声明

### 用户登录名

### 角色

















Razor类库和基架之间的关系：https://docs.microsoft.com/zh-cn/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-2.2&tabs=visual-studio











Identity：该名词中文翻译为身份；标识；特性；同一性，一致。大多数文档中翻译为身份或者标识，本文为了语句的连贯性和避免歧义，约定使用Identity本身作为讲述。



## AddDefaultIdentity和AddIdentity

在ASP.NET Core 2.1中引入了AddDefaultIdentity方法，在AddDefaultIdentity方法的内部实现中，调用了以下方法：

- AddIdentity
- AddDefaultUI
- AddDefaultTokenProviders



## 配置Identity服务

在ConfigureServices中添加服务，一般先调用所有的Add{Service}方法，然后调用所有的services.Configure{Service}方法。

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
    services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });

    services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.SlidingExpiration = true;
    });

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
}
```

上述代码使用默认的选项值来配置Identity，通过调用UseAuthentication启用Identity，UseAuthentication添加了身份验证中间件到请求管道。

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCookiePolicy();
	//添加了身份验证中间件到请求管道
    app.UseAuthentication();

    app.UseMvc();
}
```





