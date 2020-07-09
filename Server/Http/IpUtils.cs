using System.Linq;
using System.Net;

namespace Server.Http
{
    public static class IpUtils
    {
        public static string GetLocalIpAddress()
        {
            var hostName = Dns.GetHostName();
            var ipEntry = Dns.GetHostEntry(hostName);
            var addr = ipEntry.AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            return addr;
        }
    }
}
