using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections_Sample
{
    class StackTest
    {
        public static void Run()
        {
            var mystacks = new Stack<int>();
            mystacks.Push(1);
            mystacks.Push(2);
            mystacks.Push(3);

            foreach(var num in mystacks)
            {
                Console.Write(num+"\t");
            }
            Console.WriteLine();
            while (mystacks.Count > 0)
            {
                Console.Write(mystacks.Pop() + "\t");
            }


        }
             
    }
}
