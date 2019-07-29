using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private ServerBootstrap server;
        private NotifyIcon notifyIcon;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            server.Stop();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            server = new ServerBootstrap();
            server.Start();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("Resources/icons8-check-all-64.ico");
            notifyIcon.Visible = true;
            notifyIcon.Text = "Server running!";

            var menuStrip = new ContextMenuStrip();
            var help = new ToolStripMenuItem("Help");
            help.Name = "Help";
            var exit = new ToolStripMenuItem("Exit");
            exit.Name = "Exit";
            help.Click += Help_Click;
            exit.Click += (o, args) => System.Windows.Application.Current.Shutdown(0);

            menuStrip.Items.Add(help);
            menuStrip.Items.Add(exit);

            notifyIcon.ContextMenuStrip = menuStrip;
        }

        private void Help_Click(object sender, EventArgs e)
        {
            var hostName = Dns.GetHostName();
            var ipEntry = Dns.GetHostEntry(hostName);
            var addr = ipEntry.AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            System.Windows.MessageBox.Show("To connect to your device, use your smartphone client and enter " + addr);
        }
    }
}
