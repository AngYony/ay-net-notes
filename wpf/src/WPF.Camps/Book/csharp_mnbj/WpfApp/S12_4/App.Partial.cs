using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace S12_4
{
    /// <summary>
    /// ③ 注册路由事件的类处理程序，实现当鼠标指针移到窗口中的图形元素时，加上边框，移出时不显示边框的效果。
    /// </summary>
    public partial class App
    {
        static App()
        {
            // 注册类级别的路由事件处理方法
            EventManager.RegisterClassHandler(
                //声明类处理的类的类型
                typeof(Shape),
                //要处理的事件的路由事件
                UIElement.MouseEnterEvent,
                //对类处理程序实现的引用
                new MouseEventHandler(OnMouseEnter));

            EventManager.RegisterClassHandler(
                typeof(Shape),
                UIElement.MouseLeaveEvent,
                new MouseEventHandler(OnMouseLeave));
        }


        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            Shape shape = e.Source as Shape;
            if(shape!=null)
            {
                // 当鼠标指针离开图形时
                // 移除对象的轮廓
                shape.StrokeThickness = 0d;
                shape.Stroke = null;
            }
        }

        private static void OnMouseEnter(object sender, MouseEventArgs e)
        {
            Shape shape = e.Source as Shape;

            if(shape!=null)
            {
                // 当鼠标指针进入图形时
                // 为对象添加轮廓
                shape.Stroke = Brushes.Yellow;
                shape.StrokeThickness = 8D;
            }
        }
    }
}
