using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Server
{
    internal class Server : ComonServer, IServer
    {
        private IPEndPoint ipPoint;
        private Socket listenSocket;
        private Socket handler;
        private List<Car> savingCar;

        public Server(string ipAdress, int port) : base()
        {
            savingCar = new List<Car>();
            LoadCars();
            ipPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
        }

        private void LoadCars()
        {
            for (int i = 0; i < 25; i++)
            {
                if (i % 5 == 0)
                {
                    savingCar.Add(new Car(null, (ushort)(i + 1), null, (ushort?)i));
                }
                else
                {
                    savingCar.Add(new Car($"brand{i}", (ushort)(i + 1), 1.1f + i, (ushort?)i));
                }
            }
        }

        public void Connect()
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);

                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                handler = listenSocket.Accept();

                Thread clientThread = new Thread(new ThreadStart(Receve));
                clientThread.Start();

            }
            catch { }
        }

        protected override void Receve()
        {
            while (isNotStoped)
            {

                byte[] data = new byte[256];
                try
                {
                    do
                    {
                        handler.Receive(data);
                    }

                    while (handler.Available > 0);

                    int id = BitConverter.ToInt32(data, 0);

                    Send(id);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Stop();
                }
            }
        }

        protected override void Send(int id)
        {
            IProtocol<Car> protocol = new CarProtocol();

            if (id == -1)
            {
                handler.Send(protocol.getBytesFrom(savingCar));
            }

            else
            {
                if (id < savingCar.Count)
                {
                    var oneCar = new List<Car>();
                    oneCar.Add(savingCar[id]);
                    handler.Send(protocol.getBytesFrom(oneCar));
                }
            }
        }

        protected override void Disconnect()
        {
            try
            {
                listenSocket.Shutdown(SocketShutdown.Both);
                handler.Shutdown(SocketShutdown.Both);
            }
            catch
            {

            }
            try
            {
                listenSocket.Close();
                handler.Close();
            }
            catch
            {

            }
        }

        public new void Stop()
        {
            base.Stop();
        }
    }
}

