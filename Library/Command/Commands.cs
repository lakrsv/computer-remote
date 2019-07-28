using Computer_Wifi_Remote_Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;

namespace Computer_Wifi_Remote.Command
{
    public static class Commands
    {
        private static Dictionary<string, ICommand> commands;

        static Commands()
        {
            var commandType = typeof(ICommand);
            commands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => commandType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(t => (ICommand)Activator.CreateInstance(t))
                .ToDictionary(k => k.Name.ToLowerInvariant());
        }

        public static ICommand GetCommand(string name)
        {
            name = name.ToLowerInvariant();
            if (commands.ContainsKey(name))
            {
                return commands[name];
            }

            return null;
        }

        public static void ExecuteRemotely(WebSocket webSocket, Request request)
        {
            webSocket.Send(JsonConvert.SerializeObject(request));
        }
    }
}
