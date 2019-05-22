using AudioSwitcher.AudioApi.CoreAudio;
using Computer_Wifi_Remote.Command;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace Computer_Wifi_Remote_Library.Command
{
    public class VolumeUnmute : ICommand
    {
        public string Name => nameof(VolumeUnmute);

        private CoreAudioDevice defaultPlaybackDevice;

        public bool Execute()
        {
            if (defaultPlaybackDevice == null)
            {
                defaultPlaybackDevice = new CoreAudioController().GetDefaultDevice(AudioSwitcher.AudioApi.DeviceType.Playback, AudioSwitcher.AudioApi.Role.Multimedia);
            }

            defaultPlaybackDevice.Mute(false);
            return true;
        }
    }
}
