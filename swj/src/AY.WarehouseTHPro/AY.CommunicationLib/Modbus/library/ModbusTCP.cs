using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib.Library
{
    public class ModbusTCP : TCPBase, IModbusRW
    {
        private static readonly object lockobj = new object();
        private ushort _transactionId = 0;

        public ushort TransactionId
        {
            get
            {
                lock (lockobj)
                {
                    return _transactionId == ushort.MaxValue ? (ushort)1 : ++_transactionId;
                }
            }
        }

        /// <summary>
        /// 读取行为的报文格式构建
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="slaveId"></param>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        private byte[] BuildReadMessageFrame(ushort start, ushort count, byte slaveId, FunctionCode functionCode)
        {
            //创建一个ByteArray对象
            ByteArray sendCommand = new ByteArray();
            //事务处理标识符
            sendCommand.Add(this.TransactionId);
            //协议标志符
            sendCommand.Add((ushort)0);
            //长度
            sendCommand.Add((ushort)6);
            //站地址
            sendCommand.Add(slaveId);
            //功能码
            sendCommand.Add((byte)functionCode);
            //起始地址
            sendCommand.Add(start);
            //数量
            sendCommand.Add(count);
            return sendCommand.array;
        }

        private OperateResult CheckResponse(byte[] response, byte slaveId, bool isRead, ushort byteLength = 0)
        {
            //验证总长度
            int reqLength = isRead ? 9 + byteLength : 12;
            if (response.Length == reqLength)
            {
                //验证单元标识符
                if (response[6] == slaveId)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文单元标识符验证不通过：" + BitConverter.ToString(response));
                }
            }
            else
            {
                return OperateResult.CreateFailResult("返回报文长度验证不通过：" + BitConverter.ToString(response));
            }

        }

        /// <summary>
        /// 写入行为的报文格式构建
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <param name="slaveId"></param>
        /// <param name="functionCode"></param>
        /// <param name="coilLength"></param>
        /// <returns></returns>
        private byte[] BuildWriteMessageFrame(ushort start, byte[] value, byte slaveId, FunctionCode functionCode, ushort coilLength = 0)
        {
            ByteArray sendCommand = new ByteArray();
            if (functionCode == FunctionCode.WriteSingleCoil || functionCode == FunctionCode.WriteSingleRegister)
            {
                //事务处理标识符
                sendCommand.Add(this.TransactionId);
                //协议标识符
                sendCommand.Add((ushort)0);
                //长度
                sendCommand.Add((ushort)6);
                //站地址
                sendCommand.Add(slaveId);
                //功能码
                sendCommand.Add((byte)functionCode);
                //起始地址
                sendCommand.Add(start);
                //写入值
                sendCommand.Add(value);
            }
            else if (functionCode == FunctionCode.WriteMultipleCoils || functionCode == FunctionCode.WriteMultipleRegisters)
            {
                //事务处理标识符
                sendCommand.Add(this.TransactionId);
                //协议标识符
                sendCommand.Add((ushort)0);
                //长度
                sendCommand.Add((ushort)(7 + value.Length));
                //站地址
                sendCommand.Add(slaveId);
                //功能码
                sendCommand.Add((byte)functionCode);
                //起始地址
                sendCommand.Add(start);
                //数量
                sendCommand.Add(coilLength == 0 ? (ushort)(value.Length / 2) : coilLength);
                //字节计数
                sendCommand.Add((byte)value.Length);
                //写入值
                sendCommand.Add(value);
            }

            return sendCommand.array;
        }

        /// <summary>
        /// 读取报文中的数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="isBit"></param>
        /// <returns></returns>
        private byte[] AnalysisResponseMessage(byte[] response, bool isBit)
        {
            byte[] data = ByteArrayLib.GetByteArrayFromByteArray(response, 9, response.Length - 9);
            if (isBit)
            {
                //线圈处理：报文截取到的是字节，每个字节代表8个线圈的值，需要进行转换得到每个线圈的值
                bool[] values = BitLib.GetBitArrayFromByteArray(data);
                //将每个线圈的值，转换为字节数组
                var result = values.Select(c => c ? (byte)0x01 : (byte)0x00).ToArray();
                return result;
            }
            else
            {
                return data;
            }
        }

        /// <summary>
        /// 读取输出线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResult<bool[]> ReadOutputCoils(ushort start, ushort length, byte slaveId = 0x01)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadCoils);
            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    var data = AnalysisResponseMessage(result.Content, true);
                    var content = data.Select(a => a == 0x01).Take(length).ToArray();
                    return OperateResult.CreateSuccessResult(content);
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>(receive);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }

        }

        /// <summary>
        /// 读取输入线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult<bool[]> ReadInputCoils(ushort start, ushort length, byte slaveId = 0x01)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputs);
            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    var data = AnalysisResponseMessage(result.Content, true);
                    var content = data.Select(a => a == 0x01).Take(length).ToArray();
                    return OperateResult.CreateSuccessResult(content);
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>(receive);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }
        }


        /// <summary>
        /// 读取输出寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 0x01)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadHoldingRegisters);
            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    var data = AnalysisResponseMessage(result.Content, false);
                    return OperateResult.CreateSuccessResult(data);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>(receive);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }


        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 0x01)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputRegisters);
            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    var data = AnalysisResponseMessage(result.Content, false);
                    return OperateResult.CreateSuccessResult(data);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>(receive);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
        }




        /// <summary>
        /// 写入单线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 0x01)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(
                start, value ? new byte[] { 0xFF, 0x00 } : new byte[] { 0x00, 0x00 }, slaveId, FunctionCode.WriteSingleCoil);

            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, false);
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);
                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("发送与返回报文不一致");
                }
                else
                {
                    return receive;
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        /// <summary>
        /// 写入但寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 0x01)
        {
            if (value == null || value.Length != 2)
            {
                return OperateResult.CreateFailResult("写入字节长度必须是2");
            }
            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, value, slaveId, FunctionCode.WriteSingleRegister);

            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, false);
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);
                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("发送与返回报文不一致");
                }
                else
                {
                    return receive;
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        /// <summary>
        /// 写入多输出线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="values"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>

        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 0x01)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(
                start, ByteArrayLib.GetByteArrayFromBoolArray(values), slaveId, FunctionCode.WriteMultipleCoils, (ushort)values.Length);

            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, false);
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    byte[] reqdata = sendCommand.Take(12).ToArray();
                    reqdata[4] = 0x00;
                    reqdata[5] = 0x06;

                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, reqdata);
                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("返回报文不正确：" + BitConverter.ToString(result.Content));
                }
                else
                {
                    return receive;
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        /// <summary>
        /// 写入多个输出寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="values"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 0x01)
        {
            if (values == null || values.Length == 0) return OperateResult.CreateFailResult("写入的字节数组不能为空");
            if (values.Length % 2 != 0) return OperateResult.CreateFailResult("写入字节数组必须为偶数");

            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, values, slaveId, FunctionCode.WriteMultipleRegisters);

            //第二三步：发送报文、接收报文
            var result = SendAndReceive(sendCommand);
            if (result.IsSuccess)
            {
                //第四步验证报文
                var receive = CheckResponse(result.Content, slaveId, false);
                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    byte[] reqdata = sendCommand.Take(12).ToArray();
                    reqdata[4] = 0x00;
                    reqdata[5] = 0x06;

                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, reqdata);
                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("返回报文不正确：" + BitConverter.ToString(result.Content));
                }
                else
                {
                    return receive;
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }



        /// <summary>
        /// 给寄存器中的某个位赋值，原理是先取出整个寄存器的报文，再为其中的某个位赋值，再回写
        /// 注意：不建议使用，可能会出现某个位的值更改后，回写时其他位的值已经发生了变化，导致脏数据写入。
        /// </summary>
        /// <param name="address">要修改的目标寄存器上的位的地址，格式必须是0.10，表示给第0号寄存器第10位写值</param>
        /// <param name="value">写入的值</param>
        /// <param name="isLittleEndian">大小端，默认为小端</param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult WriteRegisterBit(string address, bool value, bool isLittleEndian = true, byte slaveId = 1)
        {
            string[] info = address.Split('.');
            ushort.TryParse(info[0], out ushort start); //第start号寄存器
            ushort.TryParse(info[1], out ushort index);  //第index位写值
            //一个寄存器占2个字节，共16位，因此索引必须是0~15
            if (index >= 0 && index <= 15)
            {
                //先读取一个寄存器的值
                var rResult = this.ReadHoldingRegisters(start, 1, slaveId);
                if (rResult.IsSuccess)
                {
                    //再做转换
                    byte[] wData = rResult.Content;
                    //大小端处理，由于只读取了一个寄存器（字），因此只会有2个字节
                    if (isLittleEndian) //小端
                    {
                        int byteIndex = index < 8 ? 1 : 0; //获取小端情况下的目标字节
                        //修改某个字节的某个位
                        wData[byteIndex] = ByteLib.SetbitValue(wData[byteIndex], index % 8, value);
                    }
                    else
                    {
                        int byteIndex = index < 8 ? 0 : 1;
                        wData[byteIndex] = ByteLib.SetbitValue(wData[byteIndex], index % 8, value);
                    }
                    //回写该寄存器的值
                    return this.WriteSingleRegister(start, wData, slaveId);
                }
                else
                {
                    return rResult;
                }
            }
            else
            {
                return OperateResult.CreateFailResult("位偏移索引必须在0~15之间");
            }
        }



    }
}
