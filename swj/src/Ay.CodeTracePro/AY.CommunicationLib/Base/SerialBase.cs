
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
    //����ͨ�������
    public class SerialBase
    {
        //����һ������ͨ�Ŷ���
        private SerialPort serialPort = null;

        public int ReadTimeOut { get; set; } = 500;
        public int WriteTimeOut { get; set; } = 500;

        //�ȴ�ʱ��
        public int SleepTime { get; set; } =1;

        //��ʱʱ��
        public int ReceiveTimeOut { get; set; } = 100;

        //����һ������������
        private SimpleHybirdLock hybirdLock = new SimpleHybirdLock();


        /// <summary>
        /// �򿪴��ڷ���
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public OperateResult Open(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            //�жϴ����Ƿ��Ѿ���
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }

            //ʵ��������ͨ�Ŷ���
            this.serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            //���ô��ڵ�����
            this.serialPort.ReadTimeout = ReadTimeOut;
            this.serialPort.ReadTimeout = WriteTimeOut;

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
        /// ���Ͳ�����
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndReceive(byte[] request)
        {
            //����
            hybirdLock.Enter();

            //����һ���ڴ���
            MemoryStream stream = new MemoryStream();

            try
            {
                //���ͱ���
                this.serialPort.Write(request, 0, request.Length);

                //���巢��ʱ��
                DateTime start = DateTime.Now;

                //����һ��Buffer
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
                        //�ж��Ƿ�ʱ
                        if ((DateTime.Now - start).TotalMilliseconds > this.ReceiveTimeOut)
                        {
                            stream.Dispose();
                            return OperateResult.CreateFailResult<byte[]>("����ʱ");
                        }
                        //��ȡ����
                        else if (stream.Length > 0)
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
                //����
                hybirdLock.Leave();
            }
        }
    }
}
