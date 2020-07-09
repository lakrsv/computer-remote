
namespace Computer_Wifi_Remote_Library.Connection
{
    public interface IConnection
    {
        bool IsAlive { get; }
        void Send(string command);
    }
}
