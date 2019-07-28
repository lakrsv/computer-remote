using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Computer_Wifi_Remote_Xamarin.ViewModels
{
    public class ConnectViewModel : BaseViewModel
    {
        public ConnectViewModel()
        {
            Title = "Connect";

            ConnectCommand = new Command(ipAddress =>
            {
                Services.ClientConnection.Instance.Connect(ipAddress as string);
            });
        }

        public ICommand ConnectCommand { get; }
    }
}