using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace AY.CommunicationLib
{
    //����ͨ�������
    public class SerialBase
    {
        /// <summary>
        /// ����δ���ʱ������ʱ֮ǰ�ĺ�����
        /// </summary>
        public int ReadTimeOut { get; set; } = 500;

        /// <summary>
        /// ��ȡ������д�����δ���ʱ������ʱ֮ǰ�ĺ�������
        /// </summary>
        public int WriteTimeOut { get; set; } = 500;

        /// <summary>
        /// ��ֹճ����ʱ����������
        /// </summary>
        public int SleepTime { get; set; } = 1;

        /// <summary>
        /// ��ȡ���������ۼƳ�ʱʱ��
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 3000;

        //����һ������ͨ�Ŷ���
        private SerialPort serialPort = null;

        //����һ������������
        private SimpleHybirdLock hybirdLock = new SimpleHybirdLock();

        /// <summary>
        /// �򿪴��ڷ���
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate">�����ʣ�9600/19200/38400/115200��</param>
        /// <param name="parity">У��λ</param>
        /// <param name="dataBits">����λ</param>
        /// <param name="stopBits">ֹͣλ</param>
        /// <returns></returns>
        public OperateResult Open(
            string portName,
            int baudRate = 9600,
            Parity parity = Parity.None,
            int dataBits = 8,
            StopBits stopBits = StopBits.One)
        {
            //�жϴ����Ƿ��Ѿ���
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }

            //ʵ��������ͨ�Ŷ���
            this.serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
            {
                //���ô��ڵĶ�ȡ��д�볬ʱʱ��
                ReadTimeout = this.ReadTimeOut,
                WriteTimeout = this.WriteTimeOut
            };

            try
            {
                //�򿪴���
                this.serialPort.Open();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// �رմ���
        /// </summary>
        public void Close()
        {
            if (this.serialPort != null && this.serialPort.IsOpen)
            {
                this.serialPort.Close();
            }
        }

        /// <summary>
        /// ���ڷ��Ͳ���������
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {
            MemoryStream stream = new MemoryStream();
            hybirdLock.Enter();
            try
            {
                //���ͱ���
                this.serialPort.Write(request, 0, request.Length);

                DateTime start = DateTime.Now;
                //���ձ���
                byte[] buffer = new byte[1024];
                while (true)
                {
                    Thread.Sleep(this.SleepTime);//��ֹ����ճ�������
                    //��������ݶ�ȡ
                    if (this.serialPort.BytesToRead > 0)
                    {
                        //��¼������ȡ���ֽ���
                        int bytesRead = this.serialPort.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            //����ȡ��������д�뵽�ڴ�����
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                    else
                    {
                        //�ж��Ƿ�ʱ
                        if ((DateTime.Now - start).TotalMilliseconds > this.ReceiveTimeOut)
                        {
                            stream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("����ʱ");
                        }
                        else if (stream.Length > 0)
                        {
                            break; //û�и������ݿɶ����˳�ѭ��
                        }
                    }
                }

                byte[] response = stream.ToArray();
                stream.Dispose();
                return OperateResult.CreateSuccessResult(response);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<byte[]>("����ͨ���쳣:" + ex.Message);
            }
            finally
            {
                hybirdLock.Leave();
                //if (serialPort != null && serialPort.IsOpen)
                //{
                //    serialPort.DiscardInBuffer(); //������뻺����
                //    serialPort.DiscardOutBuffer(); //������������
                //}
            }
        }
    }
}