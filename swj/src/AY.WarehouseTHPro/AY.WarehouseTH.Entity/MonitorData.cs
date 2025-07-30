using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.WarehouseTH.Entity
{
    public class MonitorData
    {
        [SugarColumn(IsPrimaryKey =true)]
        public DateTime InsertTime { get; set; }


        public string A01Temp { get; set; } 
        public string A01Humidity { get; set; }

        public string A02Temp { get; set; }
        public string A02Humidity { get; set; }

        public string A03Temp { get; set; }
        public string A03Humidity { get; set; }


        public string B01Temp { get; set; }
        public string B01Humidity { get; set; }

        public string B02Temp { get; set; }
        public string B02Humidity { get; set; }

        public string B03Temp { get; set; }
        public string B03Humidity { get; set; }


    }
}
