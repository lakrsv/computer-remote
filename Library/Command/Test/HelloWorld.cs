using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;
using System;
using System.Text;

namespace Computer_Wifi_Remote_Library.Command
{
    public class HelloWorld : IBytesCommand
    {
        public string Name => nameof(HelloWorld);
        public bool HasPayload => true;

        public IResponsePayload<byte[]> Execute(Request request)
        {
            Console.WriteLine("Hello World!");
            return BytesResponsePayload.PayloadSuccess(Encoding.UTF8.GetBytes("Hello World"), new ResponseMetadata(GetType()));
        }
    }
}
