using AY.CommunicationLib;
using AY.CommunicationLib.DataConvert;
using AY.CommunicationLib.PLC;
using AY.CommunicationLib.PLC.Siemens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    /// <summary>
    /// 西门子设备对象
    /// </summary>
    public class SiemensDevice : DeviceBase
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// PLC端口号，默认为102
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// CPU类型
        /// </summary>
        public CpuType CpuType { get; set; }
        /// <summary>
        /// 机架号
        /// </summary>
        public short Rack { get; set; }

        /// <summary>
        /// 插槽号
        /// </summary>
        public short Slot { get; set; }

        /// <summary>
        /// 通信组对象
        /// </summary>
        public List<SiemensGroup> GroupList = new List<SiemensGroup>();


        /// <summary>
        /// 西门子PLC通信对象
        /// </summary>
        private SiementsS7 siements;

        /// <summary>
        /// 变量字典集合
        /// </summary>
        public Dictionary<string, SiemensVariable> VariableDicList = new Dictionary<string, SiemensVariable>();

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
                GetSiemensValue();
            }, Cts.Token);
        }


        /// <summary>
        /// 多线程方法循环读取数据
        /// </summary>
        private void GetSiemensValue()
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
                        this.siements.DisConnect();
                        Thread.Sleep(this.ReConnectTime);
                    }
                    this.siements = new SiementsS7(this.CpuType, this.IPAddress, this.Port, this.Rack, this.Slot);

                    //通信连接
                    var result = this.siements.Connect();
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
        private bool GetGroupValue(SiemensGroup gp)
        {
            for (int i = 0; i < gp.ReadTimes; i++)
            {
                var result = this.siements.ReadByteArray(gp.StoreArea, gp.DBNo, gp.Start, gp.Count);
                if (result.IsSuccess && result.Content.Length == gp.Count)
                {
                    //解析变量
                    foreach (var variable in gp.VarList)
                    {
                        var add = AnalysisAddress(variable.Address);
                        if (add.IsSuccess == false)
                            continue;

                        int start = add.Content1 - gp.Start;
                        int offsetOrLength = add.Content2;

                        switch (variable.DataType)
                        {
                            case DataType.Bool:
                                variable.VarValue = BitLib.GetBitFromByteArray(result.Content, start, offsetOrLength);
                                break;
                            case DataType.Byte:
                                variable.VarValue = ByteLib.GetByteFromByteArray(result.Content, start);
                                break;
                            case DataType.Short:
                                variable.VarValue = ShortLib.GetShortFromByteArray(result.Content, start, this.DataFormat);
                                break;
                            case DataType.UShort:
                                variable.VarValue = UShortLib.GetUShortFromByteArray(result.Content, start, this.DataFormat);

                                break;
                            case DataType.Int:
                                variable.VarValue = IntLib.GetIntFromByteArray(result.Content, start, this.DataFormat);

                                break;
                            case DataType.UInt:
                                variable.VarValue = UIntLib.GetUIntFromByteArray(result.Content, start, this.DataFormat);

                                break;
                            case DataType.Float:
                                variable.VarValue = FloatLib.GetFloatFromByteArray(result.Content, start, this.DataFormat);

                                break;
                            case DataType.Double:
                                variable.VarValue = DoubleLib.GetDoubleFromByteArray(result.Content, start, this.DataFormat);

                                break;
                            case DataType.Long:
                                variable.VarValue = LongLib.GetLongFromByteArray(result.Content, start, this.DataFormat);

                                break;
                            case DataType.ULong:
                                variable.VarValue = ULongLib.GetULongFromByteArray(result.Content, start, this.DataFormat);

                                break;
                            case DataType.String:
                                variable.VarValue = StringLib.GetSiemensStringFromByteArray(result.Content, start);
                                break;
                            case DataType.ByteArray:
                                variable.VarValue = ByteArrayLib.GetByteArrayFromByteArray(result.Content, start, offsetOrLength);

                                break;
                            case DataType.HexString:
                                variable.VarValue = StringLib.GetHexStringFromByteArray(result.Content, start, offsetOrLength);
                                break;
                            default:
                                break;
                        }

                        //处理
                        variable.VarValue = MigrationLib.GetMigrationValue(variable.VarValue, variable.Scale, variable.Offset).Content;
                        this.UpdateVariable(variable);
                    }
                    return true;
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
                siements.DisConnect();
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
            SiemensVariable variable = VariableDicList[varName];
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
                    return siements.WriteVariable(variable.VarAddress, varValue == "1" || varValue.ToLower() == "true");


                case DataType.Short:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToInt16(varValue));

                case DataType.UShort:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToUInt16(varValue));

                case DataType.Int:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToInt32(varValue));
                case DataType.UInt:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToUInt32(varValue));
                case DataType.Float:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToSingle(varValue));
                case DataType.Double:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToDouble(varValue));
                case DataType.Long:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToInt64(varValue));
                case DataType.ULong:
                    return siements.WriteVariable(variable.VarAddress, Convert.ToUInt64(varValue));

                case DataType.String:
                    return siements.WriteVariable(variable.VarAddress, varValue);

                case DataType.ByteArray:
                    //空格分割
                    List<byte> value = ByteArrayLib.GetByteArrayFromHexString(varValue).ToList();
                    return siements.WriteVariable(variable.VarAddress, value.ToArray());
                default:
                    return OperateResult.CreateFailResult("暂不支持该数据类型");

            }

        }

    }
}
