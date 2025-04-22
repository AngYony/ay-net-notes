using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace S4_1
{
    internal class WyButton : Button
    {
        protected override void OnClick()
        {
            base.OnClick();

            MessageBox.Show("重新");
        }
    }
}
