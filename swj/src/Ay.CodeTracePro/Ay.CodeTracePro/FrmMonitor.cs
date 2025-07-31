using Ay.CodeTrace.ControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.NodeSettings;

namespace Ay.CodeTracePro
{
    public partial class FrmMonitor : Form
    {
        public FrmMonitor()
        {
            InitializeComponent();
            this.label1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void UpdateMonitor()
        {

        }

        private void UpdateA1A2()
        {
            //遍历控件，根据Name找到对应的控件进行更新


        }

        private void UpdateA3A4()
        {
            foreach (var item in this.panel2.Controls.OfType<NoiseStation>())
            {
                var plc = Global.A3A4.Where(c => c.DeviceName == item.JTName).FirstOrDefault();
                if (plc != null)
                {
                    if (plc.IsConnected)
                    {
                        item.MotorCode = plc["电机条码"]?.ToString();
                        //其他属性赋值，此处略
                    }
                    else
                    {
                        item.MotorCode = "";
                    }
                }
            }
        }

        

        private void UpdateA5A6()
        {
            foreach (var item in this.panel2.Controls.OfType<LoadStation>())
            {
                var plc = Global.A5A6.Where(c => c.DeviceName == item.JTName).FirstOrDefault();
                if (plc != null)
                {
                    if (plc.IsConnected)
                    {
                        item.MotorCode = plc["电机条码"]?.ToString();
                        //其他属性赋值，此处略
                    }
                    else
                    {
                        item.MotorCode = "";
                    }
                }
            }
        }

        public void A3A4Handle(SiemensDevice device, string code, bool result)
        {
             
        }
    }
}
