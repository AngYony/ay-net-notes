using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections_Sample
{
    class LookupDemo
    {
        public static void Run()
        {
            
            var racers = new List<Racer>();
            racers.Add(new Racer(1, "zhang", "san", "zhongguo"));
            racers.Add(new Racer(2, "li", "si", "riben"));
            racers.Add(new Racer(3, "wang", "wu", "zhongguo"));
            racers.Add(new Racer(4, "zhao", "liu", "meiguo"));

            var lookupRacers= racers.ToLookup(r => r.Country);
            foreach (var item in lookupRacers)
            {
                foreach(Racer r  in lookupRacers[item.Key])
                {
                    Console.WriteLine($"{item.Key}:{r.ToString()}");
                }
            }
        }
    }
}
