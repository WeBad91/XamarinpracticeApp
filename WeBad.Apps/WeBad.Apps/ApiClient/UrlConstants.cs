using System;
using System.Collections.Generic;
using System.Text;

namespace WeBad.Apps.ApiClient
{
    public static class UrlConstants
    {
        private static string API_KEY = "dc0f51f7cc700d74fb4df579d8752797";
        private static string BASE_URL = "https://api.openweathermap.org/data/2.5";

        public static string GET_WEATHER_DATA(string cityName)
        {
            
            return $"{BASE_URL}/weather?q={cityName}&appid={API_KEY}&lang=sr&units=metric";
        }

        public static string GET_FIVE_DAYS_FORECST(string cityName)
        {
            return $"{BASE_URL}/forecast?q={cityName}&appid={API_KEY}&lang=sr&units=metric";
        }
    }
}
