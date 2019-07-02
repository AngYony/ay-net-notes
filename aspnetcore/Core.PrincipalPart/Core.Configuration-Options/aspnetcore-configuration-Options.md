# ASP.NET Core 中的配置（Configuration）与选项（Options ）

**必读部分**

- Configuration：配置

- Options：选项

- Configuration和Options之间的关系：

  Options是Configuration的扩展模式，在未使用Options时，一般通过配置提供程序将配置数据从各种配置源读取到键值对集中，而使用Options后，可以直接使用类来表示相关设置的组。大多数情况下，更倾向于使用Options模式来处理配置。（正是因为如此，本文将配置和选项合并为一篇文章。）



## 重点总结部分

Configuration（配置）是通过IConfigurationBuilder的方法及其扩展方法进行指定的，而IConfigurationBuilder通常通过IWebHostBuilder的ConfigureAppConfiguration方法引入。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configBuilder) =>
    {
        configBuilder.SetBasePath(Directory.GetCurrentDirectory());
        configBuilder.Add****();
    })
    .UseStartup<Startup>();
```

配置的核心是以下几个成员：

- IConfigurationBuilder
- IConfigurationSource
- IConfigurationProvider
- ConfigurationProvider（派生自IConfigurationProvider）

这几个成员之间是通过IConfigurationSource联系起来的，IConfigurationSource的定义如下：

```c#
public interface IConfigurationSource
{
	IConfigurationProvider Build(IConfigurationBuilder builder);
}
```

而IConfigurationBuilder的Add()（原生方法）又需要接收IConfigurationSource变量：

```c#
public interface IConfigurationBuilder
{
	...
	IConfigurationBuilder Add(IConfigurationSource source);
	IConfigurationRoot Build();
}
```

关于这几个核心之间的详细介绍见下文中的“自定义配置提供程序”。

配置一般在Program.cs文件中进行处理，而选项是在Startup的ConfigureServices()方法中进行指定的。





## 配置（Configuration）

在讲述配置之前，一定需要强调的是，配置指的是配置源类似于键值对，即必须包含配置键和对应值的配置，可以是JSON文件、命令行参数、文件内容、环境变量等，无论配置源是什么，都是最终可以具体到一对一形式的键和值。

使用配置时，必不可少的条件：

- 添加Microsoft.AspNetCore.App元包
- 引入Microsoft.Extensions.Configuration命名空间

配置包括面向主机（Host）的配置和面向应用（Application）的配置。当基于vs模板创建一个ASP.NET Core应用程序时，默认会调用CreateDefaultBuilder方法，该方法为项目提供了一些[默认配置](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2#default-configuration)。

配置的核心是使用配置提供程序来读取配置源，在应用启动时，将按照指定的配置提供程序的顺序读取配置源。

配置键和对应的键值组成一对键值对，配置源实质上就是由多种这样的键值对组成，只是呈现的最终形式不一样，可以是JSON文件、命令行参数、环境变量等。

### 配置键和值的约定

无论配置源是哪种形式，它们的配置键都具有相同的约定：

- 键名不区分大小写。 例如，ConnectionString 和 connectionstring 被视为等效键。
- 如果多个配置提供程序设置了相同键的值，最后一次设定的值将会覆盖之前设置的值，即永远采用最后一个设置的值。
- 当配置键是分层键时（如JSON文件、XML文件）：
  - 在配置 API 中，冒号分隔符 (:) 适用于所有平台。
  - 在环境变量中，冒号分隔符可能无法适用于所有平台。 而所有平台均支持采用双下划线 (`__`)，并可以将其转换为冒号。
  - 在 [Azure Key Vault](https://docs.microsoft.com/zh-cn/aspnet/core/security/key-vault-configuration?view=aspnetcore-2.2) 中，分层键使用 `--`（两个破折号）作为分隔符。 将机密加载到应用的配置中时，必须提供代码以用冒号替换破折号。
- 当使用ConfigurationBinder（允许将强类型对象绑定到配置值）时，ConfigurationBinder 支持使用配置键中的数组索引将数组绑定到对象。

而配置值的约定如下：

- 值是字符串。
- NULL 值不能存储在配置中或绑定到对象。

### 配置提供程序

- 命令行配置提供程序（基于命令行参数的配置源）

- 环境变量配置提供程序（基于环境变量的配置源）

- 文件配置提供程序（基于文件（INI、JSON、XML）的配置源）

- Key-per-file 配置提供程序（基于目录文件的配置源）

- 内存配置提供程序（基于内存中集合的配置源）
- 自定义配置提供程序（自定义配置源）
- [Azure Key Vault 配置提供程序](https://docs.microsoft.com/zh-cn/aspnet/core/security/key-vault-configuration?view=aspnetcore-2.2) 【本文略】
- [用户机密 (Secret Manager)](https://docs.microsoft.com/zh-cn/aspnet/core/security/app-secrets?view=aspnetcore-2.2)（安全 主题），用户配置文件目录中的文件【本文略】

由于不同的配置提供程序对相同的配置键设置值时，存在覆盖的情况，因此在实际代码中，配置提供程序应以特定顺序排列以符合基础配置源的优先级。

配置提供程序的典型顺序为：

1. 文件（appsettings.json、appsettings.{Environment}.json，其中 `{Environment}` 是应用的当前托管环境）
2. [Azure 密钥保管库](https://docs.microsoft.com/zh-cn/aspnet/core/security/key-vault-configuration?view=aspnetcore-2.2)
3. [用户机密 (Secret Manager)](https://docs.microsoft.com/zh-cn/aspnet/core/security/app-secrets?view=aspnetcore-2.2)（仅限开发环境中）
4. 环境变量
5. 命令行参数

通常将命令行配置提供程序放在最后，以允许命令行参数替代由其他提供程序设置的配置。上述序列也是在使用 CreateDefaultBuilder 初始化新的 WebHostBuilder 时，采用的序列。

#### 使用ConfigureAppConfiguration设置应用的配置提供程序

应用的配置提供程序（包括CreateDefaultBuilder 自动添加的配置提供程序），都可以在构建主机时，通过调用ConfigureAppConfiguration方法来设置。

```c#
static Dictionary<string, string> arrayDict = new Dictionary<string, string>
{
    {"array:entries:0", "value0"},
    {"array:entries:1", "value1"}
};

