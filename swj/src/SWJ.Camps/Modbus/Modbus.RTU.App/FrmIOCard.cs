using Forms.Shared;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus.RTU.App
{
    public partial class FrmIOCard : FormBase
    {
        public FrmIOCard()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000; // 设置定时器间隔为1秒
            timer.Tick += Timer_Tick;
            timer.Enabled = false;
            this.FormClosing += (sender, e) =>
            {
                if (MessageBox.Show("确定要关闭吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true; // 取消关闭操作
                }
                else
                {
                    timer.Enabled = false; // 停止定时器
                    this.master = null;
                    if (serialPort != null && serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                }
            };
            RegisterDrag(this, this.topPanel, this.lblTitle, this.btnClose);
            this.portParam = new SerialPortParam()
            {
                PortName = "COM32", //此处不能使用和Slave同一个串口
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //先读取输入线圈
            bool[] value = this.master.ReadInputs(1, 0, 8);
            ReadValue(value, this.pnl_input);


            //再读取输出线圈
            bool[] value2 = this.master.ReadCoils(1, 0, 8);
            ReadValue(value2, this.pnl_Output);
        }

        private void ReadValue(bool[] value, Panel pnl)
        {
            if (value != null && value.Length == 8)
            {
                foreach (var item in pnl.Controls.OfType<Label>())
                {
                    if (item.Tag != null && int.TryParse(item.Tag.ToString(), out int index))
                    {
                        if (index >= 0 && index < value.Length)
                        {
                            //item.Text = value[index] ? "ON" : "OFF";
                            item.BackColor = value[index] ? Color.Green : Color.Red;
                        }
                    }
                }
            }
        }

        ModbusSerialMaster master = null;
        SerialPortParam portParam = null;
        SerialPort serialPort = null;
        Timer timer = null;
        /// <summary>
        /// 建立连接按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            serialPort = new SerialPort(
                portName: portParam.PortName,
                baudRate: portParam.BaudRate,
                parity: portParam.Parity,
                dataBits: portParam.DataBits,
                stopBits: portParam.StopBits
            );
            try
            {

                //打开串口连接
                serialPort.Open();
                master = ModbusSerialMaster.CreateRtu(serialPort);
                master.Transport.ReadTimeout = 1000; // 设置读取超时时间
                master.Transport.Retries = 3; // 设置重试次数

                //todo: 读取设备信息
                timer.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("连接失败，请检查串口设置或设备连接。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void PanelOutputLabel_Common_Click(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                if (ushort.TryParse(label.Tag.ToString(), out ushort index))
                {
                    //读取当前状态
                    bool currentState = this.master.ReadCoils(1, index, 1)[0];
                    //切换状态
                    bool newState = !currentState;
                    //写入新状态
                    this.master.WriteSingleCoil(1, index, newState);
                    //更新UI
                    label.BackColor = newState ? Color.Green : Color.Red;
                }
            }


        }
    }
}
