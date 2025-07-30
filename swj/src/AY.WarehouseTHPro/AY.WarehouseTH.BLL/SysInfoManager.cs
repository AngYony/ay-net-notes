using AY.Utils;
using AY.WarehouseTH.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace AY.WarehouseTH.BLL
{
    public class SysInfoManager
    {
        /// <summary>
        /// 读取系统配置信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static OperateResult<SysInfo> GetSysInfoFromPath(string path)
        {
            try
            {
                string info = IniConfigHelper.ReadIniData("系统配置", "参数配置", "", path);
                if (info.Length == 0)
                {
                    return OperateResult.CreateFailResult<SysInfo>("读取系统配置失败：配置文件中没有参数配置");
                }
                else
                {
                    var result = JsonHelper.DeserializeObject<SysInfo>(info);
                    if (result == null)
                    {
                        return OperateResult.CreateFailResult<SysInfo>("读取系统配置失败：反序列化失败");
                    }
                    return OperateResult.CreateSuccessResult(result);
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<SysInfo>("读取系统配置失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 写入系统配置信息到文件
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static OperateResult SetSysInfoToFile(SysInfo sysInfo, string path)
        {
            try
            {
                string info = JsonHelper.SerializeObject(sysInfo);
                if (info.Length == 0)
                {
                    return OperateResult.CreateFailResult("写入系统配置失败：序列化失败");
                }
                bool result = IniConfigHelper.WriteIniData("系统配置", "参数配置", info, path);
                if (!result)
                {
                    return OperateResult.CreateFailResult("写入系统配置失败：写入文件失败");
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult("写入系统配置失败：" + ex.Message);
            }
        }

    }

}
