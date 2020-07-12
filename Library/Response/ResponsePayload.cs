using Computer_Wifi_Remote_Library.Response;

namespace Computer_Wifi_Remote_Library
{
    public class ResponsePayload<T> : IResponsePayload<T>
    {
        public bool IsSuccess { get; private set; }
        public T Payload { get; private set; }

        public ResponsePayload(bool isSuccess, T payload)
        {
            IsSuccess = isSuccess;
            Payload = payload;
        }

        public static ResponsePayload<bool> NoPayloadSuccess()
        {
            return new ResponsePayload<bool>(true, true);
        }

        public static ResponsePayload<bool> NoPayloadFailure()
        {
            return new ResponsePayload<bool>(false, false);
        }

        public static ResponsePayload<T> PayloadSuccess(T payload)
        {
            return new ResponsePayload<T>(true, payload);
        }

        public static ResponsePayload<T> PayloadFailure(T payload)
        {
            return new ResponsePayload<T>(true, payload);
        }
    }
}
