using AY.LearningTag.App.ControllSample;
using AY.LearningTag.App.ControllSample.ListBox;
using AY.LearningTag.App.Services;
using AY.LearningTag.App.ViewModels;
using AY.LearningTag.Shared;
using AY.LearningTag.ToolKitShared;
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
            //todo:创建Host

            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public IServiceProvider Services { get; }
        public IConfiguration? Configuration { get; }

        public App()
        {
            //先创建配置服务
            Configuration = ConfigureAppSettings();
            Services = ConfigureServices();
           
        }

        private IConfiguration? ConfigureAppSettings()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigHelper.ReadConnectionString();
            //var store = Services.GetRequiredService<NavigationStore>();
            //store.CurrentViewModel = new HomeViewModel(store);
            new ListBoxGroupSampleA().Show();

            // Resolve the MainWindow from the service provider
            //var mainWindow = Services.GetRequiredService<MainWindow>();

            //mainWindow.Show();

        }


        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>


        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddConfigureService(App.Current.Configuration!);

            services.AddSingleton<NavigationService>();
            services.AddTransient<HomeViewModel>();

          

            //添加日志服务
            services.AddLog()
                .AddViewModel<MainWindow, MainViewModel>();




            return services.BuildServiceProvider();
        }



    }

}
