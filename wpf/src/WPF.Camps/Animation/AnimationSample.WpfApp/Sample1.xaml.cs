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
    /// Sample1.xaml 的交互逻辑
    /// </summary>
    public partial class Sample1 : Window
    {
        public Sample1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //创建一个双精度的动画
            DoubleAnimation animation = new DoubleAnimation();
            animation.By = -30;
            ////设置动画初始值
            //animation.From = btn.Width;
            ////设置动画结束值
            //animation.To = btn.Width - 30;
            //设置动画的持续时间
            animation.Duration = TimeSpan.FromSeconds(2);
            //是否往返执行
            animation.AutoReverse = true;
            //执行周期，这里设置一直重复执行
            //animation.RepeatBehavior = RepeatBehavior.Forever;
            //设置重复执行3次
            animation.RepeatBehavior = new RepeatBehavior(3);
            //动画完成之后执行的事件
            animation.Completed += Animation_Completed;

            //在当前按钮上执行该动画
            btn.BeginAnimation(Button.WidthProperty, animation);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            btn.Content = "动画已完成";
        }
    }
}
