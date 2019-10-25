using System;
using System.Collections.Generic;

namespace Collections_Sample
{
    internal class SetDemo
    {
        public static void Run()
        {
            var hsA = new HashSet<string>() { "one", "two", "three" };
            var hsB = new HashSet<string>() { "two", "three", "four" };
            if (hsA.Add("five"))
            {
                Console.WriteLine("添加了five");
            }
            if (!hsA.Add("two"))
            {
                Console.WriteLine("已经存在了two");
            }
            var hsM = new HashSet<string>() { "one", "two", "three", "four", "five", "six" };

            //hsA的每个元素是否都包含在hsB中
            if (hsA.IsSubsetOf(hsB))//false
            {
                Console.WriteLine("hsA是hsB的子集");
            }
            if (hsA.IsSubsetOf(hsM))//true
            {
                Console.WriteLine("hsA是hsM的子集");
            }
            //hsA是否是hsB的超集
            if (hsA.IsSupersetOf(hsB))//false
            {
                Console.WriteLine("hsA是hsB的超集");
            }
            if (hsM.IsSupersetOf(hsB))//true
            {
                Console.WriteLine("hsM是hsB的超集");
            }
            //判断hsA是否与hsB有公共元素
            if (hsA.Overlaps(hsB))//true
            {
                Console.WriteLine("hsA与hsB包含共同元素");
            }

            var allhs = new SortedSet<string>(hsA);
            allhs.UnionWith(hsB);
            allhs.UnionWith(hsM);
            foreach (var n in allhs)
            {
                Console.Write(n + "\t");
            }
            var ex = new HashSet<string>() { "five", "three" };
            //删除ex包含的元素
            allhs.ExceptWith(ex);
            Console.WriteLine();
            Console.WriteLine("删除后：");
            foreach (var n in allhs)
            {
                Console.Write(n + "\t");
            }
        }
    }
}