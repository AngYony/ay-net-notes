using LearningTag.PrismShared.Events;
using LearningTag.PrismShared.ViewModel;
using Prism.Commands;
using Prism.Events;

namespace LearningTag.Setting.ViewModels
{
    public class SystemSettingViewModel : TabBaseViewModel
    {
        private string _dataStoragePath;

        public string DataStoragePath
        {
            get { return _dataStoragePath; }
            set { SetProperty(ref _dataStoragePath, value); }
        }

        public DelegateCommand SaveSystemSettingCommand { get; }

        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingApplicationCommands _settingApplicationCommands;

        public SystemSettingViewModel(IEventAggregator eventAggregator, ISettingApplicationCommands settingApplicationCommands)
        {
            this._eventAggregator = eventAggregator;
            this._settingApplicationCommands = settingApplicationCommands;
            _eventAggregator.GetEvent<MessageEvent>().Subscribe(
               OnSubscribeMessage,
                ThreadOption.PublisherThread,
                false,
                msg => { return msg.MessageType == PubSubEventMessageType.SaveSettings; });

            SaveSystemSettingCommand = new DelegateCommand(OnSaveSystemSettings, canExecuteMethod).ObservesProperty(() => DataStoragePath);
            _settingApplicationCommands.SaveAllCompositeCommand.RegisterCommand(SaveSystemSettingCommand);
        }

        private bool canExecuteMethod()
        {
            return !string.IsNullOrWhiteSpace(this.DataStoragePath);
        }

        private void OnSaveSystemSettings()
        {
            //保存操作
        }

        private void OnSubscribeMessage(MassgeInfo info)
        {
        }
    }
}