public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
	//设置应用的配置提供程序
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddInMemoryCollection(arrayDict);
        config.AddJsonFile("starship.json", optional: false, reloadOnChange: false);
        config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);

        //自定义配置提供程序
        //config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));
        
        config.AddCommandLine(args);
    })
    .UseStartup<Startup>();
```

通过ConfigureAppConfiguration提供给应用的配置，可以在应用启动期间被使用，包括Startup.ConfigureServices方法。具体见下文中的“在启动期间访问配置”。

所有的配置提供程序均派生自ConfigurationProvider抽象类，ConfigurationProvider抽象类又实现了IConfigurationProvider接口：

```c#
public abstract class ConfigurationProvider : Microsoft.Extensions.Configuration.IConfigurationProvider
```

#### 命令行配置提供程序（CommandLineConfigurationProvider）

```c#
public class CommandLineConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
```

CommandLineConfigurationProvider 在运行时从命令行参数键值对加载配置。

使用 CreateDefaultBuilder 初始化新的 WebHostBuilder 时会自动调用 AddCommandLine。如果需要显式的激活命令行配置，可以在ConfigurationBuilder 的实例上调用 AddCommandLine 扩展方法：

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                // 在此处调用其他提供程序并最后调用AddCommandLine
                config.AddCommandLine(args);
            })
            .UseStartup<Startup>();
}
```

如果是直接创建 WebHostBuilder 时，使用以下配置调用 UseConfiguration：

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        var config = new ConfigurationBuilder()
        	//调用其他提供程序
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("hostsettings.json", optional: true)
            //最后调用AddCommandLine以允许参数覆盖其他配置
            .AddCommandLine(args)
            .Build();

        return WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://*:5000")
            .UseConfiguration(config)
            .Configure(app =>
            {
                app.Run(context => 
                    context.Response.WriteAsync("Hello, World!"));
            });
    }
}
```

另外，config.AddCommandLine()方法的重载版本，支持[交换映射](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2#switch-mappings)，允许键名替换。

#### 环境变量配置提供程序（EnvironmentVariablesConfigurationProvider）



```c#
public class EnvironmentVariablesConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
```

如果要激活环境变量配置，可以在 ConfigurationBuilder 的实例上调用 AddEnvironmentVariables 扩展方法，该方法允许指定将要调用的环境变量的前缀。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables(prefix: "PREFIX_");
            })
            .UseStartup<Startup>();
```

