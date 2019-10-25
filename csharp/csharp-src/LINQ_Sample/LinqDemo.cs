using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Sample
{
    class LinqDemo
    {
        static void ExtensionMethods()
        {
            var champions = new List<Racer>(Formulal.GetChampions());
            IEnumerable<Racer> brazilChampions = champions.Where(r => r.Country == "China")
                 .OrderByDescending(r => r.Wins)
            .Select(r => r);

            foreach (Racer r in brazilChampions)
            {
                Console.WriteLine($"{r:A}");
            }
        }
        public static void Run()
        {
            var query = from r in Formulal.GetChampions()
                        where r.Country == "China"
                        orderby r.Wins descending
                        select r;

            foreach (Racer r in query)
            {
                Console.WriteLine($"{r:A}");
            }

            Console.WriteLine();
            ExtensionMethods();
        }

        public static void Run2()
        {
            //var names = new List<string> { "Nino", "Alberto", "Juan", "Mike", "Phil" };
            //var namesWithJ = from n in names
            //where n.StartsWith("J")
            //orderby n
            //select n;
            //Console.WriteLine("以J开头的有：");
            //foreach (string name in namesWithJ)
            //{
            //    Console.WriteLine(name);
            //}

            //names.Add("John");
            //names.Add("Jim");
            //names.Add("Jack");
            //names.Add("Denny");
            //Console.WriteLine("添加元素后:");
            //foreach (string name in namesWithJ)
            //{
            //    Console.WriteLine(name);
            //}



            var names = new List<string> { "Nino", "Alberto", "Juan", "Mike", "Phil" };
            var namesWithJToList = (from n in names
                                    where n.StartsWith("J")
                                    orderby n
                                    select n).ToList();


            Console.WriteLine("调用ToList():");
            foreach (string name in namesWithJToList)
            {
                Console.WriteLine(name);
            }
            names.Add("John");
            names.Add("Jim");
            names.Add("Jack");
            names.Add("Denny");
            Console.WriteLine("添加了新的元素后：");
            foreach (string name in namesWithJToList)
            {
                Console.WriteLine(name);
            }
        }

        //where 条件筛选
        public static void WhereRun()
        {
            var racrers = from r in Formulal.GetChampions()
                          where r.Wins > 15 && (r.Country == "China" || r.Country == "UK")
                          select r;
            foreach (var r in racrers)
            {
                Console.WriteLine($"{r:A}");
            }


            Console.WriteLine("使用LINQ扩展方法:");

            var racres2 = Formulal.GetChampions()
                .Where(r => r.Wins > 15 && (r.Country == "China" || r.Country == "UK"))
                .Select(r => r);
            foreach (var r in racres2)
            {
                Console.WriteLine($"{r:A}");
            }

            Console.WriteLine("使用Where索引方法：");
            //使用索引筛选，只能使用扩展方法，不能使用Linq查询语句
            var racers3 = Formulal.GetChampions()
                .Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
            foreach (var r in racers3)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        //类型筛选
        public static void OfTypeRun()
        {
            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            foreach (var s in query)
            {
                Console.WriteLine(s);
            }
        }

        //复合的from子句
        public static void FromRun()
        {
            //Formulal.GetChampions()返回Racer集合，每一个Racer的属性Cars是一个字符串数组
            var ferrariDrivers = from r in Formulal.GetChampions()
                                 from c in r.Cars
                                 where c == "Ferrari"
                                 orderby r.LastName
                                 select r.FirstName + " " + r.LastName;

            foreach (string f in ferrariDrivers)
            {
                Console.WriteLine(f);
            }

            Console.WriteLine("使用LINQ扩展方法:");

            var ferrariDrivers2 = Formulal.GetChampions()
                .SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c })
                .Where(r => r.Car == "Ferrari")
                .OrderBy(r => r.Racer.LastName)
                .Select(r => r.Racer.FirstName + " " + r.Racer.LastName);


        }

        //排序
        public static void OrderByRun()
        {
            var racers = from r in Formulal.GetChampions()
                         where r.Country == "China"
                         orderby r.Wins descending, r.Country ascending
                         select r;
            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
            Console.WriteLine();
            Console.WriteLine("使用LINQ扩展方法:");


            var racers2 = Formulal.GetChampions()
                .Where(r => r.Country == "China")
                .OrderByDescending(r => r.Wins)
                .Select(r => r);

            foreach (var r in racers2)
            {
                Console.WriteLine($"{r:A}");
            }
            Console.WriteLine();
            Console.WriteLine("=======================");

            //多个排序
            var racers3 = (from r in Formulal.GetChampions()
                           orderby r.Country, r.LastName, r.FirstName ascending
                           select r).Take(10);

            foreach (var r in racers3)
            {
                Console.WriteLine($"{r:A}");
            }

            Console.WriteLine();
            Console.WriteLine("使用LINQ扩展方法:");

            var racers4 = Formulal.GetChampions()
                .OrderBy(r => r.Country)
                .ThenBy(r => r.LastName)
                .ThenByDescending(r => r.FirstName)
                .Take(10);
            foreach (var r in racers4)
            {
                Console.WriteLine($"{r:A}");
            }
        }
        //分组
        public static void GroupRun()
        {
            var countries = from r in Formulal.GetChampions()
                            group r by r.Country into g //将分组信息放入标识符g中
                            orderby g.Count() descending, g.Key
                            where g.Count() >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = g.Count()
                            };

            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }

            Console.WriteLine();
            Console.WriteLine("使用LINQ扩展方法:");

            var countries2 = Formulal.GetChampions()
                .GroupBy(r => r.Country)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Where(g => g.Count() >= 2)
                .Select(g => new { Country = g.Key, Count = g.Count() });

            foreach (var item in countries2)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        //LINQ查询中的变量
        public static void LetRun()
        {
            var countries = from r in Formulal.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count
                            };

            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }

            Console.WriteLine();
            Console.WriteLine("使用LINQ扩展方法:");

            var countries2 = Formulal.GetChampions()
                .GroupBy(r => r.Country)
                .Select(g => new { Group = g, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.Group.Key)
                .Where(g => g.Count >= 2)
                .Select(g => new
                {
                    Country = g.Group.Key,
                    Count = g.Count
                });

            foreach (var item in countries2)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }
        }

        //对嵌套的对象分组
        public static void NestingGroupRun()
        {
            var countries = from r in Formulal.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count,
                                //使用内部子句嵌套
                                Racers = from r1 in g
                                         orderby r1.LastName
                                         select r1.FirstName + " " + r1.LastName
                            };
            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
                foreach (var name in item.Racers)
                {
                    Console.Write(name + ";");
                }
                Console.WriteLine();
            }
        }


        //内连接
        public static void InnerJoinRun()
        {
            var racers = from r in Formulal.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };
            var teams = from t in Formulal.GetContructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams = (from r in racers
                                  join t in teams on r.Year equals t.Year
                                  orderby t.Year
                                  select new
                                  {
                                      r.Year,
                                      Champion = r.Name,
                                      Constructor = t.Name
                                  }).Take(10);
            Console.WriteLine("输出结果：");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}:{item.Champion,-20} {item.Constructor}");
            }

            Console.WriteLine();
            Console.WriteLine(" 将上述合并为一个LINQ查询:");
            var racersAndTeams2 =
                 (from r in from r1 in Formulal.GetChampions()
                            from yr in r1.Years
                            select new
                            {
                                Year = yr,
                                Name = r1.FirstName + " " + r1.LastName
                            }
                  join t in
                  from t1 in Formulal.GetContructorChampions()
                  from yt in t1.Years
                  select new
                  {
                      Year = yt,
                      Name = t1.Name
                  }
                  on r.Year equals t.Year
                  orderby t.Year
                  select new
                  {
                      Year = r.Year,
                      Champion = r.Name,
                      Constructor = t.Name
                  }).Take(10);

            Console.WriteLine("输出结果：");
            foreach (var item in racersAndTeams2)
            {
                Console.WriteLine($"{item.Year}:{item.Champion,-20} {item.Constructor}");
            }
        }

        //左外联接
        public static void LeftOutJoinRun()
        {
            var racers = from r in Formulal.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };
            var teams = from t in Formulal.GetContructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams = (from r in racers
                                  join t in teams on r.Year equals t.Year into rt
                                  from t in rt.DefaultIfEmpty()
                                  orderby r.Year
                                  select new
                                  {
                                      r.Year,
                                      Champion = r.Name,
                                      Constructor = t == null ? "no constructor" : t.Name
                                  }).Take(10);
            Console.WriteLine("输出结果：");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}:{item.Champion,-10} {item.Constructor}");
            }
        }

        //组联接
        public static void ZuJoinRun()
        {
            var racers = Formulal.GetChampionships()
                .SelectMany(cs => new List<RacerInfo>()
                {
                    new RacerInfo
                    {
                        Year=cs.Year,
                        Positon=1,
                        FirstName=cs.First.FirstName(),
                        LastName=cs.First.LastName()
                    },
                    new RacerInfo
                    {
                        Year=cs.Year,
                        Positon=2,
                        FirstName=cs.Second.FirstName(),
                        LastName=cs.Second.LastName()
                    },
                    new RacerInfo
                    {
                        Year=cs.Year,
                        Positon=3,
                        FirstName=cs.Third.FirstName(),
                        LastName=cs.Third.LastName()
                    }
                });

            foreach (var r in racers)
            {
                Console.WriteLine(r.FirstName + " " + r.LastName);

            }


            Console.WriteLine();
            Console.WriteLine("Linq查询：");


            var q = (from r in Formulal.GetChampions()
                     join r2 in racers on
                     new
                     {
                         FirstName = r.FirstName,
                         LastName = r.LastName
                     }
                     equals
                     new
                     {
                         r2.FirstName,
                         r2.LastName
                     }
                     into yearResults
                     select new
                     {
                         r.FirstName,
                         r.LastName,
                         r.Wins,
                         r.Starts,
                         Results = yearResults
                     });

            foreach (var r in q)
            {
                Console.WriteLine(r.FirstName + " " + r.LastName);
                foreach (var results in r.Results)
                {
                    Console.WriteLine(results.Year + " " + results.Positon);
                }
            }

        }
        //集合操作
        public static void CollectionsRun()
        {
            Func<string, IEnumerable<Racer>> racersByCar =
                car => from r in Formulal.GetChampions()
                       from c in r.Cars
                       where c == car
                       orderby r.LastName
                       select r;

            Console.WriteLine("调用Intersect()方法，显示结果：");
            foreach (var racer in racersByCar("Ferrari").Intersect(racersByCar("Lotus")))
            {
                Console.WriteLine(racer);
            }

        }
        private static IEnumerable<Racer> GetRacersByCar(string car)
        {
            var ferrariDrivers = from r in Formulal.GetChampions()
                                 from c in r.Cars
                                 where c == car
                                 orderby r.LastName
                                 select r;
            return ferrariDrivers;
        }

        //合并
        public static void ZipRun()
        {
            var racernames = from r in Formulal.GetChampions()
                             where r.Country == "Italy"
                             orderby r.Wins descending
                             select new
                             {
                                 Name = r.FirstName + " " + r.LastName
                             };
            var racerNamesAndStarts = from r in Formulal.GetChampions()
                                      where r.Country == "Italy"
                                      orderby r.Wins descending
                                      select new
                                      {
                                          LastName = r.LastName,
                                          Starts = r.Starts
                                      };
            //第一个集合中的第一项会与第二个集合中的第一项合并
            //第一个集合中的第二项会与第二个集合中的第二项合并，依次类推
            //如果两个序列的项数不同，Zip()方法就在到达较小集合的末尾时停止
            var racers = racernames.Zip(racerNamesAndStarts
                , (first, second) => first.Name + ", starts: " + second.Starts);

            foreach (var r in racers)
            {
                Console.WriteLine(r);
            }
        }

        //分区
        public static void TakeAndSkipRun()
        {
            int pageSize = 5;
            int numberPages = (int)Math.Ceiling(Formulal.GetChampions().Count() / (double)pageSize);

            for (int page = 0; page < numberPages; page++)
            {
                Console.WriteLine("Page " + page);

                var racers = (from r in Formulal.GetChampions()
                              orderby r.LastName, r.FirstName
                              select r.FirstName + " " + r.LastName)
                            .Skip(page * pageSize).Take(pageSize);

                foreach (var name in racers)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }

        //聚合操作费
        public static void JuheRun()
        {
            Console.WriteLine("Count():");
            var query = from r in Formulal.GetChampions()
                        let numberYears = r.Years.Count()
                        where numberYears >= 3
                        orderby numberYears descending, r.LastName
                        select new
                        {
                            Name = r.FirstName + " " + r.LastName,
                            TimesChampion = numberYears
                        };

            foreach (var r in query)
            {
                Console.WriteLine(r.Name + " " + r.TimesChampion);
            }

            Console.WriteLine();
            Console.WriteLine("Sum():");

            var countries = (from c in from r in Formulal.GetChampions()
                                       group r by r.Country into c
                                       select new
                                       {
                                           Country = c.Key,
                                           Wins = (from r1 in c select r1.Wins).Sum()
                                       }
                             orderby c.Wins descending, c.Country
                             select c).Take(5);
            foreach (var country in countries)
            {
                Console.WriteLine(country.Country + " " + country.Wins);
            }
        }

        //转换操作符:ToList
        public static void ToListRun()
        {
            List<Racer> racers = (from r in Formulal.GetChampions()
                                  where r.Starts > 150
                                  orderby r.Starts descending
                                  select r).ToList();

            foreach (var racer in racers)
            {
                Console.WriteLine($"{racer} {racer:S}");
            }
        }
        //转换操作符:ToLookup
        public static void ToLookupRun()
        {
            var racers = (from r in Formulal.GetChampions()
                          from c in r.Cars
                          select new
                          {
                              Car = c,
                              Racer = r
                          })
                          .ToLookup(cr => cr.Car, cr => cr.Racer);
            if (racers.Contains("Williams"))
            {
                foreach (var winll in racers["Williams"])
                {
                    Console.WriteLine(winll);
                }
            }
        }

        //转换操作符:Cast
        public static void CastRun()
        {
            var list = new System.Collections.ArrayList(Formulal.GetChampions()
                as System.Collections.ICollection);
            
            var query = from r in list.Cast<Racer>()
                        where r.Country == "China"
                        orderby r.Wins descending
                        select r;
            foreach (var r in query)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        //生成操作符
        public static void BuildRun()
        {
            var values = Enumerable.Range(1, 20);
            foreach (var item in values)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
