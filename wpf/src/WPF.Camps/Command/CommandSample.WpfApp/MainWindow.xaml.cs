using System.Windows;
using System.Windows.Input;

namespace CommandSample.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //声明并定义命令
        private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(MainWindow));

        public MainWindow()
        {
            
            InitializeComponent();

            //把命令赋值给命令源（发送者）并指定快捷键
            this.button1.Command = this.clearCmd;
            this.clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

            //指定命令目标
            this.button1.CommandTarget = this.textBoxA;

            //创建命令关联
            CommandBinding cb = new CommandBinding();
            cb.Command = this.clearCmd; //只关注与clearCmd相关的事件
            cb.CanExecute += new CanExecuteRoutedEventHandler(Cb_CanExecute);
            cb.Executed += new ExecutedRoutedEventHandler(Cb_Executed);

            //把命令关联安置在外围控件上
            this.stackPanel.CommandBindings.Add(cb);
        }

        //当探测命令是否可以执行时，此方法被调用
        private void Cb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(this.textBoxA.Text);
            //避免继续向上传而降低程序性能
            e.Handled = true;
        }

        //当命令送达目标后，此方法被调用
        private void Cb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.textBoxA.Clear();
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Window1().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new CusCommand().ShowDialog();
        }
    }
}