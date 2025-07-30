using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ay.CodeTrace.ControlLib
{
    public partial class NoLoadStation : UserControl
    {
        private string _jTName = "机台A1";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台名称")]
        public string JTName
        {
            get { return _jTName; }
            set
            {
                _jTName = value;
                this.station1.JTName = _jTName;

            }
        }

        private JTState _jTState = JTState.待机状态;
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台状态")]
        public JTState JTState
        {
            get { return _jTState; }
            set
            {
                _jTState = value;
                this.station1.JTState = _jTState;
            }
        }


        private string _motorCode;
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机电电机条码")]
        public string MotorCode
        {
            get { return _motorCode; }
            set
            {
                if (_motorCode != value)
                {
                    this.lbl_MotorCode.Text = _motorCode;
                    _motorCode = value;
                }
            }
        }

        private string _noLoadCurrent = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台空载电流")]
        public string  NoLoadCurrent
        {
            get { return _noLoadCurrent; }
            set
            {
                if (_noLoadCurrent != value)
                {
                    _noLoadCurrent = value;
                    this.lbl_NoLoadCurrent.Text = _noLoadCurrent;
                }
            }
        }



        private string _noLoadSpeed = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台空载转速")]
        public string NoLoadSpeed
        {
            get { return _noLoadSpeed; }
            set
            {
                if (_noLoadSpeed != value)
                {
                    _noLoadSpeed = value;
                    this.lbl_NoLoadSpeed.Text = _noLoadSpeed;
                }
            }
        }


        private string _axisElongation = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台轴伸长度")]
        public string AxisElongation
        {
            get { return _axisElongation; }
            set
            {
                if (_axisElongation != value)
                {
                    _axisElongation = value;
                    this.lbl_AxisElongation.Text = _axisElongation;
                }
            }
        }


        private string _diameter = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台滚花直径")]
        public string Diameter
        {
            get { return _diameter; }
            set
            {
                if (_diameter != value)
                {
                    _diameter = value;
                    this.lbl_Diameter.Text = _diameter;
                }
            }
        }

        public NoLoadStation()
        {
            InitializeComponent();
        }
    }
}