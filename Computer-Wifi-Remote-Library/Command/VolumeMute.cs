using AudioSwitcher.AudioApi.CoreAudio;
using Computer_Wifi_Remote.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Computer_Wifi_Remote_Library.Command
{
    public class VolumeMute : ICommand
    {
        public string Name => nameof(VolumeMute);

        private CoreAudioDevice defaultPlaybackDevice;

        public bool Execute()
        {
            if (defaultPlaybackDevice == null)
            {
                defaultPlaybackDevice = new CoreAudioController().GetDefaultDevice(AudioSwitcher.AudioApi.DeviceType.Playback, AudioSwitcher.AudioApi.Role.Multimedia);
            }

            defaultPlaybackDevice.Mute(true);
            return true;
        }
    }
}
