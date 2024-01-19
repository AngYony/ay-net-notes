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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S12_4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ② 路由策略
        public MainWindow()
        {
            InitializeComponent();
            //为Button类的Click路由事件添加一个处理方法onClick
            this.layoutRoot.AddHandler(Button.ClickEvent, new RoutedEventHandler(onClick));
        }

        /// <summary>
        /// 单击窗口中的任意按钮，都会触发Click事件，Button.ClickEvent是RoutingStrategy.Bubble模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onClick(object sender, RoutedEventArgs e)
        {
            //注意：此处必须通过e.Source属性来获取引发Click事件的Button实例，和winform不一样
            // sender参数引用的是调用AddHandler方法的对象实例，这里是layoutRoot实例。
            Button btn = e.Source as Button;
            if (btn != null)
            {
                string content = btn.Content as string;
                MessageBox.Show($"你点击了{content}按钮");
            }
        }
        #endregion
        private void MyTestElement_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("你好");
        }
    }
}
