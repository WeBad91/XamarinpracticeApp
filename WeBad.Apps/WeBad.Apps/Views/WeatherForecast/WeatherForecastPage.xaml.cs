using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBad.Apps.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.WeatherForecast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherForecastPage : ContentPage, IDisposable
    {
        public WeatherForecastPage()
        {
            InitializeComponent();
            BindingContext = new WeatherForecastViewModel();

            SetLayout(DeviceDisplay.MainDisplayInfo);
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

      
        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            SetLayout(e.DisplayInfo);
        }

        private void SetLayout(DisplayInfo displayInfo)
        {
            if (displayInfo.Orientation == DisplayOrientation.Portrait)
            {
                MainContainer.Orientation = StackOrientation.Vertical;
                DaysTemperatureContainer.Margin = new Thickness(0, -10, 0, 0);
            }
            else
            {
                MainContainer.Orientation = StackOrientation.Horizontal;
                DaysTemperatureContainer.Margin = new Thickness(0, 20, 0, 0);
            }
        }

        public void Dispose()
        {
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }

    }
}