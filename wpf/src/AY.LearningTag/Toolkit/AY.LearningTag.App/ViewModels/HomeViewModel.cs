using AY.LearningTag.App.Services;
using AY.LearningTag.App.Stores;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.App.ViewModels
{
    public partial class HomeViewModel:ViewModelBase
    {
        private readonly NavigationService _navigationService;

        public string WelcomeMessage=> "Welcome to Learning Tag App!";

        public HomeViewModel(NavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        [RelayCommand]
        private void NavigateToAccount()
        {
            _navigationService.NavigateTo(new AccountViewModel(_navigationService));
        }
    }
}
