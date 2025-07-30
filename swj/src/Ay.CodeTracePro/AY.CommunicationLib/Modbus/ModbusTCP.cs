using AY.CommunicationLib.DataConvert;
using System;
using System.Linq;

namespace AY.CommunicationLib.Modbus
{
    public class ModbusTCP : TCPBase, IModbusRW
    {
        #region 字段与属性

        /// <summary>
        /// 默认的单元标识符
        /// </summary>
        public byte SlaveId { get; set; } = 0x01;

        private static readonly object lockobj = new object();

        private ushort transactionId = 0;

        /// <summary>
        /// 事务处理标识符
        /// </summary>
        public ushort TransactionId
        {
            get
            {
                lock (lockobj)
                {
                    return transactionId == ushort.MaxValue ? (ushort)1 : ++transactionId;
                }
            }
        }

        #endregion 字段与属性

        #region 读取输出线圈

        public OperateResult<bool[]> ReadCoils(ushort start, ushort length)
        {
            return ReadCoils(start, length, SlaveId);
        }

        public OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadCoils);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var receive = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));

                if (receive.IsSuccess)
                {
                    //第五步：解析报文

                    byte[] data = AnalysisResponseMessage(result.Content, true).Content;

                    return OperateResult.CreateSuccessResult<bool[]>(data.Select(c => c == 0x01).Take(length).ToArray());
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

        #endregion 读取输出线圈

        #region 读取输入线圈

        public OperateResult<bool[]> ReadInputs(ushort start, ushort length)
        {
            return ReadInputs(start, length, SlaveId);
        }

        public OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputs);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var receive = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));

                if (receive.IsSuccess)
                {
                    //第五步：解析报文

                    byte[] data = AnalysisResponseMessage(result.Content, true).Content;

                    return OperateResult.CreateSuccessResult<bool[]>(data.Select(c => c == 0x01).Take(length).ToArray());
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

        #endregion 读取输入线圈

        #region 读取保持型寄存器

        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length)
        {
            return ReadHoldingRegisters(start, length, SlaveId);
        }

        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadHoldingRegisters);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var receive = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));

                if (receive.IsSuccess)
                {
                    //第五步：解析报文

                    byte[] data = AnalysisResponseMessage(result.Content, false).Content;

                    return OperateResult.CreateSuccessResult<byte[]>(data);
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

        #endregion 读取保持型寄存器

        #region 读取输入寄存器

        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length)
        {
            return ReadInputRegisters(start, length, SlaveId);
        }

        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputRegisters);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var receive = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));

                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    byte[] data = AnalysisResponseMessage(result.Content, false).Content;

                    return OperateResult.CreateSuccessResult<byte[]>(data);
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

        #endregion 读取输入寄存器

        #region 写入单输出线圈

        public OperateResult WriteSingleCoil(ushort start, bool value)
        {
            return WriteSingleCoil(start, value, SlaveId);
        }

        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, value ? new byte[] { 0xFF, 0x00 } : new byte[] { 0x00, 0x00 }, slaveId, FunctionCode.WriteSingleCoil);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var receive = CheckResponse(result.Content, slaveId, false);

                if (receive.IsSuccess)
                {
                    //第五步：解析报文

                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);

                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("发送与返回报文不一致");
                }
                else
                {
                    return OperateResult.CreateFailResult(receive.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        #endregion 写入单输出线圈

        #region 写入单输出寄存器

        public OperateResult WriteSingleRegister(ushort start, byte[] value)
        {
            return WriteSingleRegister(start, value, SlaveId);
        }

        public OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1)
        {
            if (value == null || value.Length != 2)
            {
                return OperateResult.CreateFailResult("写入字节数的长度必须是2");
            }

            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, value, slaveId, FunctionCode.WriteSingleRegister);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
                var receive = CheckResponse(result.Content, slaveId, false);

                if (receive.IsSuccess)
                {
                    //第五步：解析报文
                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);

                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("发送与返回报文不一致");
                }
                else
                {
                    return OperateResult.CreateFailResult(receive.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        #endregion 写入单输出寄存器

        #region 写入多个输出线圈

        public OperateResult WriteMultipleCoils(ushort start, bool[] values)
        {
            return WriteMultipleCoils(start, values, SlaveId);
        }

        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1)
        {
            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, ByteArrayLib.GetByteArrayFromBoolArray(values), slaveId, FunctionCode.WriteMultipleCoils, (ushort)values.Length);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
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
                    return OperateResult.CreateFailResult(receive.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        #endregion 写入多个输出线圈

        #region 写入多个输出寄存器

        public OperateResult WriteMultipleRegisters(ushort start, byte[] values)
        {
            return WriteMultipleRegisters(start, values, SlaveId);
        }

        public OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 1)
        {
            if (values == null || values.Length == 0)
            {
                return OperateResult.CreateFailResult("写入字节数组不能为空");
            }

            if (values.Length % 2 != 0)
            {
                return OperateResult.CreateFailResult("写入字节数组必须为偶数");
            }

            //第一步：拼接报文
            byte[] sendCommand = BuildWriteMessageFrame(start, values, slaveId, FunctionCode.WriteMultipleRegisters);

            //第二步：发送报文
            //第三步：接收报文
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //第四步：验证报文
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
                    return OperateResult.CreateFailResult(receive.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult(result.Message);
            }
        }

        #endregion 写入多个输出寄存器

        #region 写入寄存器位方法

        public OperateResult WriteRegisterBit(string address, bool value, bool isLittleEndian = true, byte slaveId = 1)
        {
            //address的格式必须是0.10

            if (address.Contains(".") && address.Split('.').Length == 2)
            {
                string[] info = address.Split('.');

                if (ushort.TryParse(info[0], out ushort start) && ushort.TryParse(info[1], out ushort index))
                {
                    if (index >= 0 && index <= 15)
                    {
                        //先读取寄存器的值
                        var rResult = this.ReadHoldingRegisters(start, 1, slaveId);
                        if (rResult.IsSuccess)
                        {
                            //再做转换
                            byte[] wData = rResult.Content;

                            if (isLittleEndian)
                            {
                                int byteIndex = index < 8 ? 1 : 0;
                                wData[byteIndex] = ByteLib.SetbitValue(wData[byteIndex], index % 8, value);
                            }
                            else
                            {
                                int byteIndex = index < 8 ? 0 : 1;
                                wData[byteIndex] = ByteLib.SetbitValue(wData[byteIndex], index % 8, value);
                            }
                            //最后写入
                            return this.WriteSingleRegister(start, wData, slaveId);
                        }
                        else
                        {
                            return rResult;
                        }
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("位偏移索引必须在0-15之间");
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("地址格式X.Y必须是有效的整数");
                }
            }
            else
            {
                return OperateResult.CreateFailResult("地址格式必须为X.Y");
            }
        }

        #endregion 写入寄存器位方法

        #region 私有报文处理方法

        private byte[] BuildReadMessageFrame(ushort start, ushort count, byte slaveId, FunctionCode functionCode)
        {
            //创建一个ByteArray对象
            ByteArray sendCommand = new ByteArray();

            //事务处理标识符
            sendCommand.Add(TransactionId);

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

            //数量
            sendCommand.Add(count);

            return sendCommand.array;
        }

        private byte[] BuildWriteMessageFrame(ushort start, byte[] value, byte slaveId, FunctionCode functionCode, ushort coilLength = 0)
        {
            //创建一个ByteArray对象
            ByteArray sendCommand = new ByteArray();

            if (functionCode == FunctionCode.WriteSingleCoil || functionCode == FunctionCode.WriteSingleRegister)
            {
                //事务处理标识符
                sendCommand.Add(TransactionId);

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
                sendCommand.Add(TransactionId);

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

        private OperateResult CheckResponse(byte[] response, byte slaveId, bool isRead, ushort bytelength = 0)
        {
            //验证总长度
            int reqLength = isRead ? 9 + bytelength : 12;

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

        private OperateResult<byte[]> AnalysisResponseMessage(byte[] reponse, bool isBit)
        {
            //拿到原始数据
            byte[] data = ByteArrayLib.GetByteArrayFromByteArray(reponse, 9, reponse.Length - 9);

            if (isBit)
            {
                bool[] values = BitLib.GetBitArrayFromByteArray(data);

                return OperateResult.CreateSuccessResult(values.Select(c => c == true ? (byte)0x01 : (byte)0x00).ToArray());
            }
            else
            {
                return OperateResult.CreateSuccessResult(data);
            }
        }

        #endregion 私有报文处理方法
    }
}