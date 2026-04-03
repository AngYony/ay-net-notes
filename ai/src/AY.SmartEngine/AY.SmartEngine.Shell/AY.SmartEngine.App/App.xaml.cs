using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog.Extensions.Hosting;
using Serilog.Settings.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Serilog;
using Microsoft.EntityFrameworkCore;
using AY.SmartEngine.Infrastructure.Repositories.DbContexts;
using AY.SmartEngine.Shared.Extensions;
using AY.SmartEngine.Infrastructure.Repositories.Repositories;
using AY.SmartEngine.ApplicatonServices.Users;
using AY.SmartEngine.App.ViewModels;

namespace AY.SmartEngine.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // 使用非空的 AppHost，确保生命周期内可用
        public static IHost AppHost { get; private set; } = null!;

        public new static App Current => (App)Application.Current;

        /// <summary>
        /// 公开 Services 属性以保持兼容性
        /// </summary>
        public IServiceProvider Services => AppHost.Services;


        [STAThread]
        public static void Main(string[] args)
        {
            // 1. 引导日志 (Bootstrap Logger)
            Log.Logger = new LoggerConfiguration()
            //.MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateBootstrapLogger();

            try
            {
                // 2. 构建 Host
                AppHost = CreateHostBuilder(args).Build();

                // 3. 启动 WPF 实例
                // 注意：不再手动 new MainWindow，而是交给 DI
                var app = new App();
                app.InitializeComponent();
                app.Run(); // 具体的窗口启动逻辑移到了 OnStartup
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }






        /// <summary>
        /// 创建通用 Host 构造器
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // 1. 配置加载 (appsettings.json & mysettings.json)
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile("mysettings.json", optional: false, reloadOnChange: true)
                          .AddEnvironmentVariables();
                })
                //// 2. 集成 Serilog
                .UseSerilog((context, services, loggerConfig) =>
                {
                    // 完全依赖配置文件，代码中只保留最基础的设置
                    loggerConfig.ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext();

                    //.Enrich.FromLogContext()
                    //    .WriteTo.Console()
                    //    .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day);
                })
                // 3. 服务注入
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;
                    //添加其他JSON配置项
                    AddOptionsBinding(services, configuration);
                    //添加数据库支持
                    AddDbContextFactory(services, configuration);

                    //注册Repository
                    services.RegisterServices(typeof(UserRepository).Assembly, "Repository");
                    //注册ApplicationServices
                    services.RegisterServices(typeof(UserService).Assembly, "Service");

                    //// 注册导航和其他单例服务
                    //services.AddSingleton<NavigationService>();

                    //// 注册 ViewModels
                    //services.AddTransient<HomeViewModel>();

                    // 注册 MainWindow 和其 ViewModel
                    // 使用您原有的扩展方法或标准写法
                    services.AddViewModel<MainWindow, MainViewModel>();
                });

        /// <summary>
        /// 应用数据库迁移
        /// </summary>
        private async Task ApplyDatabaseMigrationsAsync()
        {
            Log.Information("Checking database migrations...");

            using var scope = AppHost.Services.CreateScope();
            var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<LearningTagDbContext>>();
            using var db = await dbFactory.CreateDbContextAsync();
            // 仅在有待处理迁移时执行
            var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                Log.Information("Applying {Count} migrations...", pendingMigrations.Count());
                await db.Database.MigrateAsync();
            }
        }

        private static IServiceCollection AddOptionsBinding(IServiceCollection services, IConfiguration configuration)
        {
            //https://learn.microsoft.com/zh-cn/dotnet/core/extensions/configuration-providers
            //方式一：绑定配置并直接添加到服务容器中（推荐），可以直接通过注入的方式读取到配置项，最推荐方式
            //services.Configure<MySettings>(configuration.GetSection("Settings"));

            ////方式二：直接读取配置文件到实体中
            //configuration.GetRequiredSection("settings").Get<Settings>();

            ////方式三：读取配置绑定到实体
            //Settings options = new();
            //configuration.GetSection(nameof(Settings)).Bind(options);

            ////方式四：
            //configuration.GetSection(nameof(Settings)).Get<Settings>();

            return services;
        }


        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AddDbContextFactory(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //从配置文件中获取迁移程序集信息，好处是：可以不需要显式添加程序集的引用就可以注册服务
            var migrationsAssembly = configuration.GetValue("MigrationsAssembly", string.Empty);
            services.AddDbContextFactory<LearningTagDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlite(connectionString, sqlOptions =>
                {
                    if (!string.IsNullOrEmpty(migrationsAssembly))
                    {
                        //指定迁移程序集
                        sqlOptions.MigrationsAssembly(migrationsAssembly);
                    }
                });
            });

            return services;
        }






        protected override async void OnStartup(StartupEventArgs e)
        {
            //必须首先调用
            base.OnStartup(e);

            try
            {
                // 启动 Host (启动后台服务等)
                await AppHost!.StartAsync();

                // 逻辑处理：读取连接字符串（如果还有必要，通常 EF 已在 DI 配置好）
                //ConfigHelper.ReadConnectionString();

                // 执行数据库迁移
                await ApplyDatabaseMigrationsAsync();

                // 从容器中获取 MainWindow 并显示
                var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();

                Log.Information("Application started successfully.");
            }
            catch (Exception ex)
            {
                // 记录错误日志并优雅退出
                Log.Error(ex, "An error occurred during application startup.");
                MessageBox.Show($"应用初始化失败: {ex.Message}", "严重错误", MessageBoxButton.OK, MessageBoxImage.Error);
                // 优雅退出，因为 base.OnStartup 已经跑过，这里调用 Shutdown 是安全的
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            Log.Information("Application exiting...");
            try
            {
                // 设置超时时间，防止某些后台任务卡死导致进程无法关闭
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                await AppHost.StopAsync(cts.Token);
            }
            finally
            {
                AppHost.Dispose();
                base.OnExit(e);
            }
        }


    }

}
