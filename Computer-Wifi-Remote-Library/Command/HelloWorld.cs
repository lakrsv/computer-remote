using Computer_Wifi_Remote.Command;
using System;

namespace Computer_Wifi_Remote_Library.Command
{
    public class HelloWorld : ICommand
    {
        public string Name => nameof(HelloWorld);

        public bool Execute(Request request)
        {
            Console.WriteLine("Hello World!");
            return true;
        }
    }
}
