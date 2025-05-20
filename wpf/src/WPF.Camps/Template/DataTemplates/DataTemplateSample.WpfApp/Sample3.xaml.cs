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

namespace DataTemplateSample.WpfApp
{
    /// <summary>
    /// Sample3.xaml 的交互逻辑
    /// </summary>
    public partial class Sample3 : Window
    {
        public Sample3()
        {
            InitializeComponent();

            var cars = new List<Car>()
            {
                 new Car(){Name="g1",Title="AA"},
                 new Car(){Name="g2",Title="BB"}
            };
            this.listBoxCars.ItemsSource = cars;
            this.listBoxCars.SelectedIndex = 0;
        }
    }
}
