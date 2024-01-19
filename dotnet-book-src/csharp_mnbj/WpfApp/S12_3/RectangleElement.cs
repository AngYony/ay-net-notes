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
    /// 使用已注册的依赖项属性
    /// </summary>
    public class RectangleElement : FrameworkElement
    {
        //依赖项属性标识字段
        public static readonly DependencyProperty BorderProperty =
            //将Shape的依赖项属性StrokeProperty引用到自定义类型RectangleElement中
            Shape.StrokeProperty.AddOwner(typeof(RectangleElement),
                new FrameworkPropertyMetadata(Brushes.Red,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty =
            Shape.FillProperty.AddOwner(typeof(RectangleElement),
                new FrameworkPropertyMetadata(Brushes.Blue,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BorderWidthProperty =
            Shape.StrokeThicknessProperty.AddOwner(typeof(RectangleElement),
                new FrameworkPropertyMetadata(3d, FrameworkPropertyMetadataOptions.AffectsRender));


        /// <summary>
        /// 用于绘制矩形边框的画刷
        /// </summary>
        public Brush Border
        {
            get { return (Brush)GetValue(BorderProperty); }
            set { SetValue(BorderProperty, value); }
        }

        /// <summary>
        /// 用于填充矩形的画刷
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// 矩形边框的粗细
        /// </summary>
        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            // 要绘制的矩形的位置和大小
            Rect rect = new Rect(0d, 0d, this.ActualWidth, this.ActualHeight);
            // 用于绘制矩形边框的Pen对象
            Pen pen = new Pen(this.Border, this.BorderWidth);
            // 绘制矩形
            drawingContext.DrawRectangle(this.Fill, pen, rect);
        }
    }

}
