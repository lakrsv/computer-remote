using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Services
{
    public interface IQRScanningService
    {
        Task<string> ScanAsync();
    }
}
