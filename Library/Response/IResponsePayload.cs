using Computer_Wifi_Remote.Command;

namespace Computer_Wifi_Remote_Library.Response
{
    public interface IResponsePayload<out T>
    {
        bool IsSuccess { get; }
        T Payload { get; }
        byte[] Serialize();
    }
}
