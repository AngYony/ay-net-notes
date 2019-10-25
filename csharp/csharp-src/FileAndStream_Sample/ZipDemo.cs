using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace FileAndStream_Sample
{
    internal class ZipDemo
    {
        public static void CreateZipFile(string directory, string zipFile)
        {
            //将要穿件的压缩文件对应的写入流
            FileStream zipStream = File.OpenWrite(zipFile);

            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                //获取目录下的所有文件
                IEnumerable<string> files = Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    //针对每个文件创建ZipArchiveEntry对象
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
                    //创建每个文件对应的读取流
                    using (FileStream inputSream = File.OpenRead(file))
                    //打开ZipArchiveEntry对象的压缩流
                    using (Stream outputStream = entry.Open())
                    {
                        //将文件写入到压缩流中
                        inputSream.CopyTo(outputStream);
                    }
                }
            }
        }
    }
}