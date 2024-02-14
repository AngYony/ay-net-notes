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

namespace S12_8
{
    /// <summary>
    /// MyPasswordInput.xaml 的交互逻辑
    /// </summary>
    public partial class MyPasswordInput : UserControl
    {
        public MyPasswordInput()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 表示密码框中的密码，目的是为了使外部代码能够通过用户控件来访问输入的密码
        /// </summary>
        public string Password
        {
            get { return this.pswd.Password; }
            set { this.pswd.Password = value; }
        }
        #endregion


        private void pswd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            /*
             * t表示密码强度类型
             *   1 - 较弱
             *   2 - 中强
             *   3 - 较强
             *   0 - 表示字符长度为0或由空格组成
             */
            int t = 0;
            string password = pswd.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                t = 0;
            }
            else
            {
                if (password.Length > 0 && password.Length < 8)
                {
                    // 长度小于8，强度为弱
                    t = 1;
                }
                else
                {
                    int letterConunt = 0;
                    int digitCount = 0;
                    foreach (char c in password.ToCharArray())
                    {
                        // 查找字母
                        if (char.IsLetter(c))
                        {
                            letterConunt++;
                        }
                        // 查找数字
                        if (char.IsDigit(c))
                        {
                            digitCount++;
                        }
                    }
                    // 如果是纯字母或纯数字密码
                    // 则强度为中强
                    if (letterConunt == password.Length || digitCount == password.Length)
                    {
                        t = 2;
                    }
                    else
                    {
                        t = 3;
                    }
                }
            }
            // 分析该呈现什么颜色
            Color colorRes = Colors.Transparent;
            switch (t)
            {
                case 1:
                    colorRes = Colors.Yellow;
                    break;
                case 2:
                    colorRes = Colors.Green;
                    break;
                case 3:
                    colorRes = Colors.Red;
                    break;
                default:
                    colorRes = Colors.Transparent;
                    break;
            }
            // 设置渐变点的颜色
            stop1.Color = colorRes;
        }
    }
}
