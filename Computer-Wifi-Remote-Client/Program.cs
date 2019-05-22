using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Command;
using System;
using WebSocketSharp;

namespace Computer_Wifi_Remote_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var ws = new WebSocket("ws://localhost:34198/command"))
            {
                ws.Connect();
                Console.WriteLine("Client is running...");
                Console.WriteLine("Press any key to send a test message to the server");
                Console.ReadKey(true);
                ws.Send(nameof(HelloWorld));
                Console.ReadKey(true);
            }
        }
    }
}
