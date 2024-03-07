using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

//本篇代码也可以作为项目封装代码使用
namespace BindingSample.WpfApp
{
    /// <summary>
    /// Sample4.xaml 的交互逻辑
    /// </summary>
    public partial class Sample4 : Window
    {
        public Sample4()
        {
            InitializeComponent();
            this.DataContext = new Sample4Model();
        }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class Sample4Model : ViewModelBase
    {
        public MyCommand4 ShowCommand { get; set; }

        #region 实现通知更新
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged(); } }



        #endregion

        public Sample4Model()
        {
            Name = "hello";
            ShowCommand = new MyCommand4(Show);
        }



        public void Show()
        {
            //通过绑定为文本框赋值
            Name = "点击了按钮！";
            Title = "这是标题";
            MessageBox.Show(Name);
        }
    }

    public class MyCommand4 : ICommand
    {
        private readonly Action action;

        public MyCommand4(Action action)
        {
            this.action = action;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
