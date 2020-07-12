using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Response;
using System.Diagnostics;

namespace Computer_Wifi_Remote.Command
{
    public class Shutdown : ICommand<bool>
    {
        public string Name => nameof(Shutdown);
        public bool HasPayload => false;

        public IResponsePayload<bool> Execute(Request request)
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            var process = Process.Start(psi);

            return process.ExitCode == 0 ?
                ResponsePayload<bool>.NoPayloadSuccess() :
                ResponsePayload<bool>.NoPayloadFailure();
        }
    }
}
