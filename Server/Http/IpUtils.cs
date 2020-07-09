using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Server.Http
{
    public static class IpUtils
    {
        public static string GetLocalIpAddress()
        {
            string localIP;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }
    }
}
