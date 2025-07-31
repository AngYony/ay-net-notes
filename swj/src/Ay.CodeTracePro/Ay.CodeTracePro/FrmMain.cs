using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AY.Utils;
using xbd.NodeSettings;

namespace Ay.CodeTracePro
{
    public partial class FrmMain : Form
    {
        private Timer updateTimer = new Timer();
        //private string A1A2Path = Application.StartupPath + "\\Config\\A1A2";
        private string A3A4Path = Application.StartupPath + "\\Config\\A3A4";
        private string A5A6Path = Application.StartupPath + "\\Config\\A5A6";

        public FrmMain()
        {
            InitializeComponent();
            this.SetWindowDrag(this.button4, this.TopPanel, this.label1);
            this.Load += FrmMain_Load;
            this.FormClosing += FrmMain_FormClosing;
            this.updateTimer.Interval = 500;
            updateTimer.Tick += UpdateTimer_Tick;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var result2 = SiemensCFG.LoadDevice(A3A4Path);
            if (result2.IsSuccess)
            {
                Global.A3A4 = result2.Content;
                Global.A3A4.ForEach(c =>
                {

                    c.AlarmEvent += PLC_AlarmEvent;
                    c.Start();
                });
            }
            else
            {
                FrmMsgNoAck.Show("加载A3A4设备配置失败：" + result2.Message, "错误");
                return;
            }
            var result3 = ModbusTCPCFG.LoadDevice(A5A6Path);
            if (result3.IsSuccess)
            {
                Global.A5A6 = result3.Content;
                Global.A5A6.ForEach(c =>
                {
                    c.DataFormat = AY.CommunicationLib.DataConvert.DataFormat.CDAB;
                    c.AlarmEvent += PLC_AlarmEvent;
                    c.Start();
                });
            }
            else
            {
                FrmMsgNoAck.Show("加载A5A6设备配置失败：" + result3.Message, "错误");
                return;
            }

        }

        private Action<SiemensDevice, string, bool> A3A4Handle; 
        private Action<ModbusTCPDevice, string, bool> A5A6Handle;

        /// <summary>
        /// 业务处理
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void PLC_AlarmEvent(VariableBase variable, AlarmEventArgs e)
        {
            //检查触发
            if (e.IsTriggered)
            {
                if (e.VarName == "完成信号")
                {
                    if (e.CurrentValue == "1")
                    {
                        switch (e.DeviceName)
                        {
                            case "机台A3":
                            case "机台A4":
                                var device2 = Global.A3A4.FirstOrDefault(c => c.DeviceName == e.DeviceName);
                                if (device2 != null)
                                {
                                    string code = device2["电机条码"]?.ToString();
                                    if (code.Length > 0)
                                    {
                                        bool result = device2["检测结果"]?.ToString() == "1";
                                        this.A3A4Handle?.Invoke(device2, code, result);


                                        var write = device2.Write("完成信号", "0");
                                        if (write.IsSuccess)
                                        {
                                            //记录日志，复位成功
                                        }
                                    }
                                    else
                                    {
                                        FrmMsgNoAck.Show($"机台{e.DeviceName}完成，但未获取到电机条码", "提示");
                                    }
                                }
                                break;

                            case "机台A5":
                            case "机台A6":
                                var device3 = Global.A5A6.FirstOrDefault(c => c.DeviceName == e.DeviceName);
                                if (device3!= null)
                                {
                                    string code = device3["电机条码"]?.ToString();
                                    if (code.Length > 0)
                                    {
                                        bool result = device3["检测结果"]?.ToString() == "1";
                                        this.A5A6Handle?.Invoke(device3, code, result);


                                        var write = device3.Write("完成信号", "0");
                                        if (write.IsSuccess)
                                        {
                                            //记录日志，复位成功
                                        }
                                    }
                                    else
                                    {
                                        FrmMsgNoAck.Show($"机台{e.DeviceName}完成，但未获取到电机条码", "提示");
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {

        }

        private void Common_Navi_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (Enum.IsDefined(typeof(FormNames), btn.Text))
                {
                    OpenForm(btn.Text.GetEnumValue<FormNames>());
                    var oldbtn = this.TopPanel.Controls.OfType<Button>().FirstOrDefault(b => "selected".Equals(b.AccessibleDescription));
                    if (oldbtn != null)
                    {
                        oldbtn.BackColor = Color.Transparent; //未选中
                        oldbtn.AccessibleDescription = "";
                    }
                    btn.BackColor = Color.FromArgb(36, 158, 153); //选中颜色
                    btn.AccessibleDescription = "selected";
                }
            }
        }


        private void OpenForm(FormNames openName)
        {
            //先找到，并设置到最前面，然后关闭其他的
            var frm = this.MainPanel.Controls.OfType<Form>().FirstOrDefault(f => f.Text == openName.ToString());
            if (frm == null)
            {
                switch (openName)
                {
                    case FormNames.生产看板:
                        frm = new FrmBoard();
                        break;


                    case FormNames.生产监控:
                        frm = new FrmMonitor();
                        break;

                    case FormNames.条码追溯:
                        frm = new FrmRecord();
                        break;
                }

                if (frm != null)
                {
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Dock = DockStyle.Fill;
                    frm.Parent = this.MainPanel;
                    frm.BringToFront();
                    frm.Show();
                }
            }
            else
            {
                frm.BringToFront();
            }
            var closeForms = this.MainPanel.Controls.OfType<Form>()
                .Where(f => f != frm && f.Text.GetEnumValue<FormNames>() >= (FormNames)2).ToArray();
            foreach (var cf in closeForms)
            {
                cf.Close();
            }
        }
    }
}
