using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;

namespace Computer_Wifi_Remote_Library.Command
{
    public class PreviousTrack : IBytesCommand
    {
        public string Name => nameof(PreviousTrack);
        public bool HasPayload => false;
        private Audio audio = new Audio();

        public IResponsePayload<byte[]> Execute(Request request)
        {
            audio.PreviousTrack();
            return BytesResponsePayload.NoPayloadSuccess(new ResponseMetadata(GetType()));
        }
    }
}
