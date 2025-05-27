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
    /// DoubleAnimationUsingPathSample.xaml 的交互逻辑
    /// </summary>
    public partial class DoubleAnimationUsingPathSample : Window
    {
        public DoubleAnimationUsingPathSample()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //读取XAML代码中获取移动路径数据
            PathGeometry pg = this.layoutRoot.FindResource("movingPath") as PathGeometry;

            //创建动画
            DoubleAnimationUsingPath dapX = new DoubleAnimationUsingPath()
            {
                PathGeometry = pg,
                Source = PathAnimationSource.X,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                //自动返回
                AutoReverse = true,
                //永远循环
                RepeatBehavior = RepeatBehavior.Forever,
            };

            DoubleAnimationUsingPath dapY = new DoubleAnimationUsingPath
            {
                PathGeometry = pg,
                Source = PathAnimationSource.Y,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
            };

            //执行动画
            this.tt.BeginAnimation(TranslateTransform.XProperty, dapX);
            this.tt.BeginAnimation(TranslateTransform.YProperty, dapY);
        }
    }
}
