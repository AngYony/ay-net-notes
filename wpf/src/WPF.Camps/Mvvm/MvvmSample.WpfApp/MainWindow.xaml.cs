using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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

namespace MvvmSample.WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            //注册一个接收string类型参数的消息，地址是Token1
            WeakReferenceMessenger.Default.Register<string, string>(this, "Token1", (s, e) =>
            {
                MessageBox.Show(e);
            });
        }
    }

    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Name = "Hello";
            ShowCommand = new RelayCommand<string>(Show);

        }

        public RelayCommand<string> ShowCommand { get; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        string title;
        public string Title
        {
            get => title; set
            {
                title = value;
                OnPropertyChanged();
            }
        }



        public void Show(string content)
        {
            Name = "点击了按钮";
            Title = "我是标题";
            //给Toekn1的地址发送一个string类型的值 content
            WeakReferenceMessenger.Default.Send(content, "Token1");
        }
    }
}
