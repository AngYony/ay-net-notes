using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AY.Entity
{
    public class HistoryData
    {
        [ExcelColumnName("日期时间")] //设置Excel列名
        [ExcelFormat("yyyy-MM-dd HH:mm:ss")] //设置Excel日期时间格式
        [ExcelColumnWidth(50)] //设置Excel列宽
        public DateTime InsertTime { get; set; }
        [ExcelColumnName("进口压力")]
        public string PressureIn { get; set; }
        [ExcelColumnName("出口压力")]
        public string PressureOut { get; set; }
        public string TempIn1 { get; set; }
        public string TempIn2 { get; set; }
        public string TempOut { get; set; }
        public string PressureTank1 { get; set; }
        public string PressureTank2 { get; set; }
        public string LevelTank1 { get; set; }
        public string LevelTank2 { get; set; }
        public string PressureTankOut { get; set; }
    }
}
