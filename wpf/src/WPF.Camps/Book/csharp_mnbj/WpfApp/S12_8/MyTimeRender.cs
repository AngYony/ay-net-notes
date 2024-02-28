using System;
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

namespace S12_8
{
    
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyApp"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyApp;assembly=MyApp"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:MySlider/>
    ///
    /// </summary>
    [TemplatePart(Name = MyTimeRender.PART_TimePresenter, Type = typeof(ContentControl))]
    public class MyTimeRender : System.Windows.Controls.Control
    {
        /// <summary>
        /// 部件名称
        /// </summary>
        public const string PART_TimePresenter = "PART_TimePresenter";

        static MyTimeRender()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTimeRender), new FrameworkPropertyMetadata(typeof(MyTimeRender)));
        }

        // 模板中的ContentControl控件
        private ContentControl p_contenthost = null;

        System.Windows.Threading.DispatcherTimer _timer = null;

        public MyTimeRender()
        {
            // 实例化计时器
            _timer = new System.Windows.Threading.DispatcherTimer(TimeSpan.FromSeconds(1d), System.Windows.Threading.DispatcherPriority.Render, new EventHandler(OnTick), System.Windows.Threading.Dispatcher.CurrentDispatcher);
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (p_contenthost != null)
            {
                p_contenthost.Content = DateTime.Now;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (p_contenthost != null)
            {
                p_contenthost = null;
            }
            // 从控件模板中找出ContentControl控件
            p_contenthost = GetTemplateChild(PART_TimePresenter) as ContentControl;
            // 设置显示时间格式
            p_contenthost.ContentStringFormat = "HH:mm:ss";
            p_contenthost.Content = DateTime.Now;
        }
    }
}

