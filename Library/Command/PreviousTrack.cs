using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;

namespace Computer_Wifi_Remote_Library.Command
{
    public class PreviousTrack : ICommand<bool>
    {
        public string Name => nameof(PreviousTrack);
        public bool HasPayload => false;

        public IResponsePayload<bool> Execute(Request request)
        {
            Audio.PreviousTrack();
            return ResponsePayload<bool>.NoPayloadSuccess();
        }
    }
}
