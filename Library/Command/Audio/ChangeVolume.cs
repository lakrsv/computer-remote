using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;
using NAudio.CoreAudioApi;

namespace Computer_Wifi_Remote_Library.Command
{
    public class ChangeVolume : IBytesCommand
    {
        public string Name => nameof(ChangeVolume);
        public bool HasPayload => false;

        private MMDevice defaultPlaybackDevice;

        public IResponsePayload<byte[]> Execute(Request request)
        {
            if (!float.TryParse(request.Parameters[0], out float volumeChange)) return BytesResponsePayload.NoPayloadFailure(new ResponseMetadata(GetType()));

            if (defaultPlaybackDevice == null)
            {
                var enumerator = new MMDeviceEnumerator();
                defaultPlaybackDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }

            defaultPlaybackDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeChange;
            return BytesResponsePayload.NoPayloadSuccess(new ResponseMetadata(GetType()));
        }
    }
}
