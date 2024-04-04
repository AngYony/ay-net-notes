using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommandSample.WpfApp
{
    public interface IView
    {
        bool IsChanged { get; set; }

        void Clear();
    }

    //自定义命令
    public class ClearCommand : ICommand
    {
        //当命令可执行状态发生改变时，应当被激发
        public event EventHandler? CanExecuteChanged;

        //用于判断命令是否可以执行（暂不实现）
        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        //命令执行，带有与业务相关的Clear逻辑
        public void Execute(object? parameter)
        {
            IView view = parameter as IView;
            if (view != null)
            {
                view.Clear();
            }
        }
    }

    //自定义命令源
    public class MyCommandSource : UserControl, ICommandSource
    {
        // 继承自接口的三个属性
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public IInputElement CommandTarget { get; set; }

        //在组件被单击时连带执行命令
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            //在命令目标上执行命令，或称让命令作用于命令目标
            if (this.CommandTarget != null)
            {
                this.Command.Execute(this.CommandTarget);
            }
        }
    }

    /// <summary>
    /// MiniView.xaml 的交互逻辑
    /// </summary>
    public partial class MiniView : UserControl, IView
    {
        public MiniView()
        {
            InitializeComponent();
        }

        public bool IsChanged { get; set; }

        public void Clear()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
        }
    }
}