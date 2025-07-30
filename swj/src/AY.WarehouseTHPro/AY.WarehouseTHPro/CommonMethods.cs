using AY.NodeSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.WarehouseTHPro
{
     
    public class CommonMethods
    {
        //这里为了省事，直接定义了全局静态变量来存储设备信息
        //在实际项目中，建议使用依赖注入或其他方式来管理设备实例，以便于测试和维护。

        /// <summary>
        /// 
        /// </summary>
        public static ModbusRTUDevice DeviceZoneA { get; set; } = new ModbusRTUDevice();
        public static ModbusRTUDevice DeviceZoneB { get; set; } = new ModbusRTUDevice();
    }
}
