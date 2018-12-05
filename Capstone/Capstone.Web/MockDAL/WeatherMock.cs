using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Mock
{
    public class WeatherMock : IWeatherDAL
    {
        //Weather weather1 = new Weather()
        //{
        //    FiveDayForecastValue = 1,
        //    Forecast = "sunny",
        //    HighTemp = 70,
        //    LowTemp = 30,
        //};
        //Weather weather2 = new Weather()
        //{
        //    FiveDayForecastValue = 2,
        //    Forecast = "partly cludy",
        //    HighTemp = 56,
        //    LowTemp = 0,
        //};
        //Weather weather3 = new Weather()
        //{
        //    FiveDayForecastValue = 3,
        //    Forecast = "rain",
        //    HighTemp = 24,
        //    LowTemp = 19,
        //};
        //Weather weather4 = new Weather()
        //{
        //    FiveDayForecastValue = 4,
        //    Forecast = "snow",
        //    HighTemp = 90,
        //    LowTemp = 60,
        //};
        //Weather weather5 = new Weather()
        //{
        //    FiveDayForecastValue = 5,
        //    Forecast = "thunderstorms",
        //    HighTemp = 63,
        //    LowTemp = 21,
        //};

        public List<Weather> FiveDays { get; set; }

        //public WeatherMock()
        //{
        //    FiveDays.Add(weather1);
        //    FiveDays.Add(weather2);
        //    FiveDays.Add(weather3);
        //    FiveDays.Add(weather4);
        //    FiveDays.Add(weather5);

        //}

        public List<Weather> GetFiveDayForecast(string id)
        {
            Weather weather1 = new Weather()
            {
                FiveDayForecastValue = 1,
                Forecast = "sunny",
                HighTemp = 70,
                LowTemp = 30,
            };
            Weather weather2 = new Weather()
            {
                FiveDayForecastValue = 2,
                Forecast = "partly cludy",
                HighTemp = 56,
                LowTemp = 0,
            };
            Weather weather3 = new Weather()
            {
                FiveDayForecastValue = 3,
                Forecast = "rain",
                HighTemp = 24,
                LowTemp = 19,
            };
            Weather weather4 = new Weather()
            {
                FiveDayForecastValue = 4,
                Forecast = "snow",
                HighTemp = 90,
                LowTemp = 60,
            };
            Weather weather5 = new Weather()
            {
                FiveDayForecastValue = 5,
                Forecast = "thunderstorms",
                HighTemp = 63,
                LowTemp = 21,
            };
            List<Weather> FiveDays = new List<Weather>();
            FiveDays.Add(weather1);
            FiveDays.Add(weather2);
            FiveDays.Add(weather3);
            FiveDays.Add(weather4);
            FiveDays.Add(weather5);
            foreach (Weather wth in FiveDays)
            {
                wth.ParkCode = id;
            }


            return FiveDays;

        }
        public string getDateString(int day)
        {
            string dayInWeek = "";
            DayOfWeek dt = DateTime.Now.DayOfWeek - 1 + day;
            dayInWeek = dt.ToString();
            
            return dayInWeek;
        }

    }
}