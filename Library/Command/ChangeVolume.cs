using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;
using NAudio.CoreAudioApi;

namespace Computer_Wifi_Remote_Library.Command
{
    public class ChangeVolume : ICommand<bool>
    {
        public string Name => nameof(ChangeVolume);
        public bool HasPayload => false;

        private MMDevice defaultPlaybackDevice;

        public IResponsePayload<bool> Execute(Request request)
        {
            if (!float.TryParse(request.Parameters[0], out float volumeChange)) return ResponsePayload<bool>.NoPayloadFailure();

            if (defaultPlaybackDevice == null)
            {
                var enumerator = new MMDeviceEnumerator();
                defaultPlaybackDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }

            defaultPlaybackDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeChange;
            return ResponsePayload<bool>.NoPayloadSuccess();
        }
    }
}
