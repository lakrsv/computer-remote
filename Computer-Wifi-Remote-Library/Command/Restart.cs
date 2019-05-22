using System;
using System.Diagnostics;

namespace Computer_Wifi_Remote.Command
{
    public class Restart : ICommand
    {
        public string Name => nameof(Restart);

        public bool Execute()
        {
            var psi = new ProcessStartInfo("shutdown", "/r /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            var process = Process.Start(psi);

            return process.ExitCode == 0;
        }
    }
}
