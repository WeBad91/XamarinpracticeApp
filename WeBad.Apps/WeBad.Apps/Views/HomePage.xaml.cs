using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBad.Apps.Views.Hangman;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void HangmanButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HangmanLoadingPage());
        }

        private void ForecastButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WeatherForecast.WeatherForecastPage());
        }
    }
}