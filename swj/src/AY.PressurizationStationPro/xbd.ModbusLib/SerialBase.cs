using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace xbd.ModbusLib
{
    /// <summary>
    /// 串口通信基类
    /// </summary>
    public class SerialBase
    {
        //创建一个串口对象
        private SerialPort serialPort = null;
        //创建一个混合锁对象
        private static SimpleHybirdLock hybirdLock = new SimpleHybirdLock();
        public int ReadTimeout { get; set; } = 1000;
        public int WriteTimeout { get; set; } = 1000;
        /// <summary>
        /// 接收报文超时时间毫秒数
        /// </summary>
        public int ReceiveTimeOut { get; private set; } = 3000;
        /// <summary>
        /// 报文读取间隔时间毫秒数
        /// </summary>
        public int ReadInterval { get; private set; } = 20;


        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public OperateResult Open(
            string portName,
            int baudRate = 9600,
            Parity parity = Parity.None,
            int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                //设置串口的属性
                this.serialPort.ReadTimeout = ReadTimeout;
                this.serialPort.WriteTimeout = WriteTimeout;
                //打开串口  
                serialPort.Open();

                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public OperateResult Close()
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// 串口发送并接收数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {

            MemoryStream stream = new MemoryStream();
            hybirdLock.Enter();
            try
            {
                //发送报文
                this.serialPort.Write(request, 0, request.Length);

                DateTime start = DateTime.Now;
                //接收报文
                byte[] buffer = new byte[1024];
                while (true)
                {
                    Thread.Sleep(ReadInterval);//防止出现粘包的情况
                    //如果有数据读取
                    if (this.serialPort.BytesToRead > 0)
                    {
                        //记录真正读取的字节数
                        int bytesRead = this.serialPort.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            //将读取到的数据写入到内存流中
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                    else
                    {
                        //判断是否超时
                        if ((DateTime.Now - start).TotalMilliseconds > this.ReceiveTimeOut)
                        {
                            stream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("请求超时");
                        }
                        else if (stream.Length > 0)
                        {
                            break; //没有更多数据可读，退出循环
                        }
                    }
                   
                }

                byte[] response = stream.ToArray();
                stream.Dispose();
                return OperateResult.CreateSuccessResult(response);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<byte[]>("串口通信异常:"+ex.Message);
            }
            finally
            {
                hybirdLock.Leave();
                //if (serialPort != null && serialPort.IsOpen)
                //{
                //    serialPort.DiscardInBuffer(); //清空输入缓冲区
                //    serialPort.DiscardOutBuffer(); //清空输出缓冲区
                //}
            }
        }

    }
}
