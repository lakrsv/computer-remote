using Computer_Wifi_Remote.Command;

namespace Computer_Wifi_Remote_Library.Command
{
    public class PlayStop : ICommand
    {
        public string Name => nameof(PlayStop);

        public bool Execute(Request request)
        {
            Audio.PlayStop();
            return true;
        }
    }
}
