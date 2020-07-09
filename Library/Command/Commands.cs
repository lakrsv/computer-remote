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
        private static Dictionary<string, ICommand> commands;

        static Commands()
        {
            var commandType = typeof(ICommand);
            commands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => commandType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(t => (ICommand)Activator.CreateInstance(t))
                .ToDictionary(k => k.Name, StringComparer.InvariantCultureIgnoreCase);
        }

        public static ICommand GetCommand(string name)
        {
            if (commands.ContainsKey(name))
            {
                return commands[name];
            }

            return null;
        }

        public static void ExecuteRemotely(IConnection connection, Request request)
        {
            connection.Send(JsonConvert.SerializeObject(request));
        }
    }
}
