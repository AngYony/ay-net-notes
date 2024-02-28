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

namespace S12_5
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("您已经点击了。");
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            if (this.txtLoginName.Text == "" || passbox.Password == "")
            {
                MessageBox.Show("请输入用户名和密码。");
                return;
            }
            // 显示输入的内容
            this.tbresult.Text = string.Format("用户名：{0}，密码：{1}。", txtLoginName.Text, passbox.Password);
        }
    }
}
