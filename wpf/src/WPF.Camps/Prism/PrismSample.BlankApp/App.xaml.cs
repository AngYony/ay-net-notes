using ModuleA;
using Prism.Ioc;
using Prism.Modularity;
using PrismSample.BlankApp.ViewModels;
using PrismSample.BlankApp.Views;
using System.Windows;

namespace PrismSample.BlankApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //    //方式一：注册模块
            //    containerRegistry.RegisterForNavigation<ViewA>("Va");
            //    containerRegistry.RegisterForNavigation<ViewB>("Vb");
                containerRegistry.RegisterForNavigation<ViewC>("Vc");
            //指定关联的ViewModel
            containerRegistry.RegisterDialog<ViewD, ViewDViewModel>("Vd");

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //方式二：添加项目引用的方式
            moduleCatalog.AddModule<ModuleAProfile>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //将ModuleB.dll文件，复制到bin目录下的Modules目录中
            //方式三：不通过项目引用，而是复制DLL程序集文件来添加模块
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}
