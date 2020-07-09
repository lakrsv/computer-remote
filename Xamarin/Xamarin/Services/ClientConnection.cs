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

        public bool IsConnected { get { return WebSocket != null && WebSocket.IsAlive; } }

        public WebSocket WebSocket { get; private set; }

        private ClientConnection()
        {

        }

        public bool Connect(string ip)
        {
            WebSocket = new WebSocket("ws://" + ip + ":34198/command");
            // webSocket = new WebSocket("ws://echo.websocket.org");
            WebSocket.Connect();
            return WebSocket.IsAlive;
        }
    }
}
