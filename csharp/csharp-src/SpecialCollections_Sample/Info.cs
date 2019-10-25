using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpecialCollections_Sample
{
    public class Info
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"Word:{Word},Count:{Count},Color:{Color}";
        }
    }

    public static class ColoredConsole
    {
        private static object syncOutput = new object();

        public static void WriteLine(string message)
        {
            lock (syncOutput)
            {
                Console.WriteLine(message);
            }
        }

        public static void WriteLine(string message, string color)
        {
            lock (syncOutput)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(
                    typeof(ConsoleColor), color);
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }

    public static class PipeLineStages
    {
        public static Task ReadFilenamesAsync(string path, BlockingCollection<string> output)
        {
            //第一个阶段
            return Task.Factory.StartNew(() =>
            {
                //读取文件名，并把它们添加到队列中
                foreach (string filename in Directory.EnumerateFiles(
                    path, "*.cs", SearchOption.AllDirectories))
                {
                    //添加到BlockingCollection<T>中
                    output.Add(filename);
                    ColoredConsole.WriteLine($"stage 1: added {filename}");
                }
                //通知所有读取器不应再等待集合中的任何额外项
                output.CompleteAdding(); //该方法必不可少
            },TaskCreationOptions.LongRunning);
        }

        public static async Task LoadContentAsync(BlockingCollection<string> input, 
            BlockingCollection<string> output)
        {
            //第二个阶段：从队列中读取文件名并加载它们的内容，并把内容写入到另一个队列
            //如果不调用GetConsumingEnumerable()方法，而是直接迭代集合，将不会迭代之后添加的项
            foreach (var filename in input.GetConsumingEnumerable())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        output.Add(line);
                        ColoredConsole.WriteLine("stage 2: added " + line);
                    }
                }
            }
            output.CompleteAdding();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, 
            ConcurrentDictionary<string, int> output)
        {
            return Task.Factory.StartNew(() =>
            {
                //第三个阶段：读取第二个阶段中写入内容的队列，并将结果写入到一个字典中
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line?.Split(' ', ';', '\t', '{', '}', '(', ')', ':', ',', '"');
                    if (words == null) continue;
                    foreach (var word in words?.Where(w => !string.IsNullOrEmpty(w)))
                    {
                        //如果键没有添加到字典中，第二个参数就定义应该设置的值
                        //如果 键已经存在于字典中，updateValueFactory就定义值的改变方式，++i
                        output.AddOrUpdate(key: word, addValue: 1, updateValueFactory: (s, i) => ++i);
                        ColoredConsole.WriteLine("stage 3: added " + word);
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        public static Task transFerContentAsync(ConcurrentDictionary<string, int> input,
            BlockingCollection<Info> output)
        {
            //第四个阶段：从字典中读取内容，转换数据，将其写入队列中
            return Task.Factory.StartNew(() =>
            {
                foreach (var word in input.Keys)
                {
                    int value;
                    if (input.TryGetValue(word, out value))
                    {
                        var info = new Info { Word = word, Count = value };
                        output.Add(info);
                        ColoredConsole.WriteLine("stage 4: added " + info);
                    }
                }
                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }

        public static Task AddColorAsync(BlockingCollection<Info> input,
            BlockingCollection<Info> output)
        {
            //第五个阶段：读取队列信息，并添加颜色信息，同时写入另一个队列
            return Task.Factory.StartNew(() =>
            {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    if (item.Count > 40)
                    {
                        item.Color = "Red";
                    }
                    else if (item.Count > 20)
                    {
                        item.Color = "Yellow";
                    }
                    else
                    {
                        item.Color = "Green";
                    }
                    output.Add(item);
                    ColoredConsole.WriteLine("Stage 5: added color " + item.Color + " to " + item);
                }
                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }

        public static Task ShowContentAsync(BlockingCollection<Info> input)
        {
            //第六个阶段：显示最终的队列信息
            return Task.Factory.StartNew(() =>
            {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    ColoredConsole.WriteLine("Stage 6:" + item, item.Color);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }

    internal class PipelineSample
    {
        public static async Task StartPipelineAsync()
        {
            var fileNames = new BlockingCollection<string>();
            //启动第一个阶段任务，读取文件名，并写入到队列fileNames中
            Task t1 = PipeLineStages.ReadFilenamesAsync(@"../../", fileNames);
            ColoredConsole.WriteLine("started stage 1");

            var lines = new BlockingCollection<string>();
            //启动第二个阶段任务，将队列中的文件名进行读取，获取该文件的内容并写入到lines队列中
            Task t2 = PipeLineStages.LoadContentAsync(fileNames, lines);
            ColoredConsole.WriteLine("started stage 2");

            var words = new ConcurrentDictionary<string, int>();
            //启动第三个阶段任务，读取lines队列中内容并写入到words中
            Task t3 = PipeLineStages.ProcessContentAsync(lines, words);

            //同时启动1、2、3三个阶段的任务，并发执行
            await Task.WhenAll(t1, t2, t3);
            ColoredConsole.WriteLine("stages 1,2,3 completed");

            var items = new BlockingCollection<Info>();
            //启动第四个阶段任务，将words字典中的数据进行读取，写入到items中
            Task t4 = PipeLineStages.transFerContentAsync(words, items);

            var coloredItems = new BlockingCollection<Info>();
            //启动第五个阶段任务，将items的数据进行读取和修改，将结果写入到coloredItems中
            Task t5 = PipeLineStages.AddColorAsync(items, coloredItems);

            //启动第六个阶段任务，将最终的结果显示出来
            Task t6 = PipeLineStages.ShowContentAsync(coloredItems);

            ColoredConsole.WriteLine("stages 4,5,6 started");
            //同时启动4、5、6三个阶段的任务
            await Task.WhenAll(t4, t5, t6);
            ColoredConsole.WriteLine("all sages finished");
        }
    }
}