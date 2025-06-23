using LearningTag.Setting.ViewModels;
using LearningTag.Setting.Views;
using LearningTag.Shared.ViewModel;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LearningTag.Setting
{
    //[Module(ModuleName = "Setting")]
    public class SettingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

            var regionManager = containerProvider.Resolve<IRegionManager>();
            IRegion region = regionManager.Regions["SettingContentRegion"];


            var settingA = containerProvider.Resolve<SystemSettingView>();
            (settingA.DataContext as TabBaseViewModel).Title = "系统设置";
            region.Add(settingA);

            var settingB = containerProvider.Resolve<SettingBView>();
            (settingB.DataContext as TabBaseViewModel).Title = "设置B";
            region.Add(settingB);

             
            //regionManager.RegisterViewWithRegion("LeftRegion", typeof(MessageView));
            //IRegion region = regionManager.Regions["ContentRegion"];
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<SettingAView, SettingAViewModel>();
            //containerRegistry.RegisterForNavigation<SettingBView, SettingBViewModel>();

        }

    }
}