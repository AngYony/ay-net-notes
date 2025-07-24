using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AY.Correspondence;
using AY.Entity;
using xbd.DataConvertLib;
using xbd.ModbusLib.Library;
using S7DataType = xbd.s7netplus.DataType;

namespace AY.BusinessServices
{
    public partial class PlcDataService
    {

        private ModbusTCP modbus = null;


        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <returns></returns>
        public OperateResult Connect(SysInfo sysInfo)
        {
            modbus = new ModbusTCP();
            return modbus.Connect(sysInfo.IPAddress, 1000); //写死的端口
        }

        /// <summary>
        /// 断开连接
        /// </summary>

        public void DisConnect()
        {
            if (modbus != null) { modbus.DisConnect(); }
        }

        /// <summary>
        /// 批量数据读取
        /// </summary>
        /// <returns></returns>
        public OperateResult<PlcData> ReadPlcData()
        {
            //批量读取
            ushort registerCount = 22;
            var result = this.modbus.ReadHoldingRegisters(0, registerCount, 1);
            if (result.IsSuccess && result.Content.Length == registerCount * 2)
            {
                //数据解析
                var plcData = new PlcData
                {
                    //布尔解析
                    InPump1State = BitLib.GetBitFromByteArray(result.Content, 0, 0),
                    InPump2State = BitLib.GetBitFromByteArray(result.Content, 0, 1),
                    CirclePump1State = BitLib.GetBitFromByteArray(result.Content, 0, 2),
                    CirclePump2State = BitLib.GetBitFromByteArray(result.Content, 0, 3),
                    ValveInState = BitLib.GetBitFromByteArray(result.Content, 0, 4),
                    ValveOutState = BitLib.GetBitFromByteArray(result.Content, 0, 5),
                    SysRunState = BitLib.GetBitFromByteArray(result.Content, 0, 6),
                    SysAlarmState = BitLib.GetBitFromByteArray(result.Content, 0, 7),

                    //浮点数解析
                    PressureIn = FloatLib.GetFloatFromByteArray(result.Content, 4),
                    PressureOut = FloatLib.GetFloatFromByteArray(result.Content, 8),
                    TempIn1 = FloatLib.GetFloatFromByteArray(result.Content, 12),
                    TempIn2 = FloatLib.GetFloatFromByteArray(result.Content, 16),
                    TempOut = FloatLib.GetFloatFromByteArray(result.Content, 20),
                    PressureTank1 = FloatLib.GetFloatFromByteArray(result.Content, 24),
                    PressureTank2 = FloatLib.GetFloatFromByteArray(result.Content, 28),
                    LevelTank1 = FloatLib.GetFloatFromByteArray(result.Content, 32),
                    LevelTank2 = FloatLib.GetFloatFromByteArray(result.Content, 36),
                    PressureTankOut = FloatLib.GetFloatFromByteArray(result.Content, 40)

                };
                return OperateResult.CreateSuccessResult(plcData);
            }
            else
            {
                return OperateResult.CreateFailResult<PlcData>(result.Message);
            }
        }

        /// <summary>
        /// 1#进水泵控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InPump1Control(bool value)
        {
            string startAddress = "50.0";
            string stopAddress = "50.1";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = modbus.WriteRegisterBit(controlAddress, true, false, 1).IsSuccess;
            Thread.Sleep(50); //
            result &= this.modbus.WriteRegisterBit(controlAddress, false, false, 1).IsSuccess;
            return result;
        }

        /// <summary>
        /// 2#进水泵控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InPump2Control(bool value)
        {
            string startAddress = "50.2";
            string stopAddress = "50.3";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = modbus.WriteRegisterBit(controlAddress, true, false, 1).IsSuccess;
            Thread.Sleep(50); //
            result &= this.modbus.WriteRegisterBit(controlAddress, false, false, 1).IsSuccess;
            return result;
        }

        /// <summary>
        /// 1#循环泵控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CirclePump1Control(bool value)
        {
            string startAddress = "50.4";
            string stopAddress = "50.5";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = modbus.WriteRegisterBit(controlAddress, true, false, 1).IsSuccess;
            Thread.Sleep(50); //
            result &= this.modbus.WriteRegisterBit(controlAddress, false, false, 1).IsSuccess;
            return result;
        }


        /// <summary>
        /// 2#循环泵控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CirclePump2Control(bool value)
        {
            string startAddress = "50.6";
            string stopAddress = "50.7";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = modbus.WriteRegisterBit(controlAddress, true, false, 1).IsSuccess;
            Thread.Sleep(50); //
            result &= this.modbus.WriteRegisterBit(controlAddress, false, false, 1).IsSuccess;
            return result;
        }

        /// <summary>
        /// 系统复位
        /// </summary>
        /// <returns></returns>
        public bool SysReset()
        { 
            string controlAddress = "50.12";
            bool result = modbus.WriteRegisterBit(controlAddress, true, false, 1).IsSuccess;
            Thread.Sleep(50); //
            result &= this.modbus.WriteRegisterBit(controlAddress, false, false, 1).IsSuccess;
            return result;
        }


        /// <summary>
        /// 进水阀控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValveInControl(bool value)
        {
            string startAddress = "50.8";
            string stopAddress = "50.9";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = modbus.WriteRegisterBit(controlAddress, true, false, 1).IsSuccess;
            Thread.Sleep(50); //
            result &= this.modbus.WriteRegisterBit(controlAddress, false, false, 1).IsSuccess;
            return result;
        }

        /// <summary>
        /// 出水阀控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValveOutControl(bool value)
        {
            string startAddress = "50.10";
            string stopAddress = "50.11";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = modbus.WriteRegisterBit(controlAddress, true, false, 1).IsSuccess;
            Thread.Sleep(50); //
            result &= this.modbus.WriteRegisterBit(controlAddress, false, false, 1).IsSuccess;
            return result;
        }
    }
}
