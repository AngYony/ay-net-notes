using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Sample.Model
{
    public class BT_Factor_Detail_AG_MBG
    {
        public int FactorId { get; set; }
        public string CustomerID { get; set; }
        public string ProfitCenter { get; set; }
        public string APCCode { get; set; }
        public string ProductNo { get; set; }
        public string GtnTypeName { get; set; }
        public decimal FactorValue { get; set; }
        public byte Unit { get; set; }

        public int StartMonth { get; set; }
    }
}
