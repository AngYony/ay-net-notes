using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftToDO.Models
{
    public class TaskItem :BindableBase
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        private ObservableCollection<TaskInfo> tasks;

        public ObservableCollection<TaskInfo> TaskLists
        {
            get { return tasks; }
            set { tasks = value; RaisePropertyChanged(); }
        }

        private string background;

        public string Background
        {
            get { return background; }
            set { background = value;  RaisePropertyChanged(); }
        }


    }
}
