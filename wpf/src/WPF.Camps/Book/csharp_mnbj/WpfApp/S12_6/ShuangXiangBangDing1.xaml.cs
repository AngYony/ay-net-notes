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
    /// ② XAML扩展标记的使用-双项绑定
    /// </summary>
    public partial class ShuangXiangBangDing1 : Window
    {
        public ShuangXiangBangDing1()
        {
            InitializeComponent();

            this.inputBox.DataContext = new News
            {
                Title = "新闻标题",
                Author = "小飞",
                Content = "今天没有新闻",
                IsPublished = false
            };

        }
    }

    public class News
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 是否已发布
        /// </summary>
        public bool IsPublished { get; set; }
        /// <summary>
        /// 新闻正文
        /// </summary>
        public string Content { get; set; }
    }
}
