using AY.WarehouseTH.DAL;
using AY.WarehouseTH.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.WarehouseTH.BLL
{
    public class SysAlarmBLL
    {
        private SysAlarmDAL dal = new SysAlarmDAL();
        public int AddSysAlarm(SysAlarm data)
        {
            return dal. AddSysAlarm(data);

        }
    }
}
