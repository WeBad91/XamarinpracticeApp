using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeBad.Apps.ApiClient;
using WeBad.Apps.Models.WeatherForecast;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeBad.Apps.ViewModels
{
    public class WeatherForecastViewModel : BaseViewModel
    {
        private LayoutState currentState;
        private DateTime? currentDate;
        private string cityName;
        private string currentTemperature;
        private string maxTemperature;
        private string minTemperature;
        private string weatherDescription;
        private string weatherIcon;
        private Color backgroundStartColor;
        private Color backgroundEndColor;
        private ObservableCollection<DayForecastModel> fiveDaysItems;

        public ICommand RefreshCommand { get; }


        public LayoutState CurrentState
        {
            get { return currentState; }
            set { SetProperty(ref currentState, value); }
        }

        public DateTime? CurrentDate
        {
            get { return currentDate; }
            set { SetProperty(ref currentDate, value); }
        }

        public string CityName
        {
            get { return cityName; }
            set { SetProperty(ref cityName, value); }
        }        

        public string CurrentTemperature
        {
            get { return currentTemperature; }
            set { SetProperty(ref currentTemperature, value); }
        }

        public string MaxTemperature
        {
            get { return maxTemperature; }
            set { SetProperty(ref maxTemperature, value); }
        }

        public string MinTemperature
        {
            get { return minTemperature; }
            set { SetProperty(ref minTemperature, value); }
        }

        public string WeatherDescription
        {
            get { return weatherDescription; }
            set { SetProperty(ref weatherDescription, value); }
        }

        public string WeatherIcon
        {
            get { return weatherIcon; }
            set { SetProperty(ref weatherIcon, value); }
        }

        public Color BackgroundColorStart
        {
            get { return backgroundStartColor; }
            set { SetProperty(ref backgroundStartColor, value); }
        }

        public Color BackgroundColorEnd
        {
            get { return backgroundEndColor; }
            set { SetProperty(ref backgroundEndColor, value); }
        }
        
        public ObservableCollection<DayForecastModel> FiveDaysItems
        {
            get { return fiveDaysItems; }
            set { SetProperty(ref fiveDaysItems, value); }
        }
       

        public WeatherForecastViewModel()
        {
            CurrentState = LayoutState.Loading;
            BackgroundColorStart = Color.FromHex("#FEC28E");
            BackgroundColorEnd = Color.FromHex("#FFA890");
            InitData();

            RefreshCommand = new Command(async() =>
            {
                CurrentState = LayoutState.Loading;
                await Task.Delay(1000);
                await InitData();
            });
        }

        private async ValueTask InitData()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                CurrentState = LayoutState.Error;
            }
            else
            {
                WeatherForecastApiClient apiClient = new WeatherForecastApiClient();

                var data = await apiClient.GetCurrentForecast();
                var fiveDaysData = await apiClient.GetFiveDaysForecast();

                if (data != null)
                {
                    CurrentTemperature = ((int)data.main.temp).ToString();
                    MaxTemperature = ((int)data.main.temp_max).ToString();
                    MinTemperature = ((int)data.main.temp_min).ToString();
                    WeatherDescription = data.weather[0].description;
                    WeatherIcon = GetIconByCode(data.weather[0].icon);
                    SetColorsByIcon(WeatherIcon);
                    CityName = data.name;
                    CurrentDate = DateTime.UtcNow.AddSeconds(data.timezone);
                    if (fiveDaysData != null)
                    {

                        var fiveDayTemperatures = fiveDaysData.list.Where(x => x.dt_converted.Date > DateTime.Now && x.dt_converted.Hour == 12)
                                                                    .Select(x => new DayForecastModel
                                                                    {
                                                                        Icon = GetIconByCode(x.weather[0].icon),
                                                                        NameOfDay = x.dt_converted.DayOfWeek.ToString(),
                                                                        Temperature = ((int)x.main.temp).ToString()
                                                                    });
                        FiveDaysItems = new ObservableCollection<DayForecastModel>(fiveDayTemperatures);

                    }

                    CurrentState = LayoutState.Success;
                }
            }

           

        }


        private string GetIconByCode(string code)
        {
            string retVal = string.Empty;
            switch (code)
            {
                case "01d":
                    retVal = "clear_sky";
                    break;
                case "01n":
                    retVal = "few_clouds";
                    break;
                case "02d":
                    retVal = "few_clouds";
                    break;
                case "02n":
                    retVal = "few_clouds";
                    break;
                case "03d":
                    retVal = "scattered_clouds";
                    break;
                case "03n":
                    retVal = "scattered_clouds";
                    break;
                case "04d":
                    retVal = "broken_clouds";
                    break;
                case "04n":
                    retVal = "broken_clouds";
                    break;
                case "09d":
                    retVal = "shower_rain";
                    break;
                case "09n":
                    retVal = "shower_rain";
                    break;
                case "10d":
                    retVal = "rain";
                    break;
                case "10n":
                    retVal = "rain";
                    break;
                case "11d":
                    retVal = "thunderstorm";
                    break;
                case "11n":
                    retVal = "thunderstorm";
                    break;
                case "13d":
                    retVal = "snow";
                    break;
                case "13n":
                    retVal = "snow";
                    break;
                case "50d":
                    retVal = "mist";
                    break;
                case "50n":
                    retVal = "mist";
                    break;               


            }

            return retVal;
        }

        private void SetColorsByIcon(string icon)
        {
            string[] firstColorIcons = new string[] { "clear_sky", "few_clouds", "rain" };
            string[] secondColorIcons = new string[] { "scattered_clouds", "snow", "mist" };
            string[] thirdColorIcons = new string[] { "broken_clouds", "shower_rain", "thunderstorm" };


            
            if (firstColorIcons.Any(x => x.Equals(icon)))
            {
                BackgroundColorStart = Color.FromHex("#FEC28E");
                BackgroundColorEnd = Color.FromHex("#FFA890");
            }
            else if (secondColorIcons.Any(x => x.Equals(icon)))
            {
                BackgroundColorStart = Color.FromHex("#79BEC2");
                BackgroundColorEnd = Color.FromHex("#646D97");
            }
            else if (thirdColorIcons.Any(x => x.Equals(icon)))
            {
                BackgroundColorStart = Color.FromHex("#A38BA2");
                BackgroundColorEnd = Color.FromHex("#A38BA2");
            }


        }

    }
}
