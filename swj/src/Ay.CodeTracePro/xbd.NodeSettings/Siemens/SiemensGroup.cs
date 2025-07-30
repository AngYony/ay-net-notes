using AY.CommunicationLib.PLC.Siemens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    public class SiemensGroup:     GroupBase
    {
        /// <summary>
        /// 存储区
        /// </summary>
        public StoreType StoreArea { get; set; }

        /// <summary>
        /// DB号
        /// </summary>
        public int DBNo { get; set; }

        /// <summary>
        /// 开始字节
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 字节数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 西门子变量集合
        /// </summary>
        public List<SiemensVariable> VarList { get; set; } = new List<SiemensVariable>();

     }
}
