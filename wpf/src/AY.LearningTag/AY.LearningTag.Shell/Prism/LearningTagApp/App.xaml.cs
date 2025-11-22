
using AY.Utils;
using LearningTag.PrismShared.Regions;
using LearningTagApp.ViewModels;
using LearningTagApp.Views;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LearningTagApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger _logger;

        protected override Window CreateShell()
        {
            // 全局异常捕捉
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            // 任务异常捕捉
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                _logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
                e.SetObserved();
            };
            // AppDomain异常捕捉
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    _logger.LogCritical($"{ex.StackTrace},{ex.Message}");
                }
                //记录dump文件
                MiniDump.TryDump($"dumps\\LearningTag_{DateTime.Now:HH-mm-ss-ms}.dmp");
            };

            return Container.Resolve<MainWindow>();
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //通常全局异常捕捉的都是致命信息
            _logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var factory = new NLogLoggerFactory();
            _logger = factory.CreateLogger("NLog");
            containerRegistry.RegisterInstance(_logger);

            containerRegistry.RegisterForNavigation<LearnMainView, LearnMainViewModel>();

            containerRegistry.RegisterDialog<MessageDialogView, MessageDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<LearningTag.Setting.SettingModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            //regionAdapterMappings.RegisterMapping<StackPanel, StackPanelRegionAdapter>();
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing("CusViewRegionBehavior", typeof(CusViewRegionBehavior));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //添加About项目，通过目录+反射读取DLL文件中的Module
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        /// <summary>
        /// 用于将ViewModel与View进行关联
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            //ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), typeof(MainWindowViewModel));
            //ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
    }
}