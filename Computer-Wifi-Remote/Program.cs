using System;
using System.Threading.Tasks;

namespace Computer_Wifi_Remote
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server.Server();
            server.Start();
            Console.WriteLine("Server is running...");
            Console.ReadKey(true);
            server.Stop();
        }
    }
}
