using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;
using System;

namespace Computer_Wifi_Remote_Library.Command
{
    public class HelloWorld : ICommand<bool>
    {
        public string Name => nameof(HelloWorld);
        public bool HasPayload => false;

        public IResponsePayload<bool> Execute(Request request)
        {
            Console.WriteLine("Hello World!");
            return ResponsePayload<bool>.NoPayloadSuccess();
        }
    }
}
