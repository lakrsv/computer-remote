namespace Computer_Wifi_Remote_Library.Response
{
    public interface IResponsePayload<out T>
    {
        bool IsSuccess { get; }
        T Payload { get; }
    }
}
