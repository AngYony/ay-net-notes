using MyToDo.Common.Models;
using MyToDo.Extensions;
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
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IRegionManager regionManager)
        {
            menuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(bar =>
            {
                if (string.IsNullOrWhiteSpace(bar?.NameSpace)) return;
                regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(
                    bar.NameSpace,
                    back =>
                    {
                        journal = back.Context.NavigationService.Journal;
                    });
            });
            GoBackCommand = new DelegateCommand(() =>
            {
                if ((journal?.CanGoBack).GetValueOrDefault(false))
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if ((journal?.CanGoForward).GetValueOrDefault(false))
                    journal.GoForward();
            });
            this.regionManager = regionManager;
        }

        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;

        public ObservableCollection<MenuBar> MenuBars { get => menuBars; set { menuBars = value; RaisePropertyChanged(); } }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar { Icon = "Home", NameSpace = "IndexView", Title = "首页" });
            MenuBars.Add(new MenuBar { Icon = "NotebookOutline", NameSpace = "TodoView", Title = "待办事项" });
            MenuBars.Add(new MenuBar { Icon = "NotebookPlus", NameSpace = "MemoView", Title = "备忘录" });
            MenuBars.Add(new MenuBar { Icon = "Cog", NameSpace = "SettingsView", Title = "设置" });
        }


        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

    }
}
