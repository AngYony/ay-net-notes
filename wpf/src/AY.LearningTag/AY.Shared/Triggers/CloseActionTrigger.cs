using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AY.Shared.Triggers
{
    /// <summary>
    /// 该方式不常用，只作为了解，更好的使用命令
    /// </summary>
    internal class CloseActionTrigger : TriggerAction<Button>
    {
        protected override void Invoke(object parameter)
        {
            if (parameter is RoutedEventArgs args && args != null && args.OriginalSource is Button button)
            {
               
                var tabItem = FindControl<TabItem>(button);
                if (tabItem != null)
                {
                    var tabControl = FindControl<TabControl>(tabItem);
                    if (tabControl != null)
                    {
                        tabControl.Items.Remove(tabItem);
                    }
                }
                else
                {
                    // 如果没有找到 TabItem，则尝试关闭窗口
                    var window = Window.GetWindow(button);
                    if (window != null)
                    {
                        window.Close();
                    }
                }

                //var window = Window.GetWindow(button);
                //if (window != null)
                //{
                //    window.Close();
                //}
            }
            else
            {
                throw new ArgumentException("Parameter must be a RoutedEventArgs with a Button as the OriginalSource.");
            }
        }


        private T FindControl<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null) return null;

            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null)
            {
                var p = parent as T;
                if (p != null)
                {
                    return p;
                }
                return FindControl<T>(parent);

            }
            return null;
        }
    }
}
