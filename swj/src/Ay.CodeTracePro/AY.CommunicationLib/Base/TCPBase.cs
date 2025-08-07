using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace AY.CommunicationLib
{
    /// <summary>
    /// TCPͨ�������
    /// </summary>
    public class TCPBase
    {
        /// <summary>
        /// ���ͳ�ʱֵ���Ժ���Ϊ��λ����0��ʾ������ʱ
        /// </summary>
        public int SendTimeOut { get; set; } = 500;

        /// <summary>
        /// ���ӵĳ�ʱֵ���Ժ���Ϊ��λ��,0��ʾ������ʱ
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

        //����һ��Tcpͨ�Ŷ���
        private TcpClient tcpClient = null;
        

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="ip">ip��ַ</param>
        /// <param name="port">�˿ں�</param>
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
                this.tcpClient.Close();
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
                        //�ж��Ƿ�ʱ��ע��˴��ĳ�ʱ��õĽ��������ʹ��TokenCancel��ʵ��
                        if ((DateTime.Now - start).TotalMilliseconds > this.MaxWaitTime)
                        {
                            memoryStream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("����ʱ");
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