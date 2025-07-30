using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AY.CommunicationLib.Modbus
{
    /// <summary>
    /// Modbus¶ÁÐ´½Ó¿Ú
    /// </summary>
    public  interface IModbusRW
    {
        OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1);
        OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1);
        OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1);
        OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1);

        OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1);
        OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1);
        OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1);
        OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 1);
    }
}
