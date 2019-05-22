﻿using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Computer_Wifi_Remote.Server
{
    public class Server
    {
        private WebSocketServer webSocketServer;

        public Server()
        {
            webSocketServer = new WebSocketServer("ws://localhost:34198");
            webSocketServer.AddWebSocketService<CommandService>("/command");
            webSocketServer.Start();
        }

        public void Start()
        {
            webSocketServer.Start();
        }

        public void Stop()
        {
            webSocketServer.Stop();
        }
    }
}