#### 文件配置提供程序（FileConfigurationProvider）

```c#
public abstract class FileConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider, IDisposable
```

FileConfigurationProvider 是从文件系统加载配置的基类。不同类型的文件有特定的配置提供程序：

- INI 配置提供程序（IniConfigurationProvider）

  ```c#
  public class IniConfigurationProvider : Microsoft.Extensions.Configuration.FileConfigurationProvider
  ```

- JSON 配置提供程序（JsonConfigurationProvider）

  ```c#
  public class JsonConfigurationProvider : Microsoft.Extensions.Configuration.FileConfigurationProvider
  ```

- XML 配置提供程序（XmlConfigurationProvider）

  ```c#
  public class XmlConfigurationProvider : Microsoft.Extensions.Configuration.FileConfigurationProvider
  ```

这些特定文件类型的配置提供程序，都派生自FileConfigurationProvider类。

##### INI 配置提供程序（IniConfigurationProvider）

IniConfigurationProvider 在运行时从 INI 文件键值对加载配置。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddIniFile(
                    "config.ini", optional: true, reloadOnChange: true);
            })
            .UseStartup<Startup>();
```

##### JSON 配置提供程序（JsonConfigurationProvider）

JsonConfigurationProvider 在运行时期间从 JSON 文件键值对加载配置。

使用 CreateDefaultBuilder 初始化新的 WebHostBuilder 时，会自动调用 AddJsonFile 两次。分别加载appsettings.json文件和appsettings.{Environment}.json文件（根据环境名称加载文件的环境版本）。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile(
                    "config.json", optional: true, reloadOnChange: true);
            })
            .UseStartup<Startup>();
```

##### XML 配置提供程序（XmlConfigurationProvider）

XmlConfigurationProvider 在运行时从 XML 文件键值对加载配置。

创建配置键值对时，将忽略配置文件的根节点。 不要在文件中指定文档类型定义 (DTD) 或命名空间。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddXmlFile(
                    "config.xml", optional: true, reloadOnChange: true);
            })
            .UseStartup<Startup>();
```

#### Key-per-file 配置提供程序（KeyPerFileConfigurationProvider）

KeyPerFileConfigurationProvider 使用目录的文件作为配置键值对。 该键是文件名。 该值包含文件的内容。

```c#
public class KeyPerFileConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
```

Key-per-file 配置提供程序常用于 Docker 托管方案。

在 ConfigurationBuilder 的实例上调用 AddKeyPerFile 扩展方法时， 文件的参数directoryPath 必须是绝对路径。

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "path/to/files");
                config.AddKeyPerFile(directoryPath: path, optional: true);
            })
            .UseStartup<Startup>();
```

#### 内存配置提供程序（MemoryConfigurationProvider）

```c#
public class MemoryConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string,string>>
```

MemoryConfigurationProvider 使用内存中集合作为配置键值对。

若要激活内存中集合配置，可以在 ConfigurationBuilder 的实例上调用 AddInMemoryCollection 扩展方法。

```c#
public class Program
{
    public static readonly Dictionary<string, string> _dict = 
        new Dictionary<string, string>
        {
            {"MemoryCollectionKey1", "value1"},
            {"MemoryCollectionKey2", "value2"}
        };

    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddInMemoryCollection(_dict);
            })
            .UseStartup<Startup>();
}
```

### 获取配置值

通常通过注入的IConfiguration来获取配置值：

```c#
public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

    public IndexModel(IConfiguration config)
    {
        _config = config;
    }
}
```

获取配置值使用的是IConfiguration接口的成员方法及其扩展方法。

#### 成员方法：GetSection()、GetChildren()

GetSection()、GetChildren()是IConfiguration接口自身的成员方法，这两个方法主要用来获取键节点，注意，这两个方法都不是获取最终的值，而是包含了Key和节点路径的配置节点。若要获取最终的值，必须结合使用GetValue、Get、Bind等方法。

