using AY.LearningTag.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.App.Services
{
    public class NavigationService
    {
        //public static NavigationService Instance { get; } = new NavigationService();


        public event Action? CurrentViewModelChanged;

        private ViewModelBase? _currentViewMode;
        public ViewModelBase? CurrentViewModel
        {
            get { return _currentViewMode; }
            set
            {
                _currentViewMode = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public void NavigateTo<T>() where T:ViewModelBase
        {
            CurrentViewModel = App.Current.Services.GetService<T>();
        }

    }
}
