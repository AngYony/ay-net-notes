using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.NodeSettings
{
    /// <summary>
    /// ModbusRTUGroup通信组
    /// </summary>
    public class ModbusRTUGroup : GroupBase
    {
        /// <summary>
        /// 存储区
        /// </summary>
        public ModbusStore StoreArea { get; set; }
        /// <summary>
        /// 通信组Id
        /// </summary>
        public byte GroupId { get; set; }
        /// <summary>
        /// 开始地址
        /// </summary>
        public ushort Start { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public ushort Length { get; set; }

        public List<ModbusRTUVariable> VarList { get; set; } = new List<ModbusRTUVariable>();

    }
}
