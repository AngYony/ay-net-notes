using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AY.Correspondence;
using AY.Entity;
using xbd.DataConvertLib;
using S7DataType = xbd.s7netplus.DataType;

namespace AY.BusinessServices
{
    public partial class PlcDataService
    {
        /// <summary>
        /// 第一次扫描标识
        /// </summary>
        public bool IsFirstScan { get; set; } = true;
        /// <summary>
        /// 当前通信状态，true：连接成功，false：连接失败
        /// </summary>
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// 通信错误次数
        /// </summary>
        public int ErrorTimes { get; set; }

        /// <summary>
        /// 允许的错误次数
        /// </summary>
        public int AllowErrorTimes { get; set; } = 3;
        S7NetLib s7Net = null;

        ///// <summary>
        ///// 建立连接
        ///// </summary>
        ///// <param name="sysInfo"></param>
        ///// <returns></returns>
        //public OperateResult Connect(SysInfo sysInfo)
        //{
        //    s7Net = new S7NetLib(sysInfo.CpuType, sysInfo.IPAddress, sysInfo.Rack, sysInfo.Slot);
        //    return s7Net.Connect();
        //}
        ///// <summary>
        ///// 断开连接
        ///// </summary>

        //public void DisConnect()
        //{
        //    if (s7Net != null) { s7Net.Disconnect(); }
        //}

        ///// <summary>
        ///// 批量数据读取
        ///// </summary>
        ///// <returns></returns>
        //public OperateResult<PlcData> ReadPlcData()
        //{
        //    //批量读取
        //    int byteCount = 44; //该值由要读取的字节数量确认，开始位置为X0.0，结束位置为D40，所以需要读取的数量为40+4个字节（1D=4Byte)
        //    var result = s7Net.ReadByteArray(S7DataType.DataBlock, 1, 0, byteCount);
        //    if (result.IsSuccess && result.Content.Length == byteCount)
        //    {
        //        //数据解析
        //        var plcData = new PlcData
        //        {
        //            //布尔解析
        //            InPump1State = BitLib.GetBitFromByteArray(result.Content, 0, 0),
        //            InPump2State = BitLib.GetBitFromByteArray(result.Content, 0, 1),
        //            CirclePump1State = BitLib.GetBitFromByteArray(result.Content, 0, 2),
        //            CirclePump2State = BitLib.GetBitFromByteArray(result.Content, 0, 3),
        //            ValveInState = BitLib.GetBitFromByteArray(result.Content, 0, 4),
        //            ValveOutState = BitLib.GetBitFromByteArray(result.Content, 0, 5),
        //            SysRunState = BitLib.GetBitFromByteArray(result.Content, 0, 6),
        //            SysAlarmState = BitLib.GetBitFromByteArray(result.Content, 0, 7),

        //            //浮点数解析
        //            PressureIn = FloatLib.GetFloatFromByteArray(result.Content, 4),
        //            PressureOut = FloatLib.GetFloatFromByteArray(result.Content, 8),
        //            TempIn1 = FloatLib.GetFloatFromByteArray(result.Content, 12),
        //            TempIn2 = FloatLib.GetFloatFromByteArray(result.Content, 16),
        //            TempOut = FloatLib.GetFloatFromByteArray(result.Content, 20),
        //            PressureTank1 = FloatLib.GetFloatFromByteArray(result.Content, 24),
        //            PressureTank2 = FloatLib.GetFloatFromByteArray(result.Content, 28),
        //            LevelTank1 = FloatLib.GetFloatFromByteArray(result.Content, 32),
        //            LevelTank2 = FloatLib.GetFloatFromByteArray(result.Content, 36),
        //            PressureTankOut = FloatLib.GetFloatFromByteArray(result.Content, 40)

        //        };
        //        return OperateResult.CreateSuccessResult(plcData);
        //    }
        //    else
        //    {
        //        return OperateResult.CreateFailResult<PlcData>(result.Message);
        //    }
        //}

