using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Shared
{
    [Serializable]

    public class SerialPortParam
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }
    }
}
