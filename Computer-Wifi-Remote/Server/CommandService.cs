using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Computer_Wifi_Remote.Server
{
    public class CommandService : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var request = JsonConvert.DeserializeObject<Request>(e.Data);
            var command = Commands.GetCommand(request.Command);
            if (command != null)
            {
                command.Execute(request);
            }
        }
    }
}
