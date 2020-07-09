using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Computer_Wifi_Remote_Xamarin.Droid.Services;
using Xamarin.Forms;
using Xamarin.Services;
using ZXing.Mobile;

[assembly: Dependency(typeof(QRScanningService))]
namespace Computer_Wifi_Remote_Xamarin.Droid.Services
{
    public class QRScanningService : IQRScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}