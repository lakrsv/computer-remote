using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;

namespace Computer_Wifi_Remote_Library.Command
{
    public class PlayStop : ICommand<bool>
    {
        public string Name => nameof(PlayStop);
        public bool HasPayload => false;

        public IResponsePayload<bool> Execute(Request request)
        {
            Audio.PlayStop();
            return ResponsePayload<bool>.NoPayloadSuccess();
        }
    }
}
