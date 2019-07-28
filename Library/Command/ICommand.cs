﻿using Computer_Wifi_Remote_Library;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace Computer_Wifi_Remote.Command
{
    public interface ICommand
    {
        string Name { get; }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <returns>Whether the command executed successfully</returns>
        bool Execute(Request request);
    }
}