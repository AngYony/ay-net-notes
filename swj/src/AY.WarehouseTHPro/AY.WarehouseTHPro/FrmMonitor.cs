using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.WarehouseTHPro
{
    public partial class FrmMonitor : Form
    {
        
        public FrmMonitor()
        {
            InitializeComponent();
            
        }

 
        public void UpdateMonitor()
        {
            foreach (var item in this.Controls.OfType<Monitor>())
            {
                if (item.GroupName.StartsWith("A"))
                {
                    if (CommonMethods.DeviceZoneA.IsConnected)
                    {
                        var group = CommonMethods.DeviceZoneA.GroupList.FirstOrDefault(g => g.GroupName == item.GroupName);
                        if (group != null)
                        {
                            //更新状态
                            item.IsAvaliable = group.IsOk;
                            if (item.IsAvaliable)
                            {
                                item.TempValue = CommonMethods.DeviceZoneA.CurrentValue.ContainsKey(item.GroupName + "温度") ? CommonMethods.DeviceZoneA.CurrentValue[item.GroupName + "温度"].ToString() : "00.00";
                                item.HumidityValue = CommonMethods.DeviceZoneA.CurrentValue.ContainsKey(item.GroupName + "湿度") ? CommonMethods.DeviceZoneA.CurrentValue[item.GroupName + "湿度"].ToString() : "00.00";
                            }
                            else
                            {
                                item.TempValue = "0.0";
                                item.HumidityValue = "0.0";
                            }
                        }
                    }
                }

                if (item.GroupName.StartsWith("B"))
                {
                    if (CommonMethods.DeviceZoneB.IsConnected)
                    {
                        var group = CommonMethods.DeviceZoneB.GroupList.FirstOrDefault(g => g.GroupName == item.GroupName);
                        if (group != null)
                        {
                            //更新状态
                            item.IsAvaliable = group.IsOk;
                            if (item.IsAvaliable)
                            {
                                item.TempValue = CommonMethods.DeviceZoneB.CurrentValue.ContainsKey(item.GroupName + "温度") ? CommonMethods.DeviceZoneB.CurrentValue[item.GroupName + "温度"].ToString() : "00.00";
                                item.HumidityValue = CommonMethods.DeviceZoneB.CurrentValue.ContainsKey(item.GroupName + "湿度") ? CommonMethods.DeviceZoneB.CurrentValue[item.GroupName + "湿度"].ToString() : "00.00";
                            }
                            else
                            {
                                item.TempValue = "0.0";
                                item.HumidityValue = "0.0";
                            }
                        }
                    }
                }
            }
        }
    }
}
