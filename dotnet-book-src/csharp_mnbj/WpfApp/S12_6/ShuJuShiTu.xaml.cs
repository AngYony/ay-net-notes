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

namespace S12_6
{
    /// <summary>
    /// 数据视图，对数据进行排序筛选和分组
    /// </summary>
    public partial class ShuJuShiTu : Window
    {
        public ShuJuShiTu()
        {
            InitializeComponent();
            lb1.ItemsSource = Goods.GetSampleData();
            lb2.ItemsSource = Goods.GetSampleData();
            lb3.ItemsSource = Goods.GetSampleData();
        }


        //升序
        private void btnAsc_Click(object sender, RoutedEventArgs e)
        {
            lb1.Items.SortDescriptions.Clear();
            lb1.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("ID", System.ComponentModel.ListSortDirection.Ascending));
        }
        //降序
        private void btnDesc_Click(object sender, RoutedEventArgs e)
        {
            lb1.Items.SortDescriptions.Clear();
            lb1.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("ID", System.ComponentModel.ListSortDirection.Descending));
        }

        //筛选
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            decimal dvalue = default(decimal);
            // 检测输入的内容是否正确
            if (decimal.TryParse(txtFilter.Text, out dvalue) == false)
            {
                MessageBox.Show("请输入正确的价格。");
                return;
            }
            //筛选时，数据源中的每个数据项都会调用一次该委托，以确定哪些数据需要过滤
            lb2.Items.Filter = delegate (object obj)
            {
                Goods goods = obj as Goods;
                if (goods != null)
                {
                    if (goods.Price > dvalue)
                    {
                        return true;
                    }
                }
                return false;
                /*
                 * 如果符合筛选条件，则返回true
                 * 如果不符合筛选条件，则返回false
                 */
            };
        }

        //取消筛选
        private void btnFilterClear_Click(object sender, RoutedEventArgs e)
        {
            lb2.Items.Filter = null;
        }

        //按照Category分组
        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            lb3.Items.GroupDescriptions.Clear();
            lb3.Items.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        }
        //取消分组
        private void btnClearGroup_Click(object sender, RoutedEventArgs e)
        {
            lb3.Items.GroupDescriptions.Clear();

        }
    }


    public class Goods
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 获取示例数据列表
        /// </summary>
        public static List<Goods> GetSampleData()
        {
            List<Goods> goodsList = new List<Goods>();
            goodsList.Add(new Goods { ID = 1, GoodsName = "尺子", Price = 3.5M, Category = "文具" });
            goodsList.Add(new Goods { ID = 2, GoodsName = "铅笔", Price = 1.2M, Category = "文具" });
            goodsList.Add(new Goods { ID = 3, GoodsName = "排球", Price = 65M, Category = "体育用品" });
            goodsList.Add(new Goods { ID = 4, GoodsName = "篮球", Price = 130M, Category = "体育用品" });
            goodsList.Add(new Goods { ID = 5, GoodsName = "羽毛球", Price = 5M, Category = "体育用品" });
            goodsList.Add(new Goods { ID = 6, GoodsName = "英汉小词典", Price = 28M, Category = "工具书" });
            goodsList.Add(new Goods { ID = 7, GoodsName = "新华字典", Price = 32.5M, Category = "工具书" });
            goodsList.Add(new Goods { ID = 8, GoodsName = "书法字帖", Price = 8M, Category = "工具书" });
            return goodsList;
        }
    }
}
