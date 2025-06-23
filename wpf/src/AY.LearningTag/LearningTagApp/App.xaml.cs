
using LearningTag.Shared.RegionAdapters;
using LearningTagApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace LearningTagApp
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

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<LearningTag.Setting.SettingModule>();


        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            //regionAdapterMappings.RegisterMapping<StackPanel, StackPanelRegionAdapter>();
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }


    }
}
