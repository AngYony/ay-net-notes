﻿using LearningTagApp.ViewModels;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace LearningTagApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var _vm = this.DataContext as MainWindowViewModel;
            if (_vm != null) {
                _vm.WindowLoadedCommand.Execute();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UserRegistWindow().ShowDialog();
        }
    }
}
