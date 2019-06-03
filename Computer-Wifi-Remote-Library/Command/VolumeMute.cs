using AudioSwitcher.AudioApi.CoreAudio;
using Computer_Wifi_Remote.Command;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace Computer_Wifi_Remote_Library.Command
{
    public class VolumeMute : ICommand
    {
        public string Name => nameof(VolumeMute);

        private CoreAudioDevice defaultPlaybackDevice;

        public bool Execute(Request request)
        {
            if (!bool.TryParse(request.Parameters[0], out bool mute)) return false;

            if (defaultPlaybackDevice == null)
            {
                defaultPlaybackDevice = new CoreAudioController().GetDefaultDevice(AudioSwitcher.AudioApi.DeviceType.Playback, AudioSwitcher.AudioApi.Role.Multimedia);
            }


            defaultPlaybackDevice.Mute(mute);
            return true;
        }
    }
}
