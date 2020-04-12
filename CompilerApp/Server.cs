using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace CompilerServer
{
    /*
     * Server 类维护TCP连接，记录Client连接状态，接受Client发来的内容
     */
    class Server
    {
        public static int maxConnection;
        public static int port;
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public delegate void PacketHandler(int _fromClient, Packet _packet);//声明一个向Client 发包的方法类型
        public static Dictionary<int, PacketHandler> packetHandlers;

        private static TcpListener tcpListener;
        public static void Start(int max_conn, int _port)
        {
            maxConnection = max_conn;
            port = _port;

            Console.WriteLine("Starting Server...");
            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            Console.WriteLine($"Server started on {port}");
            Console.WriteLine("Push F1 to exit");
            string command;
            while(Console.ReadKey().Key != ConsoleKey.F1)
            {
                //忙等TODO: 添加Interrupt
            }
            System.Environment.Exit(0);
        }

        /*
         * 异步回调函数，当TCP连接建立时调用(在另外一个线程中执行)
         * 建立和Client的连接，保存Client信息
         */
        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
            Console.WriteLine($"Connecting from {_client.Client.RemoteEndPoint} ...");

            for(int i = 1;i <= maxConnection;i ++)
            {
                if(clients[i].tcp.socket == null)
                {
                    //TODO
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine($"[Info] {_client.Client.RemoteEndPoint} failed to connect: Server full");
        }

        private static void InitializeServerData()
        {
            for (int i = 1; i <= maxConnection; i++)
            {
                clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>();
            //添加两个包处理方法，分别处理连接信息和代码信息
            packetHandlers.Add((int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived);
            packetHandlers.Add((int)ClientPackets.code, ServerHandle.CodeReceived);
            packetHandlers.Add((int)ClientPackets.scriptName, ServerHandle.ScriptNameReceived);
            Console.WriteLine("Initialized packets.");
        }
    }
}
