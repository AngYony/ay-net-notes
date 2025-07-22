using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.PressurizationStationPro
{
    public partial class MeterShow : UserControl
    {
        public MeterShow()
        {
            InitializeComponent();
        }

        private string paramName = "出水管温度";
        [Browsable(true)] //设置属性可在属性窗口中显示
        [Category("自定义属性")]
        [Description("设置或获取参数名称")]
        public string ParamName
        {
            get { return paramName; }
            set { paramName = value; this.lbl_ParamName.Text = ParamName; }
        }

        private float paramValue = 0.0f;
        [Browsable(true)] //设置属性可在属性窗口中显示
        [Category("自定义属性")]
        [Description("设置或获取参数数值")]
        public float ParamValue
        {
            get { return paramValue; }
            set
            {
                if (paramValue != value)
                {
                    paramValue = value;
                    this.lbl_paramValue.Text = $"{paramValue.ToString("f2")} {unit}";
                }
            }
        }

        private string unit = "℃";
        [Browsable(true)] //设置属性可在属性窗口中显示
        [Category("自定义属性")]
        [Description("设置或获取参数单位")]
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                this.lbl_paramValue.Text = $"{paramValue.ToString("f2")} {unit}";
            }
        }

        private float meterMax = 100.0f;
        [Browsable(true)] //设置属性可在属性窗口中显示
        [Category("自定义属性")]
        [Description("设置或获取仪表盘量程最大值")]
        public float MeterMax
        {
            get { return meterMax; }
            set { meterMax = value; this.meter_Param.MaxValue = meterMax; }
        }


        private float meterMin = 0.0f;
        [Browsable(true)] //设置属性可在属性窗口中显示
        [Category("自定义属性")]
        [Description("设置或获取仪表盘量程最小值")]
        public float MeterMin
        {
            get { return meterMin; }
            set { meterMin = value; this.meter_Param.MinValue = meterMin; }
        }

    }
}
