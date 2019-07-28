using Computer_Wifi_Remote_Library.Extensions;
using Computer_Wifi_Remote_Xamarin.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Computer_Wifi_Remote_Xamarin.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ConnectPage : ContentPage
    {
        public ConnectPage()
        {
            InitializeComponent();
            this.IPAddress.Text = Preferences.Get("ip_address", "");
            IPAddress.TextChanged += OnIpAddressChanged;
            Connect.Clicked += OnConnectClicked;
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            await TaskExtension.WaitUntil(() => ClientConnection.Instance.IsConnected, timeout: 5000);
            if (ClientConnection.Instance.IsConnected)
            {
                await DisplayAlert("Success!", "You are connected to your device", "OK");
                await Navigation.PushAsync(new ItemsPage(), true);
            }
            else
            {
                await DisplayAlert("Error!", "Failed connecting to your device", "OK");
            }
        }

        private void OnIpAddressChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("ip_address", e.NewTextValue);
        }
    }
}