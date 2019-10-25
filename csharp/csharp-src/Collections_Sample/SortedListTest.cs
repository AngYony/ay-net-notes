using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections_Sample
{
    class SortedListTest
    {
        public static void Run()
        {
            var mysortedlist = new SortedList<string, string>();
            mysortedlist.Add("one", "一");
            mysortedlist.Add("two", "二");
            mysortedlist.Add("three", "三");
            mysortedlist.Add("four", "四");
            //还可以使用索引的形式添加元素，索引参数是键
            mysortedlist["five"] = "五";
            //修改值
            mysortedlist["three"] = "3";

            foreach (var item in mysortedlist)
            {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }
        }
    }
}
