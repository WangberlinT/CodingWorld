using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CsharpComplier
{
    class Client
    {
        public int id;
        public TCP tcp;
        public static int DATABUFFER = 4096;

        public Client(int id)
        {
            this.id = id;
            this.tcp = new TCP(id);
        }
        public class TCP
        {
            public TcpClient socket;
            private readonly int id;
            private NetworkStream stream;
            private byte[] receiveBuffer;

            public TCP(int id)
            {
                this.id = id;
            }

            public void Connect(TcpClient socket)
            {
                this.socket = socket;
                socket.ReceiveBufferSize = DATABUFFER;
                socket.SendBufferSize = DATABUFFER;

                stream = socket.GetStream();
                receiveBuffer = new byte[DATABUFFER];

                stream.BeginRead(receiveBuffer, 0, DATABUFFER, ReceiveCallback, null);
            }

            private void ReceiveCallback(IAsyncResult result)
            {
                try
                {
                    int bytelen = stream.EndRead(result);
                    if(bytelen <= 0)
                    {
                        // block
                        Console.WriteLine("[Info] Stream end");
                        return;
                    }

                    byte[] data = new byte[DATABUFFER];
                    Array.Copy(receiveBuffer, data, DATABUFFER);
                    //TODO handle data

                    Array.Clear(receiveBuffer, 0, DATABUFFER);
                    stream.BeginRead(receiveBuffer, 0, DATABUFFER, ReceiveCallback, null);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"[Error] Error receiving TCP data:{e}");
                }
            }
        }
    }
}
