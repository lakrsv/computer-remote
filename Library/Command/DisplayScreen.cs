using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;
using System.Text;
using Computer_Wifi_Remote_Library.Interop;
using System.IO;
using System.Drawing.Imaging;

namespace Computer_Wifi_Remote_Library.Command
{
    class DisplayScreen : ICommand<byte[]>
    {
        public string Name => nameof(DisplayScreen);

        public bool HasPayload => true;

        private ScreenCapture screenCapture = new ScreenCapture();

        public IResponsePayload<byte[]> Execute(Request request)
        {
            var image = screenCapture.CaptureScreen();
            using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return new ResponsePayload<byte[]>(true, ms.ToArray());
        }
    }
}
