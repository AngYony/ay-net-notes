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
    /// ModbusRTU���ý�����
    /// </summary>
    public class ModbusRTUCFG
    {
        public static OperateResult<List<ModbusRTUDevice>> LoadDevice(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            List<ModbusRTUDevice> devices = new List<ModbusRTUDevice>();

            try
            {
                foreach (var file in directoryInfo.GetFiles("*.xlsx"))
                {
                    //�������ļ�ת�ɵ�������
                    ModbusRTUDevice device = GetDevice(file.Name.Replace(".xlsx", ""));

                    //�ļ�������֤ͨ��
                    if (device != null)
                    {
                        List<string> sheets = MiniExcel.GetSheetNames(file.FullName);

                        foreach (var sheet in sheets)
                        {
                            //��ÿ��sheetת����һ��Group����
                            ModbusRTUGroup group = GetGroup(sheet);

                            if (group != null)
                            {
                                try
                                {
                                    group.VarList = MiniExcel.Query<ModbusRTUVariable>(file.FullName, sheet).ToList();

                                    device.GroupList.Add(group);
                                }
                                catch (Exception ex)
                                {
                                    return OperateResult.CreateFailResult<List<ModbusRTUDevice>>("������������" + ex.Message);
                                }
                            }
                            else
                            {
                                return OperateResult.CreateFailResult<List<ModbusRTUDevice>>("����Sheet����" + sheet);
                            }
                        }

                        devices.Add(device);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<List<ModbusRTUDevice>>("�����ļ�����" + file.Name);
                    }


                }
                return OperateResult.CreateSuccessResult(devices);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<List<ModbusRTUDevice>>("�����ļ����ϣ�" + ex.Message);
            }
        }


        private static ModbusRTUDevice GetDevice(string filename)
        {
            if (filename.Contains("_"))
            {
                string[] info = filename.Split('_');

                if (info.Length == 6)
                {
                    return new ModbusRTUDevice()
                    {
                        DeviceName = info[0],
                        PortName = info[1],
                        BaudRate = Convert.ToInt32(info[2]),
                        Parity = (Parity)Enum.Parse(typeof(Parity), info[3], true),
                        DataBits = Convert.ToInt32(info[4]),
                        StopBits = (StopBits)Enum.Parse(typeof(StopBits), info[5], true),
                        DataFormat = DataFormat.ABCD
                    };
                }
            }
            return null;
        }

        private static ModbusRTUGroup GetGroup(string sheetname)
        {
            if (sheetname.Contains("_"))
            {
                string[] info = sheetname.Split('_');

                if (info.Length == 5)
                {
                    return new ModbusRTUGroup()
                    {
                        GroupName = info[0],
                        StoreArea = (ModbusStore)Enum.Parse(typeof(ModbusStore), info[1], true),
                        GroupId = Convert.ToByte(info[2]),
                        Start = Convert.ToUInt16(info[3]),
                        Length = Convert.ToUInt16(info[4])
                    };
                }
            }
            return null;
        }
    }
}
