using LearningTag.Shared.Events;
using LearningTag.Shared.ViewModel;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Setting.ViewModels
{
    public class SystemSettingViewModel : TabBaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public SystemSettingViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageEvent>().Subscribe(
               OnSubscribeMessage,
                ThreadOption.PublisherThread,
                false,
                msg => { return msg.MessageType == PubSubEventMessageType.SaveSettings; });
        }

        private void OnSubscribeMessage(MassgeInfo info)
        {
            
        }
    }
}
