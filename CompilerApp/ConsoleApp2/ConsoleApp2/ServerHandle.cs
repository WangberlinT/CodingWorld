using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

            //TODO: 为Compiler 添加编译文件内容
            Compiler compiler = Compiler.GetInstance();
            Console.WriteLine(content);
            string[] divide = Regex.Split(content," moban ");

            compiler.SetName(divide[0]);
            compiler.SetCode(divide[1]);
            Console.WriteLine($"[Info]Code\n{content}");

            if (compiler.ConditionCheck())
            {
                ServerSend.Welcome(_fromClient, "[Info]Compiling...");
                compiler.Compile();
                ServerSend.Welcome(_fromClient, "[Info]Finished!");
            }
                
            
        }

        public static void ScriptNameReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string content = _packet.ReadString();

            ErrorCheck(_fromClient, _clientIdCheck);
            //TODO: 为Compiler 添加编译文件名
            Compiler compiler = Compiler.GetInstance();
            compiler.SetName(content);
            Console.WriteLine($"[Info]file name:{content}\n");
            if (compiler.ConditionCheck())
            {
                compiler.Compile();
                ServerSend.Welcome(_fromClient, "Compiling...");
                ServerSend.Welcome(_fromClient, "[Info]Finished!");
            }

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
