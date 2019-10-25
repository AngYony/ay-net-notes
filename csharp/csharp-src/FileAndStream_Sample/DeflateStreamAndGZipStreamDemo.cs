using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace FileAndStream_Sample
{
    internal class DeflateStreamAndGZipStreamDemo
    {
        //压缩文件
        public static void CompressFile(string fileName, string compressedFileName)
        {
            //读取要压缩的文件
            using (FileStream inputStream = File.OpenRead(fileName))
            {
                //创建要写入的文件
                FileStream outputStream = File.OpenWrite(compressedFileName);

                //创建压缩流，构造方法指明最终写入的文件流
                using (var compressStream = new DeflateStream(outputStream, CompressionMode.Compress))
                {
                    //将读取的文件流写入到压缩流中
                    inputStream.CopyTo(compressStream);
                }
            }
        }

        //解压文件
        public static void DecompressFile(string fileName)
        {
            //创建文件读取流
            FileStream inputStream = File.OpenRead(fileName);
            //这里为了直接输出文件内容使用了MemoryStream，可以换成FileStream用来保存解压后的文件
            using (MemoryStream outputStream = new MemoryStream())
            {
                //解压读取的文件
                using (var compressStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    //将解压流写入到内存流中，以便后续直接输出
                    compressStream.CopyTo(outputStream);
                    //定位内存流的当前位置
                    outputStream.Seek(0, SeekOrigin.Begin);
                    //将内存流的内容使用StreamReader输出文本
                    using (var reader = new StreamReader(outputStream, 
                        Encoding.UTF8, 
                        detectEncodingFromByteOrderMarks: 
                        true, 
                        bufferSize: 4096, 
                        //注意：此参数很有用途
                        leaveOpen: true))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
            }
        }
    }
}