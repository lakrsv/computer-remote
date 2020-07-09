using Computer_Wifi_Remote_Library.Connection;
using Computer_Wifi_Remote_Library.Extensions;
using Computer_Wifi_Remote_Xamarin.Services;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Services;

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
            //this.IPAddress.Text = Preferences.Get("ip_address", "");
            //IPAddress.TextChanged += OnIpAddressChanged;
            Connect.Clicked += OnConnectClicked;
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            var secret = SecureStorage.GetAsync("connection_payload").Result;
            if (secret != null)
            {
                var connectionPayload = JsonConvert.DeserializeObject<ConnectionPayload>(secret);
                ConnectToDevice(connectionPayload);
                return;
            }

            var scanner = DependencyService.Get<IQRScanningService>();
            var scanResult = await scanner.ScanAsync();
            if (scanResult != null)
            {
                var connectionPayload = JsonConvert.DeserializeObject<ConnectionPayload>(scanResult);
                ConnectToDevice(connectionPayload);
            }
            else
            {
                await DisplayAlert("Error!", "We could not connect using the supplied QR code", "OK");
                return;
            }
        }

        //private void OnIpAddressChanged(object sender, TextChangedEventArgs e)
        //{
        //    Preferences.Set("ip_address", e.NewTextValue);
        //}

        private async void ConnectToDevice(ConnectionPayload payload)
        {
            ClientConnection.Instance.Connect(payload);
            try
            {
                await TaskExtension.WaitUntil(() => ClientConnection.Instance.IsConnected, timeout: 5000);
            }
            catch (TimeoutException e)
            {
                Console.WriteLine("Connection timed out");
            }
            if (ClientConnection.Instance.IsConnected)
            {
                await SecureStorage.SetAsync("connection_payload", JsonConvert.SerializeObject(payload));
                await DisplayAlert("Success!", "You are connected to your device", "OK");
                await Navigation.PushAsync(new ItemsPage(), true);
            }
            else
            {
                await DisplayAlert("Error!", "Failed connecting to your device", "OK");
                SecureStorage.Remove("connection_payload");
            }
        }
    }
}