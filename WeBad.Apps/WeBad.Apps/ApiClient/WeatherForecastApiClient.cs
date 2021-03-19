using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeBad.Apps.Models.WeatherForecast;

namespace WeBad.Apps.ApiClient
{
    public class WeatherForecastApiClient
    {
        public async Task<ForecastResponseModel> GetCurrentForecast()
        {
            ForecastResponseModel retval = null;
            Uri uri = new Uri(string.Format(UrlConstants.GET_WEATHER_DATA("Beograd"), string.Empty));
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        retval = JsonConvert.DeserializeObject<ForecastResponseModel>(content);
                    }
                }
            }

            return retval;
        }

        public async Task<FiveDayForecastResponseModel> GetFiveDaysForecast()
        {
            FiveDayForecastResponseModel retval = null;
            Uri uri = new Uri(string.Format(UrlConstants.GET_FIVE_DAYS_FORECST("Beograd"), string.Empty));
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        retval = JsonConvert.DeserializeObject<FiveDayForecastResponseModel>(content);
                    }
                }
            }

            return retval;
        }
    }
}
