






using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using AY.CommunicationLib.PLC.Siemens;


namespace AY.CommunicationLib.PLC
{
    /// <summary>
    /// ͨ����
    /// </summary>
    public class SiementsS7
    {
        /// <summary>
        /// ˽���ֶ�
        /// </summary>
        private Plc siemens;

        /// <summary>
        /// ��������
        /// </summary>
        public CpuType CpuType { get; set; }
        public string IPAddress { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// �޲ι��캯��
        /// </summary>
        public SiementsS7()
        {

        }

        /// <summary>
        /// �вι��캯��
        /// </summary>
        /// <param name="cpuType"></param>
        /// <param name="ip"></param>
        /// <param name="rack"></param>
        /// <param name="slot"></param>
        public SiementsS7(CpuType cpuType, string ip,int port, short rack, short slot)
        {
            this.CpuType = cpuType;
            this.IPAddress = ip;
            this.Rack = rack;
            this.Slot = slot;
            this.Port = port;
        }

        /// <summary>
        /// ����־λ
        /// </summary>
        private static object objLock = new object();

        /// <summary>
        /// ����PLC����
        /// </summary>
        /// <returns></returns>
        public OperateResult Connect()
        {
            try
            {
                //����Ѿ����ӣ��ȶϿ�����������
                if (this.siemens != null && this.siemens.IsConnected)
                {
                    this.siemens.Close();
                }
                //����PLC
                siemens = new Plc(this.CpuType, this.IPAddress,this.Port, this.Rack, this.Slot);
                siemens.Open();
                return OperateResult.CreateSuccessResult();
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
            if (this.siemens != null && this.siemens.IsConnected)
            {
                this.siemens.Close();
            }
        }

        /// <summary>
        /// ��ȡ�ֽ�����
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="db"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public OperateResult<byte[]> ReadByteArray(Siemens.StoreType dataType, int db, int start, int count)
        {
            if (this.siemens != null && this.siemens.IsConnected)
            {
                try
                {
                    lock (objLock)
                    {
                        return OperateResult.CreateSuccessResult(siemens.ReadBytes(dataType, db, start, count));
                    }
                }
                catch (Exception ex)
                {
                    return OperateResult.CreateFailResult<byte[]>("��ȡʧ�ܣ�" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>("����PLC�����Ƿ�����");
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="varAddress"></param>
        /// <returns></returns>
        public OperateResult<object> ReadVariable(string varAddress)
        {
            if (this.siemens != null && this.siemens.IsConnected)
            {
                try
                {
                    lock (objLock)
                    {
                        return OperateResult.CreateSuccessResult(siemens.Read(varAddress));
                    }
                }
                catch (Exception ex)
                {
                    return OperateResult.CreateFailResult<object>("��ȡʧ�ܣ�" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<object>("����PLC�����Ƿ�����");
            }
        }


        /// <summary>
        /// ��ȡ�����
        /// </summary>
        /// <param name="db"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public OperateResult<T> ReadClass<T>(int db, int start) where T : class
        {
            if (this.siemens != null && this.siemens.IsConnected)
            {
                try
                {
                    lock (objLock)
                    {
                        return OperateResult.CreateSuccessResult(siemens.ReadClass<T>(db, start));
                    }
                }
                catch (Exception ex)
                {
                    return OperateResult.CreateFailResult<T>("��ȡʧ�ܣ�" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<T>("����PLC�����Ƿ�����");
            }
        }

        /// <summary>
        /// ��������д��
        /// </summary>
        /// <param name="varAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public OperateResult WriteVariable(string varAddress, object value)
        {
            if (this.siemens != null && this.siemens.IsConnected)
            {
                try
                {
                    lock (objLock)
                    {
                        siemens.Write(varAddress, value);
                        return OperateResult.CreateSuccessResult();
                    }
                }
                catch (Exception ex)
                {
                    return OperateResult.CreateFailResult("��ȡʧ�ܣ�" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult("����PLC�����Ƿ�����");
            }
        }
    }
}
