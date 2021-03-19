using System;
using WeBad.Apps.Views;
using WeBad.Apps.Views.WeatherForecast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
