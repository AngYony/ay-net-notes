using AForge.Video.DirectShow;
using AY.BusinessServices;
using AY.Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.s7netplus;

namespace AY.PressurizationStationPro
{
    public partial class FrmParamSet : Form
    {
        private SysInfo sysInfo;
        private SysInfoService infoService;
        private string sysInfoPath;


        public FrmParamSet(SysInfo sysInfo, SysInfoService infoService, string sysInfoPath)
        {
            this.sysInfo = sysInfo;
            this.infoService = infoService;
            this.sysInfoPath = sysInfoPath;
            InitializeComponent();
            this.SetWindowDrag(this.lbl_Exit, this.lbl_Title, this.panel1);
            this.cmb_CpuType.DataSource = Enum.GetNames(typeof(CpuType));

            //获取摄像头列表
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo item in videoDevices)
            {
                this.cmb_Camera.Items.Add(item.Name);
            }   


            if (this.sysInfo != null)
            {
                this.txt_IPAddress.Text = this.sysInfo.IPAddress;
                this.cmb_CpuType.Text = this.sysInfo.CpuType.ToString();
                this.txt_Rack.Text = this.sysInfo.Rack.ToString();
                this.txt_Slot.Text = this.sysInfo.Slot.ToString();

                this.chk_AutoStart.Checked = this.sysInfo.AutoStart;
                this.txt_ScreenTime.Text = this.sysInfo.ScreenTime.ToString();
                this.txt_logoffTime.Text = this.sysInfo.LogoffTime.ToString();
                if (videoDevices.Count > this.sysInfo.CameraIndex)
                {
                    this.cmb_Camera.SelectedIndex = this.sysInfo.CameraIndex;
                }
            }

            ////为了避免为CheckedBox赋值引发CheckedChanged事件，可以通过这种方式避免第一次的事件调用
            //this.chk_AutoStart.CheckedChanged += (sender, e) =>
            //{
               
            //};

        }

        private void btn_PLCSet_Click(object sender, EventArgs e)
        {
            if (this.sysInfo == null)
            {
                this.sysInfo = new SysInfo();
            }
            this.sysInfo.IPAddress = this.txt_IPAddress.Text.Trim();
            this.sysInfo.CpuType = (CpuType)Enum.Parse(typeof(CpuType), this.cmb_CpuType.Text, true);
            this.sysInfo.Rack = short.Parse(this.txt_Rack.Text.Trim());
            this.sysInfo.Slot = short.Parse(this.txt_Slot.Text.Trim());

            if (this.infoService.SetSysInfoToPath(this.sysInfo, this.sysInfoPath))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                new FrmMsgNoAck("通信参数设置失败", "提示").ShowDialog();
            }


            
        }

        private void btn_PLCCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_SysSet_Click(object sender, EventArgs e)
        {
            if (this.sysInfo == null)
            {
                this.sysInfo = new SysInfo();
            }

            this.sysInfo.AutoStart = this.chk_AutoStart.Checked;
            this.sysInfo.ScreenTime = int.Parse(this.txt_ScreenTime.Text.Trim());

            this.sysInfo.LogoffTime = int.Parse(this.txt_logoffTime.Text.Trim());
            this.sysInfo.CameraIndex = int.Parse(this.cmb_Camera.Text.Trim());
            if (this.infoService.SetSysInfoToPath(this.sysInfo, this.sysInfoPath))
            {
                AutoStart(this.sysInfo.AutoStart);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                new FrmMsgNoAck("系统参数设置失败", "提示").ShowDialog();
            }
        }

        private void btn_SysCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }


        #region 开机启动
        /// <summary>  
        /// 修改程序在注册表中的键值  
        /// </summary>  
        /// <param name="isAuto">true:开机启动,false:不开机自启</param> 
        private void AutoStart(bool isAuto = true)
        {
            if (isAuto == true)
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.SetValue("AyPressurizationStationPro", System.Windows.Forms.Application.ExecutablePath);
                R_run.Close();
                R_local.Close();
            }
            else
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.DeleteValue("AyPressurizationStationPro", false);
                R_run.Close();
                R_local.Close();
            }
        }
        #endregion
    }
}
