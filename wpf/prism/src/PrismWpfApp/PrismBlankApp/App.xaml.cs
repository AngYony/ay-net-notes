using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismBlankApp.ViewModels;
using PrismBlankApp.Views;
using System.Windows;
using System.Windows.Controls;

namespace PrismBlankApp
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
            //注册导航
            containerRegistry.RegisterForNavigation<ViewB>("pageA"); //设置导航别名为pageA
            containerRegistry.RegisterForNavigation<ViewC>();
            //注册对话
            containerRegistry.RegisterDialog<MsgView, MsgViewModel>("myDialog");//设置别名为myDialog，如果不设置默认为窗体名称msgView
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleA.ModuleAModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            //regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdaper>());
            regionAdapterMappings.RegisterMapping<StackPanel, StackPanelRegionAdaper>();
        }
    }
}
