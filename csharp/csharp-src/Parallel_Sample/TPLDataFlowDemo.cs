using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Parallel_Sample
{
    internal class TPLDataFlowDemo
    {
        #region ActionBlock 简单示例

        public static void ActionBlockRun()
        {
            //ActionBlock异步处理消息，把信息写入控制台
            var processInput = new ActionBlock<string>(s =>
            {
                Console.WriteLine("user input :" + s);
            });

            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    //把读入的所有字符串写入到ActionBlock中
                    processInput.Post(input);
                }
            }
        }

        #endregion ActionBlock 简单示例



        #region //BufferBlock 简单示例

        private static BufferBlock<string> s_buffer = new BufferBlock<string>();
        //用于从控制台读取字符串
        private static void Producer()
        {
            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    //将字符串写入到BufferBlock中
                    s_buffer.Post(input);
                }
            }
        }
        
        private static async Task ConsumerAsync()
        {
            while (true)
            {
                //接收BufferBlock中的数据，ReceiveAsync是ISourceBlock的一个扩展方法
                string data = await s_buffer.ReceiveAsync();
                Console.WriteLine("user input:" + data);
            }
        }

        //BufferBlock 简单示例
        public static void BufferBlockRun()
        {
            Task t1 = Task.Run(() => Producer());
            Task t2 = Task.Run(async () => { await ConsumerAsync(); });
            Task.WaitAll(t1, t2);
        }

        #endregion //BufferBlock 简单示例

        #region 连接块

        //返回一个路径下的所有文件名
        private static IEnumerable<string> GetFileNames(string path)
        {
            //foreach(var fileName in System.IO.Directory.EnumerateFiles(path, "*.cs"))
            //{
            //    yield return fileName;
            //}

            return System.IO.Directory.EnumerateFiles(path, "*.txt");
        }

        //读取文件中的每一行
        private static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }
        //分割每一行的所有词组
        private static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        yield return word;
                    }
                }
            }
        }

        private static ITargetBlock<string> SetupPipiline()
        {
            //第一个TransformBlock被声明为将一个字符串转换为IEnumerable<string>，通过GetFileNames()完成转换
            var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(path =>
              {
                  return GetFileNames(path);
              });
            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(fileNames =>
            {
                return LoadLines(fileNames);
            });

            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(lines2 =>
              {
                  return GetWords(lines2);
              });
            //使用ActionBlock定义最后一个块，该块只是一个用于接收数据的目标块
            var dispaly = new ActionBlock<IEnumerable<string>>(col =>
            {
                foreach (var s in col)
                {
                    Console.WriteLine(s);
                }
            });
            //将这些块彼此连接起来
            //fileNamesForPath的结果被传递给lines块
            fileNamesForPath.LinkTo(lines);
            //lines块链接到words块
            lines.LinkTo(words);
            //words块链接到display块
            words.LinkTo(dispaly);
            //返回用于启动管道的块
            return fileNamesForPath;
        }

        public static void TransformBlockRun()
        {
            var target = SetupPipiline();
            target.Post("../../");
        }

        #endregion 连接块
    }
}