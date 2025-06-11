using MyToDo.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    internal class TodoViewModel : BindableBase
    {
        public TodoViewModel()
        {
            this.TodoInfos = new ObservableCollection<TodoInfo>();

            foreach (var item in Enumerable.Range(1, 10))
            {

                this.TodoInfos.Add(new TodoInfo { Id = item, Title = "待办" + item, Content = "待办内容", Color =MyHelper. GetColor()});
            }

            AddTodoCommand = new DelegateCommand(() => {
               
                    this.IsRightDrawerOpen = true;
            });
        }

        public DelegateCommand AddTodoCommand { get; }

        private bool isRightDrawerOpen; 
        /// <summary>
        /// 右侧窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen  
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<TodoInfo> todoInfos;
        public ObservableCollection<TodoInfo> TodoInfos
        {
            get { return todoInfos; }
            set
            {
                todoInfos = value; RaisePropertyChanged();
            }

        }

        
    }
}
