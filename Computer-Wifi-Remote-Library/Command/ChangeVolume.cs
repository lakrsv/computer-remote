using AudioSwitcher.AudioApi.CoreAudio;
using Computer_Wifi_Remote.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Computer_Wifi_Remote_Library.Command
{
    public class ChangeVolume : ICommand
    {
        public string Name => nameof(ChangeVolume);

        private CoreAudioDevice defaultPlaybackDevice;

        public bool Execute(Request request)
        {
            if (!int.TryParse(request.Parameters[0], out int volumeChange)) return false;

            if (defaultPlaybackDevice == null)
            {
                defaultPlaybackDevice = new CoreAudioController().GetDefaultDevice(AudioSwitcher.AudioApi.DeviceType.Playback, AudioSwitcher.AudioApi.Role.Multimedia);
            }

            defaultPlaybackDevice.Volume += volumeChange;
            return true;
        }
    }
}
