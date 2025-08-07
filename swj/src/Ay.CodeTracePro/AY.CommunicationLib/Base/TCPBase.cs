using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace AY.CommunicationLib
{
    /// <summary>
    /// TCP通信类基类
    /// </summary>
    public class TCPBase
    {
        /// <summary>
        /// 发送超时值（以毫秒为单位），0表示永不超时
        /// </summary>
        public int SendTimeOut { get; set; } = 500;

        /// <summary>
        /// 连接的超时值（以毫秒为单位）,0表示永不超时
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 500;

        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectTimeOut { get; set; } = 2000;

        /// <summary>
        /// 最大等待时间
        /// </summary>
        public int MaxWaitTime { get; set; } = 2000;

        //锁对象
        private SimpleHybirdLock simpleHybirdLock = new SimpleHybirdLock();

        //创建一个Tcp通信对象
        private TcpClient tcpClient = null;
        

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public OperateResult Connect(string ip, int port)
        {
            //实例化TcpClient对象
            this.tcpClient = new TcpClient();

            try
            {
                this.tcpClient.ConnectAsync(IPAddress.Parse(ip), port).Wait(ConnectTimeOut);

                if (this.tcpClient.Connected)
                {
                    //设置超时时间
                    this.tcpClient.SendTimeout = this.SendTimeOut;
                    this.tcpClient.ReceiveTimeout = this.ReceiveTimeOut;
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailResult("连接超时");
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
            }
        }

        /// <summary>
        /// 发送并接收方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {
            simpleHybirdLock.Enter();
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                var tcpStream = this.tcpClient.GetStream();
                tcpStream.Write(request, 0, request.Length);
                DateTime start = DateTime.Now;
                byte[] buffer = new byte[1024];
                while (true)
                {
                    if (this.tcpClient.Available > 0)
                    {
                        int count = tcpStream.Read(buffer, 0, buffer.Length);
                        memoryStream.Write(buffer, 0, count);
                    }
                    else
                    {
                        //判断是否超时：注意此处的超时最好的解决方案是使用TokenCancel来实现
                        if ((DateTime.Now - start).TotalMilliseconds > this.MaxWaitTime)
                        {
                            memoryStream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("请求超时");
                        }
                        else if (memoryStream.Length > 0)
                        {
                            break;
                        }
                    }
                }

                byte[] response = memoryStream.ToArray();
                memoryStream.Dispose();
                return OperateResult.CreateSuccessResult(response);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<byte[]>(ex.Message);
            }
            finally
            {
                simpleHybirdLock.Leave();
            }
        }
    }
}