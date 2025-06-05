using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismBlankApp.ViewModels
{
    public class ViewCViewModel : BindableBase, IConfirmNavigationRequest
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = MessageBox.Show("确认要离开当前页吗？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
            continuationCallback(result);
           
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// 导航离开当前页时触发
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// 导航完成前，接收用户传递的参数一级是否允许导航等控制
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.Title = navigationContext.Parameters.GetValue<string>("wy");
        }
    }
}
