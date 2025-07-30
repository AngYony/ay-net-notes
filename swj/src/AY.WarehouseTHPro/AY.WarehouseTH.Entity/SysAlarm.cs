using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.WarehouseTH.Entity
{
    public class SysAlarm
    {
        [SugarColumn(IsPrimaryKey =true)]
        public DateTime AlarmTime { get; set; }

        public string AlarmNote { get; set; }
        public string AlarmValue { get; set; }
        public string AlarmSet { get; set; }
        public string AlarmType { get; set; } // 0:温度报警, 1:湿度报警, 2:设备离线报警
        public string Operator { get; set; } // 操作人
    }
}
