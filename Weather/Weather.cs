using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot.Debug;

namespace CommonService.Weather
{
    public enum WeatherLocation
    {
        WL_UNKNOWN = 0,
        WL_AUS = 0,
        WL_SA = 8,
        WL_VIC = 3,
        WL_NSW = 2,
        WL_TAS = 4,
        WL_ACT = 1,
        WL_WA = 6,
        WL_NT = 5,
        WL_QLD = 7,
    }

    /// <summary>
    /// return the weather according to the location and weixin ID
    /// </summary>
    public class Weather
    {
        private WeatherLocation iLocation = WeatherLocation.WL_UNKNOWN;
        private ToFile toFile = new ToFile();

        YahooWeather yw;
        private string city_name = "";
        /// <summary>
        /// return the weather for all australia
        /// </summary>
        public Weather()
        {
            yw = new YahooWeather();
            iLocation = WeatherLocation.WL_AUS;
        }

        //return weather for a city
        public Weather(string city)
        {
            iLocation = WeatherLocation.WL_AUS;
            yw = new YahooWeather();
            for (int i = 0; i < yw.WeatherAustralia.Length; i++)
            {
                if (city == yw.WeatherAustralia[i].city.ToLower() || city == yw.WeatherAustralia[i].city.ToLower().Substring(0, 3))
                {
                    iLocation = (WeatherLocation)yw.WeatherAustralia[i].index;
                    city_name = yw.WeatherAustralia[i].city_chn;
                    break;
                }
            }
        }
        /// <summary>
        /// get the weather according to location
        /// </summary>
        /// <param name="iLoc"></param>
        /// <returns></returns>
        public string GetWeather()
        {
            StringBuilder ws = new StringBuilder();
            ws.Append(yw.GetWeatherInAustralia((int)iLocation, city_name));
            return ws.ToString();
        }
    }
}
