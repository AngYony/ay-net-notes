using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace S12_3
{
    /// <summary>
    /// 依赖项属性示例二：使用元数据
    /// </summary>
    public class MyVisualB : FrameworkElement
    {
        #region 注册依赖项属性，属性名称，属性值的类型，注册该依赖项属性的类型
        public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register(
            "FillBrushB", typeof(Brush), typeof(MyVisualB),
            new FrameworkPropertyMetadata(
                //设置依赖项属性默认值
                Brushes.Red,FrameworkPropertyMetadataOptions.AffectsRender
                    //当依赖项属性的值被修改后会调用
                    //new PropertyChangedCallback(FillBrushPropertyChanged)
                    ));
        #endregion

        //private static void FillBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    MyVisualB v = d as MyVisualB;
        //    // 强制可视化元素重绘
        //    v.InvalidateVisual();
        //}



        #region 封装依赖项属性
        /// <summary>
        /// 用于绘制背景的画刷
        /// </summary>
        public Brush FillBrush
        {
            get { return (Brush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }
        #endregion

        protected override void OnRender(DrawingContext dc)
        {
            // 获取呈现当前元素所需要的空间
            Size s = this.DesiredSize;
            // 计算圆心坐标
            double cx = s.Width / 2d;
            double cy = s.Height / 2d;
            // 绘制图形
            dc.DrawEllipse(this.FillBrush, null, new Point(cx, cy), 100d, 100d);
           
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            // 返回呈现该元素所需要的空间
            return new Size(300d, 300d);
        }
        //protected override Size MeasureCore(Size availableSize)
        //{
        //    // 返回呈现该元素所需要的空间
        //    return new Size(300d, 300d);
        //}
         

    }
}
