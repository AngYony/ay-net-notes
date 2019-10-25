using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndStream_Sample
{
    class StreamWriterAndReaderDemo
    {
        public static void ReadFileUsingReader(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            //该构造方法接收一个Stream对象
            using (var reader = new StreamReader(stream))
            {
                //当前流位置是否在流末尾
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }


        public static void WriteFileUsingWriter(string fileName,string [] lines)
        {
            var outputStream = File.OpenWrite(fileName);
            using(var writer=new StreamWriter(outputStream))
            {
                //获取UTF-8代表的序言转换的字节数组
                byte[] preable = Encoding.UTF8.GetPreamble();
                //先写入编码格式
                outputStream.Write(preable, 0, preable.Length);
                //在写入字符串数组
                writer.Write(lines);
            }
        }
    }
}
