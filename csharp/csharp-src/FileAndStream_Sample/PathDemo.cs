using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndStream_Sample
{
    class PathDemo
    {
        /// <summary>
        /// 使用Environment.SpecialFolder
        /// </summary>
        public static void GetDocumentsFolder()
        {
            Console.WriteLine("Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments):");
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            Console.WriteLine();
            Console.WriteLine("Environment.GetEnvironmentVariable(\"HOMEDRIVE\"):");
            Console.WriteLine(Environment.GetEnvironmentVariable("HOMEDRIVE"));

        }
    }
}
