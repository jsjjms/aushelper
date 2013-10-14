using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Robot.Debug;

namespace CommonService.Weather
{
    struct CityCode
    {
        public int index;
        public string code;
        public string city;
        public string city_chn;

        public CityCode(int i, string c, string ci, string c_chn)
        {
            index = i;
            code = c;
            city = ci;
            city_chn = c_chn;
        }
    }

    class YahooWeather
    {
        private ToFile toFile;
        public CityCode[] WeatherAustralia =
        {
            new CityCode(8, "ASXX0001", "Adelaide", "阿德莱德"),
            new CityCode(3, "ASXX0075", "Melbourne", "墨尔本"),
            new CityCode(2, "ASXX0112", "Sydney", "悉尼"),
            new CityCode(7, "ASXX0016", "Brisbane", "布里斯班"),
            new CityCode(4, "ASXX0057", "Hobart", "霍巴特"),
            new CityCode(5, "ASXX0032", "Darwin", "达尔文"),
            new CityCode(6, "ASXX0089", "Perth", "佩斯"),
            new CityCode(1, "ASXX0023", "Canberra", "堪培拉")
        };

        public YahooWeather()
        {
            toFile = new ToFile();
        }

        private string FToC(int f)
        {
            return Math.Round((f - 32) / 1.8, 1).ToString();
        }

        private string GetCodeByIndex(int iCity)
        {
            string code = "";
            for (int i = 0; i < WeatherAustralia.Length; i++)
            {
                if (iCity == WeatherAustralia[i].index)
                {
                    code = WeatherAustralia[i].code;
                    break;
                }
            }

            return code;
        }

        public string GetWeatherInAustralia(int iCity, string cityName_chn)
        {
            StringBuilder sb = new StringBuilder();
            int iCityNum = (iCity <= 0) ? WeatherAustralia.Length : 1;
            for (int i = 0; i < iCityNum; i++)
            {
                string CityCode = (iCity <= 0) ? WeatherAustralia[i].code : GetCodeByIndex(iCity);
                XmlDocument document = new XmlDocument();
                document.Load("http://xml.weather.yahoo.com/forecastrss?p=" + CityCode);

                XmlNodeList nodes = document.GetElementsByTagName("forecast", @"http://xml.weather.yahoo.com/ns/rss/1.0");

                foreach (XmlNode node in nodes)
                {
                    string cityName = (iCityNum == 1 ? cityName_chn : WeatherAustralia[i].city_chn);
                    sb.Append(cityName + "\r\n");
                    sb.Append("Date:" + node.Attributes["date"].InnerText + " [" + node.Attributes["day"].InnerText+ "] Weather: " + 
                        node.Attributes["text"].InnerText + ", Temperature: " +  FToC(int.Parse(node.Attributes["low"].InnerText)) + " to " + 
                        FToC(int.Parse(node.Attributes["high"].InnerText))+ " degree \r\n");
                    //<0 means getting the weather of whole country
                    if (iCity <= 0)
                    {
                        break;
                    }
                }
            }

            return sb.ToString();
        }
    }
}
