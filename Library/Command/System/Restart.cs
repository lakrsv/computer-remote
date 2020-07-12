using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using Computer_Wifi_Remote_Library.Response;
using System.Diagnostics;

namespace Computer_Wifi_Remote.Command
{
    public class Restart : IBytesCommand
    {
        public string Name => nameof(Restart);
        public bool HasPayload => false;

        public IResponsePayload<byte[]> Execute(Request request)
        {
            var psi = new ProcessStartInfo("shutdown", "/r /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            var process = Process.Start(psi);

            return process.ExitCode == 0 ?
                BytesResponsePayload.NoPayloadSuccess(new ResponseMetadata(GetType())) :
                BytesResponsePayload.NoPayloadFailure(new ResponseMetadata(GetType()));
        }
    }
}
