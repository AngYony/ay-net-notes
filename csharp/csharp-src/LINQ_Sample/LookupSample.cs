using LINQ_Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Sample
{
    public class LookupSample
    {
        public static void Test()
        {
            List<BT_Factor_Detail_AG_MBG> factorList = new List<BT_Factor_Detail_AG_MBG>{
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="A", FactorId=1, FactorValue=10},
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="C", FactorId=2, FactorValue=20},
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="B", FactorId=3, FactorValue=30},
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="A", FactorId=4, FactorValue=40},
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="B", FactorId=5, FactorValue=50},
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="D", FactorId=6, FactorValue=60},
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="B", FactorId=7, FactorValue=70},
                new BT_Factor_Detail_AG_MBG(){ GtnTypeName="D", FactorId=8, FactorValue=80},

            };

            var gtntype_factor = factorList.ToLookup(a => a.GtnTypeName);
            //等同于下面的语句，原因是ILookup派生自IEnumerable<IGrouping<TKey, TElement>>
            //gtntype_factor = factorList.GroupBy(a => a.GtnTypeName);

            foreach (IGrouping<string, BT_Factor_Detail_AG_MBG> factors in gtntype_factor)
            {
                string gtntype = factors.Key;
                Console.WriteLine("GTNType:" + gtntype);

                foreach (BT_Factor_Detail_AG_MBG y in factors)
                {
                    Console.WriteLine("  FactorId:" + y.FactorId.ToString());

                }
            }

            Console.ReadLine();
        }
    }
}
