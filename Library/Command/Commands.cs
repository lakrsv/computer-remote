using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computer_Wifi_Remote.Command
{
    public static class Commands
    {
        private static Dictionary<string, ICommand<bool>> noPayloadCommands;

        static Commands()
        {
            var noResponseCommandType = typeof(ICommand<bool>);
            noPayloadCommands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => noResponseCommandType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(t => (ICommand<bool>)Activator.CreateInstance(t))
                .ToDictionary(k => k.Name, StringComparer.InvariantCultureIgnoreCase);
        }

        public static ICommand<bool> GetCommand(string name)
        {
            if (noPayloadCommands.ContainsKey(name))
            {
                return noPayloadCommands[name];
            }

            return null;
        }

        public static void ExecuteRemotely(IConnection connection, Request request)
        {
            connection.Send(JsonConvert.SerializeObject(request));
        }
    }
}
