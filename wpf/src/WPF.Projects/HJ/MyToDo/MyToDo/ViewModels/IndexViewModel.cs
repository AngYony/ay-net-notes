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
    internal class IndexViewModel : BindableBase
    {
        public IndexViewModel()
        {
            this.TaskBars = new ObservableCollection<TaskBar>
            {
                new TaskBar{ Color="#cc3344", Content="10", Icon="ClockFast", Target="", Title="汇总" },
                new TaskBar{ Color="#990088", Content="20", Icon="ClockCheckOutline", Target="", Title="已完成" },
                new TaskBar{ Color="#ee9933", Content="30%", Icon="ChartLineVariant", Target="", Title="完成比例" },
                new TaskBar{ Color="#aa55ee", Content="19", Icon="PlaylistStar", Target="", Title="备忘录" },
            };

            this.TodoInfos = new ObservableCollection<TodoInfo>();
            this.MemoInfos = new ObservableCollection<MemoInfo>();
            foreach (var item in Enumerable.Range(1, 10))
            {
                this.TodoInfos.Add(new TodoInfo { Id = item, Title = "待办" + item, Content = "待办内容" }); 
                this.MemoInfos.Add(new  MemoInfo { Id = item, Title = "备忘" + item, Content = "备忘内容" });
            }

        }

         


        private ObservableCollection<TaskBar> taskBars;
        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set
            {
                taskBars = value; RaisePropertyChanged();
            }

        }



        private ObservableCollection<TodoInfo>   todoInfos;
        public ObservableCollection<TodoInfo>  TodoInfos
        {
            get { return todoInfos; }
            set
            {
                todoInfos = value; RaisePropertyChanged();
            }

        }

        private ObservableCollection<MemoInfo>  memoInfos;
        public ObservableCollection<MemoInfo>  MemoInfos
        {
            get { return memoInfos; }
            set
            {
                memoInfos = value; RaisePropertyChanged();
            }

        }
    }
}
