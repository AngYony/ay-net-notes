//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
//信必达，版权所有，关注微信公众号：上位机Guide
using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSettings
{
    public class ModbusRTUVariable : VariableBase
    {
        /// <summary>
        /// 对应的通信组的从站地址
        /// </summary>
        [ExcelIgnore]
        public byte SlaveId { get; set; }
    }
}
