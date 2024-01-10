# EF Core Code-First 与 DB-Frist

[TOC]

EF Core的开发模式有两种形式：

- Code-First
- DB-First



## Code-First 开发模式

有两种方式：

- [EF Core 命令行工具](https://docs.microsoft.com/zh-cn/ef/core/cli/dotnet)，如果安装了多个版本的EF Core，需要以指定版本的EF Core来操作运行，否则将以最新版进行操作，因此实际使用中，建议先确定要使用的EF Core的版本。
- [Visual Studio中的包管理器控制台（推荐）](https://docs.microsoft.com/zh-cn/ef/core/cli/powershell)



### EF Core 命令行工具

#### 需要安装的工具和Nuget包

```
Pomelo.EntityFrameworkCore.MySql
Microsoft.EntityFrameworkCore.Design
```

#### 安装 dotnet tool ef 工具

进入到解决方案目录，安装dotnet tool ef工具

```
dotnet tool install --global dotnet-ef
```

#### 验证安装

运行以下命令，验证是否已正确安装 EF Core CLI 工具：

```
dotnet ef
```

#### 添加迁移文件

进入到具体的项目目录，添加迁移文件，“Init”是迁移脚本的名字：

```
dotnet ef migrations add Init
```

如果后期对数据库进行了更改，可以继续使用该命令添加新的迁移文件，例如：

```
dotnet ef migrations add ProjectIdAutoGenerate
```

如果要删除上一次迁移 (回滚对迁移) 所做的代码更改，可以执行下述命令：

```
dotnet ef migrations remove
```

上述命令只能移除最新的迁移文件。

#### 列出所有迁移版本

```
dotnet ef migrations list
```

#### 更新数据库

然后执行更新数据库：

```
dotnet ef database update
```

#### EF Core 生成SQL脚本

以下命令可以指定输出到文件中，如果不指定，默认输出到控制台中。

从空白开始生成sql脚本（从初始化到最新的生成脚本）：

```
dotnet ef migrations script
```

生成指定版本到最新版本的sql：

```
dotnet ef migrations script 版本名
```

从A-B版本生成迁移SQL脚本：

```
dotnet ef migrations script 版本A 版本B
```

#### 对比更新数据库

```
dotnet ef database update
```

#### 强制更新某个版本到数据库 

```
dotnet ef database update AddNe
```



### Visual Studio中的包管理器控制台

需要依赖包：Microsoft.EntityFrameworkCore.Tools，该包主要用于通过Visual Studio的包管理控制台来操作EF。

见官方文档说明：https://docs.microsoft.com/zh-cn/ef/core/cli/powershell



### 如何同时进行多个CodeFirst操作

在添加了必要的Nuget包之后。

第一步：手动创建dbContext

```c#
public class MyCodeFirstDbContext : DbContext
{
	public MyCodeFirstDbContext()
	{
	
	}
	public MyCodeFirstDbContext(DbContextOptions<MyCodeFirstDbContext> options) : base(options)
	{
	
	}
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		//需要操作的数据库的连接字符串
		optionsBuilder.UseNpgsql("server=192.168.1.1;port=5432");
		base.OnConfiguring(optionsBuilder);
	}
	
	protected override void OnModelCreating(ModelBuilder builder)
	{
		var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"*Entity.dll")
			.Select(a=>Assembly.LoadFrom(a))
			.Where(a=>!a.IsDynamic);
		var types = assemblies.SelectMany(a=>a.GetTypes()).Where(a=>a.GetCustomAttribue(typeof(TableAttribute),false)!=null).ToList();
		types.ForEach(type=>{
			builder.Model.AddEntityType(type);
		});
		
		base.OnModelCreating(builder);
	}
}
```

第二步：执行`Add-Migration Init -Context MyCodeFirstDbContext`命令即可。注意：必须指定需要应用到的DbContext名称。



### 使用独立的迁移项目

可以使用单独的项目来存放迁移代码文件。需要注意以下几点：

- 迁移项目中不存在DbContext上下文类型时，必须添加对上下文项目的引用。

- 必须在启动项目中添加对迁移项目的引用。（启动项目包含了连接字符串信息）。

- 必须在启动项目中安装NuGet包 Microsoft.EntityFrameworkCore.Tools，注意：不是安装在迁移项目或上下文项目中，必须是安装在启动项目中，否则执行迁移命令会报错。

- 在启动项目所在的文件夹中执行迁移命令：

  ```
  Add-Migration {迁移名称|例：V1_Initial} -Context {上下文类型|例：MyDbContext} -Project {迁移程序集，相对路径|例：My.Migrations.Project} -StartupProject {启动项目|例：My.WebApi}
  ```

  补充说明：

  如果是首次添加迁移，在启动项目所在的文件夹中执行迁移命令：`Add-Migration <名称>`，然后把生成的迁移代码移动到迁移项目中；如果不是首次添加迁移，执行上述文件。【该描述未经验证】



## Database-First 开发模式

EF 提供了一种根据数据库连接字符串来生成实体的命令工具，如下：

```
dotnet ef dbcontext scaffold "server=192.168.0.2;port=7306;user=root;password=root123456@;database=lighter" Pomelo.EntityFrameworkCore.MySql -o Models
```

上述脚本将会在项目下的“Models”目录中生成上下文文件。

虽然可以生成实体，但是实体类需要进行修改才可以使用，因此不太建议使用Database-First模式。
