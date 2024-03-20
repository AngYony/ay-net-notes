using Prism.Ioc;
using Prism.Modularity;
using PrismSample.FullApp.Modules.ModuleName;
using PrismSample.FullApp.Services;
using PrismSample.FullApp.Services.Interfaces;
using PrismSample.FullApp.Views;
using System.Windows;

namespace PrismSample.FullApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
