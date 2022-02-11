using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    internal abstract class SocketClient<T,B>: Client<T,B>
    {
        private IPEndPoint ipPoint;
        protected Socket socketClient;
        public SocketClient(string server, int port, B view) : base(view) 
        {
            ipPoint = new IPEndPoint(IPAddress.Parse(server), port);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try 
            {
                Connect();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Stop();
            }
        }
        protected override void Connect()
        {
            socketClient.Connect(ipPoint);
            Thread clientThread = new Thread(new ThreadStart(ListenServer));
            clientThread.Start();
        }

        protected void ListenServer() 
        {
            while (isNotStoped) 
            {
                byte[] data = new byte[250];

                int bytes = 0;
                List<byte> resBytes = new List<byte>();

                do
                {
                    bytes = socketClient.Receive(data, data.Length, 0);

                    for (int i = 0; i < bytes; i++)
                    {
                        resBytes.Add(data[i]);
                    }
                }
                while (socketClient.Available > 0);

                IProtocol<Car> protocol = new CarProtocol();
                Receive(protocol.buildFromBytes(resBytes.ToArray()));
            }
        }

        protected abstract void Receive(List<Car> cars);

        protected override void Disonnect()
        {
            try
            {
                socketClient.Shutdown(SocketShutdown.Both);
                socketClient.Close();
            }
            catch
            {

            }
        }

    }
}
