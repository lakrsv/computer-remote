
namespace Computer_Wifi_Remote_Library.Connection
{
    public class ConnectionPayload
    {
        public string Scheme { get; private set; }
        public string Url { get; private set; }
        public int Port { get; private set; }
        public string AuthenticationUsername { get; private set; }
        public string AuthenticationPassword { get; private set; }

        public string CreateConnectionString()
        {
            return $"{Scheme}:{Url}:{Port}";
        }

        public ConnectionPayload(string scheme, string url, int port, string authenticationUsername, string authenticationPassword)
        {
            Scheme = scheme;
            Url = url;
            Port = port;
            AuthenticationUsername = authenticationUsername;
            AuthenticationPassword = authenticationPassword;
        }
    }
}
