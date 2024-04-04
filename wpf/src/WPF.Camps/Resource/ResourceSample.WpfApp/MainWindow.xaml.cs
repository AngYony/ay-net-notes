using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResourceSample.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var uri = new Uri(@"pack://application:,,,/images/user.png",UriKind.Absolute);
            this.img.Source=new BitmapImage(uri);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["res1"] = new TextBlock { Text = "什么都不是" };
            this.Resources["res2"] = new TextBlock { Text = "什么都不是" };
        }
    }
}