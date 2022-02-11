using System;

namespace Server
{
    class Program
    {
        static Server server;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            server = new Server("127.0.0.1", 8000);
            server.Connect();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            server.Stop();
        }
    }
}
