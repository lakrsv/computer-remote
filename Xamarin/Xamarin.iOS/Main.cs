using FFImageLoading.Forms.Platform;
using UIKit;

namespace Computer_Wifi_Remote_Xamarin.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
