using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    /// <summary>
    /// 西门子变量类
    /// </summary>
    public class SiemensVariable : VariableBase
    {
        /// <summary>
        /// 变量地址，根据通信组参数、变量类型以及偏移量共同计算出来的
        /// </summary>
        [ExcelIgnore]
        public string VarAddress { get; set; }
    }
}
