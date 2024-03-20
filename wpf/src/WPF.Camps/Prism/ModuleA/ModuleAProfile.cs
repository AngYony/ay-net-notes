using ModuleA.View;
using ModuleA.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    public class ModuleAProfile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
             
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //将视图和视图模型进行显式绑定
            containerRegistry.RegisterForNavigation<ViewA,ViewAViewModel>("Va");
        }
    }
}
