using System.Threading.Tasks;

namespace Xamarin.Services
{
    public interface IQRScanningService
    {
        Task<string> ScanAsync();
    }
}
