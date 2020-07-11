using Computer_Wifi_Remote_Library.Connection;

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

        public bool IsConnected { get { return Connection != null && Connection.IsAlive; } }

        public IConnection Connection { get; private set; }

        private ClientConnection()
        {

        }

        public bool Connect(ConnectionPayload connectionPayload)
        {
            Connection = ConnectionFactory.GetConnection(connectionPayload);
            return Connection.IsAlive;
        }
    }
}
