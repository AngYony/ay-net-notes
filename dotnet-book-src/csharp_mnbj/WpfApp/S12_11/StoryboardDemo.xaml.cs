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

namespace S12_11
{
    /// <summary>
    /// StoryboardDemo.xaml 的交互逻辑
    /// </summary>
    public partial class StoryboardDemo : Window
    {
        System.Windows.Media.Animation.Storyboard storyboard = null;
        public StoryboardDemo()
        {
            InitializeComponent();
            storyboard = (System.Windows.Media.Animation.Storyboard)this.Resources["std"];
        }

        private void OnPlay(object sender, RoutedEventArgs e)
        {
            storyboard.Begin(); //开始播放
        }

        private void OnPause(object sender, RoutedEventArgs e)
        {
            storyboard.Pause(); //暂停播放
        }

        private void OnResume(object sender, RoutedEventArgs e)
        {
            storyboard.Resume(); //继续播放
        }

        private void OnStop(object sender, RoutedEventArgs e)
        {
            storyboard.Stop(); //停止播放
        }
    }
}
