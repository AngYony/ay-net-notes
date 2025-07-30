using AY.CommunicationLib.DataConvert;
using System;
using System.Linq;

namespace AY.CommunicationLib.Modbus
{
    public class ModbusTCP : TCPBase, IModbusRW
    {
        #region �ֶ�������

        /// <summary>
        /// Ĭ�ϵĵ�Ԫ��ʶ��
        /// </summary>
        public byte SlaveId { get; set; } = 0x01;

        private static readonly object lockobj = new object();

        private ushort transactionId = 0;

        /// <summary>
        /// �������ʶ��
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

        #endregion �ֶ�������

        #region ��ȡ�����Ȧ

        public OperateResult<bool[]> ReadCoils(ushort start, ushort length)
        {
            return ReadCoils(start, length, SlaveId);
        }

        public OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1)
        {
            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadCoils);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));

                if (receive.IsSuccess)
                {
                    //���岽����������

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

        #endregion ��ȡ�����Ȧ

        #region ��ȡ������Ȧ

        public OperateResult<bool[]> ReadInputs(ushort start, ushort length)
        {
            return ReadInputs(start, length, SlaveId);
        }

        public OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1)
        {
            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputs);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, true, UShortLib.GetByteLengthFromBoolLength(length));

                if (receive.IsSuccess)
                {
                    //���岽����������

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

        #endregion ��ȡ������Ȧ

        #region ��ȡ�����ͼĴ���

        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length)
        {
            return ReadHoldingRegisters(start, length, SlaveId);
        }

        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadHoldingRegisters);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));

                if (receive.IsSuccess)
                {
                    //���岽����������

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

        #endregion ��ȡ�����ͼĴ���

        #region ��ȡ����Ĵ���

        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length)
        {
            return ReadInputRegisters(start, length, SlaveId);
        }

        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildReadMessageFrame(start, length, slaveId, FunctionCode.ReadInputRegisters);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, true, (ushort)(length * 2));

                if (receive.IsSuccess)
                {
                    //���岽����������
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

        #endregion ��ȡ����Ĵ���

        #region д�뵥�����Ȧ

        public OperateResult WriteSingleCoil(ushort start, bool value)
        {
            return WriteSingleCoil(start, value, SlaveId);
        }

        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1)
        {
            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildWriteMessageFrame(start, value ? new byte[] { 0xFF, 0x00 } : new byte[] { 0x00, 0x00 }, slaveId, FunctionCode.WriteSingleCoil);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, false);

                if (receive.IsSuccess)
                {
                    //���岽����������

                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);

                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("�����뷵�ر��Ĳ�һ��");
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

        #endregion д�뵥�����Ȧ

        #region д�뵥����Ĵ���

        public OperateResult WriteSingleRegister(ushort start, byte[] value)
        {
            return WriteSingleRegister(start, value, SlaveId);
        }

        public OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1)
        {
            if (value == null || value.Length != 2)
            {
                return OperateResult.CreateFailResult("д���ֽ����ĳ��ȱ�����2");
            }

            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildWriteMessageFrame(start, value, slaveId, FunctionCode.WriteSingleRegister);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, false);

                if (receive.IsSuccess)
                {
                    //���岽����������
                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, sendCommand);

                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("�����뷵�ر��Ĳ�һ��");
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

        #endregion д�뵥����Ĵ���

        #region д���������Ȧ

        public OperateResult WriteMultipleCoils(ushort start, bool[] values)
        {
            return WriteMultipleCoils(start, values, SlaveId);
        }

        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1)
        {
            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildWriteMessageFrame(start, ByteArrayLib.GetByteArrayFromBoolArray(values), slaveId, FunctionCode.WriteMultipleCoils, (ushort)values.Length);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, false);

                if (receive.IsSuccess)
                {
                    //���岽����������

                    byte[] reqdata = sendCommand.Take(12).ToArray();

                    reqdata[4] = 0x00;
                    reqdata[5] = 0x06;

                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, reqdata);

                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("���ر��Ĳ���ȷ��" + BitConverter.ToString(result.Content));
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

        #endregion д���������Ȧ

        #region д��������Ĵ���

        public OperateResult WriteMultipleRegisters(ushort start, byte[] values)
        {
            return WriteMultipleRegisters(start, values, SlaveId);
        }

        public OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 1)
        {
            if (values == null || values.Length == 0)
            {
                return OperateResult.CreateFailResult("д���ֽ����鲻��Ϊ��");
            }

            if (values.Length % 2 != 0)
            {
                return OperateResult.CreateFailResult("д���ֽ��������Ϊż��");
            }

            //��һ����ƴ�ӱ���
            byte[] sendCommand = BuildWriteMessageFrame(start, values, slaveId, FunctionCode.WriteMultipleRegisters);

            //�ڶ��������ͱ���
            //�����������ձ���
            var result = SendAndReceive(sendCommand);

            if (result.IsSuccess)
            {
                //���Ĳ�����֤����
                var receive = CheckResponse(result.Content, slaveId, false);

                if (receive.IsSuccess)
                {
                    //���岽����������

                    byte[] reqdata = sendCommand.Take(12).ToArray();

                    reqdata[4] = 0x00;
                    reqdata[5] = 0x06;

                    bool compare = ByteArrayLib.GetByteArrayEquals(result.Content, reqdata);

                    return compare ? OperateResult.CreateSuccessResult() : OperateResult.CreateFailResult("���ر��Ĳ���ȷ��" + BitConverter.ToString(result.Content));
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

        #endregion д��������Ĵ���

        #region д��Ĵ���λ����

        public OperateResult WriteRegisterBit(string address, bool value, bool isLittleEndian = true, byte slaveId = 1)
        {
            //address�ĸ�ʽ������0.10

            if (address.Contains(".") && address.Split('.').Length == 2)
            {
                string[] info = address.Split('.');

                if (ushort.TryParse(info[0], out ushort start) && ushort.TryParse(info[1], out ushort index))
                {
                    if (index >= 0 && index <= 15)
                    {
                        //�ȶ�ȡ�Ĵ�����ֵ
                        var rResult = this.ReadHoldingRegisters(start, 1, slaveId);
                        if (rResult.IsSuccess)
                        {
                            //����ת��
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
                            //���д��
                            return this.WriteSingleRegister(start, wData, slaveId);
                        }
                        else
                        {
                            return rResult;
                        }
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("λƫ������������0-15֮��");
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("��ַ��ʽX.Y��������Ч������");
                }
            }
            else
            {
                return OperateResult.CreateFailResult("��ַ��ʽ����ΪX.Y");
            }
        }

        #endregion д��Ĵ���λ����

        #region ˽�б��Ĵ�����

        private byte[] BuildReadMessageFrame(ushort start, ushort count, byte slaveId, FunctionCode functionCode)
        {
            //����һ��ByteArray����
            ByteArray sendCommand = new ByteArray();

            //�������ʶ��
            sendCommand.Add(TransactionId);

            //Э���ʶ��
            sendCommand.Add((ushort)0);

            //����
            sendCommand.Add((ushort)6);

            //վ��ַ
            sendCommand.Add(slaveId);

            //������
            sendCommand.Add((byte)functionCode);

            //��ʼ��ַ
            sendCommand.Add(start);

            //����
            sendCommand.Add(count);

            return sendCommand.array;
        }

        private byte[] BuildWriteMessageFrame(ushort start, byte[] value, byte slaveId, FunctionCode functionCode, ushort coilLength = 0)
        {
            //����һ��ByteArray����
            ByteArray sendCommand = new ByteArray();

            if (functionCode == FunctionCode.WriteSingleCoil || functionCode == FunctionCode.WriteSingleRegister)
            {
                //�������ʶ��
                sendCommand.Add(TransactionId);

                //Э���ʶ��
                sendCommand.Add((ushort)0);

                //����
                sendCommand.Add((ushort)6);

                //վ��ַ
                sendCommand.Add(slaveId);

                //������
                sendCommand.Add((byte)functionCode);

                //��ʼ��ַ
                sendCommand.Add(start);

                //д��ֵ
                sendCommand.Add(value);
            }
            else if (functionCode == FunctionCode.WriteMultipleCoils || functionCode == FunctionCode.WriteMultipleRegisters)
            {
                //�������ʶ��
                sendCommand.Add(TransactionId);

                //Э���ʶ��
                sendCommand.Add((ushort)0);

                //����
                sendCommand.Add((ushort)(7 + value.Length));

                //վ��ַ
                sendCommand.Add(slaveId);

                //������
                sendCommand.Add((byte)functionCode);

                //��ʼ��ַ
                sendCommand.Add(start);

                //����
                sendCommand.Add(coilLength == 0 ? (ushort)(value.Length / 2) : coilLength);

                //�ֽڼ���
                sendCommand.Add((byte)value.Length);

                //д��ֵ
                sendCommand.Add(value);
            }

            return sendCommand.array;
        }

        private OperateResult CheckResponse(byte[] response, byte slaveId, bool isRead, ushort bytelength = 0)
        {
            //��֤�ܳ���
            int reqLength = isRead ? 9 + bytelength : 12;

            if (response.Length == reqLength)
            {
                //��֤��Ԫ��ʶ��
                if (response[6] == slaveId)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailResult("���ر��ĵ�Ԫ��ʶ����֤��ͨ����" + BitConverter.ToString(response));
                }
            }
            else
            {
                return OperateResult.CreateFailResult("���ر��ĳ�����֤��ͨ����" + BitConverter.ToString(response));
            }
        }

        private OperateResult<byte[]> AnalysisResponseMessage(byte[] reponse, bool isBit)
        {
            //�õ�ԭʼ����
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

        #endregion ˽�б��Ĵ�����
    }
}