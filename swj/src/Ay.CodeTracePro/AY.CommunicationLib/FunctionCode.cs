using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib
{
    /// <summary>
    /// 功能码枚举
    /// </summary>
    public enum FunctionCode
    {
        ReadCoils = 0x01,
        ReadInputs = 0x02,
        ReadHoldingRegisters = 0x03,
        ReadInputRegisters = 0x04,
        WriteSingleCoil = 0x05,
        WriteSingleRegister = 0x06,
        WriteMultipleCoils = 0x0F,
        WriteMultipleRegisters = 0x10
    }
}
