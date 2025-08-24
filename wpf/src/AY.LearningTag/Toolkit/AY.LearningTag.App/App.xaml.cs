using AY.LearningTag.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using AY.LearningTag.Shared;
using Serilog;
using System.Configuration;
using System.Data;
using System.Windows;
using System;
using AY.LearningTag.ToolKitShared;
using AY.LearningTag.App.ControllSample;
using AY.LearningTag.App.Stores;
using AY.LearningTag.App.Services;
using AY.LearningTag.App.ControllSample.ListBox;

namespace AY.LearningTag.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //将App.xaml设为生成页
        [STAThread]
        static void Main(string[] args)
        {
            //todo:创建Host

            var app = new App();
            app.InitializeComponent();
            app.Run();
        }


        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigHelper.ReadConnectionString();
            //var store = Services.GetRequiredService<NavigationStore>();
            //store.CurrentViewModel = new HomeViewModel(store);
            new YuanShen().Show();

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
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<NavigationService>();



            //添加日志服务
            services.AddLog()
                .AddViewModel<MainWindow, MainViewModel>();




            return services.BuildServiceProvider();
        }



    }

}
