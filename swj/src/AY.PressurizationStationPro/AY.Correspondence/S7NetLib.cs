using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;
using xbd.s7netplus;
using S7DataType = xbd.s7netplus.DataType;

namespace AY.Correspondence
{
    public class S7NetLib
    {
        private Plc siemens;
        private static object lockobj = new object();

        public CpuType CpuType { get; set; }
        public string IPAddress { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }

        public S7NetLib(CpuType cpuType, string ipAddress, short rack, short slot)
        {
            CpuType = cpuType;
            IPAddress = ipAddress;
            Rack = rack;
            Slot = slot;
        }

        public OperateResult Connect()
        {
            try
            {
                if (this.siemens != null && this.siemens.IsConnected)
                {
                    this.siemens.Close();
                }
                siemens = new Plc(CpuType, IPAddress, Rack, Slot);
                siemens.Open();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// 断开与PLC的连接
        /// </summary>
        /// <returns></returns>
        public OperateResult Disconnect()
        {
            try
            {
                if (this.siemens != null && this.siemens.IsConnected)
                {
                    this.siemens.Close();
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }

        /// <summary>
        /// 读取字节
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="db"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public OperateResult<byte[]> ReadByteArray(S7DataType dataType, int db, int start, int count)
        {
            if (this.siemens != null && siemens.IsConnected)
            {
                try
                {
                    lock (lockobj)
                    {
                        var data = siemens.ReadBytes(dataType, db, start, count);
                        return OperateResult.CreateSuccessResult(data);
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
        /// 读取变量值
        /// </summary>
        /// <param name="varAddress"></param>
        /// <returns></returns>
        public OperateResult<object> ReadVariable(string varAddress)
        {
            if (this.siemens != null && siemens.IsConnected)
            {
                try
                {
                    lock (lockobj)
                    {
                        var data = siemens.Read(varAddress);
                        return OperateResult.CreateSuccessResult(data);
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
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="startByteAdr"></param>
        /// <returns></returns>
        public OperateResult<T> ReadClass<T>(int db, int startByteAdr) where T : class
        {
            if (this.siemens != null && siemens.IsConnected)
            {
                try
                {
                    lock (lockobj)
                    {
                        var data = siemens.ReadClass<T>(db, startByteAdr);
                        return OperateResult.CreateSuccessResult(data);
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



        public OperateResult WriteVariable(string varAddress, object value)
        {
            if (this.siemens != null && siemens.IsConnected)
            {
                try
                {
                    lock (lockobj)
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
