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

namespace ShapeSample.WpfApp
{
    /// <summary>
    /// VisualBrushSample.xaml 的交互逻辑
    /// </summary>
    public partial class VisualBrushSample : Window
    {
        public VisualBrushSample()
        {
            InitializeComponent();
        }
        double o = 1.0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VisualBrush vBrush = new VisualBrush(this.realButton);
            Rectangle rect = new Rectangle();
            rect.Width = realButton.ActualWidth;
            rect.Height = realButton.ActualHeight;
            rect.Fill = vBrush;
            rect.Opacity = o;
            o -= 0.2;
            this.stackpanelRight.Children.Add(rect);
        }
    }
}
