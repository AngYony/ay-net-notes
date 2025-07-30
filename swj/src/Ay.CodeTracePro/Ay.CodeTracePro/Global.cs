using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.NodeSettings;

namespace Ay.CodeTracePro
{
    public class Global
    {
        public static List<SiemensDevice> A3A4 { get; set; }
        public static List<ModbusTCPDevice> A5A6 { get; set; }
    }
}
