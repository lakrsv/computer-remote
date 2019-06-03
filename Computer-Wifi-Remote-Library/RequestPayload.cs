using Computer_Wifi_Remote.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Computer_Wifi_Remote_Library
{
    public class Request
    {
        public string Command { get; set; }
        public string[] Parameters { get; set; }

        public Request(string command, params string[] parameters)
        {
            Command = command;
            Parameters = parameters;
        }
    }
}
