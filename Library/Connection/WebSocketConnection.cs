
namespace Computer_Wifi_Remote_Library.Connection
{
    public class WebSocketConnection : IConnection
    {
        private WebSocketSharp.WebSocket webSocket;
        public event System.EventHandler<WebSocketSharp.MessageEventArgs> OnMessageReceived
        {
            add { webSocket.OnMessage += value; }
            remove { webSocket.OnMessage -= value; }
        }

        public WebSocketConnection(WebSocketSharp.WebSocket webSocket, string username, string password)
        {
            this.webSocket = webSocket;
            this.webSocket.SslConfiguration.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                // Ignore SSL validation
                return true;
            };

            this.webSocket.SetCredentials(username, password, true);
            this.webSocket.Connect();
        }

        public bool IsAlive => webSocket.IsAlive;

        public void Send(string command)
        {
            webSocket.Send(command);
        }
    }
}
