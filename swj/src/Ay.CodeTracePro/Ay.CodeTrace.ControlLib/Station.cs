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

    public partial class Station : UserControl
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
                this.label1.Text = _jTName + "：" + _jTState.ToString();

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
                if (_jTState != value)
                {
                    _jTState = value;
                    this.label1.Text = _jTName + "：" + _jTState.ToString();
                    switch (_jTState)
                    {
                        case JTState.待机状态:
                            this.pictureBox1.Image = Properties.Resources.待机;
                            break;
                        case JTState.运行状态:
                            this.pictureBox1.Image = Properties.Resources.运行;
                            break;
                        case JTState.故障状态:
                            this.pictureBox1.Image = Properties.Resources.故障;
                            break;
                        case JTState.离线状态:
                            this.pictureBox1.Image = Properties.Resources.离线;
                            break;
                        default:
                            break;
                    }
                }
            }
        }




        public Station()
        {
            InitializeComponent();
        }
    }


    public enum JTState
    {
        待机状态,
        运行状态,
        故障状态,
        离线状态
    }



}
