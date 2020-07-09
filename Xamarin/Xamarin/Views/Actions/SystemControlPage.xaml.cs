using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Xamarin.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Views.Actions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SystemControlPage : ContentPage
    {
        public SystemControlPage()
        {
            InitializeComponent();
        }

        private void Restart_Clicked(object sender, EventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.WebSocket, new Request(nameof(Restart)));
        }

        private void Shutdown_Clicked(object sender, EventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.WebSocket, new Request(nameof(Shutdown)));
        }
    }
}