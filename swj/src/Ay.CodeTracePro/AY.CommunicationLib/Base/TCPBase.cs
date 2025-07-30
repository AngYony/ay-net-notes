using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace AY.CommunicationLib
{
    public class TCPBase
    {
        //����һ��Tcpͨ�Ŷ���
        private TcpClient tcpClient;

        /// <summary>
        /// ���ͳ�ʱʱ��
        /// </summary>
        public int SendTimeOut { get; set; } = 500;

        /// <summary>
        /// ���ճ�ʱʱ��
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 500;

        /// <summary>
        /// ���ӳ�ʱʱ��
        /// </summary>
        public int ConnectTimeOut { get; set; } = 2000;

        /// <summary>
        /// ���ȴ�ʱ��
        /// </summary>
        public int MaxWaitTime { get; set; } = 2000;

        //������
        private SimpleHybirdLock simpleHybirdLock = new SimpleHybirdLock();

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public OperateResult Connect(string ip, int port)
        {
            //ʵ����TcpClient����
            this.tcpClient = new TcpClient();

            try
            {
                this.tcpClient.ConnectAsync(IPAddress.Parse(ip), port).Wait(ConnectTimeOut);

                if (this.tcpClient.Connected)
                {
                    //���ó�ʱʱ��
                    this.tcpClient.SendTimeout = this.SendTimeOut;
                    this.tcpClient.ReceiveTimeout = this.ReceiveTimeOut;
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailResult("���ӳ�ʱ");
                }
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// �Ͽ�����
        /// </summary>
        public void DisConnect()
        {
            if (this.tcpClient != null)
            {
                try
                {
                    this.tcpClient.Close();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        /// <summary>
        /// ���Ͳ����շ���
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {
            simpleHybirdLock.Enter();

            MemoryStream memoryStream = new MemoryStream();

            try
            {
                this.tcpClient.GetStream().Write(request, 0, request.Length);

                DateTime start = DateTime.Now;

                byte[] buffer = new byte[1024];

                while (true)
                {
                    if (this.tcpClient.Available > 0)
                    {
                        int count = this.tcpClient.GetStream().Read(buffer, 0, this.tcpClient.Available);

                        memoryStream.Write(buffer, 0, count);
                    }
                    else
                    {
                        //�ж��Ƿ�ʱ
                        if ((DateTime.Now - start).TotalMilliseconds > this.MaxWaitTime)
                        {
                            memoryStream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("����ʱ");
                        }
                        //��ȡ����
                        else if (memoryStream.Length > 0)
                        {
                            break;
                        }
                        //������ȡ
                        else
                        {
                            continue;
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
