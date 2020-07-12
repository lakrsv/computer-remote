using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library.Response;
using Computer_Wifi_Remote_Library.Interop;
using System.IO;
using System.Drawing.Imaging;

namespace Computer_Wifi_Remote_Library.Command
{
    public class DisplayScreen : IBytesCommand
    {
        public string Name => nameof(DisplayScreen);

        public bool HasPayload => true;

        private ScreenCapture screenCapture = new ScreenCapture();

        public IResponsePayload<byte[]> Execute(Request request)
        {
            var image = screenCapture.CaptureScreen();
            using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return new BytesResponsePayload(true, ms.ToArray(), new ResponseMetadata(GetType()));
        }
    }
}
