using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndStream_Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            //DriveInfo示例
            //DriveInfoDemo.Run();

            //Path示例
            //PathDemo.GetDocumentsFolder();


            //写入流
            //FileStreamDemo.CreateSampleFileAsync(1000);

            //读取流
            //FileStreamDemo.RandomAccessSample();


            ////写入二进制到文件中
            //BinaryReaderAndWriterDemo.WriteFileUsingBinaryWriter("abc.bin");
            ////读取二进制数据
            //BinaryReaderAndWriterDemo.ReadFileUsingBinaryReader("abc.bin");

            //压缩
            //DeflateStreamAndGZipStreamDemo.CompressFile("samplefile.data", "b.ys");
            //解压
            //DeflateStreamAndGZipStreamDemo.DecompressFile("b.ys");


            //zip格式压缩
            //ZipDemo.CreateZipFile("test", "test.zip");


            //SuperDemo.Run();


            //监视文件
            // WatcherFileDemo.WatchFiles("./", "*");

            //内存映射文件
            new MemoryMappedFilesDemo().Run();

            Console.WriteLine("-----程序执行完毕-----");
            Console.Read();
        }
    }
}
