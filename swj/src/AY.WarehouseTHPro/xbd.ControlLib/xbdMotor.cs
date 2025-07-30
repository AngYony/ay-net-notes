using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xbd.ControlLib
{
   public  enum PumpState
    {
        停止,
        运行,
        故障,
        备用
    }
    public partial class xbdMotor : UserControl
    {
        public xbdMotor()
        {
            InitializeComponent();
        }

        private PumpState pumpState = 0;

        [Browsable(true), Category("自定义属性"), Description("获取或设置泵的设备状态")]
        /// <summary>
        /// 0：停止  1：运行  2：故障  3：备用
        /// </summary>
        public PumpState PumpState
        {
            get { return pumpState; }

            set
            {
                if (value != pumpState)
                {
                    pumpState = value;

                    switch (pumpState)
                    {
                        case PumpState.停止:
                            this.MainPic.Image = Properties.Resources.PumpStop;
                            break;
                        case PumpState.运行:
                            this.MainPic.Image = Properties.Resources.PumpRun;
                            break;
                        case PumpState.故障:
                            this.MainPic.Image = Properties.Resources.PumpFault;
                            break;
                        case PumpState.备用:
                            this.MainPic.Image = Properties.Resources.PumpSpare;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public event EventHandler PumpClickEvent;

        private void MainPic_DoubleClick(object sender, EventArgs e)
        {
            PumpClickEvent?.Invoke(this, e);
        }
    }
}
