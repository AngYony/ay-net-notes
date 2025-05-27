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
    /// StoryboardSample.xaml 的交互逻辑
    /// </summary>
    public partial class StoryboardSample : Window
    {
        public StoryboardSample()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //红色小球匀速移动
            DoubleAnimation daRx = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                To = 400
            };

            //绿色小球变速运动
            DoubleAnimationUsingKeyFrames dakGx = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            SplineDoubleKeyFrame kfG = new SplineDoubleKeyFrame(400, KeyTime.FromPercent(1.0))
            {
                KeySpline = new KeySpline(1, 0, 0, 1)
            };
            dakGx.KeyFrames.Add(kfG);


            //蓝色小球变速运动
            DoubleAnimationUsingKeyFrames dakBx = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            SplineDoubleKeyFrame kfB = new SplineDoubleKeyFrame(400, KeyTime.FromPercent(1.0))
            {
                KeySpline = new KeySpline(0, 1, 1, 0)
            };
            dakBx.KeyFrames.Add(kfB);


            //创建场景
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTargetName(daRx, "ttR");
            Storyboard.SetTargetProperty(daRx, new PropertyPath(TranslateTransform.XProperty));

            Storyboard.SetTargetName(dakGx, "ttG");
            Storyboard.SetTargetProperty(dakGx,new PropertyPath(TranslateTransform.XProperty));
            
            Storyboard.SetTargetName(dakBx, "ttB");
            Storyboard.SetTargetProperty(dakBx, new PropertyPath(TranslateTransform.XProperty));

            storyboard.Duration = new Duration(TimeSpan.FromSeconds(1));
            storyboard.Children.Add(daRx);
            storyboard.Children.Add(dakGx);
            storyboard.Children.Add(dakBx);

            storyboard.Begin(this);
            storyboard.Completed += (a, b) => { MessageBox.Show(ttR.X.ToString()); };
        }
    }
}
