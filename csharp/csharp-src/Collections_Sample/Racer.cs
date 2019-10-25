using System;
using System.Collections.Generic;

namespace Collections_Sample
{
    public class Racer : IComparable<Racer>
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Wins { get; set; }

        public int CompareTo(Racer other)
        {
            int compare = LastName?.CompareTo(other?.LastName) ?? -1;
            if (compare == 0)
            {
                return FirstName?.CompareTo(other?.FirstName) ?? -1;
            }
            return compare;
        }

        public Racer(int id, string firstName, string lastName, string country, int wins)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Wins = wins;
        }

        public Racer(int id, string firstName, string lastName, string country)
            : this(id, firstName, lastName, country, wins: 0) { }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        //public string ToString(string format, IFormatProvider formatProvider)
        //{
        //    if (format == null) format = "N";
        //    switch (format.ToUpper())
        //    {
        //        case "N":
        //            return ToString();

        //        case "F":
        //            return FirstName;

        //        case "L":
        //            return LastName;

        //        case "W":
        //            return $"{ToString()},Wins:{Wins}";

        //        case "C":
        //            return $"{ToString()},Country:{Country}";

        //        case "A":
        //            return $"{ToString()},Country:{Country} Wins:{Wins}";

        //        default:
        //            throw new FormatException(string.Format(formatProvider, $"Format {format} is not supproted"));
        //    }
        //}

        //public string ToString(string format) => ToString(format, null);
    }

    public class RacerComparer : IComparer<Racer>
    {
        public enum CompareType
        {
            FirstName,
            LastName,
            Country,
            Wins
        }

        private CompareType _compareType;

        public RacerComparer(CompareType compareType)
        {
            _compareType = compareType;
        }

        public int Compare(Racer x, Racer y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            int result;
            switch (_compareType)
            {
                case CompareType.FirstName:
                    return string.Compare(x.FirstName, y.FirstName);
                case CompareType.LastName:
                    return string.Compare(x.LastName, y.LastName);
                case CompareType.Country:
                    result = string.Compare(x.Country, y.Country);
                    if (result == 0)
                        return string.Compare(x.LastName, y.LastName);
                    else return result;
                case CompareType.Wins:
                    return x.Wins.CompareTo(y.Wins);
                default:
                    throw new ArgumentException("Invalid Compare Type");
            }
        }
    }

    public class Racer_Program
    {
        public static void Run()
        {
            List<int> intList = new List<int>();
            //获取初始容量大小
            Console.WriteLine("初始容量大小：" + intList.Capacity);
            intList.Add(1);
            Console.WriteLine($"添加了一个元素后，容量大小为：{intList.Capacity}");
            //获取或设置该内部数据结构在不调整大小的情况下能够容纳的元素总数
            intList.Capacity = 5;
            Console.WriteLine("设置了指定的容量大小为5后：" + intList.Capacity);
            intList.AddRange(new[] { 2, 3, 4, 5, 6 });
            Console.WriteLine($"添加了{intList.Count}个元素后，容量大小为：{intList.Capacity}");
            intList.TrimExcess();
            Console.WriteLine("调用了TrimExcess()方法后，容量大小为：" + intList.Capacity);
            Console.WriteLine($"原来的元素个数为：{intList.Count} 容量大小为：" + intList.Capacity);
            intList.TrimExcess();
            Console.WriteLine("调用了TrimExcess()方法后，容量大小为：" + intList.Capacity);
            //重新调整容量大小，未使用容量小于总容量10%
            intList.Capacity = 7;
            intList.TrimExcess();
            Console.WriteLine($"最终元素个数为：{intList.Count} 容量大小为：" + intList.Capacity);

            var racers = new List<Racer>();

            intList = new List<int>() { 1, 2, 3 };
            intList = new List<int> { 4, 5, 6 };
            intList.Add(7);
            intList.AddRange(new int[] { 7, 8, 9 });
            intList.Insert(2, 0);
            foreach (var i in intList)
            {
                Console.Write(i + "\t");
            }
            //删除元素
            intList.RemoveAt(2);//删除索引2的元素
            intList.Remove(7);//删除元素7
            intList.RemoveRange(4, 2);//删除索引为4及之后的2个元素
            intList.RemoveAll(a => a > 5); //删除大于5的
            Console.WriteLine("");
            foreach (var i in intList)
            {
                Console.Write(i + "\t");
            }

            //搜索元素
        }

        public static void Run2()
        {
            var racers = new List<Racer> {
                new Racer(1,"zhang","bsan","中国"),
                new Racer(3,"li","asi","中国"),
                new Racer(2,"wang","dwu","中国")
            };
            //sort()无参排序
            racers.Sort();
            //传递对象排序         
            racers.Sort(new RacerComparer(RacerComparer.CompareType.FirstName));
            //委托排序
            racers.Sort((r1, r2) => r1.Id.CompareTo(r2.Id));
            foreach(var r in racers)
            {
                Console.WriteLine(r.ToString());
            }
        }
    }
}