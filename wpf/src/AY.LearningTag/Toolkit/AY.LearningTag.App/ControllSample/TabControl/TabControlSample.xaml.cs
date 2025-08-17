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

namespace AY.LearningTag.App.ControllSample.TabControl
{
    /// <summary>
    /// TabControlSample.xaml 的交互逻辑
    /// </summary>
    public partial class TabControlSample : Window
    {
        public TabControlSample()
        {
            InitializeComponent();
            this.DataContext = new TabControlSampleViewModel();
        }
    }

    public class TabBaseViewModel
    {
        public string Header { get; set; }
    }



    public class TabItemViewModelA : TabBaseViewModel
    {
        
        public string ContentA { get; set; }
    }

    public class TabItemViewModelB : TabBaseViewModel
    {
        public string ContentB { get; set; }
    }

    public class TabControlSampleViewModel
    {
        public string Title { get; set; } = "Tab Control Sample";
        public List<TabBaseViewModel> ViewModels { get; set; }
        public TabControlSampleViewModel()
        {
            ViewModels = new List<TabBaseViewModel>
            {
                new TabItemViewModelA { Header = "Tab 1", ContentA = "Content for Tab 1" },
                new TabItemViewModelB { Header = "Tab 2", ContentB = "Content for Tab 2" },
                
            };
        }
    }
}
