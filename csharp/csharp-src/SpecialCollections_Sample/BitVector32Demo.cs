using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCollections_Sample
{
    class BitVector32Demo
    {

        public static void Run()
        {
            //使用默认构造函数创建一个BitVactor32结构，默认每一位都是false。
            var bits1 = new BitVector32();
            //调用CreateMask()方法创建用来访问第一位的一个掩码，bit1被设置为1
            int bit1 = BitVector32.CreateMask();
            //再次调用CreateMask()方法，并将一个掩码作为参数进行传递，返回第二位掩码 
            int bit2 = BitVector32.CreateMask(bit1);
            int bit3 = BitVector32.CreateMask(bit2);
            int bit4 = BitVector32.CreateMask(bit3);
            int bit5 = BitVector32.CreateMask(bit4);
            //使用掩码和索引器访问位矢量中的位，并设置值
            bits1[bit1] = true;
            bits1[bit2] = false;
            bits1[bit3] = true;
            bits1[bit4] = true;
            bits1[bit5] = true;
            Console.WriteLine(bits1);
            bits1[0xabcdef] = true;
            Console.WriteLine(bits1);
            
            int received = 0x79abcdef;
            //直接传入十六进制数来创建掩码
            BitVector32 bits2 = new BitVector32(received);
            Console.WriteLine(bits2);

            //分割片段
            BitVector32.Section sectionA = BitVector32.CreateSection(0xfff);
            BitVector32.Section sectionB = BitVector32.CreateSection(0xff, sectionA);
            BitVector32.Section sectionC = BitVector32.CreateSection(0xf, sectionB);
            BitVector32.Section sectionD = BitVector32.CreateSection(0x7, sectionC);
            BitVector32.Section sectionE = BitVector32.CreateSection(0x7, sectionD);
            BitVector32.Section sectionF = BitVector32.CreateSection(0x3, sectionE);
            Console.WriteLine("Section A:" + IntToBinaryString(bits2[sectionA], true));
            Console.WriteLine("Section B:" + IntToBinaryString(bits2[sectionB], true));
            Console.WriteLine("Section C:" + IntToBinaryString(bits2[sectionC], true));
            Console.WriteLine("Section D:" + IntToBinaryString(bits2[sectionD], true));
            Console.WriteLine("Section E:" + IntToBinaryString(bits2[sectionE], true));
            Console.WriteLine("Section F:" + IntToBinaryString(bits2[sectionF], true));


        }

        public static string IntToBinaryString(int bits, bool removeTrailingZero)
        {
            var sb = new StringBuilder(32);
            for (int i = 0; i < 32; i++)
            {
                if ((bits & 0x80000000) != 0)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
                bits = bits << 1;
            }
            string s = sb.ToString();
            if (removeTrailingZero)
            {
                return s.TrimStart('0');
            }
            else
            {
                return s;
            }
        }
    }
}

