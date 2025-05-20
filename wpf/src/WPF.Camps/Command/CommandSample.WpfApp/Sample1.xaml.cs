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
    /// CommandSample1.xaml 的交互逻辑
    /// </summary>
    public partial class Sample1 : Window
    {
        public Sample1()
        {
            InitializeComponent();
        }
    }

    public class WyCommand : ICommand
    {
        Action<object> executeMethod;
        Func<object, bool> canExecuteMethod;

        public WyCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler? CanExecuteChanged;


        public bool CanExecute(object? parameter)
        {
            return canExecuteMethod(parameter);
        }

        public void Execute(object? parameter)
        {
            executeMethod?.Invoke(parameter);
        }
    }

    public class ViewModel
    {
        public ICommand MyCommand { get; set; }

        public ViewModel()
        {
            MyCommand = new WyCommand(Execute, CanExecute);
        }


        private bool CanExecute(object? parameter)
        {
            return true;
            //return false;
        }

        private void Execute(object? parameter)
        {
            MessageBox.Show("这是一个命令");
        }
    }
}
