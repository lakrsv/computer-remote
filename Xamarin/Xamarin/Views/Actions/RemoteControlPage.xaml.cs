using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using Computer_Wifi_Remote_Xamarin.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
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

        private SKCanvasView canvasView;
        private SKBitmap skBitmap;

        public RemoteControlPage()
        {
            InitializeComponent();
            //ScreenCapture.BitmapOptimizations = true;
            //ScreenCapture.DownsampleToViewSize = true;
            //ScreenCapture.Finish += ScreenCapture_Finish;
            //ScreenCapture.Source = new StreamImageSource()
            //{
            //    Stream = async (token) => await Task.FromResult<Stream>(new MemoryStream(imageBytes))
            //};

            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;

            ClientConnection.Instance.Connection.OnMessageReceived += async (sender, e) =>
            {
                await Task.Run(() =>
                {
                    Debug.WriteLine("Got message");
                    if (e.IsBinary)
                    {
                        var response = BytesResponsePayload.Deserialize(e.RawData);

                        if (response.Metadata.Source == typeof(DisplayScreen))
                        {
                            // TODO - This won't work. High GC overhead and serious latency. Take a look at SkiaSharp!
                            //ScreenCapture.Source = ImageSource.FromStream(() => new MemoryStream(response.Payload));
                            //ScreenCapture.ReloadImage();
                            //viewModel.UpdateImage(response.Payload);
                            skBitmap = SKBitmap.Decode(response.Payload);
                            canvasView.InvalidateSurface();
                        }
                    }
                });
            };

            //Commands.ExecuteRemotely(ClientConnection.Instance.Connection, new Request(nameof(DisplayScreen)));
            cancellationToken = new CancellationToken();
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Commands.ExecuteRemotely(ClientConnection.Instance.Connection, new Request(nameof(DisplayScreen)));
                    await Task.Delay(16);
                }
            });
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();

            if (skBitmap != null)
            {
                float x = (info.Width - skBitmap.Width) / 2;
                float y = (info.Height / 3 - skBitmap.Height) / 2;
                canvas.DrawBitmap(skBitmap, x, y);
            }
        }

        //private void ScreenCapture_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        //{
        //    Commands.ExecuteRemotely(ClientConnection.Instance.Connection, new Request(nameof(DisplayScreen)));
        //}

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {

        }
    }
}