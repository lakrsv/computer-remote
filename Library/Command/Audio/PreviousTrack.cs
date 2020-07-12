using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;

namespace Computer_Wifi_Remote_Library.Command
{
    public class PreviousTrack : ICommand<bool>
    {
        public string Name => nameof(PreviousTrack);
        public bool HasPayload => false;
        private Audio audio = new Audio();

        public IResponsePayload<bool> Execute(Request request)
        {
            audio.PreviousTrack();
            return ResponsePayload<bool>.NoPayloadSuccess();
        }
    }
}
