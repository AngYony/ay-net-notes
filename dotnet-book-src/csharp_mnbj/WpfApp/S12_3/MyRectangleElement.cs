using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace S12_3
{
    /// <summary>
    /// ④ 只读的依赖项属性
    /// </summary>
    public class MyRectangleElement : FrameworkElement
    {
        #region 注册只读依赖项属性
        //设置为私有的，只能内部进行SetValue修改依赖项的值
        private static readonly DependencyPropertyKey PointKey =
            DependencyProperty.RegisterReadOnly("AbcPoint", 
                typeof(Point),
                typeof(MyRectangleElement),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.None));
        // 以下字段声明为公共成员
        public static readonly DependencyProperty PointProperty = PointKey.DependencyProperty;
        #endregion


        #region 封装依赖项属性
        /// <summary>
        /// 所绘制矩形左上角的坐标点
        /// </summary>
        public Point MyPoint
        {
            get { return (Point)GetValue(PointProperty); }
            //没有提供set，内部的赋值直接通过调用SetValue进行设置
        }
        #endregion


        Random rand = new Random();

        /// <summary>
        /// 随机生成坐标点来绘制一个矩形
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            // 调用以下方法产生一个随机点
            double newX = rand.Next(0, (int)(this.ActualWidth - 100d));
            double newY = rand.Next(0, (int)(ActualHeight - 85d));
            // 设置只读依赖项属性的值
            // 此类内部可以设置该属性
            SetValue(PointKey, new Point(newX, newY));


            Rect r = new Rect(this.MyPoint, new Size(100d, 85d));
            // 绘制矩形
            drawingContext.DrawRectangle(Brushes.Purple, null, r);
        }
    }

}
