using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.WarehouseTH.Entity
{
    public class SysInfo
    {
        public string PortNameA { get; set; } = "COM31";

        public int BaudRateA { get; set; } = 9600;
        public string ParityA { get; set; } = "None";
        public int DataBitsA { get; set; } = 8;
        public string StopBitsA { get; set; } = "One";

        public string PortNameB { get; set; } = "COM41";
        public int BaudRateB { get; set; } = 9600;
        public string ParityB { get; set; } = "None";
        public int DataBitsB { get; set; } = 8;
        public string StopBitsB { get; set; } = "One";

        public float A01TempH { get; set; }
        public float A01TempL { get; set; }
        public float A01HumidityH { get; set; }
        public float A01HumidityL { get; set; }

        public float A02TempH { get; set; }
        public float A02TempL { get; set; }
        public float A02HumidityH { get; set; }
        public float A02HumidityL { get; set; }

        public float A03TempH { get; set; }
        public float A03TempL { get; set; }
        public float A03HumidityH { get; set; }
        public float A03HumidityL { get; set; }


        public float B01TempH { get; set; }
        public float B01TempL { get; set; }
        public float B01HumidityH { get; set; }
        public float B01HumidityL { get; set; }
                     
        public float B02TempH { get; set; }
        public float B02TempL { get; set; }
        public float B02HumidityH { get; set; }
        public float B02HumidityL { get; set; }
                     
        public float B03TempH { get; set; }
        public float B03TempL { get; set; }
        public float B03HumidityH { get; set; }
        public float B03HumidityL { get; set; }


    }
}
