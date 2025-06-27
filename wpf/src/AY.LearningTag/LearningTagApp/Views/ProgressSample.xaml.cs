using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LearningTagApp.Views
{
    /// <summary>
    /// ProgressSample.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressSample : Window
    {
        public ProgressSample()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            //此处不能使用.Wait()，会造成死锁。
            await DoJobAsync(CancellationToken.None);
            btnStart.IsEnabled = true;
        }

        async Task DoJobAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                // 如果取消请求已发出，直接返回
                return;
            }
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(50, token).ConfigureAwait(false);//这里限制不回到主线程
                // 使用Dispatcher.Invoke来更新UI
                Dispatcher.Invoke(()=> progressBar.Value = i + 1);
                //Application.Current.Dispatcher.Invoke(() => { progressBar.Value = i + 1; }, System.Windows.Threading.DispatcherPriority.Background);
                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
