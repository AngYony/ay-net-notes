using Ay.Utils;
using AY.BusinessServices;
using AY.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.ControlLib;
using xbd.s7netplus;
using Timer = System.Windows.Forms.Timer;


namespace AY.PressurizationStationPro
{
    public partial class FrmMain : Form
    {
        private string sysInfoPath = Path.Combine(Application.StartupPath, "SysInfo.ini");
        private SysInfoService infoService = new SysInfoService();
        private SysInfo sysInfo = null;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private PlcDataService plcDataService = new PlcDataService();
        private Timer timer = new Timer(); //更新界面时间显示
        private WindowsMessageFilter windowsMessageFilter = null;
        private CameraHelper cameraHelper = null;
        private HistoryDataService historyDataService = new HistoryDataService();
        private DateTime lastHistoryTime = DateTime.Now;

        public FrmMain()
        {
            InitializeComponent();
            this.SetWindowDrag(this.btnExit, this.TopPanel, this.label1);
            this.timer.Interval = 1000; //每秒更新一次
            this.timer.Tick += (sender, e) =>
            {
                this.lbl_Time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " "
                + new CultureInfo("zh-CN").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

                this.led_PLCState.State = plcDataService.IsConnected;

                //if (sysInfo != null && sysInfo.ScreenTime > 0 && DateTime.Now - Program.ProgramUseStartTime > TimeSpan.FromSeconds(sysInfo.ScreenTime))
                //{
                //    //执行锁屏
                //    LockWorkStation();
                //}

                if (Program.CurrentUser == null || (sysInfo != null && Program.CurrentUser != null
                && sysInfo.LogoffTime > 0 && DateTime.Now - Program.CurrentUser.LoginTime > TimeSpan.FromSeconds(sysInfo.LogoffTime)))
                {
                    //执行注销
                    Program.CurrentUser = null;
                    btn_userLogin.Text = "用户登录";
                }
            };
            this.timer.Start();

            this.Load += (sender, e) =>
            {
                this.sysInfo = infoService.GetSysInfoFromPath(sysInfoPath);
                if (this.sysInfo == null)
                {
                    new FrmMsgNoAck("系统信息读取失败，请检查配置文件 SysInfo.ini 是否存在或格式是否正确。").ShowDialog();
                    return;
                }
                Task.Run(PLCCommunication);
                if (sysInfo != null && sysInfo.ScreenTime > 0)
                {
                    windowsMessageFilter = new WindowsMessageFilter();
                    //如果设置了熄屏时间，则开启消息过滤器
                    Application.AddMessageFilter(windowsMessageFilter);
                }

                ////采集摄像头
                //if (sysInfo.CameraIndex >= 0)
                //{
                //    cameraHelper = new CameraHelper(sysInfo.CameraIndex, this.vsp_panel);
                //    cameraHelper.StartCamera();
                //}


            };
            this.FormClosing += (sender, e) =>
            {
                //cameraHelper?.StopCamera();
                cts?.Cancel();

            };
        }



        /// <summary>
        /// 断续重连
        /// </summary>
        private void PLCCommunication()
        {
            while (!cts.IsCancellationRequested)
            {
                if (!plcDataService.IsConnected)
                {
                    //如果是第一次扫描，直接连接，如果不是第一次扫描，先断开再连接
                    if (!plcDataService.IsFirstScan)
                    {
                        Thread.Sleep(3000);
                        plcDataService.DisConnect();
                    }
                    //连接
                    var result = plcDataService.Connect(this.sysInfo);
                    plcDataService.IsConnected = result.IsSuccess;
                }

                if (plcDataService.IsConnected)
                {
                    var data = plcDataService.ReadPlcData();
                    if (data.IsSuccess)
                    {
                        plcDataService.ErrorTimes = 0; //重置错误次数
                        //更新存储
                        this.UpdateUIData(data.Content, plcDataService.IsFirstScan);
                        plcDataService.IsFirstScan = false;
                        int timeSpan = DateTime.Now.Second - lastHistoryTime.Second;
                        if (timeSpan == 1 || timeSpan == -59)
                        {
                            //每秒记录一次历史数据
                            this.AddHistoryData(data.Content);
                        }
                        lastHistoryTime = DateTime.Now;
                        Thread.Sleep(500);
                    }
                    else
                    {
                        plcDataService.ErrorTimes++;
                    }
                }

                if (plcDataService.ErrorTimes >= plcDataService.AllowErrorTimes)
                {
                    //todo：处理连接失败的情况
                    plcDataService.IsConnected = false;
                }

            }
        }

        private void AddHistoryData(PlcData data)
        {
            historyDataService.AddHisotryData(new HistoryData
            {
                InsertTime = DateTime.Now,
                PressureIn = data.PressureIn.ToString("f2"),
                PressureOut = data.PressureOut.ToString("f2"),
                LevelTank1 = data.LevelTank1.ToString("f2"),
                LevelTank2 = data.LevelTank2.ToString("f2"),
                PressureTank1 = data.PressureTank1.ToString("f2"),
                PressureTank2 = data.PressureTank2.ToString("f2"),
                TempIn1 = data.TempIn1.ToString("f2"),
                TempIn2 = data.TempIn2.ToString("f2"),
                TempOut = data.TempOut.ToString("f2"),
                PressureTankOut = data.PressureTankOut.ToString("f2")
            });
        }

