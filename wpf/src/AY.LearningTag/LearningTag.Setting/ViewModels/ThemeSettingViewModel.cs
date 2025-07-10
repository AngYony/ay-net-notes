using LearningTag.Shared.Events;
using LearningTag.Shared.ViewModel;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Setting.ViewModels
{
    public class ThemeSettingViewModel:TabBaseViewModel
    {

        private string _theme;

        public string Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }
        public DelegateCommand SaveThemeSettingCommand { get; }

        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingApplicationCommands _settingApplicationCommands;

        public ThemeSettingViewModel(IEventAggregator eventAggregator, ISettingApplicationCommands settingApplicationCommands)
        {
            this._eventAggregator = eventAggregator;
            this._settingApplicationCommands = settingApplicationCommands;
            _eventAggregator.GetEvent<MessageEvent>().Subscribe(
               OnSubscribeMessage,
                ThreadOption.PublisherThread,
                false,
                msg => { return msg.MessageType == PubSubEventMessageType.SaveSettings; });
            this._eventAggregator = eventAggregator;

            SaveThemeSettingCommand = new DelegateCommand(OnSaveThemeSetting);
            _settingApplicationCommands.SaveAllCompositeCommand.RegisterCommand(SaveThemeSettingCommand);
        }

        private void OnSaveThemeSetting()
        { 
        }

        private void OnSubscribeMessage(MassgeInfo info)
        {

        }
    }
}
