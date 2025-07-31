using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace AY.CommunicationLib
{
    //串口通信类基类
    public class SerialBase
    {
        /// <summary>
        /// 操作未完成时发生超时之前的毫秒数
        /// </summary>
        public int ReadTimeOut { get; set; } = 500;

        /// <summary>
        /// 获取或设置写入操作未完成时发生超时之前的毫秒数。
        /// </summary>
        public int WriteTimeOut { get; set; } = 500;

        /// <summary>
        /// 防止粘包的时间间隔毫秒数
        /// </summary>
        public int SleepTime { get; set; } = 1;

        /// <summary>
        /// 读取串口数据累计超时时间
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 3000;

        //创建一个串口通信对象
        private SerialPort serialPort = null;

        //创建一个互斥锁对象
        private SimpleHybirdLock hybirdLock = new SimpleHybirdLock();

        /// <summary>
        /// 打开串口方法
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate">波特率（9600/19200/38400/115200）</param>
        /// <param name="parity">校验位</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        /// <returns></returns>
        public OperateResult Open(
            string portName,
            int baudRate = 9600,
            Parity parity = Parity.None,
            int dataBits = 8,
            StopBits stopBits = StopBits.One)
        {
            //判断串口是否已经打开
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }

            //实例化串口通信对象
            this.serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
            {
                //设置串口的读取和写入超时时间
                ReadTimeout = this.ReadTimeOut,
                WriteTimeout = this.WriteTimeOut
            };

            try
            {
                //打开串口
                this.serialPort.Open();
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
        public void Close()
        {
            if (this.serialPort != null && this.serialPort.IsOpen)
            {
                this.serialPort.Close();
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
                    Thread.Sleep(this.SleepTime);//防止出现粘包的情况
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
                return OperateResult.CreateFailResult<byte[]>("串口通信异常:" + ex.Message);
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