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
        public string Title { get; set; } = "标题列";

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog)
        {
            this._regionManager = regionManager;
            this._moduleCatalog = moduleCatalog;
            this._regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
            this._regionManager.RegisterViewWithRegion("ContentRegion", typeof(SettingMainView));
            LoadModulesCommand = new DelegateCommand(LoadMoudels);
            this.MenuInfos.Add(new MenuInfoDto { Title = "测试3",Color=MyHelper.GetColor() });
            this.MenuInfos.Add(new MenuInfoDto { Title = "测试2",Color=MyHelper.GetColor() });
        }


        private MenuInfoDto _selectMenuInfo;

        public MenuInfoDto SelectMenuInfo
        {
            get { return _selectMenuInfo; }
            set { _selectMenuInfo = value; }
        }   



        private void LoadMoudels()
        {
            var dirModuleCatalog = _moduleCatalog as DirectoryModuleCatalog;
            foreach (var m in dirModuleCatalog.Modules)
            {
                if (m.ModuleName == "About")
                {
                    this.MenuInfos.Add(new MenuInfoDto
                    {
                        Title = "关于",
                        Icon = "",
                         Color = MyHelper.GetColor()
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

        
        private ObservableCollection<LearningWorkDto> _learningWorks;


        public ObservableCollection<LearningWorkDto> LearningWorks
        {
            get { return _learningWorks; }
            set { _learningWorks = value; RaisePropertyChanged(); }
        }




        private IEnumerable<LearningWorkDto> GetAllLearningWorks()
        {
            return new List<LearningWorkDto>() {
                new LearningWorkDto{ Id=1, Name="ASP.NET Core3", },
                new LearningWorkDto{ Id=2, Name="ASP.NET Core2", },
                new LearningWorkDto{ Id=3, Name="ASP.NET Core4", },
            };
        }
    }
}
