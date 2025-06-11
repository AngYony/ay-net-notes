using MyToDo.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>()
            {
                new MenuBar(){ Icon="Home", Title="首页", NameSpace="IndexView" },
                new MenuBar(){ Icon="NotebookOutline", Title="待办事项", NameSpace="TodoView" },
                new MenuBar(){ Icon="NotebookPlus", Title="备忘录", NameSpace="MemoView" },
                new MenuBar(){ Icon="Cog", Title="设置", NameSpace="SettingsView" },

            };

            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;

            GoBackCommand = new DelegateCommand(() =>
            {
                if(journal!=null && journal.CanGoBack) journal.GoBack();
            });

            GoForwardCommand = new DelegateCommand(() => {
                if (journal != null && journal.CanGoForward) journal.GoForward();
            });
        }

        private void Navigate(MenuBar bar)
        {
            if (!string.IsNullOrWhiteSpace(bar?.NameSpace))
                regionManager.Regions[Extensions.PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace,
                    back =>
                    {
                        journal = back.Context.NavigationService.Journal;
                    });
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; }
        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GoForwardCommand { get; }
        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

    }
}
