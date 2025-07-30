using System.Net.Sockets;

namespace xbd.s7netplus
{

    /// <summary>
    /// TcpClientMixins
    /// </summary>
    public static class TcpClientMixins
    {
#if NETSTANDARD1_3

        /// <summary>
        /// �ر�����
        /// </summary>
        /// <param name="tcpClient"></param>
        public static void Close(this TcpClient tcpClient)
        {
            tcpClient.Dispose();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public static void Connect(this TcpClient tcpClient, string host, int port)
        {
            tcpClient.ConnectAsync(host, port).GetAwaiter().GetResult();
        }
#endif
    }
}
