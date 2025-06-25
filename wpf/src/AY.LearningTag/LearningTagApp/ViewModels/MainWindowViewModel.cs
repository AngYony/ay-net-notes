using LearningTag.Setting.Views;
using LearningTag.Shared;
using LearningTagApp.Dtos;
using LearningTagApp.Views;
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
        public string Title { get; set; } = "学习记录";

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog)
        {
            this._regionManager = regionManager;
            this._moduleCatalog = moduleCatalog;
            this._regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
            LoadModulesCommand = new DelegateCommand(LoadMoudels);



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
                    //选择菜单项跳转
                    _regionManager.RequestNavigate("MainContentRegion", _selectMenuInfo.ViewName);
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

        private ObservableCollection<MenuInfoDto> _menuInfos;

        public ObservableCollection<MenuInfoDto> MenuInfos
        {
            get => _menuInfos ?? (_menuInfos = new ObservableCollection<MenuInfoDto>());
            set { _menuInfos = value; }
        }

        public DelegateCommand LoadModulesCommand { get; }


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
