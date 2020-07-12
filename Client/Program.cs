using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using Computer_Wifi_Remote_Library.Connection;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using WebSocketSharp;

namespace Computer_Wifi_Remote_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to connect...");
            Console.ReadKey();

            using var ws = new WebSocket("wss://localhost:34198/command");

            var connection = new WebSocketConnection(ws, "username", "password");
            connection.OnMessageReceived += (sender, e) => OnMessageReceived(sender, e);

            Console.WriteLine("Client is running...");
            Console.WriteLine("Type a command. Type quit to exit");

            var nextLine = Console.ReadLine();
            while (nextLine.ToLowerInvariant() != "quit")
            {
                var commands = nextLine.Split(' ');
                var commandHasPayload = Commands.CommandHasPayload(commands[0]);
                Commands.ExecuteRemotely(connection, new Request(commands[0], commands.SubArray(1, commands.Length - 1)));
                nextLine = Console.ReadLine();
            }
        }

        private static void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (e.IsPing)
            {
                Console.WriteLine("We were pinged");
            }
            else if (e.IsBinary)
            {
                Console.WriteLine("Got binary response from websocket");
                var response = BytesResponsePayload.Deserialize(e.RawData);
                Console.WriteLine($"Success: {response.IsSuccess}");
                Console.WriteLine($"Metadata: {response.Metadata.Serialize()}");

                if (response.Metadata.Source == typeof(DisplayScreen))
                {
                    using var ms = new MemoryStream(response.Payload);
                    var displayCapture = Image.FromStream(ms);

                    DrawImage.ConsoleWriteImage((Bitmap)displayCapture);
                }
                else
                {
                    Console.WriteLine($"Raw Payload: {GetPrettyByteArrayString(response.Payload)}");
                }
            }
            else if (e.IsText)
            {
                Console.WriteLine("Got text response from websocket");
                Console.WriteLine($"Response: {e.Data}");
            }
        }

        private static string GetPrettyByteArrayString(byte[] bytes)
        {
            var sb = new StringBuilder("new byte[] {");
            for (var i = 0; i < bytes.Length; ++i)
            {
                sb.Append(bytes[i]);
                if (i != bytes.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
