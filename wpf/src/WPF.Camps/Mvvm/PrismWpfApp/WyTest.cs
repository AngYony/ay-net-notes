using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismWpfApp
{
    public class WyTest:FrameworkElement
    {


        public string  WyName
        {
            get { return (string )GetValue(WyNameProperty); }
            set { SetValue(WyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WyName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WyNameProperty =
            DependencyProperty.Register("WyName", typeof(string ), typeof(WyTest), new PropertyMetadata(""));


    }
}
