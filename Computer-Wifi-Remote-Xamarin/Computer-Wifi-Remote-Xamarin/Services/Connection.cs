using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace Computer_Wifi_Remote_Xamarin.Services
{
    public class ClientConnection
    {
        private static ClientConnection instance;

        public static ClientConnection Instance
        {
            get
            {
                if (instance == null) instance = new ClientConnection();
                return instance;
            }
        }

        private WebSocket webSocket;

        private ClientConnection()
        {

        }

        public bool Connect(string ip)
        {
            webSocket = new WebSocket("ws://" + ip + ":34198/command");
            // webSocket = new WebSocket("ws://echo.websocket.org");
            webSocket.Connect();
            return webSocket.IsAlive;
        }
    }
}
