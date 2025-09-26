using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// ListBoxGroupSampleA.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxGroupSampleA : Window
    {
        public ListBoxGroupSampleA()
        {
            InitializeComponent();
            this.DataContext = new ListBoxGroupSampleAiewModel();
        }
    }

    public class ListBoxGroupSampleAiewModel
    {
        //public ICollectionView ProductsView { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public ListBoxGroupSampleAiewModel()
        {
            var products = new ObservableCollection<Product>
        {
            new Product { Name = "苹果", Category = "水果", Title = "水果专区" ,Wy="1234"},
            new Product { Name = "香蕉", Category = "水果", Title = "水果专区",Wy="1234" },
            new Product { Name = "西红柿", Category = "蔬菜", Title = "蔬菜专区" ,Wy="1234"},
            new Product { Name = "黄瓜", Category = "蔬菜", Title = "蔬菜专区" ,Wy="1234"},
            new Product { Name = "牛奶", Category = "饮品", Title = "饮品专区" ,Wy="1234"},
        };

            Products = products;

            //var collectionView = CollectionViewSource.GetDefaultView(products);

            //// 使用 Converter 生成分组对象
            //collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Category", new CategoryTitleGroupConverter()));

            //ProductsView = collectionView;
        }
    }



    public class CategoryTitleGroupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value 就是 Product.Category
            string category = value?.ToString();

            // 这里返回一个匿名对象（或者自定义类都行）
            return new { Category = category, Title = category + "专区" };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Wy { get; set; }
    }
}
