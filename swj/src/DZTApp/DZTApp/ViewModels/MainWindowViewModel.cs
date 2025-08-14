using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DZTApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<string> DateList { get; set; }

        public DelegateCommand<string> ShowDetailCommand { get; set; }

        private ICollectionView fieldName;
        public ICollectionView MyView
        {
            get { return fieldName; }
            set { SetProperty(ref fieldName, value); }
        }




        public MainWindowViewModel()
        {
            DateList = new ObservableCollection<string>();
            DateList.Add("2023-01-01");
            DateList.Add("2023-02-01");
            DateList.Add("2023-03-01");
            DateList.Add("2023-04-01");
            DateList.Add("2023-05-01");
            DateList.Add("2023-06-01");
            DateList.Add("2023-07-01");
            DateList.Add("2023-08-01");
            DateList.Add("2023-09-01");
            DateList.Add("2023-10-01");
            DateList.Add("2023-11-01");
            DateList.Add("2023-12-01");

            ShowDetailCommand = new DelegateCommand<string>(OnShowDetail);
        }

        private void OnShowDetail(string str)
        {
            var data = new ObservableCollection<VersionDetail>
            {
                new VersionDetail { Time =str, UserName = "小明", FileName = "文件1.txt", Text = "这是文件1的内容",Group=str },
                new VersionDetail { Time = str, UserName = "小红", FileName = "文件2.txt", Text = "这是文件2的内容",Group="历史记录" },
                new VersionDetail { Time = str, UserName = "小刚", FileName = "文件3.txt", Text = "这是文件3的内容",Group="历史记录" },
            };

            MyView = CollectionViewSource.GetDefaultView(data);
            var groupDescription = new PropertyGroupDescription("Group");
            MyView.GroupDescriptions.Add(groupDescription);



        }
    }
}
