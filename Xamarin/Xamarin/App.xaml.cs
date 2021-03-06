﻿using Computer_Wifi_Remote_Xamarin.Services;
using Computer_Wifi_Remote_Xamarin.Views;
using Xamarin.Forms;

namespace Computer_Wifi_Remote_Xamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ActionItemStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
