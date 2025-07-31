using AY.CommunicationLib;
using AY.CommunicationLib.DataConvert;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    /// <summary>
    /// ModbusTCP配置解析类
    /// </summary>
    public class ModbusTCPCFG
    {
        public static OperateResult<List<ModbusTCPDevice>> LoadDevice(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            List<ModbusTCPDevice> devices = new List<ModbusTCPDevice>();

            try
            {
                foreach (var file in directoryInfo.GetFiles("*.xlsx"))
                {
                    //将单个文件转成单个对象
                    ModbusTCPDevice device = GetDevice(file.Name.Replace(".xlsx", ""));

                    //文件名称验证通过
                    if (device != null)
                    {
                        List<string> sheets = MiniExcel.GetSheetNames(file.FullName);

                        foreach (var sheet in sheets)
                        {
                            //将每个sheet转换成一个Group对象
                            ModbusTCPGroup group = GetGroup(sheet);

                            if (group != null)
                            {
                                try
                                {
                                    group.VarList = MiniExcel.Query<ModbusTCPVariable>(file.FullName, sheet).ToList();

                                    device.GroupList.Add(group);
                                }
                                catch (Exception ex)
                                {
                                    return OperateResult.CreateFailResult<List<ModbusTCPDevice>>("解析变量错误：" + ex.Message);
                                }
                            }
                            else
                            {
                                return OperateResult.CreateFailResult<List<ModbusTCPDevice>>("解析Sheet错误：" + sheet);
                            }
                        }

                        devices.Add(device);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<List<ModbusTCPDevice>>("解析文件错误：" + file.Name);
                    }


                }
                return OperateResult.CreateSuccessResult(devices);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<List<ModbusTCPDevice>>("解析文件故障：" + ex.Message);
            }
        }


        private static ModbusTCPDevice GetDevice(string filename)
        {
            if (filename.Contains("_"))
            {
                string[] info = filename.Split('_');

                if (info.Length == 3)
                {
                    return new ModbusTCPDevice()
                    {
                        DeviceName = info[0],
                        IPAddress = info[1],
                        Port = Convert.ToInt32(info[2]),
                        DataFormat = EndianType.ABCD
                    };
                }
            }
            return null;
        }

        private static ModbusTCPGroup GetGroup(string sheetname)
        {
            if (sheetname.Contains("_"))
            {
                string[] info = sheetname.Split('_');

                if (info.Length == 4)
                {
                    //sheetName:通信组1_保持型寄存器_0_0
                    return new ModbusTCPGroup()
                    {
                        GroupName = info[0],
                        StoreArea = (ModbusStore)Enum.Parse(typeof(ModbusStore), info[1], true),
                        
                        Start = Convert.ToUInt16(info[2]),
                        Length = Convert.ToUInt16(info[3])
                    };
                }
            }
            return null;
        }
    }
}
