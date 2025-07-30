using AY.WarehouseTH.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AY.Utils;
using AY.WarehouseTHPro.Extensions;
using AY.NodeSettings;
using AY.WarehouseTH.BLL;
using System.IO.Ports;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AY.WarehouseTHPro
{
    public partial class FrmMain : Form
    {
        private Timer timer = new Timer();
        private Action updateMonitor;
        private Action updateTrend;
        private string sysInfoPath = Application.StartupPath + "\\sysinfo.ini";
        private SysInfo sysInfo = null;
        private ObservableCollection<string> actualAlarmList = new ObservableCollection<string>();
        private DateTime lastTime = DateTime.Now;
        private MonitorBLL monitorBLL = new MonitorBLL();
        private SysAlarmBLL alarmBLL = new SysAlarmBLL();
        public FrmMain()
        {
            InitializeComponent();
            this.SetWindowDrag(btn_Colse, btn_Min, btn_Max, this.topPanel, this.label1, this.xbdScrollText1);
            this.timer.Interval = 200;
            this.timer.Tick += Timer_Tick;
            this.Load += FrmMain_Load;
            this.FormClosing += FrmMain_FormClosing;
            this.actualAlarmList.CollectionChanged += ActualAlarmList_CollectionChanged;
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            //读取系统配置信息
            var sysinfoResult = SysInfoManager.GetSysInfoFromPath(sysInfoPath);
            if (sysinfoResult.IsSuccess)
            {
                this.sysInfo = sysinfoResult.Content;
            }
            else
            {
                FrmMsgNoAck.Show("读取系统配置信息失败：" + sysinfoResult.Message);
                return;
            }

            //读取Excel配置
            var result = ModbusRTU_CFG.LoadDevice(Application.StartupPath);
            if (result.IsSuccess && result.Content.Count == 2)
            {

                CommonMethods.DeviceZoneA = result.Content[0];
                CommonMethods.DeviceZoneB = result.Content[1];

                //将参数配置覆盖Excel中的设备配置
                CommonMethods.DeviceZoneA.PortName = this.sysInfo.PortNameA;
                CommonMethods.DeviceZoneA.BaudRate = this.sysInfo.BaudRateA;
                CommonMethods.DeviceZoneA.Parity = this.sysInfo.ParityA.GetEnumValue<Parity>();
                CommonMethods.DeviceZoneA.DataBits = this.sysInfo.DataBitsA;
                CommonMethods.DeviceZoneA.StopBits = this.sysInfo.StopBitsA.GetEnumValue<StopBits>();

                CommonMethods.DeviceZoneB.PortName = this.sysInfo.PortNameB;
                CommonMethods.DeviceZoneB.BaudRate = this.sysInfo.BaudRateB;
                CommonMethods.DeviceZoneB.Parity = this.sysInfo.ParityB.GetEnumValue<Parity>();
                CommonMethods.DeviceZoneB.DataBits = this.sysInfo.DataBitsB;
                CommonMethods.DeviceZoneB.StopBits = this.sysInfo.StopBitsB.GetEnumValue<StopBits>();


                foreach (var g in CommonMethods.DeviceZoneA.GroupList)
                {
                    g.VarList.ForEach(v =>
                    {
                        //将参数配置里的报警限值覆盖到已读取的Excel配置文件对应的变量的值
                        switch (v.VarName)
                        {
                            case "A01温度":
                                v.HAlarmValue = this.sysInfo.A01TempH;
                                v.LAlarmValue = this.sysInfo.A01TempL;
                                break;
                            case "A01湿度":
                                v.HAlarmValue = this.sysInfo.A01HumidityH;
                                v.LAlarmValue = this.sysInfo.A01HumidityL;
                                break;
                            case "A02温度":
                                v.HAlarmValue = this.sysInfo.A02TempH;
                                v.LAlarmValue = this.sysInfo.A02TempL;
                                break;
                            case "A02湿度":
                                v.HAlarmValue = this.sysInfo.A02HumidityH;
                                v.LAlarmValue = this.sysInfo.A02HumidityL;
                                break;
                            case "A03温度":
                                v.HAlarmValue = this.sysInfo.A03TempH;
                                v.LAlarmValue = this.sysInfo.A03TempL;
                                break;
                            case "A03湿度":
                                v.HAlarmValue = this.sysInfo.A03HumidityH;
                                v.LAlarmValue = this.sysInfo.A03HumidityL;
                                break;
                            default:
                                break;
                        }

                    });
                }

                foreach (var g in CommonMethods.DeviceZoneB.GroupList)
                {
                    g.VarList.ForEach(v =>
                    {
                        //将参数配置里的报警限值覆盖到已读取的Excel配置文件对应的变量的值
                        switch (v.VarName)
                        {
                            case "B01温度":
                                v.HAlarmValue = this.sysInfo.B01TempH;
                                v.LAlarmValue = this.sysInfo.B01TempL;
                                break;
                            case "B01湿度":
                                v.HAlarmValue = this.sysInfo.B01HumidityH;
                                v.LAlarmValue = this.sysInfo.B01HumidityL;
                                break;
                            case "B02温度":
                                v.HAlarmValue = this.sysInfo.B02TempH;
                                v.LAlarmValue = this.sysInfo.B02TempL;
                                break;
                            case "B02湿度":
                                v.HAlarmValue = this.sysInfo.B02HumidityH;
                                v.LAlarmValue = this.sysInfo.B02HumidityL;
                                break;
                            case "B03温度":
                                v.HAlarmValue = this.sysInfo.B03TempH;
                                v.LAlarmValue = this.sysInfo.B03TempL;
                                break;
                            case "B03湿度":
                                v.HAlarmValue = this.sysInfo.B03HumidityH;
                                v.LAlarmValue = this.sysInfo.B03HumidityL;
                                break;
                            default:
                                break;
                        }

                    });
                }

                CommonMethods.DeviceZoneA.Start();
                CommonMethods.DeviceZoneA.AlarmEvent += CommonDeviceZone_AlarmEvent;
                CommonMethods.DeviceZoneB.Start();
                CommonMethods.DeviceZoneB.AlarmEvent += CommonDeviceZone_AlarmEvent;

            }

            this.timer.Start();

            Common_Navi_Click(this.btn_Monitor, null);


        }

        private void CommonDeviceZone_AlarmEvent(VariableBase variable, AlarmEventArgs e)
        {
            if (e.IsTriggered)
            {
                if (!this.actualAlarmList.Contains(e.AlarmNote))
                {
                    this.actualAlarmList.Add(e.AlarmNote);
                }
            }
            //报警消除
            else
            {
                if (this.actualAlarmList.Contains(e.AlarmNote))
                {
                    this.actualAlarmList.Remove(e.AlarmNote);
                }
            }

            alarmBLL.AddSysAlarm(new SysAlarm
            {
                AlarmTime = DateTime.Now,
                AlarmNote = e.AlarmNote,
                AlarmSet = e.AlarmValue,
                AlarmValue = e.CurrentValue,
                AlarmType = e.IsTriggered ? "报警" : "消除",
                Operator = "未登录"
            });

        }

        private void ActualAlarmList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                if (this.actualAlarmList.Any())
                {
                    this.xbdScrollText1.Visible = true;
                    this.xbdScrollText1.TextScroll = string.Join("  ", this.actualAlarmList);
                    this.xbdScrollText1.IsScoll = this.actualAlarmList.Count > 1; //如果有多条报警信息，则滚动
                }
                else
                {
                    this.xbdScrollText1.Visible = false;
                }
            }));
        }




        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmMsgWithAck.Show("确定要退出系统吗？", "提示") != DialogResult.OK)
            {
                e.Cancel = true; //取消关闭
                return;
            }

            CommonMethods.DeviceZoneA?.Stop();
            CommonMethods.DeviceZoneB?.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            this.lbl_SysTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (CommonMethods.DeviceZoneA.IsConnected)
            {
                this.lbl_PortStatreA.Text = "串口打开";
                this.lbl_CommPeriodA.Text = CommonMethods.DeviceZoneA.CommPeriod.ToString() + " ms";
            }
            else
            {
                this.lbl_PortStatreA.Text = "串口关闭";
                this.lbl_CommPeriodA.Text = "0 ms";
            }

            if (CommonMethods.DeviceZoneB.IsConnected)
            {
                this.lbl_PortStateB.Text = "串口打开";
                this.lbl_CommPeriodB.Text = CommonMethods.DeviceZoneB.CommPeriod.ToString() + " ms";
            }
            else
            {
                this.lbl_PortStateB.Text = "串口关闭";
                this.lbl_CommPeriodB.Text = "0 ms";
            }

            //执行Action，调用FrmMonitor的UpdateMonitor方法
            this.updateMonitor?.Invoke();
            this.updateTrend?.Invoke();

            //定时存储
            int timeSpan = DateTime.Now.Second - lastTime.Second;
            if (timeSpan == 1 || timeSpan == -59)
            {
                //设备通信状态满足
                var zoneAOk = CommonMethods.DeviceZoneA.IsConnected && CommonMethods.DeviceZoneA.GroupList.Any(g => g.IsOk);
                var zoneBOk = CommonMethods.DeviceZoneB.IsConnected && CommonMethods.DeviceZoneB.GroupList.Any(g => g.IsOk);
                if (zoneAOk || zoneBOk)
                {
                    monitorBLL.AddMonitorData(new MonitorData
                    {
                        InsertTime = DateTime.Now,//.ToString("yyyy-MM-dd HH:mm:ss"),
                        A01Temp = CommonMethods.DeviceZoneA.CurrentValue.TryGetValue("A01温度", out object value) ? value.ToString() : "00:00",
                        A01Humidity = CommonMethods.DeviceZoneA.CurrentValue.TryGetValue("A01湿度", out object value2) ? value2.ToString() : "00:00",
                        A02Temp = CommonMethods.DeviceZoneA.CurrentValue.TryGetValue("A02温度", out object value3) ? value3.ToString() : "00:00",
                        A02Humidity = CommonMethods.DeviceZoneA.CurrentValue.TryGetValue("A02湿度", out object value4) ? value4.ToString() : "00:00",
                        A03Temp = CommonMethods.DeviceZoneA.CurrentValue.TryGetValue("A03温度", out object value5) ? value5.ToString() : "00:00",
                        A03Humidity = CommonMethods.DeviceZoneA.CurrentValue.TryGetValue("A03湿度", out object value6) ? value6.ToString() : "00:00",

                        B01Temp = CommonMethods.DeviceZoneB.CurrentValue.TryGetValue("B01温度", out object value7) ? value7.ToString() : "00:00",
                        B01Humidity = CommonMethods.DeviceZoneB.CurrentValue.TryGetValue("B01湿度", out object value8) ? value8.ToString() : "00:00",
                        B02Temp = CommonMethods.DeviceZoneB.CurrentValue.TryGetValue("B02温度", out object value9) ? value9.ToString() : "00:00",
                        B02Humidity = CommonMethods.DeviceZoneB.CurrentValue.TryGetValue("B02湿度", out object value10) ? value10.ToString() : "00:00",
                        B03Temp = CommonMethods.DeviceZoneB.CurrentValue.TryGetValue("B03温度", out object value11) ? value11.ToString() : "00:00",
                        B03Humidity = CommonMethods.DeviceZoneB.CurrentValue.TryGetValue("B03湿度", out object value12) ? value12.ToString() : "00:00",

                    });
                }

            }
            lastTime = DateTime.Now;
        }



        private bool OpenForm(FormNames openName)
        {
            //先找到，并设置到最前面，然后关闭其他的
            var frm = this.MainPanel.Controls.OfType<Form>().FirstOrDefault(f => f.Text == openName.ToString());
            if (frm == null)
            {
                switch (openName)
                {
                    case FormNames.集中监控:
                        frm = new FrmMonitor();
                        //将FrmMonitor窗体内的UpdateMonitor方法赋值给updateMonitor
                        this.updateMonitor = ((FrmMonitor)frm).UpdateMonitor;
                        break;

                    case FormNames.实时趋势:
                        frm = new FrmTrend();
                        this.updateTrend = ((FrmTrend)frm).UpdateTrend;
                        break;

                    case FormNames.参数配置:
                        frm = new FrmParam(sysInfoPath, sysInfo);
                        break;

                    case FormNames.历史趋势:
                        frm = new FrmHistory();
                        break;

                    case FormNames.报警记录:
                        frm = new FrmAlarm();
                        break;

                    case FormNames.数据报表:
                        frm = new FrmReport();
                        break;

                    case FormNames.用户管理:
                        frm = new FrmUserManager();
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
            return true;
        }

        private void Common_Navi_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (Enum.IsDefined(typeof(FormNames), btn.Text))
                {
                    OpenForm(btn.Text.GetEnumValue<FormNames>());
                    var oldbtn = this.NaviPanel.Controls.OfType<Button>().FirstOrDefault(b => "true".Equals(b.Tag));
                    if (oldbtn != null)
                    {
                        oldbtn.BackColor = Color.FromArgb(43, 50, 120); //未选中
                        oldbtn.Tag = null;
                    }
                    btn.BackColor = Color.FromArgb(1, 106, 189); //选中颜色
                    btn.Tag = "true";
                }
            }
        }
    }
}