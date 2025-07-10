using LearningTag.Setting.ViewModels;
using LearningTag.Setting.Views;
using LearningTag.Shared.ViewModel;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace LearningTag.Setting
{
    public class SettingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

            var regionManager = containerProvider.Resolve<IRegionManager>();
            


            //var contengRegion = regionManager.Regions["MainContentRegion"];
            
            
            ////初始化SettingMainView页面
            //contengRegion.RequestNavigate(nameof(SettingMainView));




            //IRegion region = regionManager.Regions["SettingContentRegion"];
            //var tabSystemSetting = containerProvider.Resolve<SystemSettingView>();
            //(tabSystemSetting.DataContext as TabBaseViewModel).Title = "系统设置";
            //region.Add(tabSystemSetting);

            //var tabThemeSetting = containerProvider.Resolve<ThemeSettingView>();
            //(tabThemeSetting.DataContext as TabBaseViewModel).Title = "设置B";
            //region.Add(tabThemeSetting);

        }

        void SetTitle(TabBaseViewModel tab, string title)
        {
            tab.Title = title;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISettingApplicationCommands, SettingApplicationCommands>();
            containerRegistry.RegisterForNavigation<SettingMainView, SettingMainViewModel>();





            //ViewModelLocationProvider.Register<SettingMainView, SettingMainViewModel>(); //这种方式可以让Prism按照指定的View和ViewModel进行绑定，当Prism无法自动绑定时会很有用

            //containerRegistry.RegisterForNavigation<SettingAView, SettingAViewModel>();
            //containerRegistry.RegisterForNavigation<SettingBView, SettingBViewModel>();

        }

    }
}