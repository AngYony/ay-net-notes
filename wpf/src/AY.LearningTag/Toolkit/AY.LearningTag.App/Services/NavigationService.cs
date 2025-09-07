﻿using AY.LearningTag.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.App.Services
{
    /// <summary>
    /// Toolkit实现导航
    /// </summary>
    public class NavigationService
    {
        public event Action? CurrentViewModelChanged;

        private ViewModelBase? _currentViewMode;
        public ViewModelBase? CurrentViewModel
        {
            get { return _currentViewMode; }
            set
            {
                _currentViewMode = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public void NavigateTo(ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
        }

        public void NavigateTo<T>() where T : ViewModelBase
        {
            CurrentViewModel = App.Current.Services.GetService<T>();
        }
    }
}
