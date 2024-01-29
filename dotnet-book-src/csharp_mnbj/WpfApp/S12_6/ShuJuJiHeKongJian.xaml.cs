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

namespace S12_6
{
    /// <summary>
    /// ShuJuJiHeKongJian.xaml 的交互逻辑
    /// </summary>
    public partial class ShuJuJiHeKongJian : Window
    {
        public ShuJuJiHeKongJian()
        {
            InitializeComponent();

            // 定义一个int数组
            int[] arrInt = { 23556, 300001, 100054, 88300, 409900, 72668 };
            // 将数组实例赋给ItemsSource属性
            lb2.ItemsSource = arrInt;

            string[] items = { "选项 1", "选项 2", "选项 3", "选项 4", "选项 5" };
            cb.ItemsSource = items;

            string[] items2 = { "足球", "羽毛球", "排球", "篮球", "乒乓球" };
            lb.ItemsSource = items2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                return;
            }
            lb1.Items.Add(txtInput.Text);
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb.SelectedIndex < 0)
            {
                return;
            }
            // 显示当前选择项的索引和内容
            if (runTextIndex != null && runTextContent != null)
            {
                runTextIndex.Text = cb.SelectedIndex.ToString();
                runTextContent.Text = cb.SelectedItem.ToString();
            }
        }
    }
}
