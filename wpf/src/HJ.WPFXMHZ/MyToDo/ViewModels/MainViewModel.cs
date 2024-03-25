using MyToDo.Common.Models;
using Prism.Mvvm;
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
        public MainViewModel()
        {
            menuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
        }

        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars { get => menuBars; set { menuBars = value; RaisePropertyChanged(); } }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar { Icon = "Home", NameSpace = "IndexView", Title = "首页" });
            MenuBars.Add(new MenuBar { Icon = "NotebookOutline", NameSpace = "TodoView", Title = "待办事项" });
            MenuBars.Add(new MenuBar { Icon = "NotebookPlus", NameSpace = "MemoView", Title = "备忘录" });
            MenuBars.Add(new MenuBar { Icon = "Cog", NameSpace = "SettingsView", Title = "设置" });
        }
    }
}
