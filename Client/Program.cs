using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Connection;
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

            using (var ws = new WebSocket("wss://localhost:34198/command"))
            {
                var connection = new WebSocketConnection(ws, "username", "password");
                Console.WriteLine("Client is running...");
                Console.WriteLine("Type a command. Type quit to exit");

                var nextLine = Console.ReadLine();
                while (nextLine.ToLowerInvariant() != "quit")
                {
                    var commands = nextLine.Split(' ');
                    Commands.ExecuteRemotely(connection, new Request(commands[0], commands.SubArray(1, commands.Length - 1)));
                    nextLine = Console.ReadLine();
                }
            }
        }
    }
}
