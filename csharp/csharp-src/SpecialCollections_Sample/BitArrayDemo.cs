using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCollections_Sample
{
    class BitArrayDemo
    {
        public static void Run()
        {
            //创建一个包含8位的数组，其索引是0~7
            var bits1 = new BitArray(8);
            //把8位都设置为true
            bits1.SetAll(true);
            //把对应于1的位设置为false
            bits1.Set(1, false);
            bits1[5] = false;
            bits1[7] = false;
            DisplayBits(bits1); 
            Console.WriteLine(); 

            //Not()方法对所有的位取反
            bits1.Not();
            DisplayBits(bits1);
            Console.WriteLine();

            var bits2 = new BitArray(bits1);
            bits2[0] = true;
            bits2[1] = false;
            bits2[4] = true;
            DisplayBits(bits1);
            Console.Write (" Or ");
            DisplayBits(bits2);
            Console.Write (" = ");
            //比较两个数组上的同一个位置上的位，如果有一个为true，结果就为true
            bits1.Or(bits2);
            DisplayBits(bits1);
            Console.WriteLine();

            DisplayBits(bits2);
            Console.Write(" and ");
            DisplayBits(bits1);
            Console.Write (" = " );
            //如果两个数组上的同一个位置的位都为true，结果才为true
            bits2.And(bits1);
            DisplayBits(bits2);
            Console.WriteLine();

            DisplayBits(bits1);
            Console.Write (" xor ");
            DisplayBits(bits2);
            //比较两个数组上的同一个位置上的位，只有一个（不能是二个）设置为1，结果才是1
            bits1.Xor(bits2);
            Console.Write(" = ");
            DisplayBits(bits1);
            Console.WriteLine();
        }

        public static void DisplayBits(BitArray bits)
        {
            foreach (bool bit in bits)
            {
                Console.Write(bit ? 1 : 0);
            }
        }
    }
}
