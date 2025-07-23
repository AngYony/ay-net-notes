using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib
{
    public interface IModbusRW
    {
        /// <summary>
        /// 读取输出线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult<bool[]> ReadOutputCoils(ushort start, ushort length, byte slaveId = 1);
        /// <summary>
        /// 读取输入线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult<bool[]> ReadInputCoils(ushort start ,ushort length ,byte slaveId = 1);

        /// <summary>
        /// 读取输出寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult<byte[]> ReadHoldingRegisters(ushort start ,ushort length,byte slaveId = 1);

        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult<byte[]> ReadInputRegisters(ushort start ,ushort length ,byte slaveId = 1);

        /// <summary>
        /// 写入单线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult WriteSingleCoil(ushort start ,bool value ,byte  slaveId = 1);
        /// <summary>
        /// 写入单寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1);

        /// <summary>
        /// 写入多线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="values"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult WriteMultipleCoils(ushort start, bool[] values, byte  slaveId = 1);

        /// <summary>
        /// 写入多寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="values"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        OperateResult WriteMultipleRegisters(ushort start, byte[] values,byte  slaveId = 1);
    }
}
