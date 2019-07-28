using Computer_Wifi_Remote.Command;
using NAudio.CoreAudioApi;

namespace Computer_Wifi_Remote_Library.Command
{
    public class MuteVolume : ICommand
    {
        public string Name => nameof(MuteVolume);

        private MMDevice defaultPlaybackDevice;

        public bool Execute(Request request)
        {
            if (!bool.TryParse(request.Parameters[0], out bool mute)) return false;

            if (defaultPlaybackDevice == null)
            {
                var enumerator = new MMDeviceEnumerator();
                defaultPlaybackDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }

            defaultPlaybackDevice.AudioEndpointVolume.Mute = mute;

            return true;
        }
    }
}
