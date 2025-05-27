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
    /// AnimationUsingKeyFramesSample.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationUsingKeyFramesSample : Window
    {
        public AnimationUsingKeyFramesSample()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //声明两个关键帧动画实例，分别控制TranslateTransform的X属性、Y属性
            DoubleAnimationUsingKeyFrames dakX = new DoubleAnimationUsingKeyFrames()
            {
                //设置动画总时长
                Duration = new Duration(TimeSpan.FromSeconds(2))
            };
            DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames()
            {
                //设置动画总时长
                Duration = new Duration(TimeSpan.FromSeconds(2))
            };

            //创建和添加关键帧
            LinearDoubleKeyFrame x_kf_1 = new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)),
                Value = 200
            };
            LinearDoubleKeyFrame x_kf_2 = new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2)),
                Value = 0
            };
            LinearDoubleKeyFrame x_kf_3 = new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.4)),
                Value = 200
            };

            //添加关键帧到关键帧动画中
            dakX.KeyFrames.Add(x_kf_1);
            dakX.KeyFrames.Add(x_kf_2);
            dakX.KeyFrames.Add(x_kf_3);



            LinearDoubleKeyFrame y_kf_1 = new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)),
                Value = 0
            };
            LinearDoubleKeyFrame y_kf_2 = new LinearDoubleKeyFrame()
            {
                //KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2)),
                KeyTime = KeyTime.FromPercent(0.33) , //以整个关键帧动画的时长（Duration）中的百分比计算相对时间点
                Value = 180
            };
            LinearDoubleKeyFrame y_kf_3 = new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.4)),
                Value = 180
            };
            dakY.KeyFrames.Add(y_kf_1);
            dakY.KeyFrames.Add(y_kf_2);
            dakY.KeyFrames.Add(y_kf_3);


            //执行动画
            this.tt.BeginAnimation(TranslateTransform.XProperty, dakX);
            this.tt.BeginAnimation(TranslateTransform.YProperty, dakY);
        }
    }
}
