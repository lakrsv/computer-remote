using Computer_Wifi_Remote.Command;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Computer_Wifi_Remote_Library.Command
{
    public class ChangeVolume : ICommand
    {
        public string Name => nameof(ChangeVolume);

        private MMDevice defaultPlaybackDevice;

        public bool Execute(Request request)
        {
            if (!float.TryParse(request.Parameters[0], out float volumeChange)) return false;

            if (defaultPlaybackDevice == null)
            {
                var enumerator = new MMDeviceEnumerator();
                defaultPlaybackDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }

            defaultPlaybackDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeChange;
            return true;
        }
    }
}
