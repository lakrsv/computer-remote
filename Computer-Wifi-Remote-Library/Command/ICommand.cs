using System;
using System.Collections.Generic;
using System.Text;

namespace Computer_Wifi_Remote.Command
{
    public interface ICommand
    {
        string Name { get; }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <returns>Whether the command executed successfully</returns>
        bool Execute();
    }
}
