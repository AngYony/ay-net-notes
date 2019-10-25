using System;
using System.Text;

namespace FileAndStream_Sample
{
    internal class SuperDemo
    {
        public static void Run()
        {
            #region 字符串、字节数组之间的相互转换

            string str = "你好吗？好好学习，天天向上！";

            //将字符串转换为字节
            byte[] bs = Encoding.UTF8.GetBytes(str);

            //将字节数组转换为等效字符串，实际应用中可以存储该字符串到文件中
            string abc = Convert.ToBase64String(bs);
            Console.WriteLine("将字节数组转换为字符串:");
            Console.WriteLine(abc);

            //将等效字符串还原为数组
            byte[] bs2 = Convert.FromBase64String(abc);

            //将字节数组转换为字符串
            Console.WriteLine("还原后的结果：");
            Console.WriteLine(Encoding.UTF8.GetString(bs2));

            #endregion 字符串、字节数组之间的相互转换

            //TODO：字符串二进制之间的相互转换
            //using(MemoryStream ms=new MemoryStream(bs))
            //{
            //    BinaryReader br = new BinaryReader(ms);
            //    string re= br.ReadString();

            //}
        }
    }
}