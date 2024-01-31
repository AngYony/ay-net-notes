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

namespace S12_6
{
    /// <summary>
    /// ① XAML扩展标记的使用-单项绑定
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             
            Employee emp = new Employee
            {
                Name = "刘备",
                Age = 20,
                Partment = "蜀国"
            };
            this.layoutRoot.DataContext = emp; 
        }

        /// <summary>
        /// 双向绑定示例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShuangXiang_Click(object sender, RoutedEventArgs e)
        {
            ShuangXiangBangDing1 sxbd = new ShuangXiangBangDing1();
            sxbd.ShowDialog();
                
        }

        private void btnShuangXiang2_Click(object sender, RoutedEventArgs e)
        {
            ShuangXiangBangDing2 sxbd2 = new ShuangXiangBangDing2();
            sxbd2.ShowDialog();

        }

        private void btnShangXiaWenBangDing_Click(object sender, RoutedEventArgs e)
        {
            ShangXiaWenBangDing1 sxwbd = new ShangXiaWenBangDing1();
            sxwbd.ShowDialog();
        }

        private void btnShangXiaWenBangDing2_Click(object sender, RoutedEventArgs e)
        {
            ShangXianWenBangDing2 sxwbd = new ShangXianWenBangDing2();
            sxwbd.ShowDialog();
        }

        private void btnShangXiaWenBangDing3_Click(object sender, RoutedEventArgs e)
        {
            ShangXiaWenBangDing3 sxw3 = new ShangXiaWenBangDing3();
            sxw3.ShowDialog();
        }

        private void btnBangDingzhuanHuanQi_Click(object sender, RoutedEventArgs e)
        {
            BangDingZhuanHuanQi zhq = new BangDingZhuanHuanQi();
            zhq.ShowDialog();
        }

        private void btnShuJuJiHeKongJian_Click(object sender, RoutedEventArgs e)
        {
            ShuJuJiHeKongJian sjjhkj = new ShuJuJiHeKongJian();
            sjjhkj.ShowDialog();
        }

        private void btnShuJuMoBan_Click(object sender, RoutedEventArgs e)
        {
            ShuJuMuBan sjmb = new ShuJuMuBan();
            sjmb.ShowDialog();
        }

        private void btnShuJuMoBanXuanZeQi_Click(object sender, RoutedEventArgs e)
        {
            ShuJuMoBanXuanZeQi xzq = new ShuJuMoBanXuanZeQi();
            xzq.ShowDialog();
        }

        private void btnShuJuShiTu_Click(object sender, RoutedEventArgs e)
        {
            ShuJuShiTu sjst = new ShuJuShiTu();
            sjst.ShowDialog();
        }

        private void btnZhuCongShiTu_Click(object sender, RoutedEventArgs e)
        {
            ZhuCongShiTu zcst = new ZhuCongShiTu();
            zcst.ShowDialog();
                
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Partment { get; set; }

    }
}
