






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
    /// 通信类
    /// </summary>
    public class SiementsS7
    {
        /// <summary>
        /// 私有字段
        /// </summary>
        private Plc siemens;

        /// <summary>
        /// 公有属性
        /// </summary>
        public CpuType CpuType { get; set; }
        public string IPAddress { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SiementsS7()
        {

        }

        /// <summary>
        /// 有参构造函数
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
        /// 锁标志位
        /// </summary>
        private static object objLock = new object();

        /// <summary>
        /// 建立PLC连接
        /// </summary>
        /// <returns></returns>
        public OperateResult Connect()
        {
            try
            {
                //如果已经连接，先断开再重新连接
                if (this.siemens != null && this.siemens.IsConnected)
                {
                    this.siemens.Close();
                }
                //连接PLC
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
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (this.siemens != null && this.siemens.IsConnected)
            {
                this.siemens.Close();
            }
        }

        /// <summary>
        /// 读取字节数组
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
                    return OperateResult.CreateFailResult<byte[]>("读取失败：" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>("请检查PLC连接是否正常");
            }
        }

        /// <summary>
        /// 读取单个变量
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
                    return OperateResult.CreateFailResult<object>("读取失败：" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<object>("请检查PLC连接是否正常");
            }
        }


        /// <summary>
        /// 读取类对象
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
                    return OperateResult.CreateFailResult<T>("读取失败：" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult<T>("请检查PLC连接是否正常");
            }
        }

        /// <summary>
        /// 单个变量写入
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
                    return OperateResult.CreateFailResult("读取失败：" + ex.Message);
                }
            }
            else
            {
                return OperateResult.CreateFailResult("请检查PLC连接是否正常");
            }
        }
    }
}
