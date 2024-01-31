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
    /// 将ObservableCollection集合的实例作为集合控件的数据源，实现动态向集合中添加和删除项，ListBox会动态更新其子项列表
    /// </summary>
    public partial class DongTaiShuJuJiHeZhanShi : Window
    {
        //声明一个动态数据收集，该集合在添加或删除项或刷新整个列表时提供通知。
        System.Collections.ObjectModel.ObservableCollection<string> m_Collection = null;

        public DongTaiShuJuJiHeZhanShi()
        {
            InitializeComponent();
            m_Collection = new System.Collections.ObjectModel.ObservableCollection<string>();
            lb.ItemsSource = m_Collection;
        }
        //无论添加新项还是删除现有项，都不用直接操作ListBox，而是操作动态集合来完成。

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // 如果未输入内容，则忽略
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                return;
            }
            // 如果要添加的项已存在，就不再添加
            if (m_Collection.Contains(txtInput.Text))
            {
                MessageBox.Show("此项已存在。");
                return;
            }
            // 向集合添加新项
            m_Collection.Add(txtInput.Text);
            // 添加后清除文本框中的文本
            txtInput.Clear();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            // 判断当前是否有选中的项
            if (lb.SelectedIndex > -1)
            {
                string item = lb.SelectedItem as string;
                if (item != null)
                {
                    // 从集合中移除指定项
                    m_Collection.Remove(item);
                }
            }
        }


    }
}
