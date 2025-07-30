using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    public class ModbusTCPGroup : GroupBase
    {
        /// <summary>
        /// Modbus存储区
        /// </summary>
        public ModbusStore StoreArea { get; set; }

        /// <summary>
        /// 起始地址
        /// </summary>
        public ushort Start { get; set; }
        /// <summary>
        /// 读取长度
        /// </summary>
        public ushort Length { get; set; }

        /// <summary>
        /// 通信变量集合
        /// </summary>
        public List<ModbusTCPVariable> VarList { get; set; } = new List<ModbusTCPVariable>();

    }
}
