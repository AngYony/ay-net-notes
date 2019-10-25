using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndStream_Sample
{
    class BinaryReaderAndWriterDemo
    {
        public static void WriteFileUsingBinaryWriter(string binFile)
        {
            var outputStream = File.Create(binFile);
            using (var writer = new BinaryWriter(outputStream))
            {
                double d = 47.47;
                int i = 42;
                long l = 987654321;
                string s = "sample";
                writer.Write(d);
                writer.Write(i);
                writer.Write(l);
                writer.Write(s);
            }
        }


        public static void ReadFileUsingBinaryReader(string binFile)
        {
            var inputStream = File.Open(binFile, FileMode.Open);
            using(var reader=new BinaryReader(inputStream))
            {
                //读取并定位
                double d= reader.ReadDouble();
                int i = reader.ReadInt32();
                long l = reader.ReadInt64();
                string s = reader.ReadString();
                Console.WriteLine($"d:{d} \t i:{i} \t l:{l} \t s:{s} ");

               
            }
        }


     
    }
}
