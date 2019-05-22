﻿using System;
using System.Diagnostics;

namespace Computer_Wifi_Remote.Command
{
    public class Shutdown : ICommand
    {
        public string Name => nameof(Shutdown);

        public bool Execute()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            var process = Process.Start(psi);

            return process.ExitCode == 0;
        }
    }
}