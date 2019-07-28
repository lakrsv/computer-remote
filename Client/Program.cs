using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using System;
using WebSocketSharp;

namespace Computer_Wifi_Remote_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to connect...");
            Console.ReadKey();

            using(var ws = new WebSocket("ws://192.168.1.113:34198/command"))
            {
                ws.Connect();
                Console.WriteLine("Client is running...");
                Console.WriteLine("Type a command. Type quit to exit");

                var nextLine = Console.ReadLine();
                while(nextLine.ToLowerInvariant() != "quit")
                {
                    var commands = nextLine.Split(' ');
                    Commands.ExecuteRemotely(ws, new Request(commands[0], commands.SubArray(1, commands.Length - 1)));
                    nextLine = Console.ReadLine();
                }
            }
        }
    }
}
