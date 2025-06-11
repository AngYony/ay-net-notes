using MyToDo.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    internal class SettingsViewModel : BindableBase
    {

        public SettingsViewModel()
        {
            SettingsMenuBars = new ObservableCollection<MenuBar>()
            {
                new MenuBar(){ Icon="Home", Title="首页", NameSpace="IndexView" },
                new MenuBar(){ Icon="NotebookOutline", Title="待办事项", NameSpace="TodoView" },
                new MenuBar(){ Icon="NotebookPlus", Title="备忘录", NameSpace="MemoView" },
                new MenuBar(){ Icon="Cog", Title="设置", NameSpace="SettingsView" },

            };

        }

        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> SettingsMenuBars
        {
            get { return menuBars; }
            set { menuBars = value; }
        }


    }
}
