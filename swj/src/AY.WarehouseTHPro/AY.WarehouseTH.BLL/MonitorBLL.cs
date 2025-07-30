using AY.WarehouseTH.DAL;
using AY.WarehouseTH.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace AY.WarehouseTH.BLL
{
    public class MonitorBLL
    {
        private MonitorDAL dal = new MonitorDAL();
        public int AddMonitorData(MonitorData data)
        {
            return dal.AddMonitorData(data);

        }
    }
}
