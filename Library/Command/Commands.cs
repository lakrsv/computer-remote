using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using Computer_Wifi_Remote_Library.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computer_Wifi_Remote.Command
{
    public static class Commands
    {
        private static Dictionary<string, IBytesCommand> bytesCommands;

        static Commands()
        {
            var commandType = typeof(IBytesCommand);
            bytesCommands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => commandType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(t => (IBytesCommand)Activator.CreateInstance(t))
                .ToDictionary(k => k.Name, StringComparer.InvariantCultureIgnoreCase);
        }

        public static bool CommandHasPayload(string name)
        {
            return bytesCommands.ContainsKey(name) && bytesCommands[name].HasPayload;
        }

        public static IBytesCommand GetBytesCommand(string name)
        {
            return bytesCommands.ContainsKey(name) ? bytesCommands[name] : null;
        }

        public static void ExecuteRemotely(IConnection connection, Request request)
        {
            connection.Send(JsonConvert.SerializeObject(request));
        }
    }
}
