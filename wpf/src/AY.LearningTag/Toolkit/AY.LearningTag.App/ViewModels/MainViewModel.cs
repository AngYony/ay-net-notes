using AY.LearningTag.App.Messages;
using AY.LearningTag.App.Services;
using AY.LearningTag.App.Stores;
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
    public partial class MainViewModel : ViewModelBase
    {


        [ObservableProperty]
        private string title = "Learning Tag App";
        private readonly NavigationService _navigationService;

        public ViewModelBase CurrentViewModel => _navigationService.CurrentViewModel;

        public MainViewModel(NavigationService navigationService)
        {
            this._navigationService = navigationService;
            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _navigationService.CurrentViewModel = new HomeViewModel(navigationService);

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }




        //public MainViewModel(ILogger<MainViewModel> logger)
        //{
        //    logger.LogInformation(Title);

        //    WeakReferenceMessenger.Default.Register<ValueChangedMessage<RecordMessage>>(this, Receive);
        //}

        //private void Receive(object recipient, ValueChangedMessage<RecordMessage> message)
        //{
        //    //message.Value.RecordTitle;
        //}
    }
}
