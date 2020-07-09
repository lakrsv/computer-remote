using Computer_Wifi_Remote.Command;
using Computer_Wifi_Remote_Library;
using Computer_Wifi_Remote_Library.Command;
using Computer_Wifi_Remote_Xamarin.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Views.Actions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelloWorldPage : ContentPage
    {
        public HelloWorldPage()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Clicked(object sender, EventArgs e)
        {
            Commands.ExecuteRemotely(ClientConnection.Instance.Connection, new Request(nameof(HelloWorld)));
        }
    }
}