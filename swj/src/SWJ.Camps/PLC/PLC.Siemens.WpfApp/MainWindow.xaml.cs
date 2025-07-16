using S7.Net;
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

namespace PLC.Siemens.WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //1.S7 - 200Smart：plc.xbdswj.cn:103
            //2.S7 - 1200：plc.xbdswj.cn:102

            Plc plc = new Plc(CpuType.S71200, "plc.xbdswj.cn", 102, 0, 0);
            plc.Open();
            //
            var value = ((uint)plc.Read("DB1.DBD4")).ConvertToFloat();
            MessageBox.Show(value.ToString());

          
            Plc plc2 = new Plc(CpuType.S7200Smart, "plc.xbdswj.cn", 103, 0, 0);
            plc2.Open();
            //
            var value2 = ((uint)plc2.Read("DB1.DBD4")).ConvertToFloat();
            MessageBox.Show(value2.ToString());

        }
    }
}
