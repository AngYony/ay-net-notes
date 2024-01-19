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
    /// ① 定义和使用依赖项属性
    /// </summary>
    public class MyVisualA : UIElement
    {
        #region 注册依赖项属性，属性名称，属性值的类型，注册该依赖项属性的类型
        public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register("FillBrushA", typeof(Brush), typeof(MyVisualA));
        #endregion

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
            // 用FillBrush属性指定的画刷绘矩形
            dc.DrawRectangle(this.FillBrush, null, new Rect(0d, 0d, 80d, 65d));
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == FillBrushProperty)
            {
                InvalidateVisual();
            }
        }
    }
}
