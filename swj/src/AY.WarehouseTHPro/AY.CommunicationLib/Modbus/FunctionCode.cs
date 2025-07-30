using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.ModbusLib
{
    /// <summary>
    /// 功能码枚举
    /// </summary>
    public enum FunctionCode
    {
        /// <summary>
        /// 读取输出线圈功能码
        /// </summary>
        ReadCoils = 0x01,
        /// <summary>
        /// 读取输入线圈功能码
        /// </summary>
        ReadInputs = 0x02,
        /// <summary>
        /// 读取保持型寄存器功能码（输出寄存器）
        /// </summary>
        ReadHoldingRegisters = 0x03,
        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        ReadInputRegisters = 0x04,
        /// <summary>
        /// 写入单线圈功能码
        /// </summary>
        WriteSingleCoil = 0x05,
        /// <summary>
        /// 写入单寄存器功能码
        /// </summary>
        WriteSingleRegister = 0x06,
        /// <summary>
        /// 写入多线圈功能码
        /// </summary>
        WriteMultipleCoils = 0x0F,
        /// <summary>
        /// 写入多寄存器功能码
        /// </summary>
        WriteMultipleRegisters = 0x10
    }
}