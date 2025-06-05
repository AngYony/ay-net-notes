using MicrosoftToDO.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftToDO.ViewModels
{
    public class MainViewModel
    {
        private ObservableCollection<MenuItem> menuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }

        public MainViewModel()
        {
            MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(new MenuItem()
            {
                Name = "这是一个标题",
                Icon = "\xe635",
                Count = 20,
                BackColor = "Red"
            });

            MenuItems.Add(new MenuItem()
            {
                Name = "这是一个标题2",
                Icon = "\xe635",
                Count = 20,
                BackColor = "Green"
            });
        }
    }
}
