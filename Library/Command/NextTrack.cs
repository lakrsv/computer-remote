using Computer_Wifi_Remote.Command;

namespace Computer_Wifi_Remote_Library.Command
{
    public class NextTrack : ICommand
    {
        public string Name => nameof(NextTrack);

        public bool Execute(Request request)
        {
            Audio.NextTrack();
            return true;
        }
    }
}
