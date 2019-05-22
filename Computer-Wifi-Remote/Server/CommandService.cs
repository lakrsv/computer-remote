using Computer_Wifi_Remote.Command;
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
            var message = e.Data;
            var command = Commands.GetCommand(message);
            if(command != null)
            {
                command.Execute();
            }
        }
    }
}
