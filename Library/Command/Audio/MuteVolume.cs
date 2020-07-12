using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;
using NAudio.CoreAudioApi;

namespace Computer_Wifi_Remote_Library.Command
{
    public class MuteVolume : ICommand<bool>
    {
        public string Name => nameof(MuteVolume);
        public bool HasPayload => false;

        private MMDevice defaultPlaybackDevice;

        public IResponsePayload<bool> Execute(Request request)
        {
            if (!bool.TryParse(request.Parameters[0], out bool mute)) return ResponsePayload<bool>.NoPayloadFailure();

            if (defaultPlaybackDevice == null)
            {
                var enumerator = new MMDeviceEnumerator();
                defaultPlaybackDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }

            defaultPlaybackDevice.AudioEndpointVolume.Mute = mute;

            return ResponsePayload<bool>.NoPayloadSuccess();
        }
    }
}
