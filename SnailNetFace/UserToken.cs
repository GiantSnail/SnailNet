using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace SnailNetFace
{
    internal class UserToken
    {
        private Stream _stream;
        private BinaryReader _reader;
        private BinaryWriter _writer;
        private Socket _socket = null;
        private byte[] _buffer = new byte[1024];
        public UserToken(Socket socket)
        {
            _socket = socket;
            _stream = new MemoryStream();
            _reader = new BinaryReader(_stream);
            _writer = new BinaryWriter(_stream);
            Console.WriteLine("客户端连接:"+_socket.RemoteEndPoint.ToString());
            receive();
        }

        private ushort _count;
        private void receive()
        {
            _socket.BeginReceive(_buffer,0,_count, SocketFlags.None, onReceive,null);
        }

        /// <summary>
        /// 接受到数据
        /// </summary>
        /// <param name="ar"></param>
        private void onReceive(IAsyncResult ar)
        {
            _writer.Write(_buffer, 0, _count);//将接收到的数据写到流中
            while(_stream.Length >= 2)
            {
                ushort dataLen = _reader.ReadUInt16();
                if (_stream.Length < dataLen)
                {
                    return;
                }
                byte[] message = _reader.ReadBytes(dataLen);
                onMessage(message);
            }
            receive();
        }

        private void onMessage(byte[] message)
        {
            //将数据序列化(json/ProtoBuff)
            //通知消息中心，处理消息
        }

        public void SendMsg<T>(ushort cmd,T data)
        {

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(byte[] msg)
        {
            _socket.Send(parseMsgPack(msg));
        }

        private byte[] parseMsgPack(byte[] msg)
        {
            List<byte> ret = new List<byte>();
            ushort dataLen = (ushort)msg.Length;
            ret.AddRange(BitConverter.GetBytes(dataLen));
            ret.AddRange(msg);
            return ret.ToArray();
        }

    }
}
