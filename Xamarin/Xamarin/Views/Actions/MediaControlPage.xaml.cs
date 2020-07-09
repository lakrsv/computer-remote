using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using Computer_Wifi_Remote_Xamarin.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Views.Actions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaControlPage : ContentPage
    {
        public MediaControlPage()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.WebSocket, new Request(nameof(ChangeVolume), ((float)e.NewValue).ToString()));
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.WebSocket, new Request(nameof(MuteVolume), e.Value.ToString()));
        }

        private void OnNextTrackClicked(object sender, EventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.WebSocket, new Request(nameof(NextTrack)));

        }

        private void OnPreviousTrackClicked(object sender, EventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.WebSocket, new Request(nameof(PreviousTrack)));

        }

        private void OnPlayPauseClicked(object sender, EventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.WebSocket, new Request(nameof(PlayStop)));
        }
    }
}