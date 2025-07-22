using Ay.Utils;
using AY.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.s7netplus;

namespace AY.BusinessServices
{
    public class SysInfoService
    {
        public SysInfo GetSysInfoFromPath(string path)
        {
            try
            {

                SysInfo sysinfo = new SysInfo
                {
                    IPAddress = IniConfigHelper.ReadIniData("通信参数", "IP地址", "127.0.0.1", path),
                    CpuType = (CpuType)Enum.Parse(typeof(CpuType), IniConfigHelper.ReadIniData("通信参数", "CPU类型", "S7200Smart", path), true),
                    Rack = short.Parse(IniConfigHelper.ReadIniData("通信参数", "机架号", "0", path)),
                    Slot = short.Parse(IniConfigHelper.ReadIniData("通信参数", "插槽号", "0", path)),
                    AutoStart = IniConfigHelper.ReadIniData("系统参数", "开机启动", "1", path) == "1",
                    ScreenTime = int.Parse(IniConfigHelper.ReadIniData("系统参数", "熄屏时间", "0", path)),
                    LogoffTime = int.Parse(IniConfigHelper.ReadIniData("系统参数", "注销时间", "0", path)),
                    CameraIndex = int.Parse(IniConfigHelper.ReadIniData("系统参数", "摄像头序号", "0", path))
                };
                return sysinfo;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SetSysInfoToPath(SysInfo sysinfo, string path)
        {
            bool result = true;
            try
            {
                result &= IniConfigHelper.WriteIniData("通信参数", "IP地址", sysinfo.IPAddress, path);
                result &= IniConfigHelper.WriteIniData("通信参数", "CPU类型", sysinfo.CpuType.ToString(), path);
                result &= IniConfigHelper.WriteIniData("通信参数", "机架号", sysinfo.Rack.ToString(), path);
                result &= IniConfigHelper.WriteIniData("通信参数", "插槽号", sysinfo.Slot.ToString(), path);
                result &= IniConfigHelper.WriteIniData("系统参数", "开机启动", sysinfo.AutoStart ? "1" : "0", path);
                result &= IniConfigHelper.WriteIniData("系统参数", "熄屏时间", sysinfo.ScreenTime.ToString(), path);
                result &= IniConfigHelper.WriteIniData("系统参数", "注销时间", sysinfo.LogoffTime.ToString(), path);
                result &= IniConfigHelper.WriteIniData("系统参数", "摄像头序号", sysinfo.CameraIndex.ToString(), path);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
