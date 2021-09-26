using System;
using System.Net;
using System.Net.Sockets;

namespace SnailNetFace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket client = new(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPEndPoint ip = new(IPAddress.Parse("127.0.0.1"), 8080);
            try
            {
                client.Bind(ip);
            }catch(Exception ex)
            {
                Console.WriteLine("Bind Error:"+ex.Message);
            }

            client.Listen(10);
            while (true)
            {
                Socket _remoteClient = client.Accept();//Accept方法会中断，有连接时才会返回
                new UserToken(_remoteClient);
            }
        }


    }
}
