using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace PrismBlankApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";

        //导航记录
        private IRegionNavigationJournal journal;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager regionManager;
        private readonly IDialogService dialog;

        public DelegateCommand OpenViewBCommand { get; }
        public DelegateCommand OpenViewCCommand { get; }
        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GoForwordCommand { get; }

        public DelegateCommand DialogCommand { get; }

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialog)
        {
            this.regionManager = regionManager;
            this.dialog = dialog;
            OpenViewBCommand = new DelegateCommand(OpenViewB);
            OpenViewCCommand = new DelegateCommand(OpenViewC);
            GoBackCommand = new DelegateCommand(() =>
            {
                journal.GoBack();
            });

            GoForwordCommand = new DelegateCommand(() =>
            {
                journal.GoForward();
            });
            DialogCommand = new DelegateCommand(() =>
            {
                DialogParameters param = new DialogParameters();
                param.Add("p1", "好好学习，天天向上");
                dialog.ShowDialog("myDialog", param, (arg) =>
                {
                    if (arg.Result == ButtonResult.OK)
                    {
                        var text = arg.Parameters.GetValue<string>("text");
                        MessageBox.Show(text);
                    }
                    else if(arg.Result == ButtonResult.Cancel)
                    {
                        MessageBox.Show("关闭");
                    }

                });

            });
        }

        private void OpenViewB()
        {
            //方式一传递参数：
            NavigationParameters param = new NavigationParameters();
            param.Add("Value", "Hello");
            regionManager.RequestNavigate("ViewBRegion", "pageA", arg =>
            {
                journal = arg.Context.NavigationService.Journal;
            }, param);
        }

        private void OpenViewC()
        {
            //方式二：另一种传参形式，类似于url
            regionManager.RequestNavigate("ViewBRegion", "ViewC?wy=small");
        }


    }
}