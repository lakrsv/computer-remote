using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;

namespace Computer_Wifi_Remote_Library.Command
{
    public class NextTrack : IBytesCommand
    {
        public string Name => nameof(NextTrack);
        public bool HasPayload => false;
        private Audio audio = new Audio();


        public IResponsePayload<byte[]> Execute(Request request)
        {
            audio.NextTrack();
            return BytesResponsePayload.NoPayloadSuccess(new ResponseMetadata(GetType()));
        }
    }
}
