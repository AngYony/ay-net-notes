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

namespace CommandSample.WpfApp
{
    /// <summary>
    /// Sample2.xaml 的交互逻辑
    /// </summary>
    public partial class Sample2 : Window
    {
        public Sample2()
        {
            InitializeComponent();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //获取命令参数
            var obj =  e.Parameter as string;
            MessageBox.Show(obj);
            Sample1 sample1 = new Sample1();
            sample1.Show();
        }
    }

    public static class AyCommands
    {
        public static RoutedUICommand Hello { get; set; }
        static AyCommands()
        {
            //设置命令快捷键
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            Hello = new RoutedUICommand(text: "Say Hi", "Hello", typeof(AyCommands),inputs);

        }

    }
}
