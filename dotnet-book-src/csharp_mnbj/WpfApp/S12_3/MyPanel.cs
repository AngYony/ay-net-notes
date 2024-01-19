using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace S12_3
{
    /// <summary>
    /// ⑤ 附加属性
    /// </summary>
    public class MyPanel : Panel
    {

        //注册附加属性
        static readonly DependencyProperty LocationProperty = DependencyProperty.RegisterAttached(
             "Location", typeof(Point), typeof(MyPanel),
             //AffectsParentArrange：更改此依赖属性的值会影响父元素上的布局。
             new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsParentArrange));


        #region 附加属性访问器封装的标准形式
        public static void SetLocation(UIElement el, Point locpt)
        {
            el.SetValue(LocationProperty, locpt);
        }

        public static Point GetLocation(UIElement el)
        {
            return (Point)el.GetValue(LocationProperty);
        }
        #endregion


        /// <summary>
        /// 定位子元素并确定大小
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            // 取出每个子元素的Location附加属性的值
            // 以便对子元素进行定位
            foreach (UIElement el in this.InternalChildren)
            {
                Point location = GetLocation(el);
                Size size = el.DesiredSize;
                el.Arrange(new Rect(location, size));
            }
            return finalSize;
        }

        /// <summary>
        /// 测量子元素在布局中所需的大小
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement el in InternalChildren)
            {
                el.Measure(availableSize);
            }
            return new Size();
        }
    }
}
