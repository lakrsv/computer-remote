using Computer_Wifi_Remote.Command;

namespace Computer_Wifi_Remote_Library.Command
{
    public class PreviousTrack : ICommand
    {
        public string Name => nameof(PreviousTrack);

        public bool Execute(Request request)
        {
            Audio.PreviousTrack();
            return true;
        }
    }
}
