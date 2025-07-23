using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib.Library
{
    public partial class ModbusRTU : SerialBase, IModbusRW
    {
        public const byte FC_R_OUTPUT_COILS = 0x01;      //读取输出线圈功能码
        public const byte FC_R_INPUT_COILS = 0x02;       //读取输入线圈功能码
        public const byte FC_R_HOLDINGREGISTERS = 0x03;  //读取保持型寄存器功能码（输出寄存器）
        public const byte FC_R_INPUTREGISTERS = 0x04;    //读取输入寄存器
        public const byte FC_W_SINGLECOIL = 0x05;        //写入单线圈功能码
        public const byte FC_W_SINGLEREGISTER = 0x06;    //写入单寄存器功能码
        public const byte FC_W_MULTIPLECOILS = 0x0F;     //写入多线圈功能码
        public const byte FC_W_MULTIPLEREGISTERS = 0x10; //写入多寄存器功能码

        /// <summary>
        /// 读取输出线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length">要读取的线圈的数量</param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult<bool[]> ReadOutputCoils(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式：从站地址+功能码+起始线圈地址+线圈数量+CRC（校验码）
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);   //从站地址
            sendCommand.Add(FC_R_OUTPUT_COILS);//功能码
            //起始线圈，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            //线圈数量
            sendCommand.Add((byte)(length / 256));
            sendCommand.Add((byte)(length % 256));
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));

            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                int dataByteLength = length % 8 == 0 ? (length / 8) : (length / 8) + 1; //一个线圈等于1/8字节
                //5= 从站地址 + 功能码 +字节计数 + CRC的2个字节
                if (response.Content.Length == 5 + dataByteLength && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    byte[] data = response.Content.Skip(3).Take(dataByteLength).ToArray();
                    //将字节数组转换为bool数组，然后截取要读取的长度
                    var result = BitLib.GetBitArrayFromByteArray(data).Take(length).ToArray();
                    return OperateResult.CreateSuccessResult(result);
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(response);
            }
        }

        /// <summary>
        /// 读取输入线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResult<bool[]> ReadInputCoils(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式：从站地址+功能码+起始线圈地址+线圈数量+CRC（校验码）
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);           //从站地址
            sendCommand.Add(FC_R_INPUT_COILS);    //输入线圈功能码
            //起始线圈，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            //线圈数量
            sendCommand.Add((byte)(length / 256));
            sendCommand.Add((byte)(length % 256));
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));

            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                int dataByteLength = length % 8 == 0 ? (length / 8) : (length / 8) + 1; //一个线圈等于1/8字节
                //5= 从站地址 + 功能码 +字节计数 + CRC的2个字节
                if (response.Content.Length == 5 + dataByteLength && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    byte[] data = response.Content.Skip(3).Take(dataByteLength).ToArray();
                    //将字节数组转换为bool数组，然后截取要读取的长度
                    var result = BitLib.GetBitArrayFromByteArray(data).Take(length).ToArray();
                    return OperateResult.CreateSuccessResult(result);
                }
                else
                {
                    return OperateResult.CreateFailResult<bool[]>("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return OperateResult.CreateFailResult<bool[]>(response);
            }
        }



        /// <summary>
        /// 读取保持型寄存器（输出寄存器）
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式：从站地址+功能码+起始寄存器地址+寄存器数量+CRC（校验码）
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);   //从站地址
            sendCommand.Add(FC_R_HOLDINGREGISTERS);//功能码
            //起始寄存器，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            //寄存器数量
            sendCommand.Add((byte)(length / 256));
            sendCommand.Add((byte)(length % 256));
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));

            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                int dataByteLength = length * 2; //一个寄存器等于2个字节
                //5= 从站地址 + 功能码 +字节计数 + CRC的2个字节
                if (response.Content.Length == 5 + dataByteLength && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    byte[] data = response.Content.Skip(3).Take(dataByteLength).ToArray();

                    return OperateResult.CreateSuccessResult(data);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(response);
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
        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            //发送报文格式：从站地址+功能码+起始寄存器地址+寄存器数量+CRC（校验码）
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);   //从站地址
            sendCommand.Add(FC_R_INPUTREGISTERS);//功能码
            //起始寄存器，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            //寄存器数量
            sendCommand.Add((byte)(length / 256));
            sendCommand.Add((byte)(length % 256));
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));

            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                int dataByteLength = length * 2; //一个寄存器等于2个字节
                //5= 从站地址 + 功能码 +字节计数 + CRC的2个字节
                if (response.Content.Length == 5 + dataByteLength && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    byte[] data = response.Content.Skip(3).Take(dataByteLength).ToArray();

                    return OperateResult.CreateSuccessResult(data);
                }
                else
                {
                    return OperateResult.CreateFailResult<byte[]>("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>(response);
            }
        }


        /// <summary>
        /// 写入单个线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1)
        {
            //发送报文格式：从站地址+功能码+线圈地址+写入值+CRL
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);   //从站地址
            sendCommand.Add(FC_W_SINGLECOIL);//功能码
            //起始线圈，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            //写入值占两个字节，true:0xFF 0x00；false:0x00 0x00
            if (value)
            {
                sendCommand.Add((byte)0xFF);
                sendCommand.Add((byte)0x00);
            }
            else
            {
                sendCommand.Add((byte)0x00);
                sendCommand.Add((byte)0x00);
            }
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));


            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                if (response.Content.Length ==  sendCommand.Count && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    if (ByteArrayLib.GetByteArrayEquals(sendCommand.ToArray(), response.Content))
                    {
                        return OperateResult.CreateSuccessResult();
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("发送报文与接收报文不一致");
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return response;
            }
        }

        /// <summary>
        /// 写入单寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="value"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResult WriteSingleRegister(ushort start, byte[] value, byte slaveId = 1)
        {
            //一个寄存器占2个字节，因此写入单寄存器长度必须为2
            if (value == null || value.Length != 2)
            {
                return OperateResult.CreateFailResult("写入字节数据长度必须为2");
            }

            //发送报文格式：从站地址+功能码+寄存器地址+写入值+CRL
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);   //从站地址
            sendCommand.Add(FC_W_SINGLEREGISTER);//功能码
            //起始寄存器，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            sendCommand.AddRange(value);//写入值
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));


            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                if (response.Content.Length ==  sendCommand.Count && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    if (ByteArrayLib.GetByteArrayEquals(sendCommand.ToArray(), response.Content))
                    {
                        return OperateResult.CreateSuccessResult();
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("发送报文与接收报文不一致");
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return response;
            }

        }


        /// <summary>
        /// 写入多线圈
        /// </summary>
        /// <param name="start"></param>
        /// <param name="values"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1)
        {
            //发送报文格式：从站地址+功能码+起始线圈地址+线圈数量+字节数+写入值+CRL
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);   //从站地址
            sendCommand.Add(FC_W_MULTIPLECOILS);//功能码
            //起始线圈，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            //线圈数量
            sendCommand.Add((byte)(values.Length / 256));
            sendCommand.Add((byte)(values.Length % 256));
            //将要写入的bool数组转换为字节数组，长度即为字节个数
            byte[] wByteArray = ByteArrayLib.GetByteArrayFromBoolArray(values);
            //字节数
            sendCommand.Add((byte)wByteArray.Length);
            //写入值
            sendCommand.AddRange(wByteArray);
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));


            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                if (response.Content.Length == 8 && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    if (ByteArrayLib.GetByteArrayEquals(sendCommand.Take(6).ToArray(), response.Content.Take(6).ToArray()))
                    {
                        return OperateResult.CreateSuccessResult();
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("接收报文不正确：" + BitConverter.ToString(response.Content));
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return response;
            }
        }


        /// <summary>
        /// 写入多寄存器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="values"></param>
        /// <param name="slaveId"></param>
        /// <returns></returns>
        public OperateResult WriteMultipleRegisters(ushort start, byte[] values, byte slaveId = 1)
        {
            //一个寄存器占2个字节，因此values必须是2的倍数
            if (values == null || values.Length == 0 || values.Length % 2 != 0)
            {
                return OperateResult.CreateFailResult("写入字节数组的长度必须是大于2的偶数");
            }

            //发送报文格式：从站地址+功能码+起始地址+寄存器数量+字节数+写入值+CRL
            //第一步：拼接报文
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);   //从站地址
            sendCommand.Add(FC_W_MULTIPLEREGISTERS);//功能码
            //起始寄存器，ushort占用两个字节，所以需要将length转换为2个字节
            sendCommand.Add((byte)(start / 256));
            sendCommand.Add((byte)(start % 256));
            //寄存器数量
            sendCommand.Add((byte)(values.Length / 2 / 256));
            sendCommand.Add((byte)(values.Length / 2 % 256));
            //字节数
            sendCommand.Add((byte)values.Length);
            //写入值
            sendCommand.AddRange(values);
            //CRC
            sendCommand.AddRange(CRC16(sendCommand.ToArray(), sendCommand.Count));


            //第二步：发送并接收报文
            var response = SendAndReceive(sendCommand.ToArray());
            if (response.IsSuccess)
            {
                //第三步：验证接收到的报文
                if (response.Content.Length == 8 && CheckCRC(response.Content))
                {
                    //第四步：解析接收到的报文
                    if (ByteArrayLib.GetByteArrayEquals(sendCommand.Take(6).ToArray(), response.Content.Take(6).ToArray()))
                    {
                        return OperateResult.CreateSuccessResult();
                    }
                    else
                    {
                        return OperateResult.CreateFailResult("接收报文不正确：" + BitConverter.ToString(response.Content));
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult("返回报文错误：" + BitConverter.ToString(response.Content)); ;
                }
            }
            else
            {
                return response;
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
