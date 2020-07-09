namespace Computer_Wifi_Remote_Library
{
    public class Request
    {
        public string Command { get; private set; }
        public string[] Parameters { get; private set; }

        public Request(string command, params string[] parameters)
        {
            Command = command;
            Parameters = parameters;
        }
    }
}
