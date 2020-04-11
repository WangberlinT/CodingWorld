using System;
using System.Collections.Generic;
using System.Text;

namespace CompilerServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string content = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            ErrorCheck(_fromClient, _clientIdCheck);

            Console.WriteLine($"Content:\n{content}");
            
        }

        public static void CodeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string content = _packet.ReadString();

            ErrorCheck(_fromClient, _clientIdCheck);

            //TODO: handle content 编译content, 最好写成一个新的类的静态方法。
            Console.WriteLine($"[Info]Code\n{content}");
            ServerSend.Welcome(_fromClient, "Compiling...");
        }

        private static void ErrorCheck(int _fromClient, int _clientIdCheck)
        {
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"(ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
        }
    }
}
