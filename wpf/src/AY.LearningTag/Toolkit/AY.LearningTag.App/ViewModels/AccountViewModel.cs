using AY.LearningTag.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AY.LearningTag.App.ViewModels
{
    public partial class AccountViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;
        [ObservableProperty]
        private string name = "Account View Model";

        public AccountViewModel(NavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        [RelayCommand]
        private void NavigateToHome()
        {
            _navigationService.NavigateTo(new HomeViewModel(_navigationService));
        }
    }
}