using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.s7netplus;

namespace AY.Entity
{
    public class SysInfo
    {
        /// <summary>
        /// CPU类型
        /// </summary>
        public CpuType CpuType{ get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 机架号
        /// </summary>
        public short  Rack { get; set; }
        /// <summary>
        /// 插槽号
        /// </summary>
        public short  Slot { get; set; }
        /// <summary>
        /// 是否开机启动
        /// </summary>
        public bool AutoStart{ get; set; }
        /// <summary>
        /// 无操作息屏时间
        /// </summary>
        public int ScreenTime { get; set; }

        /// <summary>
        /// 自动注销时间
        /// </summary>
        public int LogoffTime { get; set; }

        /// <summary>
        /// 摄像头序号
        /// </summary>
        public int CameraIndex { get; set; }
    }
}
