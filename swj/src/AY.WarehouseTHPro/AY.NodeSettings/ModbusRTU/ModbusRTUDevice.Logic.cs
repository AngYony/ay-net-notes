using AY.CommunicationLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xbd.DataConvertLib;
using xbd.ModbusLib.Library;

namespace AY.NodeSettings
{
    public partial class ModbusRTUDevice
    {

        public Dictionary<string, ModbusRTUVariable> VariableDicList = null;
        private ModbusRTU modbus = new ModbusRTU();


        /// <summary>
        /// 是否进行离线检查策略
        /// </summary>
        public bool OfflineCheckPolicy { get; set; } = true;
        /// <summary>
        /// 在启用离线检查策略时，多少次未读取到数据后，认为设备离线
        /// </summary>
        public int CheckTimesSet { get; set; } = 10;

        public void Init()
        {
           
            this.VariableDicList = this.GroupList.SelectMany(gp => gp.VarList).ToDictionary(v => v.VarName, v => v);
        }
        /// <summary>
        /// 开启多线程
        /// </summary>
        public void Start()
        {
            Init();
            this.Cts = new CancellationTokenSource();
            Task.Run(GetModbusRTUValue, Cts.Token);
        }


        private void GetModbusRTUValue()
        {
            int cycleTimers = 0;
            while (!Cts.IsCancellationRequested)
            {
                //如果连接成功
                if (IsConnected)
                {
                    this.Stopwatcher = Stopwatch.StartNew();
                    if (this.OfflineCheckPolicy)
                    {
                        //获取所有OK组
                        var okGroupList = this.GroupList.Where(gp => gp.IsOk).ToList();
                        foreach (var gp in okGroupList)
                        {
                            //循环读取每个通信组数据
                            gp.IsOk = GetGroupValue(gp);
                        }
                        cycleTimers++;
                        if (cycleTimers >= this.CheckTimesSet)
                        {
                            //获取所有未OK组
                            var ngGroupList = this.GroupList.Where(gp => !gp.IsOk).ToList();
                            foreach (var gp in ngGroupList)
                            {
                                //如果未OK组超过设定次数，则认为设备离线
                                gp.IsOk = GetGroupValue(gp);
                            }
                            cycleTimers = 0;
                        }
                    }
                    else
                    {

                        foreach (var gp in this.GroupList)
                        {
                            //循环读取每个通信组数据
                            gp.IsOk = GetGroupValue(gp);
                        }
                    }
                    //如果所有的组都读取失败，并且端口号不存在
                    if (this.GroupList.All(gp => !gp.IsOk))
                    {
                        if (!Common.GetPortNames().Contains(this.PortName))
                        {
                            this.IsConnected = false; //端口不存在，断开连接
                        }
                    }

                    this.CommPeriod = this.Stopwatcher.ElapsedMilliseconds;

                }
                else
                {
                    //如果不是第一次，间隔重连
                    if (!FirstConnectSign) Thread.Sleep(ReConnectTime);
                    this.IsConnected = this.modbus.Open(this.PortName, this.BaudRate, this.Parity, this.DataBits, this.StopBits).IsSuccess;
                    FirstConnectSign = false; //连接过一次无论失败成功，均为非第一次
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
                            cResult = modbus.ReadOutputCoils(gp.Start, gp.Length, gp.GroupId);
                            break;
                        case ModbusStore.输入线圈:
                            cResult = modbus.ReadInputCoils(gp.Start, gp.Length, gp.GroupId);
                            break;

                    }

                    //长度的验证
                    if (cResult.IsSuccess && cResult.Content.Length == gp.Length)
                    {
                        //变量解析
                        foreach (var variable in gp.VarList)
                        {
                            var add = AnalysisAddress(variable.Address);
                            if (!add.IsSuccess) continue;

                            int start = add.Content1 - gp.Start;
                            switch (variable.DataType)
                            {
                                case DataType.Bool:
                                    variable.VarValue = BitLib.GetBitFromBitArray(cResult.Content, start);
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
                            if (!add.IsSuccess) continue;

                            //从0开始读取100个寄存器，地址是10，偏移量为20
                            int start = add.Content1 - gp.Start; //寄存器偏移量
                            int offsetOrLength = add.Content2;
                            start *= 2;
                            switch (variable.DataType)
                            {
                                case DataType.Bool:
                                    variable.VarValue = BitLib.GetBitFrom2BytesArray(
                                        rResult.Content, start, offsetOrLength,
                                        DataFormat == DataFormat.BADC || DataFormat == DataFormat.DCBA);
                                    break;
                                //case DataType.Byte:
                                //    break;
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
                                    if (this.DataFormat == DataFormat.BADC || this.DataFormat == DataFormat.DCBA)
                                    {
                                        variable.VarValue = StringLib.GetStringFromByteArrayByEncoding(
                                            bytes, 0, bytes.Length, Encoding.ASCII).Replace("\0", "");
                                    }
                                    else
                                    {
                                        //1234  2143
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
                                //case DataType.HexString:
                                //    break;
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

        public void Stop()
        {
            if (this.Cts != null)
            {
                this.Cts.Cancel();
                this.Cts.Dispose();
    
            }
            if(IsConnected)
            {
                modbus.Close();
            }
            this.IsConnected = false;
            this.Stopwatcher?.Stop();
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

            string[] result = address.Split('.');
            bool flag = true;
            if (result.Length > 0)
            {
                flag &= ushort.TryParse(result[0], out start);
            }
            if (result.Length > 1)
            {
                flag &= ushort.TryParse(result[1], out offsetOrLength);
            }
            if (flag)
            {
                return OperateResult.CreateSuccessResult(start, offsetOrLength);
            }
            else
            {
                return OperateResult.CreateFailResult<ushort, ushort>("地址格式不正确：" + address);
            }
        }
    }
}
