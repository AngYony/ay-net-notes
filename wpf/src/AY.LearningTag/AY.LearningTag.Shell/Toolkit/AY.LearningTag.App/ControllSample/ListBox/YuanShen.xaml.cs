using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AY.LearningTag.App.ControllSample.ListBox
{
    /// <summary>
    /// YuanShen.xaml 的交互逻辑
    /// </summary>
    public partial class YuanShen : Window
    {
        public YuanShen()
        {
            InitializeComponent();
            this.DataContext = new YSViewModel();
        }

    }

    public record ImageItem(string ImageUrl, string Title);

    public partial class YSViewModel :ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<ImageItem> iamgeList = new();

        public YSViewModel()
        {
            iamgeList.Add(new ImageItem("https://c-ssl.duitang.com/uploads/blog/202209/24/20220924165257_61325.jpg", "张三"));
            iamgeList.Add(new ImageItem("https://c-ssl.duitang.com/uploads/blog/202209/24/20220924165257_371f9.jpg", "李四"));
        }
    }
}
