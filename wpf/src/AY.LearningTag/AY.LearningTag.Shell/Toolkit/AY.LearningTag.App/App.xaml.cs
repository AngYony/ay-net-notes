using AY.LearningTag.App.ControllSample;
using AY.LearningTag.App.ControllSample.ListBox;
using AY.LearningTag.App.Services;
using AY.LearningTag.App.ViewModels;
using AY.LearningTag.Domain.EFCore.Repositories;
using AY.LearningTag.Infrastructure.EntityFrameworkCore;
using AY.LearningTag.Shared;
using AY.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AY.LearningTag.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //将App.xaml的属性设为生成页
        [STAThread]
        static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }
        public IConfiguration? Configuration { get; }

        public App()
        {
            //先创建配置服务
            Configuration = BuildConfiguration();
            Services = BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigHelper.ReadConnectionString();
            // 应用迁移
            ApplyDatabaseMigrations();
             



            //var store = Services.GetRequiredService<NavigationStore>();
            //store.CurrentViewModel = new HomeViewModel(store);
            //new ListBoxGroupSampleA().Show();

            // Resolve the MainWindow from the service provider
            var mainWindow = Services.GetRequiredService<MainWindow>();

            mainWindow.Show();

        }

        /// <summary>
        /// 应用数据库迁移
        /// </summary>
        private void ApplyDatabaseMigrations()
        {
            using var scope = Services.CreateScope();
            var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<LearningTagDbContext>>();
            using var db = dbFactory.CreateDbContext();
            db.Database.Migrate();  // <-- 自动执行迁移（如果还没应用）
        }




        /// <summary>
        /// 生成配置组件
        /// </summary>
        /// <returns></returns>
        private IConfiguration? BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile("mysettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }


        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            //添加其他json配置项
            services.AddConfigureEx(this.Configuration)
                    //添加数据库服务
                    .AddPooledDbContextFactoryEx(this.Configuration)
                    //添加文件服务
                    .AddLogEx();

            services.AddScoped(typeof(IEFCoreRepository<,>), typeof(EFCoreRepository<,>));
            services.AddScoped(typeof(IEFCoreRepository<,,>), typeof(EFCoreRepository<,,>));
            services.AddScoped<ISectionRepository<LearningTagDbContext>, SectionRepository>();



            services.AddSingleton<NavigationService>();
            services.AddTransient<HomeViewModel>();
            //添加日志服务
            services.AddViewModel<MainWindow, MainViewModel>();


            // 假设你有多个接口和实现类需要注入
            //services.Scan(scan => scan
            //    .FromAssemblyOf<IMyService>()  // 扫描包含接口和实现的程序集
            //    .AddClasses(classes => classes.AssignableTo<IMyService>())
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime()
            //);

            return services.BuildServiceProvider();
        }
















    }

}
