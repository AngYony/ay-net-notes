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
    /// SplineDoubleKeyFrameSample.xaml 的交互逻辑
    /// </summary>
    public partial class SplineDoubleKeyFrameSample : Window
    {
        public SplineDoubleKeyFrameSample()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //声明关键帧动画
            DoubleAnimationUsingKeyFrames dakX = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(1000))
            };
            //创建、添加关键帧
            SplineDoubleKeyFrame kf = new SplineDoubleKeyFrame
            {
                KeyTime = KeyTime.FromPercent(1),
                Value = 400,
                //设置两个控制点，定义此关键帧的动画进度（变化速率）
                KeySpline = new KeySpline
                {
                    ControlPoint1 = new Point(0, 1),
                    ControlPoint2 = new Point(1, 0),
                }
            };

            SplineDoubleKeyFrame kf2 = new SplineDoubleKeyFrame
            {
                KeyTime = KeyTime.FromPercent(1),
                Value = 0,
            };

            dakX.KeyFrames.Add(kf);
            dakX.KeyFrames.Add(kf2);
            //执行动画
            this.tt.BeginAnimation(TranslateTransform.XProperty, dakX);
        }
    }
}
