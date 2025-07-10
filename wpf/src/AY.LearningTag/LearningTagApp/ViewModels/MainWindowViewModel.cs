using LearningTag.Setting.Views;
using LearningTag.Shared;
using LearningTag.Shared.Consts.Regions;
using LearningTagApp.Dtos;
using LearningTagApp.Views;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace LearningTagApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IModuleCatalog _moduleCatalog;
        private readonly ILogger<MainWindowViewModel> _logger;
        private IRegionNavigationJournal _navigationJournal;
        public string Title { get; set; } = "学习记录";

        private ObservableCollection<MenuInfoDto> _menuInfos;
        public ObservableCollection<MenuInfoDto> MenuInfos
        {
            get { return _menuInfos ?? (_menuInfos = new ObservableCollection<MenuInfoDto>()); }
            set { SetProperty(ref _menuInfos, value); }
        }

        private DelegateCommand _windowLoadedCommand;
        public DelegateCommand WindowLoadedCommand =>
            _windowLoadedCommand ?? (_windowLoadedCommand = new DelegateCommand(OnWindowLoaded));

        void OnWindowLoaded()
        {
            //加载模块
            LoadMoudels();
            //设置默认显示页
            this._regionManager.RequestNavigate(AppRegions.MainContentRegion, nameof(LearnMainView));
        }







        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog)
        {
            this._regionManager = regionManager;
            this._moduleCatalog = moduleCatalog;
            //this._logger = logger;
            //添加头部区域
            this._regionManager.RegisterViewWithRegion(AppRegions.HeaderRegion, typeof(HeaderView));
            

        }


     
 

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        void ExecuteNavigateCommand(string navPar)
        {
            if (!string.IsNullOrEmpty(navPar))
            {
                _regionManager.RequestNavigate(AppRegions.MainContentRegion, navPar, arg =>
                {
                    _navigationJournal = arg.Context.NavigationService.Journal;
                });
            }
        }




        private MenuInfoDto _selectMenuInfo;

        public MenuInfoDto SelectMenuInfo
        {
            get { return _selectMenuInfo; }
            set
            {
                _selectMenuInfo = value;

                if (!string.IsNullOrEmpty(_selectMenuInfo.ViewName))
                {
                    var parameter = new NavigationParameters();
                    parameter.Add("FromMainWindowPar", _selectMenuInfo.ViewName);
                    //选择菜单项跳转
                    _regionManager.RequestNavigate(AppRegions.MainContentRegion, _selectMenuInfo.ViewName, arg =>
                    {
                        _navigationJournal = arg.Context.NavigationService.Journal;

                    }, parameter);
                }
                else
                {
                    this._regionManager.RequestNavigate(AppRegions.MainContentRegion, nameof(LearnMainView));
                }
            }
        }



        private void LoadMoudels()
        {
            this.MenuInfos.Add(new MenuInfoDto { Title = "测试3", Color = MyHelper.GetColor() });
            this.MenuInfos.Add(new MenuInfoDto { Title = "测试2", Color = MyHelper.GetColor() });
            this.MenuInfos.Add(new MenuInfoDto { Title = "设置", Color = MyHelper.GetColor(), ViewName = nameof(SettingMainView) });
            var dirModuleCatalog = _moduleCatalog as DirectoryModuleCatalog;
            foreach (var m in dirModuleCatalog.Modules)
            {
                if (m.ModuleName == "About")
                {
                    this.MenuInfos.Add(new MenuInfoDto
                    {
                        Title = "关于",
                        Icon = "",
                        Color = MyHelper.GetColor(),
                        ViewName = "AboutView"  //由于没有引用About项目，所以此处写死的字符串
                    });
                }
            }
            

        }








        //private ObservableCollection<LearningWorkDto> _learningWorks;


        //public ObservableCollection<LearningWorkDto> LearningWorks
        //{
        //    get { return _learningWorks; }
        //    set { _learningWorks = value; RaisePropertyChanged(); }
        //}




        //private IEnumerable<LearningWorkDto> GetAllLearningWorks()
        //{
        //    return new List<LearningWorkDto>() {
        //        new LearningWorkDto{ Id=1, Name="ASP.NET Core3", },
        //        new LearningWorkDto{ Id=2, Name="ASP.NET Core2", },
        //        new LearningWorkDto{ Id=3, Name="ASP.NET Core4", },
        //    };
        //}
    }
}
