using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using Computer_Wifi_Remote_Xamarin.Services;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Views.Actions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemoteControlPage : ContentPage
    {
        private CancellationToken cancellationToken;

        public RemoteControlPage()
        {
            InitializeComponent();

            ClientConnection.Instance.Connection.OnMessageReceived += (sender, e) =>
            {
                Debug.WriteLine("Got message");
                if (e.IsBinary)
                {
                    var response = BytesResponsePayload.Deserialize(e.RawData);

                    if (response.Metadata.Source == typeof(DisplayScreen))
                    {
                        // TODO - This won't work. High GC overhead and serious latency
                        ScreenCapture.Source = ImageSource.FromStream(() => new MemoryStream(response.Payload));
                        //viewModel.UpdateImage(response.Payload);
                    }
                }
            };

            cancellationToken = new CancellationToken();
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Commands.ExecuteRemotely(ClientConnection.Instance.Connection, new Request(nameof(DisplayScreen)));
                    await Task.Delay(1000);
                }
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {

        }
    }
}