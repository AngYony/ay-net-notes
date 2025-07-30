using AY.WarehouseTH.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.WarehouseTH.DAL
{
    public class SysAlarmDAL
    {
        public int AddSysAlarm(SysAlarm  sysAlarm)
        {
            using (var db = SqlSugarHelper.SqlSugarClient)
            {
                return db.Insertable(sysAlarm).ExecuteCommand();
            }
        }
    }
}
