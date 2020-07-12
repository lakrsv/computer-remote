using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server
{
    public class CommandService : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var request = JsonConvert.DeserializeObject<Request>(e.Data);
            var command = Commands.GetBytesCommand(request.Command);
            if (command != null)
            {
                var response = command.Execute(request);
                if (command.HasPayload)
                {
                    Send(response.Serialize());
                }
            }
        }
    }
}
