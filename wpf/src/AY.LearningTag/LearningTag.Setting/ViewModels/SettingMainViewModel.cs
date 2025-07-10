using LearningTag.Setting.Views;
using LearningTag.Shared.Events;
using LearningTag.Shared.ViewModel;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningTag.Setting.ViewModels
{
    public class SettingMainViewModel :ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator; 

        public SettingMainViewModel(IRegionManager regionManager, IContainerProvider containerProvider,

            IEventAggregator eventAggregator,ISettingApplicationCommands settingApplicationCommands)
        {
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;
            SettingApplicationCommands = settingApplicationCommands;

            //IRegion region = regionManager.Regions["SettingContentRegion"];
            //var tabSystemSetting = containerProvider.Resolve<SystemSettingView>();
            //(tabSystemSetting.DataContext as TabBaseViewModel).Title = "系统设置";
            //region.Add(tabSystemSetting);

            //var tabThemeSetting = containerProvider.Resolve<ThemeSettingView>();
            //(tabThemeSetting.DataContext as TabBaseViewModel).Title = "设置B";
            //region.Add(tabThemeSetting);

        }

        public ISettingApplicationCommands SettingApplicationCommands { get; }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // 根据业务需要调整该视图，是否创建新实例。为true表示不创建新实例，直接复用当前的实例，页面还是之前的
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //导航离开时执行的逻辑
            SettingApplicationCommands.SaveAllCompositeCommand.Execute(null);


            ////离开时发送保存通知
            //_eventAggregator.GetEvent<MessageEvent>().Publish(new MassgeInfo
            //{
            //    MessageType = PubSubEventMessageType.SaveSettings
            //});
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            

            var parameter = navigationContext.Parameters["FromMainWindowPar"];
            if (parameter != null)
            {

            }
        }
    }
}
