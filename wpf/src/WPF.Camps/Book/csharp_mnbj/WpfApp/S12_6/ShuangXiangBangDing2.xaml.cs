using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// ③ XAML扩展标记的使用-双项绑定，INotifyPropertyChanged接口
    /// </summary>
    public partial class ShuangXiangBangDing2 : Window
    {
        public ShuangXiangBangDing2()
        {
            InitializeComponent();
            this.panel.DataContext = new Person { Xing = "张", Ming = "三" };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 判断是否输入内容
            if (txtXing.Text == "" || txtMing.Text == "")
            {
                MessageBox.Show("请输入姓名。");
                return;
            }
            // 修改Person实例的属性
            Person p = panel.DataContext as Person;
            p.Xing = txtXing.Text;
            p.Ming = txtMing.Text;
        }
    }

    public class Person:INotifyPropertyChanged
    {
        private string m_xing;
        /// <summary>
        /// 姓
        /// </summary>
        public string Xing
        {
            get { return m_xing; }
            set
            {
                if (this.m_xing != value)
                {
                    m_xing = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_ming;
        /// <summary>
        /// 名
        /// </summary>
        public string Ming
        {
            get { return m_ming; }
            set
            {
                if (m_ming != value)
                {
                    m_ming = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 通知属性已更改的方法
        /// </summary>
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyname = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyname));
            }
        }

        /// <summary>
        /// 来自INotifyPropertyChanged的事件
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }

}
