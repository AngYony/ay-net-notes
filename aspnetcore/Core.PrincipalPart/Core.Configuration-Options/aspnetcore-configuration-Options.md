# ASP.NET Core 中的配置（Configuration）与选项（Options ）

**必读部分**

- Configuration：配置

- Options：选项

- Configuration和Options之间的关系：

  Options是Configuration的扩展模式，在未使用Options时，一般通过配置提供程序将配置数据从各种配置源读取到键值对集中，而使用Options后，可以直接使用类来表示相关设置的组。大多数情况下，更倾向于使用Options模式来处理配置。（正是因为如此，本文将配置和选项合并为一篇文章。）



## 配置

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

获取配置值需要用到的主要有以下几个方法：

- GetValue：从具有指定键的配置中提取一个值，并将其转换为指定类型。如果没有找到配置键，允许提供默认值。

  ```c#
  public int NumberConfig { get; private set; }
  
      public void OnGet()
      {
          NumberConfig = _config.GetValue<int>("NumberKey", 99);
      }
  ```

- GetSection：使用指定的子节键提取配置子节。

  ```c#
  var configSection = _config.GetSection("section1");
  var configSection = _config.GetSection("section2:subsection0");
  ```

- GetChildren：会获得 `IEnumerable<IConfigurationSection>`。

  ```c#
  var configSection = _config.GetSection("section2");
  var children = configSection.GetChildren();
  ```

- Exists：用于判断配置节是否存在

  ```c#
  var sectionExists = _config.GetSection("section2:subsection2").Exists();
  ```

### 将配置绑定至类







