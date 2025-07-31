using AY.CommunicationLib;
using AY.CommunicationLib.DataConvert;
using AY.CommunicationLib.Modbus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    public class ModbusTCPDevice : DeviceBase
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 通信组集合
        /// </summary>
        public List<ModbusTCPGroup> GroupList { get; set; } = new List<ModbusTCPGroup>();

        /// <summary>
        /// 通信对象
        /// </summary>
        private ModbusTCP modbus { get; set; } = new ModbusTCP();

        /// <summary>
        /// 单元标识符
        /// </summary>
        public byte SlaveId { get; set; } = 0x01;

        /// <summary>
        /// 变量字典集合
        /// </summary>
        public Dictionary<string, ModbusTCPVariable> VariableDicList = new Dictionary<string, ModbusTCPVariable>();


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
                        VariableDicList[variable.VarName] = variable;
                    }
                    else
                    {
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
                GetModbusTCPValue();
            }, Cts.Token);
        }

        /// <summary>
        /// 多线程方法循环读取数据
        /// </summary>
        private void GetModbusTCPValue()
        {
            while (!Cts.IsCancellationRequested)
            {
                if (IsConnected)
                {
                    this.StopWatch = Stopwatch.StartNew();

                    //循环读取
                    foreach (var gp in GroupList)
                    {
                        gp.IsOK = GetGroupValue(gp);
                    }
                    //如果所有的通信组都读取失败，这时候需要去重连
                    if (this.GroupList.All(c => !c.IsOK))
                    {
                        this.IsConnected = false;
                    }
                    //通信周期的计算
                    this.CommPeriod = this.StopWatch.ElapsedMilliseconds;
                }
                else
                {
                    //建立连接
                    if (!FirstConnectSign)
                    {
                        this.modbus.DisConnect();
                        Thread.Sleep(this.ReConnectTime);
                    }

                    this.modbus.SlaveId = this.SlaveId;
                    //通信连接
                    var result = this.modbus.Connect(this.IPAddress, this.Port);
                    this.IsConnected = result.IsSuccess;
                    FirstConnectSign = false;


                }
            }
        }

        /// <summary>
        /// 单个通信组的读取
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private bool GetGroupValue(ModbusTCPGroup gp)
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
                            cResult = modbus.ReadCoils(gp.Start, gp.Length);
                            break;
                        case ModbusStore.输入线圈:
                            cResult = modbus.ReadInputs(gp.Start, gp.Length);
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
                            rResult = modbus.ReadInputRegisters(gp.Start, gp.Length);
                            break;
                        case ModbusStore.保持型寄存器:
                            rResult = modbus.ReadHoldingRegisters(gp.Start, gp.Length);
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
        /// 停止多线程
        /// </summary>
        public void Stop()
        {
            Cts?.Cancel();

            if (IsConnected)
            {
                modbus.DisConnect();
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
            if(VariableDicList.ContainsKey(varName))
            {
                return OperateResult.CreateFailResult("无法通过变量名称找到变量对象");
            }
            ModbusTCPVariable variable = VariableDicList[varName];
            var result = AnalysisAddress(variable.Address);

            if(result.IsSuccess==false)
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
                    return modbus.WriteSingleCoil(start, varValue == "1" || varValue.ToLower() == "true");
                    
                
                case DataType.Short:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromShort(Convert.ToInt16(varValue), this.DataFormat));
                  
                case DataType.UShort:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromUShort(Convert.ToUInt16(varValue), this.DataFormat));
                     
                case DataType.Int:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromInt(Convert.ToInt32(varValue), this.DataFormat));
                case DataType.UInt:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromUInt(Convert.ToUInt32(varValue), this.DataFormat));
                case DataType.Float:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromFloat(Convert.ToSingle(varValue), this.DataFormat));
                case DataType.Double:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromDouble(Convert.ToDouble(varValue), this.DataFormat));
                case DataType.Long:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromLong(Convert.ToInt64(varValue), this.DataFormat));
                case DataType.ULong:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromULong(Convert.ToUInt64(varValue), this.DataFormat));
                case DataType.String:

                    varValue = varValue.PadRight(result.Content2 * 2, ' ');
                    byte[] bytes = ByteArrayLib.GetByteArrayFromString(varValue, Encoding.ASCII);
                    if(this.DataFormat==EndianType.BADC || this.DataFormat==EndianType.DCBA)
                    {
                        return modbus.WriteMultipleRegisters(start, bytes);
                    }
                    else
                    {
                        //每相隔的2个字节颠倒一下
                        List<byte> data = new List<byte>();
                        for(int i=0;i<bytes.Length;i+=2)
                        {
                            data.Add(bytes[i + 1]);
                            data.Add(bytes[i]);
                        }
                        return modbus.WriteMultipleRegisters(start, data.ToArray());

                    }

                case DataType.ByteArray:
                    //空格分割
                    List<byte> value = ByteArrayLib.GetByteArrayFromHexString(varValue).ToList();
                    if(value.Count%2!=0)
                    {
                        value.Add(0x00);
                    }
                    return modbus.WriteMultipleRegisters(start, value.ToArray());
                    
                
                default:
                    return OperateResult.CreateFailResult("暂不支持该数据类型");
                    
            }

        }

    }
}
