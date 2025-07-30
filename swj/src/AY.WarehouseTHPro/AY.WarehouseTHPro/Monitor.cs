using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.WarehouseTHPro
{
    public partial class Monitor : UserControl
    {


        private string zoneName = "仓库分区：A区-01";
        [Category("自定义属性")]
        [Description("设置或获取区域标题名称")]
        [Browsable(true)]
        public string ZoneName
        {
            get { return zoneName; }
            set
            {
                zoneName = value;
                this.headPanel.TitleText = zoneName;
            }
        }


        private string tempValue = "0.0";
        [Category("自定义属性")]
        [Description("设置或获取温度值")]
        [Browsable(true)]
        public string TempValue
        {
            get { return tempValue; }
            set
            {
                if (tempValue != value)
                {
                    tempValue = value;
                    if (float.TryParse(tempValue, out float temp))
                    {
                        this.lbl_temp.Text = temp.ToString("F1");
                        this.the_Temp.Value = Convert.ToInt32(temp);
                    }
                    else
                    {
                        this.lbl_temp.Text = "NaN";
                        this.the_Temp.Value = 0;
                    }
                }
            }
        }


        private string humidityValue = "0.0";
        [Category("自定义属性")]
        [Description("设置或获取湿度值")]
        [Browsable(true)]
        public string HumidityValue
        {
            get { return humidityValue; }
            set
            {
                if (humidityValue != value)
                {
                    humidityValue = value;
                    if (float.TryParse(humidityValue, out float humidity))
                    {
                        this.lbl_Humidity.Text = humidity.ToString("F1");
                        this.wave_Humidity.Value = Convert.ToInt32(humidity);
                    }
                    else
                    {
                        this.lbl_Humidity.Text = "NaN";
                        this.wave_Humidity.Value = 0;
                    }
                }
            }
        }

        private bool isAvaliable = true;
        [Category("自定义属性")]
        [Description("设置或获取当前设备是否在线")]
        [Browsable(true)]
        public bool IsAvaliable
        {
            get { return isAvaliable; }
            set
            {
                if (isAvaliable != value)
                {
                    isAvaliable = value;
                    this.headPanel.ThemeColor = value ? OkColor : NGColor;
                }
            }

        }
        [Category("自定义属性")]
        [Description("设置或获取当前设备在线显示颜色")]
        [Browsable(true)]
        public Color OkColor { get; set; } = Color.FromArgb(1, 106, 189);


        [Category("自定义属性")]
        [Description("设置或获取当前设备离线显示颜色")]
        [Browsable(true)]
        public Color NGColor { get; set; } = Color.Red;

        [Category("自定义属性")]
        [Description("设置或获取通信组名称")]
        [Browsable(true)]
        public string GroupName { get; set; } = "A01";


        public Monitor()
        {
            InitializeComponent();
        }
    }
}
