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
    public partial class NoiseStation
        : UserControl
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

        private string _positiveNoise = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台正转噪音")]
        public string PositiveNoise
        {
            get { return _positiveNoise; }
            set
            {
                if (_positiveNoise != value)
                {
                    _positiveNoise = value;
                    this.lbl_PositiveNoise.Text = _positiveNoise;
                }
            }
        }


        private string _negativeNoise = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台反转噪音")]
        public string NegativeNoise
        {
            get { return _negativeNoise; }
            set
            {
                if (_negativeNoise != value)
                {
                    _negativeNoise = value;
                    this.lbl_NegativeNoise.Text = _negativeNoise;
                }
            }
        }


        private string _diffNoise = "0.000";
        [Category("自定义属性")]
        [Browsable(true)]
        [Description("设置或获取机台噪音差值")]
        public string DiffNoise
        {
            get { return _diffNoise; }
            set
            {
                if (_diffNoise != value)
                {
                    _diffNoise = value;
                    this.lbl_DiffNoise.Text = _diffNoise;
                }
            }
        }

 

        public NoiseStation()
        {
            InitializeComponent();
        }
    }
}