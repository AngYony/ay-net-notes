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
    public partial class LoadStation : UserControl
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

        private string _LoadCurrent = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台负载电流")]
        public string  LoadCurrent
        {
            get { return _LoadCurrent; }
            set
            {
                if (_LoadCurrent != value)
                {
                    _LoadCurrent = value;
                    this.lbl_LoadCurrent.Text = _LoadCurrent;
                }
            }
        }



        private string _LoadSpeed = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台负载转速")]
        public string LoadSpeed
        {
            get { return _LoadSpeed; }
            set
            {
                if (_LoadSpeed != value)
                {
                    _LoadSpeed = value;
                    this.lbl_LoadSpeed.Text = _LoadSpeed;
                }
            }
        }


        
        public LoadStation()
        {
            InitializeComponent();
        }
    }
}