        private void UpdateUIData(PlcData plcData, bool firstScan)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    //委托处理
                    this.Invoke(new Action<PlcData, bool>(UpdateUIData), plcData, firstScan);

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                if (firstScan)
                {
                    this.toggle_Pump1.Checked = plcData.InPump1State;
                    this.toggle_Pump2.Checked = plcData.InPump2State;
                }
                //左侧仪表
                this.lbl_PressureIn.Text = plcData.PressureIn.ToString("f2") + " bar";
                this.lbl_PressureOut.Text = plcData.PressureOut.ToString("f2") + " bar";
                this.meter_PressureIn.Value = plcData.PressureIn;
                this.meter_PressureOut.Value = plcData.PressureOut;

                //底侧仪表
                this.ms_TempIn1.ParamValue = plcData.TempIn1;
                this.ms_TempIn2.ParamValue = plcData.TempIn2;
                this.ms_TempOut.ParamValue = plcData.TempOut;
                this.ms_PressureTank1.ParamValue = plcData.PressureTank1;
                this.ms_PressureTank2.ParamValue = plcData.PressureTank2;
                this.ms_PressureTankOut.ParamValue = plcData.PressureTankOut;

                //系统状态
                this.led_RunState.State = plcData.SysRunState;
                this.led_SysAlarmState.State = !plcData.SysAlarmState;

                //系统参数
                this.lbl_PressureTank1.Text = plcData.PressureTank1.ToString("f2");
                this.lbl_LevelTank1.Text = plcData.LevelTank1.ToString("f2");
                this.lbl_PressureTank2.Text = plcData.PressureTank2.ToString("f2");
                this.lbl_LevelTank2.Text = plcData.LevelTank2.ToString("f2");
                this.lbl_PressureTankOut.Text = plcData.PressureTankOut.ToString("f2");

                //流程图数据
                this.lbl_TempIn1.Text = plcData.TempIn1.ToString("f2");
                this.lbl_TempIn2.Text = plcData.TempIn2.ToString("f2");
                this.lbl_TempOut.Text = plcData.TempOut.ToString("f2");

                this.pump_In1.IsRun = plcData.InPump1State;
                this.pump_In2.IsRun = plcData.InPump2State;

                this.valve_In.State = plcData.ValveInState;
                this.valve_Out.State = plcData.ValveOutState;

                this.motor_Pump1.PumpState = plcData.CirclePump1State ? xbd.ControlLib.PumpState.运行 : xbd.ControlLib.PumpState.停止;
                this.motor_Pump2.PumpState = plcData.CirclePump2State ? xbd.ControlLib.PumpState.运行 : xbd.ControlLib.PumpState.停止;

                //量程 2米
                this.wave_Tank1.Value = Convert.ToInt32(plcData.LevelTank1 / 2 * 100); //转换为百分比
                this.wave_Tank2.Value = Convert.ToInt32(plcData.LevelTank2 / 2 * 100); //转换为百分比

                this.lbl_PreTankOut.Text = plcData.PressureTankOut.ToString("f2");

                this.btn_Pump1.Text = plcData.CirclePump1State ? "停止" : "启动";
                this.btn_Pump2.Text = plcData.CirclePump2State ? "停止" : "启动";
            }
        }

        private void btn_ParamSet_Click(object sender, EventArgs e)
        {
            new FrmParamSet(this.sysInfo, this.infoService, this.sysInfoPath).ShowDialog();
        }

        private void btn_Pump1_Click(object sender, EventArgs e)
        {
            plcDataService.CirclePump1Control(this.btn_Pump1.Text == "启动");
        }

        private void btn_Pump2_Click(object sender, EventArgs e)
        {
            plcDataService.CirclePump2Control(this.btn_Pump2.Text == "启动");
        }

        private void toggle_Pump1_CheckedChanged(object sender, EventArgs e)
        {
            if (!plcDataService.InPump1Control(toggle_Pump1.Checked))
            {
                //由于每次为Checked赋值都会触发CheckedChanged事件，因此需要反复取消和订阅该事件来避免死循环
                toggle_Pump1.CheckedChanged -= toggle_Pump1_CheckedChanged;
                toggle_Pump1.Checked = !toggle_Pump1.Checked; //恢复原状态
                toggle_Pump1.CheckedChanged += toggle_Pump1_CheckedChanged;
            }
        }

        private void toggle_Pump2_CheckedChanged(object sender, EventArgs e)
        {
            if (!plcDataService.InPump2Control(toggle_Pump2.Checked))
            {
                //由于每次为Checked赋值都会触发CheckedChanged事件，因此需要反复取消和订阅该事件来避免死循环
                toggle_Pump2.CheckedChanged -= toggle_Pump2_CheckedChanged;
                toggle_Pump2.Checked = !toggle_Pump2.Checked; //恢复原状态
                toggle_Pump2.CheckedChanged += toggle_Pump2_CheckedChanged;
            }
        }

        private void btn_SysReset_Click(object sender, EventArgs e)
        {
            plcDataService.SysReset();
        }

        private void CommonValve_DoubleClick(object sender, EventArgs e)
        {
            if (sender is xbdValve valve)
            {
                FrmValueControl frmValueControl = new FrmValueControl(valve.ValveName, valve.State, plcDataService);
                frmValueControl.ShowDialog();
            }
        }


        [DllImport("user32")]
        public static extern bool LockWorkStation();

        private void btn_userLogin_Click(object sender, EventArgs e)
        {
            if (btn_userLogin.Text == "退出登录")
            {
                //退出登录
                Program.CurrentUser = null;
                lbl_LoginName.Text = "未登录";
                btn_userLogin.Text = "用户登录";
                return;
            }

            DialogResult dialogResult = new FrmLogin().ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                lbl_LoginName.Text = Program.CurrentUser.LoginName;
                btn_userLogin.Text = "退出登录";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FrmHistory().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FrmReport().ShowDialog();
        }
    }
}