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
    /// 主从视图（列表-详情展示）
    /// </summary>
    public partial class ZhuCongShiTu : Window
    {
        public ZhuCongShiTu()
        {
            InitializeComponent();

            List<Order> orders = new List<Order>()
            {
                new Order { OrderID = 1, OrderDate = new DateTime(2013,5,22), CustomName = "A公司", ContactName = "吴先生", ContactPhoneNo = "66584485", ContactEmail = "samp22@test.cn", Qty = 400f, Remarks = "颜色：绿色；材质：轻纱。" },
                new Order { OrderID = 2, OrderDate = new DateTime(2014,3,29), CustomName = "B公司", ContactName = "张先生", ContactPhoneNo = "121003568", ContactEmail = "sam32@test.net", Qty = 250f, Remarks = "材质：任意。" },
                new Order { OrderID = 3, OrderDate = new DateTime(2013,12,30), CustomName = "C公司", ContactName = "胡女士", ContactPhoneNo = "8210924", ContactEmail = "samp52@test.com", Qty = 650f, Remarks = "红、蓝、白三种颜色。" },
                new Order { OrderID = 4, OrderDate = new DateTime(2013,2,17), CustomName = "D公司", ContactName = "吕先生", ContactPhoneNo = "9300002", ContactEmail = "samp30@demo.net", Qty = 285f, Remarks = "材质：纱；颜色：纯黑。" },
                new Order { OrderID = 5, OrderDate = new DateTime(2013,10,5), CustomName = "E公司", ContactName = "周女士", ContactPhoneNo = "133288659", ContactEmail = "samp85@test.cn", Qty = 1000f, Remarks = "材质：棉、纱；颜色：紫、天蓝复合色。" }
            };
            lv.ItemsSource = orders;
        }
    }

    public class Order
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomName { get; set; }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string ContactPhoneNo { get; set; }
        /// <summary>
        /// 客户联系邮箱
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// 订货数量
        /// </summary>
        public float Qty { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
