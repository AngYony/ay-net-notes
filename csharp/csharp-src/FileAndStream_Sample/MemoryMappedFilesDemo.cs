using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;

namespace FileAndStream_Sample
{
    internal class MemoryMappedFilesDemo
    {
        //创建两个事件状态，用来收发信号
        private ManualResetEventSlim _mapCreated = new ManualResetEventSlim(initialState: false);

        private ManualResetEventSlim _dataWritenEvent = new ManualResetEventSlim(initialState: false);

        //映射名称
        private const string MAPNAME = "SampleMap";

        public void Run()
        {
            //启动一个用于创建内存映射文件和写入数据的任务
            Task.Run(() => WriterAsync());
            //启动一个读取数据的任务
            Task.Run(() => Reader());
            Console.WriteLine("任务已经启动...");
            Console.Read();
        }

        //创建内存映射文件和写入数据
        private async Task WriterAsync()
        {
            try
            {
                //创建一个基于内存打车内存映射文件
                using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateOrOpen(
                    MAPNAME, 10000, MemoryMappedFileAccess.ReadWrite))
                {
                    //给事件_mapCrated发出信号，给其他任务提供信息，说明已经创建了内存映射文件，可以打开它了
                    _mapCreated.Set();
                    Console.WriteLine("shared memory segment created");
                    //创建视图访问器，用来访问共享的内存
                    using (MemoryMappedViewAccessor accessor = mappedFile.CreateViewAccessor(
                        0, 10000, MemoryMappedFileAccess.Write))
                    {
                        for (int i = 0, pos = 0; i < 100; i++, pos += 4)
                        {
                            //将数据写入到共享内存中
                            accessor.Write(pos, i);
                            Console.WriteLine($"written {i} at position {pos}");
                            await Task.Delay(10);
                        }
                        //写入完数据后，给事件发出信号，通知读取器，现在可以开始读取了
                        _dataWritenEvent.Set();
                        Console.WriteLine("data written");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("writer " + ex.Message);
            }
        }

        //读取数据
        private void Reader()
        {
            try
            {
                Console.WriteLine("reader");
                //读取器首先等待创建内存映射文件
                _mapCreated.Wait();
                Console.WriteLine("reader starting");
                //打开内存映射文件
                using (MemoryMappedFile mappedFile = MemoryMappedFile.OpenExisting(
                    MAPNAME, MemoryMappedFileRights.Read))
                {
                    //创建一个视图访问器
                    using (MemoryMappedViewAccessor accessor = mappedFile.CreateViewAccessor(
                        0, 10000, MemoryMappedFileAccess.Read))
                    {
                        //等待设置_dataWritenEvent
                        _dataWritenEvent.Wait();
                        Console.WriteLine("reading can start now");
                        for (int i = 0; i < 400; i += 4)
                        {
                            int result = accessor.ReadInt32(i);
                            Console.WriteLine($"reading {result} from position {i}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("reader " + ex.Message);
            }
        }

        private async Task WriterUsingStreams()
        {
            try
            {
                using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateOrOpen(
                    MAPNAME, 10000, MemoryMappedFileAccess.ReadWrite))
                {
                    _mapCreated.Set();
                    Console.WriteLine("shared memory segment created");
                    //在映射内定义一个视图，注意此处和之前的CreateViewAccessor()的不同
                    MemoryMappedViewStream stream = mappedFile.CreateViewStream(
                        0, 10000, MemoryMappedFileAccess.Write);
                    using (var writer = new StreamWriter(stream))
                    {
                        //为了用每次写入的内容刷新缓存，此处需要设置为true
                        writer.AutoFlush = true;
                        for (int i = 0; i < 100; i++)
                        {
                            string s = "some data " + i;
                            Console.WriteLine($"writing {s} at {stream.Position}");
                            //StreamWriter以缓存的方式写入操作，所以流的位置不是在每个写入操作中都更新，只在写入器写入块时才更新
                            //所以，每次写入的内容都需要刷新缓存，可以手动调用writer.Flush()方法，也可以在写入之前设置writer.AutoFlush = true
                            await writer.WriteLineAsync(s);
                        }
                    }
                    _dataWritenEvent.Set();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("writer " + ex.Message);
            }
        }

        private async Task ReaderUsingStreams()
        {
            try
            {
                Console.WriteLine("reader");
                _mapCreated.Wait();
                Console.WriteLine("reader starting");
                using (MemoryMappedFile mappedFile = MemoryMappedFile.OpenExisting(
                    MAPNAME, MemoryMappedFileRights.Read))
                {
                    MemoryMappedViewStream stream = mappedFile.CreateViewStream(
                        0, 10000, MemoryMappedFileAccess.Read);
                    using (var reader = new StreamReader(stream))
                    {
                        _dataWritenEvent.Wait();
                        Console.WriteLine("reading can start now");
                        for (int i = 0; i < 100; i++)
                        {
                            long pos = stream.Position;
                            string s = await reader.ReadLineAsync();
                            Console.WriteLine($"read {s} from {pos}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("reader " + ex.Message);
            }
        }
    }
}