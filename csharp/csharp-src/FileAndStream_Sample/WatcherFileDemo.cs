using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndStream_Sample
{
    class WatcherFileDemo
    {
        public static void WatchFiles(string path, string filter)
        {
            var watcher = new FileSystemWatcher(path, filter)
            {
                //是否应该监视指定目录的子目录
                IncludeSubdirectories = true
            };
            //创建文件或目录时发生
            watcher.Created += OnFileChanged;
            //更改文件或目录时发生
            watcher.Changed += OnFileChanged;
            //删除文件或目录时触发
            watcher.Deleted += OnFileChanged;
            //重命名文件或目录时触发
            watcher.Renamed += OnFileRenamed;
            //开始启用监听
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("文件监视中。。。");
        }
        private static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name + "\t" + e.ChangeType);
        }
        private static void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"旧名称：{e.OldName} 新名称：{e.Name}  Type：{e.ChangeType}");
        }



    }
}
