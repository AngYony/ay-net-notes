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
    /// 数据模板选择器，将在ListBox控件中显示一个蔬菜列表，如果编号是奇数，就把图像放在左侧，如果数据项的编号是偶数就把图像放在右侧。
    /// </summary>
    public partial class ShuJuMoBanXuanZeQi : Window
    {
        public ShuJuMoBanXuanZeQi()
        {
            InitializeComponent();

            // 第1项
            Vegetable vet1 = new Vegetable();
            vet1.Name = "佛手瓜";
            vet1.No = 1;
            vet1.Description = "是一种葫芦科佛手瓜属植物，原产于墨西哥、中美洲和西印度群岛，1915年传入中国。清脆，含有丰富营养。佛手瓜既可做菜，又能当水果生吃。加上瓜形如两掌合十，有佛教祝福之意，深受人们喜爱。";
            // 导入图像
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit(); //开始初化
            bmp.UriSource = new Uri("/images/佛手瓜.jpg", UriKind.Relative);
            //为防止图片变形，DecodePixelHeight和DecodePixelWidth最好不要同时设置，只需要设置其中一个即可，会自动同比调整大小。
            bmp.DecodePixelWidth = 150;
            bmp.EndInit(); //完成初始化
            vet1.PreviewImage = bmp;

            // 第2项
            Vegetable vet2 = new Vegetable();
            vet2.No = 2;
            vet2.Name = "苦瓜";
            vet2.Description = "一年生攀援草本苦瓜的果实。叶互生，掌状 5-7 深裂。花小，单性，雌雄同株，黄色。长不超过 2 厘米。果实纺锤形，有瘤状凸起，成熟时橙黄色，味苦，瓤鲜红色，味甜。嫩果是绿白色，成熟时呈白色，后熟果是黄红色，嫩果或老果均可食用。";
            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("/images/苦瓜.jpg", UriKind.Relative);

            bmp.DecodePixelWidth = 150;
            bmp.EndInit();
            vet2.PreviewImage = bmp;

            // 第3项
            Vegetable vet3 = new Vegetable();
            vet3.No = 3;
            vet3.Name = "芹菜";
            vet3.Description = "芹菜是高纤维食物,它经肠内消化作用产生一种木质素或肠内脂的物质，这类物质是一种抗氧化剂，常吃芹菜，尤其是吃芹菜叶，对预防高血压、动脉硬化等都十分有益，并有辅助治疗作用。";
            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("/images/芹菜.jpg", UriKind.Relative);
            bmp.DecodePixelWidth = 150;
            bmp.EndInit();
            vet3.PreviewImage = bmp;

            // 第4项
            Vegetable vet4 = new Vegetable();
            vet4.No = 4;
            vet4.Name = "山药";
            vet4.Description = "多年生草本植物，茎蔓生，常带紫色，块根圆柱形，叶子对生，卵形或椭圆形，花乳白色，雌雄异株。块根含淀粉和蛋白质，可以吃。单子叶植物。";
            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("/images/山药.jpg", UriKind.Relative);
            bmp.DecodePixelWidth = 150;
            bmp.EndInit();
            vet4.PreviewImage = bmp;

            // 第5项
            Vegetable vet5 = new Vegetable();
            vet5.No = 5;
            vet5.Name = "甜瓜";
            vet5.Description = "一年生蔓性草本植物。学名Cucumis melo L.，别名香瓜、果瓜、哈密瓜。别名香瓜、果瓜。葫科，甜瓜属，蔓性草本。原产非洲和亚洲热带地区，中国华北为薄皮甜瓜次级起源中心，新疆为厚皮甜瓜起源中心。";
            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("/images/甜瓜.jpg", UriKind.Relative);
            bmp.DecodePixelWidth = 150;
            bmp.EndInit();
            vet5.PreviewImage = bmp;

            // 第6项
            Vegetable vet6 = new Vegetable();
            vet6.No = 6;
            vet6.Name = "洋葱";
            vet6.Description = "属百合科、葱属，两年生草本。起源于亚洲后传至世界各地，20世纪初传入我国。洋葱含咖啡酸、芥子酸、桂皮酸、柠檬酸盐、多糖和多种氨基酸、蛋白质、钙、铁、磷、硒、B族维生素、维生素C、维生素E、粗纤维、碳水化合物等。";
            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("/images/洋葱.jpg", UriKind.Relative);
            bmp.DecodePixelWidth = 150;
            bmp.EndInit();
            vet6.PreviewImage = bmp;

            List<Vegetable> vetlist = new List<Vegetable>
            {
                vet1, vet2, vet3, vet4, vet5, vet6
            };
            // 设置项的数据源
            lb.ItemsSource = vetlist;
        }
    }

    public class Vegetable
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 蔬菜名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 预览图像
        /// </summary>
        public BitmapSource PreviewImage { get; set; }
    }


    //定义要套用的数据模板
    public class MyDatatemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// 如果数据项的编号为奇数则使用该模板
        /// </summary>
        public DataTemplate Template1 { get; set; }
        /// <summary>
        /// 如果数据项的编号为偶数则使用该模板
        /// </summary>
        public DataTemplate Template2 { get; set; }

        /// <summary>
        /// 重写该方法以选择合适的数据模板
        /// </summary>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Vegetable vt = item as Vegetable;
            // 检查编号是否为偶数
            if ((vt.No % 2) == 0)
            {
                return Template2;
            }

            return Template1;
        }
    }


}
