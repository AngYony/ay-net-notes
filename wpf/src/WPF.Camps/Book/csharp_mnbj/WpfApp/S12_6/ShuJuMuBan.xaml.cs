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
    /// 
    /// </summary>
    public partial class ShuJuMuBan : Window
    {
        public ShuJuMuBan()
        {
            InitializeComponent();

            // 设置数据源
            List<Student> listStu = new List<Student>
            {
                new Student { No = 1, Name = "小王", Description = "认真，细心。", Course = "Visual Basic基础课程", Mark = 73 },
                new Student { No = 2, Name = "小曾", Description = "有上进心。", Course = "C语言", Mark = 80 },
                new Student { No = 3, Name = "小张", Description = "不太用功。", Course = "ASP.NET专业网站开发", Mark = 48 },
                new Student { No = 4, Name = "小李", Description = "好问，好学。", Course = "数据库设计", Mark = 81 },
                new Student { No = 5, Name = "小范", Description = "基础较弱。", Course = "C++入门教程", Mark = 50 }
            };
            lbStudents.ItemsSource = listStu;
        }
    }

    public class Student
    {
        /// <summary>
        /// 学员编号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 学员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学员简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 培训课程
        /// </summary>
        public string Course { get; set; }
        /// <summary>
        /// 考试分数
        /// </summary>
        public float Mark { get; set; }
    }

    /// <summary>
    /// 定义一个转换器，如果学员的考试分数在60分以上就返回蓝色的画刷，否则返回红色的画刷。
    /// </summary>
    public class ColorBdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Student stu = value as Student;
            if (stu.Mark < 60)
            {
                return Brushes.Red;
            }
            return Brushes.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // 单向绑定，不需要处理
            return null;
        }
    }
}
