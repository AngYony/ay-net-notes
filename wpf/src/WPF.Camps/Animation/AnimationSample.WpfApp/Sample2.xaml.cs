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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnimationSample.WpfApp
{
    /// <summary>
    /// Sample2.xaml 的交互逻辑
    /// </summary>
    public partial class Sample2 : Window
    {
        public Sample2()
        {
            InitializeComponent();
             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation daX = new DoubleAnimation();
            DoubleAnimation daY = new DoubleAnimation();

            BounceEase be = new BounceEase();
            be.Bounces = 3;//弹跳3次
            be.Bounciness = 3; //弹性程度，值越大反弹越低
            daY.EasingFunction = be;

            //指定起点
            daX.From = 0D;
            daY.From = 0d;

            //指定终点
            daX.To = 345;
            daY.To = 555;

            //指定时长
            Duration duration = new Duration(TimeSpan.FromSeconds(2));
            daX.Duration = duration;
            daY.Duration = duration;

            //动画的主体是TranslateTransform变形，而非Button
            this.tt.BeginAnimation(TranslateTransform.XProperty, daX);
            this.tt.BeginAnimation(TranslateTransform.YProperty, daY);
        }
    }
}
