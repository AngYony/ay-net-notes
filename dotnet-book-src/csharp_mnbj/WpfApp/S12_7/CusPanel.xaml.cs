using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace S12_7
{
    /// <summary>
    /// CusPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CusPanel : Window
    {
        public CusPanel()
        {
            InitializeComponent();
        }
    }

    public class MyPanel : Panel
    {
        /// <summary>
        /// 重写MeasureOverride方法以计算子元素所需要的布局空间总量
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new Size();
            // 计算布局所有子元素所需要的空间
            foreach (UIElement ele in InternalChildren)
            {
                // 必须调用子元素的Measure方法
                // 以计算子元素所需要的空间大小
                ele.Measure(availableSize);
                // 调用完Measure后，子元素所需要的布局大小将存放在DesiredSize属性中
                size.Height = Math.Max(ele.DesiredSize.Height, size.Height);
                size.Width += ele.DesiredSize.Width;
            }
            return size;
        }

        /// <summary>
        /// 排列，对计算后的元素重新定位
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect rect = new Rect();
            // 排列子元素
            foreach (UIElement ele in InternalChildren)
            {
                rect.Width = ele.DesiredSize.Width;
                rect.Height = ele.DesiredSize.Height;
                // 调用子元素的Arrange方法来定位
                ele.Arrange(rect);
                rect.X += ele.DesiredSize.Width;
            }
            return finalSize;
        }
    }
}
