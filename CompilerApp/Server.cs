using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace CsharpComplier
{
    class Server
    {
        public static int maxConnection;
        public static int port;
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
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
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
            Console.WriteLine($"Connecting from {_client.Client.RemoteEndPoint} ...");

            for(int i = 1;i <= maxConnection;i ++)
            {
                if(clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine($"[Info] {_client.Client.RemoteEndPoint} failed to connect: Server full");
        }

        private static void InitializeServerData()
        {
            for(int i = 1;i <= maxConnection;i ++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }
}