##### GetSection()

使用指定的子节键提取配置节点，参数为节名称：

```c#
var configSection = _config.GetSection("section1");
var configSection = _config.GetSection("section2:subsection0");
```

GetSection 永远不会返回 null。 如果找不到匹配的节，则返回空 IConfigurationSection。因此使用它时，不用担心空引用问题。

##### GetChildren()

获取配置节点的子节点，该方法会获得 `IEnumerable<IConfigurationSection>`：

```c#
var configSection = _config.GetSection("section2");
var children = configSection.GetChildren();
```

##### IConfigurationSection的扩展方法：Exists()

Exists()是IConfigurationSection接口的扩展方法，用于判断配置节是否存在：

```c#
var sectionExists = _config.GetSection("section2:subsection2").Exists();
```

#### 扩展方法：GetValue()、Get()、Bind()

这三个方法是IConfiguration接口的扩展方法，被定义在`Microsoft.Extensions.Configuration.ConfigurationBinder`类中。

##### GetValue()

从具有指定键的配置中提取一个值，并将其转换为指定类型。如果没有找到配置键，允许提供默认值。

```c#
public int NumberConfig { get; private set; }

    public void OnGet()
    {
        NumberConfig = _config.GetValue<int>("NumberKey", 99);
    }
```

##### Bind()

使用Bind()可以将一个配置节点绑定到一个类。这个配置节点可以是JSON格式的节点，也可以是XML格式的节点。

JSON格式的节点：

```json
{
  "starship": {
    "name": "USS Enterprise",
    "registry": "NCC-1701",
    "class": "Constitution",
    "length": 304.8,
    "commissioned": false
  },
  "trademark": "Paramount Pictures Corp. http://www.paramount.com"
}
```

使用Bind()将该节点绑定到对应的类，类的结构需要JSON结构保持一致：

```c#
public class Starship
{
    public string Name { get; set; }
    public string Registry { get; set; }
    public string Class { get; set; }
    public decimal Length { get; set; }
    public bool Commissioned { get; set; }
}
//Bind()用法
var starship = new Starship(); //实例化一个对象
_config.GetSection("starship").Bind(starship); //将该对象和节点绑定
Starship = starship;//获取绑定后得到的对象
```

