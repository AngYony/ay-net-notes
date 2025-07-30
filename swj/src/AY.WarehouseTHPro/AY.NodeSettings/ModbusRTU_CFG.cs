using AY.Utils;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace AY.NodeSettings
{
    /// <summary>
    /// ModbusRTU配置解析类
    /// </summary>
    public class ModbusRTU_CFG
    {
        public static OperateResult<List<ModbusRTUDevice>> LoadDevice(string path)
        {
            var result =new List<ModbusRTUDevice>();
            var fileNames = Directory.GetFiles(path, "*.xlsx");
            foreach (var fn in fileNames)
            {
                ModbusRTUDevice device = GetDevice(fn);
                if (device == null)
                {
                    return OperateResult.CreateFailResult<List<ModbusRTUDevice>>("解析文件错误：" + fn);
                }

                var sheetNames = MiniExcel.GetSheetNames(fn);
                foreach (var sn in sheetNames)
                {
                    //将sheetName的值转换为一个Group对象
                    ModbusRTUGroup group = GetGroup(sn);
                    if (group == null)
                    {
                        return OperateResult.CreateFailResult<List<ModbusRTUDevice>>("解析Sheet名称错误：" + fn);
                    }

                    group.VarList = MiniExcel.Query<ModbusRTUVariable>(fn, sn).ToList();
                    device.GroupList.Add(group);
                }
                result.Add(device);

            }
            return OperateResult.CreateSuccessResult(result);
        }



        private static ModbusRTUDevice GetDevice(string fileName)
        {
            string[] info = Path.GetFileNameWithoutExtension(fileName).Split('_');
            if (info.Length != 6)
            {
                return null;
            }

            return new ModbusRTUDevice
            {
                DeviceName = info[0],
                PortName = info[1],
                BaudRate = Convert.ToInt32(info[2]),
                Parity = info[3].GetEnumValue<Parity>(),
                DataBits = Convert.ToInt32(info[4]),
                StopBits = info[5].GetEnumValue<StopBits>(),
                DataFormat = DataFormat.ABCD
            };
        }

        private static ModbusRTUGroup GetGroup(string sheetName)
        {
            string[] info = sheetName.Split('_');
            if (info.Length != 5) { return null; }
            return new ModbusRTUGroup
            {
                GroupName = info[0],
                StoreArea = info[1].GetEnumValue<ModbusStore>(),
                GroupId = Convert.ToByte(info[2]),
                Start = Convert.ToUInt16(info[3]),
                Length = Convert.ToUInt16(info[4]),
            };

        }

    }
}
