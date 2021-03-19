using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WeBad.Apps.Models.WeatherForecast
{

    public class FiveDayForecastResponseModel
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public ForecastList[] list { get; set; }
        public ForecastCity city { get; set; }
    }

    public class ForecastCity
    {
        public int id { get; set; }
        public string name { get; set; }
        public ForecastCoord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }


    public class ForecastList
    {
        public int dt { get; set; }
        public ForecastMainData main { get; set; }
        public Weather[] weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public int visibility { get; set; }
        public float pop { get; set; }
        public Snow snow { get; set; }
        public Sys sys { get; set; }        
        private string _dt_txt;

        public string dt_txt
        {
            get { return _dt_txt; }
            set 
            { 
                _dt_txt = value;
                dt_converted = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            
            }
        }

        public DateTime dt_converted { get; set; }
        public Rain rain { get; set; }
    }

   


    public class Snow
    {
        public float _3h { get; set; }
    }



    public class Rain
    {
        public float _3h { get; set; }
    }


}