Bind()方法不仅可以绑定简单的类（类的成员属性都是简单类型，不是其他类的引用），[也可以绑定复杂的类](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2#bind-to-an-object-graph)（类的成员属性引用了其他类）。前提是配置节点的结构一定要和类结构保持一致，否则不能正确绑定。

如果定义的类中包含了数组属性，而配置节点又包含数组节点（例如JSON格式的对象，值为一个数组），也可以使用Bind()方法将[配置节点的值绑定到包含了数组属性的类对象上](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2#bind-an-array-to-a-class)。

##### Get()

Get()方法和Bind()一样，都实现绑定并返回指定类型，不同的是Get()比使用Bind()要更方便。上述代码改用Get()调用：

```c#
Starship = _config.GetSection("starship").Get<Starship>();
```

### 自定义配置提供程序

上文中讲到的所有配置提供程序，都是通过如下代码添加激活的：

```c#
WebHost.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddInMemoryCollection(arrayDict);
        config.AddJsonFile(
            "json_array.json", optional: false, reloadOnChange: false);
        config.AddJsonFile(
            "starship.json", optional: false, reloadOnChange: false);
        config.AddXmlFile(
            "tvshow.xml", optional: false, reloadOnChange: false);
        config.AddEFConfiguration(
            options => options.UseInMemoryDatabase("InMemoryDb"));
        config.AddCommandLine(args);
    })
    .UseStartup<Startup>();
```

无论是使用AddJsonFile()、AddXmlFile()，还是AddCommandLine()等其他方法，这些方法的内部，调用的都是IConfigurationBuilder的扩展方法Add()方法，该方法位于Microsoft.Extensions.Configuration.ConfigurationExtensions类中，其定义和实现如下：

```c#
public static IConfigurationBuilder Add<TSource>(this IConfigurationBuilder builder, Action<TSource> configureSource) where TSource : IConfigurationSource, new()
{
	TSource val = new TSource();
	configureSource?.Invoke(val); //执行委托方法
    //此处调用的是IConfigurationBuilder成员方法Add()
	return builder.Add((IConfigurationSource)(object)val); 
}
```

需要特别注意的是，该扩展方法的内部，调用的是IConfigurationBuilder接口的Add()方法，该方法位于Microsoft.Extensions.Configuration.IConfigurationBuilder接口中，定义如下：

```c#
IConfigurationBuilder Add(IConfigurationSource source);
```

因此，当需要自定义配置程序时，也需要进行IConfigurationBuilder的Add()方法的调用，无论该方法采用的是扩展版本还是原生版本，IConfigurationSource都必不可少。因此可以围绕IConfigurationSource自定义配置程序。

#### 第一步：实现IConfigurationSource接口

该接口只有一个成员方法Build()，当定义了一个实现IConfigurationSource接口的类时，必须实现该方法：

```c#
public class AyConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        //此处需要返回IConfigurationProvider
        throw new NotImplementedException();
        //return new AyConfigurationProvider();
    }
}
```

该方法的builder参数来自于外部的调用，可以添加断点调试看看。由于该方法返回的是IConfigurationProvider，因此还需要提供实现了IConfigurationProvider接口的类。

#### 第二步：实现IConfigurationProvider接口

该接口提供了应用程序的配置键/值操作，可以自定义一个实现了IConfigurationProvider接口的类。通常，可以直接使用ConfigurationProvider类，该类已经实现了IConfigurationProvider接口，因此可以直接定义一个类派生自ConfigurationProvider类即可，并且需要重写Load()方法，该方法用于加载配置值，将配置键/对，赋值给ConfigurationProvider类的Data属性。

```c#
public class AyConfigurationProvider: ConfigurationProvider
{
    public override void Load()
    {
        Data = new Dictionary<string, string>
        {
            { "quote1", "I aim to misbehave." },
            { "quote2", "I swallowed a bug." },
            { "quote3", "You can't stop the signal, Mal." }
        };
    }
}
```

#### 只有两步的完成调用

一旦完成了上述两个步骤，就可以在Program.cs中，通过ConfigureAppConfiguration方法进行配置提供程序的添加操作，需要调用IConfigurationBuilder的Add()方法进行添加：

```c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) => {
        config.Add(new AyConfigurationSource());
    })
    .UseStartup<Startup>();
```

可以在Startup.cs中，访问配置，查看运行情况：

```c#
public void Configure(
    IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.Run(async (context) =>
    {
        await context.Response.WriteAsync(config["quote1"]);
    });
}
```

虽然可以直接通过IConfigurationBuilder.Add()方法直接添加配置提供程序，但一般为了代码结构更加易用，会定义一个额外的扩展方法，在扩展方法中，执行IConfigurationBuilder.Add()的调用。

#### 第三步：定义IConfigurationBuilder的扩展方法（非必需，但推荐使用）

```c#
public static class AyConfigurationExtensions
{
    public static IConfigurationBuilder AddAyConfiguration(this IConfigurationBuilder builder)
    {
        return builder.Add(new AyConfigurationSource());
    }
}
```

改写Program.cs中的代码：

```c#
WebHost.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((hostingContext, config) => {
    config.AddAyConfiguration(); //类似于config.AddJsonFile()形式
})
.UseStartup<Startup>();
```

上述就是创建自定义配置提供程序的3个步骤，代码相对比较简洁，不包含外部的其他调用，相对不太实用，可以对其进行扩展。

只需要对AyConfigurationProvider类定义一个匿名委托的属性，并在该类的构造方法中进行引入：

```c#
public class AyConfigurationProvider: ConfigurationProvider
{
    Action<AyInfo> AyAction{ get; }

    public AyConfigurationProvider(Action<AyInfo> _ayAction)
    {
        AyAction = _ayAction;
    }

    public override void Load()
    {
       var ay= new AyInfo { Key = "AAA", Value = "BBB" };
        //为匿名委托传入实参
        AyAction(ay);

        Data = new Dictionary<string, string>
        {
            { "quote1", "I aim to misbehave." },
            { "quote2", "I swallowed a bug." },
            { "quote3", "You can't stop the signal, Mal." }
        };
        Data[ay.Key] = ay.Value;

    }
}
```

AyConfigurationSource类也同样使用构造函数进行引入：

```c#
public class AyConfigurationSource : IConfigurationSource
{
    private readonly Action<AyInfo> AyAction;
    public AyConfigurationSource(Action<AyInfo> _ayAction)
    {
        AyAction = _ayAction;
    }
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        //此处需要返回IConfigurationProvider
        return new AyConfigurationProvider(AyAction);
    }
}
```

AyConfigurationExtensions类同样如此：

```c#
public static class AyConfigurationExtensions
{
    public static IConfigurationBuilder AddAyConfiguration(
    this IConfigurationBuilder builder,
    Action<AyInfo> AyAction)
    {
        return builder.Add(new AyConfigurationSource(AyAction));
    }
}
```

在Program.cs中，进行匿名委托函数的操作指定：

```c#
WebHost.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((hostingContext, config) => {
    //ayinfo是形参
    config.AddAyConfiguration(ayinfo=> {
        //为委托指定操作
        ayinfo.Value = ayinfo.Key + ayinfo.Value;
    });
})
.UseStartup<Startup>();
```

以上就是自定义配置提供程序的全部内容，官方提供了一个基于EF的配置提供程序，原理都是一样的。

需要注意的是，在提供匿名委托方法时，参数一定要是引用类型。例如上文中的AyInfo类型。

另外，可以在Startup中的Configure()方法中，直接访问配置值，代码见上文第二步。



## 选项（Options）

选项是配置的扩展，使用选项：

- 可以将相关设置通过类来表示
- 能够更好的遵循关注点分离原则和接口分离原则（封装）
- 支持配置数据的验证机制

基于选项的配置，都会调用IServiceCollection的AddOptions()扩展方法，该方法的定义和实现如下：

```c#
public static IServiceCollection AddOptions(this IServiceCollection services)
{
    if (services == null)
    {
        throw new ArgumentNullException("services");
    }
	services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptions<>), typeof(OptionsManager<>)));
	services.TryAdd(ServiceDescriptor.Scoped(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));
	services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptionsMonitor<>), typeof(OptionsMonitor<>)));
	services.TryAdd(ServiceDescriptor.Transient(typeof(IOptionsFactory<>), typeof(OptionsFactory<>)));
	services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptionsMonitorCache<>), typeof(OptionsCache<>)));
	return services;
}
```

该方法用于添加使用选项所需的服务，可以简单理解为初始化选项配置服务。所有的选项配置方法，都会调用该方法。

特别注意：该方法返回的类型为IServiceCollection，需要和另一个AddOptions<T>()方法区分开来，后者返回的是OptionsBuilder类型（下文会介绍该方法的用法）。

当在Startup.ConfigureServices()方法中，对选项进行配置时，常见的几种方法如下：

- services.AddOptions<T>() 及其重载方法（该方法的内部会调用IServiceCollection.AddOptions()方法）
- services.Configure<T>()及其重载方法（该方法的内部也会调用IServiceCollection.AddOptions()方法）
- services.ConfigureAll<T>()（该方法的内部调用的是services.Configure()方法）
- service.PostConfigure<T>()及其重载方法（该方法的内部也会调用IServiceCollection.AddOptions()方法）
- services.PostConfigureAll<T>()（该方法的内部调用的是services.PostConfigure()方法）

上述这些方法的内部，都会先调用IServiceCollection.AddOptions()方法，完成必要服务的添加工作后，才能注入选项配置。

### `IServiceCollection.Configure<T>()`

Configure()是IServiceCollection的扩展方法，由Microsoft.Extensions.DependencyInjection.OptionsConfigurationServiceCollectionExtensions类和Microsoft.Extensions.DependencyInjection.OptionsServiceCollectionExtensions类提供。这两个类提供的Configure()方法的主要区别是：

OptionsConfigurationServiceCollectionExtensions中的Configure()方法，都需要传入IConfiguration类型的参数；而OptionsServiceCollectionExtensions中的Configure()方法，不需要提供IConfiguration类型的参数。

#### 带有IConfiguration类型参数的Configure()方法

##### `Configure<TOptions>(IConfiguration config) `



##### `Configure<TOptions>(string name, IConfiguration config)`





















