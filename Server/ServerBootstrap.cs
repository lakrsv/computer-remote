using System;
using System.Runtime.InteropServices;
using WebSocketSharp.Server;
namespace Server
{
    public class ServerBootstrap
    {
        private WebSocketServer webSocketServer;

        public ServerBootstrap()
        {
            webSocketServer = new WebSocketServer("ws://0.0.0.0:34198");
            webSocketServer.AddWebSocketService<CommandService>("/command");
            webSocketServer.Start();

            //ShowWindow(GetConsoleWindow(), SW_HIDE);
        }

        public void Start()
        {
            webSocketServer.Start();
        }

        public void Stop()
        {
            webSocketServer.Stop();
        }

        //[DllImport("kernel32.dll")]
        //static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //const int SW_HIDE = 0;
        //const int SW_SHOW = 5;
    }
}
