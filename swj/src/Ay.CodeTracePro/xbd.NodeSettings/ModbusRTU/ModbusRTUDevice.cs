

using AY.CommunicationLib;
using AY.CommunicationLib.DataConvert;
using AY.CommunicationLib.Modbus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace xbd.NodeSettings
{
    /// <summary>
    /// ModbusRTUDevice设备
    /// </summary>
    public class ModbusRTUDevice : DeviceBase
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }

        /// <summary>
        /// 通信组集合
        /// </summary>
        public List<ModbusRTUGroup> GroupList { get; set; } = new List<ModbusRTUGroup>();

        /// <summary>
        /// 通信对象
        /// </summary>
        private ModbusRTU modbus = new ModbusRTU();

        /// <summary>
        /// 变量字典集合
        /// </summary>
        public Dictionary<string, ModbusRTUVariable> VariableDicList = new Dictionary<string, ModbusRTUVariable>();

        /// <summary>
        /// 是否使用策略模式
        /// </summary>
        public bool OfflineCheckPolicy { get; set; } = true;

        /// <summary>
        /// 检测循环次数设置
        /// </summary>
        public int CheckTimesSet { get; set; } = 100;

        //循环次数
        private int cycleTimers = 0;

        /// <summary>
        /// 初始化变量字典集合
        /// </summary>
        public void Init()
        {
            foreach (var gp in this.GroupList)
            {
                foreach (var variable in gp.VarList)
                {
                    if (VariableDicList.ContainsKey(variable.VarName))
                    {
                        variable.SlaveId = gp.GroupId;
                        VariableDicList[variable.VarName] = variable;
                    }
                    else
                    {
                        variable.SlaveId = gp.GroupId;
                        VariableDicList.Add(variable.VarName, variable);
                    }
                }
            }
        }

        /// <summary>
        /// 开启多线程
        /// </summary>
        public void Start()
        {
            Init();

            Cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                GetModbusRTUValue();
            }, Cts.Token);
        }

        /// <summary>
        /// 多线程方法循环读取数据
        /// </summary>
        private void GetModbusRTUValue()
        {
            while (!Cts.IsCancellationRequested)
            {
                //如果连接成功
                if (IsConnected)
                {
                    this.StopWatch = Stopwatch.StartNew();
                    if (this.OfflineCheckPolicy == false)
                    {
                        //循环读取
                        foreach (var gp in this.GroupList)
                        {
                            gp.IsOK = GetGroupValue(gp);
                        }
                    }
                    else
                    {
                        //获取所有的OK组
                        List<ModbusRTUGroup> OKGroupList = this.GroupList.Where(c => c.IsOK).ToList();
                        //循环读取OK组
                        foreach (var gp in OKGroupList)
                        {
                            gp.IsOK = GetGroupValue(gp);
                        }
                        cycleTimers++;

                        if (cycleTimers>=CheckTimesSet)
                        {
                            //获取所有的NG组
                            List<ModbusRTUGroup> NGGroupList = this.GroupList.Where(c => c.IsOK==false).ToList();
                            //循环读取OK组
                            foreach (var gp in NGGroupList)
                            {
                                gp.IsOK = GetGroupValue(gp);
                            }
                            cycleTimers = 0;
                        }
                    }

                    //如果所有的组都读取失败，并且端口号不存在
                    if (this.GroupList.Where(c => c.IsOK == false).Count() == this.GroupList.Count)
                    {
                        if (!Common.GetPortNames().Contains(this.PortName))
                        {
                            this.IsConnected = false;
                        }
                    }
                    this.CommPeriod = this.StopWatch.ElapsedMilliseconds;
                }
                else
                {
                    //重连延时
                    if (!FirstConnectSign)
                    {
                        Thread.Sleep(this.ReConnectTime);
                    }

                    this.IsConnected = this.modbus.Open(this.PortName, this.BaudRate, this.Parity, this.DataBits, this.StopBits).IsSuccess;

                    //复位重连标志位
                    if (FirstConnectSign)
                    {
                        FirstConnectSign = false;
                    }
                }
            }
        }

        /// <summary>
        /// 单个通信组读取
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        private bool GetGroupValue(ModbusRTUGroup gp)
        {
            for (int i = 0; i < gp.ReadTimes; i++)
            {
                OperateResult<bool[]> cResult = OperateResult.CreateFailResult<bool[]>("");
                OperateResult<byte[]> rResult = OperateResult.CreateFailResult<byte[]>("");

                if (gp.StoreArea == ModbusStore.输入线圈 || gp.StoreArea == ModbusStore.输出线圈)
                {
                    switch (gp.StoreArea)
                    {
                        case ModbusStore.输出线圈:
                            cResult = modbus.ReadCoils(gp.Start, gp.Length, gp.GroupId);
                            break;
                        case ModbusStore.输入线圈:
                            cResult = modbus.ReadInputs(gp.Start, gp.Length, gp.GroupId);
                            break;
                        default:
                            break;
                    }

                    //长度的验证
                    if (cResult.IsSuccess && cResult.Content.Length == gp.Length)
                    {
                        //变量解析
                        foreach (var variable in gp.VarList)
                        {
                            var add = AnalysisAddress(variable.Address);

                            if (add.IsSuccess == false)
                            {
                                continue;
                            }

                            int start = add.Content1 - gp.Start;

                            switch (variable.DataType)
                            {
                                case DataType.Bool:
                                    variable.VarValue = BitLib.GetBitFromBitArray(cResult.Content, start);
                                    break;
                                default:
                                    break;
                            }

                            //更新变量
                            this.UpdateVariable(variable);
                        }

                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    switch (gp.StoreArea)
                    {
                        case ModbusStore.输入寄存器:
                            rResult = modbus.ReadInputRegisters(gp.Start, gp.Length, gp.GroupId);
                            break;
                        case ModbusStore.保持型寄存器:
                            rResult = modbus.ReadHoldingRegisters(gp.Start, gp.Length, gp.GroupId);
                            break;
                        default:
                            break;
                    }

                    //读取成功
                    if (rResult.IsSuccess && rResult.Content.Length == gp.Length * 2)
                    {
                        foreach (var variable in gp.VarList)
                        {
                            var add = AnalysisAddress(variable.Address);

                            if (add.IsSuccess == false)
                            {
                                continue;
                            }


                            //0 100   200      10 => 20
                            int start = add.Content1 - gp.Start;
                            int offsetOrLength = add.Content2;

                            start *= 2;

                            switch (variable.DataType)
                            {
                                case DataType.Bool:
                                    variable.VarValue = BitLib.GetBitFrom2BytesArray(rResult.Content, start, offsetOrLength, DataFormat == EndianType.BADC || DataFormat == EndianType.DCBA);
                                    break;
                                case DataType.Short:
                                    variable.VarValue = ShortLib.GetShortFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.UShort:
                                    variable.VarValue = UShortLib.GetUShortFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Int:
                                    variable.VarValue = IntLib.GetIntFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.UInt:
                                    variable.VarValue = UIntLib.GetUIntFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Float:
                                    variable.VarValue = FloatLib.GetFloatFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Double:
                                    variable.VarValue = DoubleLib.GetDoubleFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Long:
                                    variable.VarValue = LongLib.GetLongFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.ULong:
                                    variable.VarValue = ULongLib.GetULongFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.String:
                                    //10.5
                                    byte[] bytes = ByteArrayLib.GetByteArrayFromByteArray(rResult.Content, start, offsetOrLength * 2);

                                    //小端处理
                                    if (this.DataFormat == EndianType.BADC || this.DataFormat == EndianType.DCBA)
                                    {
                                        variable.VarValue = StringLib.GetStringFromByteArrayByEncoding(bytes, 0, bytes.Length, Encoding.ASCII).Replace("\0", "");
                                    }
                                    else
                                    {
                                        //1 2 3 4    2 1 4 3
                                        List<byte> data = new List<byte>();

                                        for (int j = 0; j < bytes.Length; j += 2)
                                        {
                                            data.Add(bytes[j + 1]);
                                            data.Add(bytes[j]);
                                        }
                                        variable.VarValue = StringLib.GetStringFromByteArrayByEncoding(data.ToArray(), 0, data.Count, Encoding.ASCII).Replace("\0", "");
                                    }
                                    break;
                                case DataType.ByteArray:
                                    variable.VarValue = ByteArrayLib.GetByteArrayFromByteArray(rResult.Content, start, offsetOrLength * 2);
                                    break;
                                default:
                                    break;
                            }

                            //先做线性转换
                            variable.VarValue = MigrationLib.GetMigrationValue(variable.VarValue, variable.Scale, variable.Offset).Content;

                            this.UpdateVariable(variable);
                        }

                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 停止多线程
        /// </summary>
        public void Stop()
        {
            Cts?.Cancel();

            if (IsConnected)
            {
                modbus.Close();
            }
        }

        /// <summary>
        /// 地址解析
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private OperateResult<ushort, ushort> AnalysisAddress(string address)
        {
            ushort start = 0;
            ushort offsetOrLength = 0;

            if (address.Contains('.'))
            {
                string[] result = address.Split('.');

                if (result.Length == 2)
                {
                    if (ushort.TryParse(result[0], out start) && ushort.TryParse(result[1], out offsetOrLength))
                    {
                        return OperateResult.CreateSuccessResult(start, offsetOrLength);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<ushort, ushort>("地址格式不正确：" + address);
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult<ushort, ushort>("地址格式不正确：" + address);
                }
            }
            else
            {
                if (ushort.TryParse(address, out start))
                {
                    return OperateResult.CreateSuccessResult(start, offsetOrLength);
                }
                else
                {
                    return OperateResult.CreateFailResult<ushort, ushort>("地址格式不正确：" + address);
                }
            }
        }


        /// <summary>
        /// 通用写入方法
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="varValue"></param>
        /// <returns></returns>
        public OperateResult Write(string varName, string varValue)
        {
            if (VariableDicList.ContainsKey(varName))
            {
                return OperateResult.CreateFailResult("无法通过变量名称找到变量对象");
            }
            ModbusRTUVariable variable = VariableDicList[varName];
            var result = AnalysisAddress(variable.Address);

            if (result.IsSuccess == false)
            {
                return result;
            }

            //拿到偏移量
            ushort start = result.Content1;
            ushort offsetOrLength = result.Content2;

            //对写入值做反向的线性转换
            var migrationResult = MigrationLib.SetMigrationValue(varValue, variable.DataType, variable.Scale, variable.Offset);
            if (!migrationResult.IsSuccess) return migrationResult;

            //最终写入值
            varValue = migrationResult.Content;
            switch (variable.DataType)
            {
                case DataType.Bool:
                    return modbus.WriteSingleCoil(start, varValue == "1" || varValue.ToLower() == "true",variable.SlaveId);


                case DataType.Short:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromShort(Convert.ToInt16(varValue), this.DataFormat), variable.SlaveId);

                case DataType.UShort:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromUShort(Convert.ToUInt16(varValue), this.DataFormat), variable.SlaveId);

                case DataType.Int:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromInt(Convert.ToInt32(varValue), this.DataFormat), variable.SlaveId);
                case DataType.UInt:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromUInt(Convert.ToUInt32(varValue), this.DataFormat), variable.SlaveId);
                case DataType.Float:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromFloat(Convert.ToSingle(varValue), this.DataFormat), variable.SlaveId);
                case DataType.Double:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromDouble(Convert.ToDouble(varValue), this.DataFormat), variable.SlaveId);
                case DataType.Long:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromLong(Convert.ToInt64(varValue), this.DataFormat), variable.SlaveId);
                case DataType.ULong:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromULong(Convert.ToUInt64(varValue), this.DataFormat), variable.SlaveId);
                case DataType.String:
                    if (varValue.Length % 2 != 0)
                    {
                        varValue += " ";
                    }
                    byte[] bytes = ByteArrayLib.GetByteArrayFromString(varValue, Encoding.ASCII);
                    if (this.DataFormat == EndianType.BADC || this.DataFormat == EndianType.DCBA)
                    {
                        return modbus.WriteMultipleRegisters(start, bytes, variable.SlaveId);
                    }
                    else
                    {
                        //每相隔的2个字节颠倒一下
                        List<byte> data = new List<byte>();
                        for (int i = 0; i < bytes.Length; i += 2)
                        {
                            data.Add(bytes[i + 1]);
                            data.Add(bytes[i]);
                        }
                        return modbus.WriteMultipleRegisters(start, data.ToArray(), variable.SlaveId);

                    }

                case DataType.ByteArray:
                    //空格分割
                    List<byte> value = ByteArrayLib.GetByteArrayFromHexString(varValue).ToList();
                    if (value.Count % 2 != 0)
                    {
                        value.Add(0x00);
                    }
                    return modbus.WriteMultipleRegisters(start, value.ToArray(), variable.SlaveId);


                default:
                    return OperateResult.CreateFailResult("暂不支持该数据类型");

            }

        }
    }
}
