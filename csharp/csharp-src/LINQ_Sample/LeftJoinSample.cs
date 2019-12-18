using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Sample
{
    class LeftJoinSample
    {
        class Factor
        {
            public string CustomerNumber { get; set; }
            public string APCCode { get; set; }
            public string PN { get; set; }

        }


        public void Test()
        {

            List<Factor> leftfactors = new List<Factor>(){
                 new Factor(){ APCCode="A1", CustomerNumber="CN1" , PN="PN1"},
                 new Factor(){ APCCode="A2", CustomerNumber="CN2" , PN="PN2"},
                 new Factor(){ APCCode="A3", CustomerNumber="CN3" , PN="PN3"},
                 new Factor(){ APCCode="A4", CustomerNumber="CN4" , PN="PN4"},
                 new Factor(){ APCCode="A5", CustomerNumber="CN5" , PN="PN5"}

            };


            List<Factor> rightfactors = new List<Factor>(){
                  
                 new Factor(){ APCCode="A2", CustomerNumber="CN3" , PN="PN2"},
                 new Factor(){ APCCode="A4", CustomerNumber="CN4" , PN="PN4"},
                 
            };

            var query = from left in leftfactors
                        join right in rightfactors
                        //on left.APCCode equals right.APCCode into pp

                         on new { a=left.APCCode, b=left.CustomerNumber }
                         equals new {a=right.APCCode, b=right.CustomerNumber }
                         into pp
                         from w in pp.DefaultIfEmpty()
                         where w == null && left.APCCode!="A1"
                         select left;

            var ddd= query.ToList();

            foreach(var d in ddd){
                Console.WriteLine(d.APCCode);
            }

        }
    }
}
