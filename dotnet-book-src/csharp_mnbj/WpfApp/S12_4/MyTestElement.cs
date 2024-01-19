using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace S12_4
{
    /// <summary>
    /// ① 路由事件
    /// </summary>
    public class MyTestElement : FrameworkElement
    {
        //全局注册路由事件
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
            "Click",
            //指示路由事件的路由策略
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MyTestElement));

        public event RoutedEventHandler MyClick
        {
            add
            {
                AddHandler(ClickEvent, value);
            }
            remove
            {
                RemoveHandler(ClickEvent, value);
            }
        }

        bool isMouseLeftButtonDown = false;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            isMouseLeftButtonDown = true;
            base.OnMouseLeftButtonDown(e);
        }


        /// <summary>
        /// 当鼠标左键按下并弹起时触发Click事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (isMouseLeftButtonDown)
            {
                //事件参数
                RoutedEventArgs arg = new RoutedEventArgs(ClickEvent);
                OnClick(arg);
                isMouseLeftButtonDown = false;
            }
            base.OnMouseLeftButtonUp(e);
        }

        protected virtual void OnClick(RoutedEventArgs arg)
        {
            //引发路由事件
            RaiseEvent(arg);
        }


        /// <summary>
        /// 在元素的内部绘制一个四边形（菱形）和文本
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            //绘制几何图形
            StreamGeometry sg = new StreamGeometry();
            using (StreamGeometryContext sc = sg.Open())
            {
                // 设置图形起点
                sc.BeginFigure(new Point(ActualWidth / 2d, 0d), true, true);
                // 计算另外三个点的坐标
                Point pt1 = new Point(ActualWidth, ActualHeight / 2d);
                Point pt2 = new Point(ActualWidth / 2d, ActualHeight);
                Point pt3 = new Point(0d, ActualHeight / 2d);
                List<Point> pts = new List<Point>() { pt1, pt2, pt3 };
                // 建立几何图形
                sc.PolyLineTo(pts, false, false);
            }
            // 绘制多边形
            drawingContext.DrawGeometry(Brushes.Green, null, sg);
            // 要绘制的文本
            string drawText = "请单击这里";
            FormattedText ft = new FormattedText(drawText, System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("宋体"), 36d, Brushes.White);
            // 计算文本的位置
            Point ptText = new Point((ActualWidth - ft.Width) / 2d, (ActualHeight - ft.Height) / 2d);
            // 绘制文本
            drawingContext.DrawText(ft, ptText);
        }
    }
}
