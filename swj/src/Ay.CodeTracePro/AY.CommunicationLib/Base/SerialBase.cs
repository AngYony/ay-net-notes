
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AY.CommunicationLib
{
    //串口通信类基类
    public class SerialBase
    {
        //创建一个串口通信对象
        private SerialPort serialPort = null;

        public int ReadTimeOut { get; set; } = 500;
        public int WriteTimeOut { get; set; } = 500;

        //等待时间
        public int SleepTime { get; set; } =1;

        //超时时间
        public int ReceiveTimeOut { get; set; } = 100;

        //创建一个互斥锁对象
        private SimpleHybirdLock hybirdLock = new SimpleHybirdLock();


        /// <summary>
        /// 打开串口方法
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public OperateResult Open(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            //判断串口是否已经打开
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }

            //实例化串口通信对象
            this.serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            //设置串口的属性
            this.serialPort.ReadTimeout = ReadTimeOut;
            this.serialPort.ReadTimeout = WriteTimeOut;

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
        /// 发送并接收
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {
            //加锁
            hybirdLock.Enter();

            //定义一个内存流
            MemoryStream stream = new MemoryStream();

            try
            {
                //发送报文
                this.serialPort.Write(request, 0, request.Length);

                //定义发送时间
                DateTime start = DateTime.Now;

                //定义一个Buffer
                byte[] buffer = new byte[1024];

                while (true)
                {
                    Thread.Sleep(SleepTime);

                    if (this.serialPort.BytesToRead > 0)
                    {
                        int count = this.serialPort.Read(buffer, 0, this.serialPort.BytesToRead);
                        stream.Write(buffer, 0, count);
                    }
                    else
                    {
                        //判断是否超时
                        if ((DateTime.Now - start).TotalMilliseconds > this.ReceiveTimeOut)
                        {
                            stream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("请求超时");
                        }
                        //读取完了
                        else if (stream.Length > 0)
                        {
                            break;
                        }
                        //继续读取
                        else
                        {
                            continue;
                        }
                    }
                }

                byte[] response = stream.ToArray();

                stream.Dispose();

                return OperateResult.CreateSuccessResult(response);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<byte[]>(ex.Message);
            }
            finally
            {
                //解锁
                hybirdLock.Leave();
            }
        }
    }
}
