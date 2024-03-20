using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase,IConfirmNavigationRequest, INavigationAware
    {
        public ViewAViewModel()
        {

        }

        string title;
        public string Title
        {
            get => title; set { title = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 每次重新导航的时候是否重用原来的实例
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Title"))
                Title = navigationContext.Parameters.GetValue<string>("Title");
        }

        /// <summary>
        /// 用于导航拦截
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            var result = MessageBox.Show("确认导航？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            continuationCallback?.Invoke(result);
        }
    }
}
