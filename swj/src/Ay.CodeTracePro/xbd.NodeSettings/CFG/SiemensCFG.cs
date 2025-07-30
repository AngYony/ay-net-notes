using AY.CommunicationLib;
using AY.CommunicationLib.DataConvert;
using AY.CommunicationLib.PLC.Siemens;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace xbd.NodeSettings
{
    public class SiemensCFG
    {
        public static OperateResult<List<SiemensDevice>> LoadDevice(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            List<SiemensDevice> devices = new List<SiemensDevice>();

            try
            {
                foreach (var file in directoryInfo.GetFiles("*.xlsx"))
                {
                    //将单个文件转成单个对象
                    SiemensDevice device = GetDevice(file.Name.Replace(".xlsx", ""));

                    //文件名称验证通过
                    if (device != null)
                    {
                        List<string> sheets = MiniExcel.GetSheetNames(file.FullName);

                        foreach (var sheet in sheets)
                        {
                            //将每个sheet转换成一个Group对象
                            SiemensGroup group = GetGroup(sheet);

                            if (group != null)
                            {
                                try
                                {
                                    group.VarList = MiniExcel.Query<SiemensVariable>(file.FullName, sheet).ToList();
                                    //计算出变量的地址
                                    foreach (var item in group.VarList)
                                    {
                                        item.VarAddress = GetSiemensAddress(group, item);

                                    }


                                    device.GroupList.Add(group);
                                }
                                catch (Exception ex)
                                {
                                    return OperateResult.CreateFailResult<List<SiemensDevice>>("解析变量错误：" + ex.Message);
                                }
                            }
                            else
                            {
                                return OperateResult.CreateFailResult<List<SiemensDevice>>("解析Sheet错误：" + sheet);
                            }
                        }

                        devices.Add(device);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<List<SiemensDevice>>("解析文件错误：" + file.Name);
                    }


                }
                return OperateResult.CreateSuccessResult(devices);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<List<SiemensDevice>>("解析文件故障：" + ex.Message);
            }
        }


        private static SiemensDevice GetDevice(string filename)
        {
            if (filename.Contains("_"))
            {
                string[] info = filename.Split('_');

                //设备名称_CPU类型_IP地址_端口号_机架号_插槽号
                if (info.Length == 6)
                {
                    //机台1_S71200_192.168.0.1_102_0_0
                    return new SiemensDevice()
                    {
                        DeviceName = info[0],
                        CpuType = (CpuType)Enum.Parse(typeof(CpuType), info[1], true),
                        IPAddress = info[2],
                        Port = Convert.ToInt32(info[3]),
                        Rack = Convert.ToInt16(info[4]),
                        Slot = Convert.ToInt16(info[5])
                    };
                }
            }
            return null;
        }

        private static SiemensGroup GetGroup(string sheetname)
        {
            if (sheetname.Contains("_"))
            {
                string[] info = sheetname.Split('_');

                if (info.Length == 5)
                {
                    //sheetName:通信组1_存储区_DB号_0_0
                    return new SiemensGroup()
                    {
                        GroupName = info[0],
                        StoreArea = (StoreType)Enum.Parse(typeof(StoreType), info[1], true),
                        DBNo = Convert.ToInt32(info[2]),
                        Start = Convert.ToUInt16(info[3]),
                        Count = Convert.ToUInt16(info[4]),
                    };
                }
            }
            return null;
        }

        private static string GetSiemensAddress(SiemensGroup gp, SiemensVariable variable)
        {
            string address = string.Empty;
            switch (gp.StoreArea)
            {
                case StoreType.Input:
                case StoreType.Output:
                case StoreType.Memory:
                    address = GetStoreAreaName(gp.StoreArea);
                    switch (variable.DataType)
                    {
                        case DataType.Bool:
                            address += variable.Address;
                            break;
                        case DataType.Byte:
                            address += "B" + variable.Address;
                            break;
                        case DataType.Short:
                        case DataType.UShort:
                            address += "W" + variable.Address;
                            break;
                        case DataType.Int:
                        case DataType.UInt:
                        case DataType.Float:
                            address += "D" + variable.Address;
                            break;
                        default:
                            break;
                    }
                    break;
                case StoreType.DataBlock:
                    address = GetStoreAreaName(gp.StoreArea) + gp.DBNo + ".";
                    switch (variable.DataType)
                    {
                        case DataType.Bool:
                            address += "DBX" + variable.Address;
                            break;
                        case DataType.Byte:
                            address += "DBB" + variable.Address;
                            break;
                        case DataType.Short:
                        case DataType.UShort:
                            address += "DBW" + variable.Address;
                            break;
                        case DataType.Int:
                        case DataType.UInt:
                        case DataType.Float:
                            address += "DBD" + variable.Address;
                            break;

                        case DataType.Double:
                        case DataType.Long:
                        case DataType.ULong:
                            address += "DBR" + variable.Address;
                            break;

                        case DataType.String:
                            address += "DBS" + variable.Address;
                            break;
                        
                        default:
                            break;
                    }
                    break;
                    //Timer 和 Counter不建议使用
                case StoreType.Timer:
                case StoreType.Counter:
                    address = GetStoreAreaName(gp.StoreArea);
                    switch(variable.DataType)
                    {
                        case DataType.Short:
                        case DataType.UShort:
                            address += variable.Address;
                            break;
                    }
                    break;
                default:
                    address = string.Empty;
                    break;
            }

            return address;
        }
        private static string GetStoreAreaName(StoreType storeType)
        {
            switch (storeType)
            {
                case StoreType.Input:
                    return "I";
                case StoreType.Output:
                    return "O";
                case StoreType.Memory:
                    return "M";
                case StoreType.DataBlock:
                    return "DB";
                case StoreType.Timer:
                    return "T";
                case StoreType.Counter:
                    return "C";
                default:
                    return string.Empty;

            }
        }

    }
}
