using AY.LearningTag.App.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.App.ViewModels
{
    public partial class MainViewModel : ObservableRecipient
    {
        [ObservableProperty]
        string title = "Learning Tag App";



        public MainViewModel(ILogger<MainViewModel> logger)
        {
            logger.LogInformation(Title);

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<RecordMessage>>(this, Receive);
        }

        private void Receive(object recipient, ValueChangedMessage<RecordMessage> message)
        {
            //message.Value.RecordTitle;
        }
    }
}
