using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace AY.NodeSettings
{
    /// <summary>
    /// 一个组对应多个变量
    /// </summary>
    public class VariableBase
    {

        /// <summary>
        /// 变量名称
        /// </summary>
        [ExcelColumnName("变量名称")]
        public string VarName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [ExcelColumnName("数据类型")]
        public DataType DataType { get; set; }
        /// <summary>
        /// 变量地址
        /// </summary>
        [ExcelColumnName("变量地址")]
        public string Address { get; set; }
        /// <summary>
        /// 比例系数
        /// </summary>
        [ExcelColumnName("比例系数")]
        public float Scale { get; set; }
        //偏移量
        [ExcelColumnName("偏移量")]
        public float Offset { get; set; }
        /// <summary>
        /// 高报警
        /// </summary>
        [ExcelColumnName("高报警")]
        public bool HAlarm { get; set; }

        /// <summary>
        /// 高报警值
        /// </summary>
        [ExcelColumnName("高报警值")]
        public float HAlarmValue { get; set; }
        /// <summary>
        /// 高报警说明
        /// </summary>
        [ExcelColumnName("高报警说明")]
        public string HAlarmNote { get; set; }


        /// <summary>
        /// 低报警
        /// </summary>
        [ExcelColumnName("低报警")]
        public bool LAlarm { get; set; }

        /// <summary>
        /// 低报警限值
        /// </summary>
        [ExcelColumnName("低报警值")]
        public float LAlarmValue { get; set; }
        /// <summary>
        /// 低报警说明
        /// </summary>
        [ExcelColumnName("低报警说明")]
        public string LAlarmNote { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        [ExcelIgnore]
        public object VarValue { get; set; }

        /// <summary>
        /// 高报警缓存值
        /// </summary>
        [ExcelIgnore]
        public float HCacheValue { get; set; } = float.MinValue;

        /// <summary>
        /// 低报警缓存值
        /// </summary>
        [ExcelIgnore]
        public float LCacheValue { get; set; } = float.MaxValue;

    }
}
