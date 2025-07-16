using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearningTag.Shared
{
    /// <summary>
    /// 用于在XAML中绑定数据上下文的代理类
    /// </summary>
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy));
    }
}