        ///// <summary>
        ///// 1#进水泵控制
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool InPump1Control(bool value)
        //{
        //    string startAddress = "DB1.DBX100.0";
        //    string stopAddress = "DB1.DBX100.1";
        //    string controlAddress = value ? startAddress : stopAddress;
        //    //发送脉冲信号：先发一个true，再发一个false
        //    bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
        //    Thread.Sleep(50);
        //    result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
        //    return result;
        //}

        ///// <summary>
        ///// 2#进水泵控制
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool InPump2Control(bool value)
        //{
        //    string startAddress = "DB1.DBX100.2";
        //    string stopAddress = "DB1.DBX100.3";
        //    string controlAddress = value ? startAddress : stopAddress;
        //    bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
        //    Thread.Sleep(50);
        //    result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
        //    return result;
        //}

        ///// <summary>
        ///// 1#循环泵控制
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool CirclePump1Control(bool value)
        //{
        //    string startAddress = "DB1.DBX100.4";
        //    string stopAddress = "DB1.DBX100.5";
        //    string controlAddress = value ? startAddress : stopAddress;
        //    //发送脉冲信号：先发一个true，再发一个false
        //    bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
        //    Thread.Sleep(50);
        //    result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
        //    return result;
        //}

        ///// <summary>
        ///// 2#循环泵控制
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool CirclePump2Control(bool value)
        //{
        //    string startAddress = "DB1.DBX100.6";
        //    string stopAddress = "DB1.DBX100.7";
        //    string controlAddress = value ? startAddress : stopAddress;
        //    //发送脉冲信号：先发一个true，再发一个false
        //    bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
        //    Thread.Sleep(50);
        //    result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
        //    return result;
        //}

        ///// <summary>
        ///// 系统复位
        ///// </summary>
        ///// <returns></returns>
        //public bool SysReset()
        //{
        //    string controlAddress = "DB1.DBX101.4"; //假设复位信号在DB1.DBX101.4
        //    bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
        //    Thread.Sleep(50);
        //    result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
        //    return result;
        //}


        ///// <summary>
        ///// 进水阀控制
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool ValveInControl(bool value)
        //{
        //    string startAddress = "DB1.DBX101.0";
        //    string stopAddress = "DB1.DBX101.1";
        //    string controlAddress = value ? startAddress : stopAddress;
        //    //发送脉冲信号：先发一个true，再发一个false
        //    bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
        //    Thread.Sleep(50);
        //    result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
        //    return result;
        //}

        ///// <summary>
        ///// 出水阀控制
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool ValveOutControl(bool value)
        //{
        //    string startAddress = "DB1.DBX101.2";
        //    string stopAddress = "DB1.DBX101.3";
        //    string controlAddress = value ? startAddress : stopAddress;
        //    //发送脉冲信号：先发一个true，再发一个false
        //    bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
        //    Thread.Sleep(50);
        //    result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
        //    return result;
        //}



        //siemens = new S7NetLib(CpuType.S71500, "192.168.10.200", 0, 0);
        //var result = siemens.Connect();
        //if (result.IsSuccess)
        //{
        //    //#region 1:读取单个变量
        //    //var data = siemens.ReadVariable("DB1.DBD4");
        //    //var value = Convert.ToUInt32(data.Content).ConvertToFloat();
        //    //MessageBox.Show($"读取数据成功: {value}");
        //    //#endregion

        //    //#region 2:批量读取字节数组
        //    //var data = siemens.ReadByteArray(DataType.DataBlock, 1, 0, 20);
        //    //var value = BitConverter.ToString(data.Content);
        //    //MessageBox.Show($"读取数据成功: {value}");
        //    //#endregion
        //    #region 变量写入
        //    siemens.WriteVariable("DB1.DBX100.0", true);
        //    Thread.Sleep(100);
        //    siemens.WriteVariable("DB1.DBX100.0", false);
        //    #endregion

        //    #region 3.读取类对象
        //    var data = siemens.ReadClass<PlcData>(1, 0);
        //    this.label36.Text = data.Content.TempIn1.ToString("f2");
        //    this.xbdPump1.IsRun = data.Content.InPump1State;
        //    #endregion

        //}
        //else
        //{
        //    MessageBox.Show($"连接失败: {result.Message}");
        //}
    }
}
