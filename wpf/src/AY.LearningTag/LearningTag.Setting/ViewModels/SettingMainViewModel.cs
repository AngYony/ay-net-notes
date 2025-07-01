using LearningTag.Shared.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningTag.Setting.ViewModels
{
    public class SettingMainViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;

        public SettingMainViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // 根据业务需要调整该视图，是否创建新实例。为true表示不创建新实例，直接复用当前的实例，页面还是之前的
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //导航离开时执行的逻辑

            //离开时发送保存通知
            _eventAggregator.GetEvent<MessageEvent>().Publish(new MassgeInfo
            {
                MessageType = PubSubEventMessageType.SaveSettings
            });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            

            var parameter = navigationContext.Parameters["FromMainWindowPar"];
            if (parameter != null)
            {

            }
        }
    }
}
