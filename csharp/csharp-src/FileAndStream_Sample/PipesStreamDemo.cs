using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileAndStream_Sample
{
    internal class PipesStreamDemo
    {
        #region  命名管道
        //创建命名管道服务器
        private static void PipesReader(string pipeName)
        {
            try
            {
                //创建对象，构造函数需要管道的名称，通过管道通信的多个进程可以使用该管道
                //第二个参数定义了管道的方向，此处用于读取，因此设置为了向内
                using (var pipeReader = new NamedPipeServerStream(pipeName, PipeDirection.In))
                {
                    //命名管道等待写入的连接
                    pipeReader.WaitForConnection();
                    Console.WriteLine("reader connected");

                    const int BUFFERSIZE = 256;
                    bool completed = false;
                    while (!completed)
                    {
                        byte[] buffer = new byte[BUFFERSIZE];
                        //管道服务器把消息读入缓冲区数组
                        int nRead = pipeReader.Read(buffer, 0, BUFFERSIZE);
                        //获取消息内容并打印显示
                        string line = Encoding.UTF8.GetString(buffer, 0, nRead);
                        Console.WriteLine(line);
                        if (line == "bye")
                            completed = true;
                    }
                }
                Console.WriteLine("completed reading");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //简化版
        private static void PipesReader2(string pipeName)
        {
            try
            {
                var pipeReader = new NamedPipeServerStream(pipeName, PipeDirection.In);
                using (var reader = new StreamReader(pipeReader))
                {
                    pipeReader.WaitForConnection();
                    Console.WriteLine("reader connected");
                    bool completed = false;

                    while (!completed)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                        if (line == "bye")
                            completed = true;
                    }
                }
                Console.WriteLine("completed reading");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void PipesWriter(string pipeName)
        {
            var pipeWriter = new NamedPipeClientStream("TheRokcs", pipeName, PipeDirection.Out);
            using (var writer = new StreamWriter(pipeWriter))
            {
                //连接管道
                pipeWriter.Connect();
                Console.WriteLine("writer connected");
                bool completed = false;
                while (!completed)
                {
                    string input = Console.ReadLine();
                    if (input == "bye")
                        completed = true;
                    //把消息发送给服务器，默认情况下，消息不立即发送，而是缓存起来。
                    writer.WriteLine(input);
                    //调用Flush()把消息推送到服务器上
                    writer.Flush();
                }
            }
            Console.WriteLine("completed writing");
        }

        #endregion



        #region 匿名管道

        private string _pipeHandle;
        //创建一个信号状态（是否有收到信号）
        private ManualResetEventSlim _pipeHandleSet;

        public void Run()
        {
            _pipeHandleSet = new ManualResetEventSlim(initialState: false);
            Task.Run(() => Reader());
            Task.Run(() => Writer());
        }

        //服务端
        private void Writer()
        {
            try
            {
                //把服务器充当读取器
                var pipeReader = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.None);
                using (var reader = new StreamReader(pipeReader))
                {
                    //获取管道的客户端句柄，被转换为一个字符串后赋予变量_pipeHandle
                    //这个变量以后有充当写入器的客户端使用
                    _pipeHandle = pipeReader.GetClientHandleAsString();
                    Console.WriteLine("pipe handle:" + _pipeHandle);
                    _pipeHandleSet.Set();

                    bool end = false;
                    while (!end)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                        if (line == "end")
                            end = true;
                    }
                    Console.WriteLine("finished reading");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //客户端
        private void Reader()
        {
            Console.WriteLine("anonymous pipe writer");
            //客户端等到变量_pipeHandleSet发出信号
            _pipeHandleSet.Wait();
            //收到信号后，就打开由_pipeHandle变量引用的管道句柄
            var pipeWriter = new AnonymousPipeClientStream(PipeDirection.Out, _pipeHandle);
            using (var writer = new StreamWriter(pipeWriter))
            {
                writer.AutoFlush = true;
                Console.WriteLine("starting writer");
                for (int i = 0; i < 5; i++)
                {
                    writer.WriteLine("Message " + i);
                    Task.Delay(500).Wait();
                }
                Console.WriteLine("end");
            }
        }

        #endregion
    }
}