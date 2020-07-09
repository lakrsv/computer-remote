using System;
using WebSocketSharp;

namespace Computer_Wifi_Remote_Library.Connection
{
    public static class ConnectionFactory
    {
        public static IConnection GetConnection(ConnectionPayload connectionPayload)
        {
            switch (connectionPayload.Scheme)
            {
                case "ws":
                case "wss":
                    return new WebSocketConnection(
                        new WebSocket(
                        connectionPayload.CreateConnectionString()),
                        connectionPayload.AuthenticationUsername,
                        connectionPayload.AuthenticationPassword);
                default:
                    throw new NotImplementedException();

            }
            throw new NotImplementedException();
        }
    }
}
