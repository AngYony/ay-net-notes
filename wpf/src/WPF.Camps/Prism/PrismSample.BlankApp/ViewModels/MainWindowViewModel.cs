using ModuleA.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismSample.BlankApp.Views;
using System;
using System.Windows;

namespace PrismSample.BlankApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand<string> OpenCommand { get; private set; }
        public DelegateCommand BackCommand { get; private set; }

        public DelegateCommand<string> DiaglogCommand { get; private set; }

        private readonly IRegionManager _regionManager;
        private readonly IDialogService dialogService;
        private readonly IEventAggregator aggregator;

        public DelegateCommand<string> OpenCommand2 { get; private set; }

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private object _body;
        public object Body
        {
            get { return _body; }
            set { _body = value; RaisePropertyChanged(); }
        }

        public MainWindowViewModel(
            //区域
            IRegionManager regionManager, 
            //弹窗
            IDialogService dialogService,
            IEventAggregator aggregator)
        {
            OpenCommand = new DelegateCommand<string>(Open);
            OpenCommand2 = new DelegateCommand<string>(Open2);
            BackCommand = new DelegateCommand(Back);
            DiaglogCommand = new DelegateCommand<string>(RunDialog);
            _regionManager = regionManager;
            this.dialogService = dialogService;
            this.aggregator = aggregator;
        }

        private void RunDialog(string obj)
        {
            DialogParameters keys = new DialogParameters();
            keys.Add("Title", "测试弹窗");
            dialogService.ShowDialog(obj, keys, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    //获取回传的结果
                    var result = callback.Parameters.GetValue<string>("Value");
                    //aggregator.GetEvent<MessageEvent>().Publish("发送消息");
                    MessageBox.Show(result);
                }
            });
        }

        private IRegionNavigationJournal _navigationJournal;
        private void Back()
        {
            if (_navigationJournal.CanGoBack)
                _navigationJournal.GoBack();
        }

        //不推荐做法，代码高度耦合
        private void Open(string obj)
        {
            switch (obj)
            {
                case "ViewA":
                    Body = new ViewA(); break;
                case "ViewB": Body = new ViewB(); break;
                case "ViewC":
                    Body = new ViewC(); break;
            }
        }

        //推荐做法，解耦
        private void Open2(string obj)
        {
            //定义传递的参数
            NavigationParameters keys = new NavigationParameters();
            keys.Add("Title", "这是传递给ModuleA.ViewA的参数值");
            //获取全局定义的可用区域
            //往这个区域动态设置内容，通过依赖注入的方式
            _regionManager.Regions["ContentRegion"].RequestNavigate(obj, callBack =>
            {
                if (callBack.Result.GetValueOrDefault())
                {
                    _navigationJournal = callBack.Context.NavigationService.Journal;
                }
            }, keys);
        }
    }
}
