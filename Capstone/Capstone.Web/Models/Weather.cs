using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        // A park code string used in the SQL string for the Weather DAL
        public string ParkCode { get; set; }

        // An int used for representing the 5 days of each park's 
        // 5-day weather forecast (Detail view)
        public int FiveDayForecastValue { get; set; }

        // Ints representing high and low temps in the weather forecast (Detail view)
        public int LowTemp { get; set; }
        public int HighTemp { get; set; }

        // String representing the forecast (e.g. cloudy/sunny) for the weather forecast (Detail view)
        public string Forecast { get; set; }

        // A derived property which allows us to display the day of the week
        // on the Detail page; the numbers 0 - 6 represent each day, so if the
        // number goes above 6, we subtract 7 to bring it back down to the valid range
        public string DayOfWeekStr {
            get
            {
                DayOfWeek dt = DateTime.Now.DayOfWeek + FiveDayForecastValue - 1;
                if((int)dt > 6)
                {
                    dt = (DayOfWeek)((int)dt - 7);
                }
                return dt.ToString();
            }
        }

        // Derived properties for converting the temperature from Fahrenheit to Celsius (Detail view)
        public int HighTempCelsius
        {
            get
            {
                return (HighTemp - 32) * 5 / 9;
            }
        }
        public int LowTempCelsius
        {
            get
            {
                return (LowTemp - 32) * 5 / 9;
            }
        }     

        // Derived property for converting the "partly cloudy" forecast
        // to be "partlyCloudy" to pull the proper image from our Content folder
        public string ForecastImageName
        {
            get
            {
                if (Forecast == "partly cloudy")
                {
                    return "partlyCloudy";
                }
                else
                {
                    return Forecast;
                }
            }
        }

        // A derived property to return a string for the weather advisory;
        // These conditions aren't mutually exclusive, meaning it's possible for
        // multiple conditions to be met, so each advisory is appended to the advisory string
        public string ForecastAdvisory
        {
            get
            {
                string advisory = "";
                bool clearWeather = true;
                if(Forecast == "snow")
                {
                    advisory +=  "It might snow! Make sure you pack snowshoes! ";
                    clearWeather = false;
                }
                else if(Forecast == "rain")
                {
                    advisory += "It might rain! Make sure you pack rain gear and waterproof shoes! ";
                    clearWeather = false;
                }
                else if(Forecast == "thunderstorms")
                {
                    advisory += "Heavy rain expected; be prepared to seek shelter and avoid hiking on exposed ridges! ";
                    clearWeather = false;
                }
                else if(Forecast == "sunny")
                {
                    advisory += "Be prepared for sun exposure; bring sunblock! ";
                    clearWeather = false;
                }
                if (HighTemp > 75)
                {
                    advisory += "With high temperatures, bring an extra gallon of water! ";
                    clearWeather = false;
                }
                if ((HighTemp - LowTemp) > 20)
                {
                    advisory += "With large temperature change throughout the day, be sure to wear breathable layers! ";
                    clearWeather = false;
                }
                if (LowTemp < 20)
                {
                    advisory += "Very cold temperatures expected; stay warm and be careful of hypothermia! ";
                    clearWeather = false;
                }
                else
                {
                    advisory += "";
                }
                if(clearWeather)
                {
                    advisory += "Weather looks great, enjoy your stay!";
                }
                return advisory;
            }
        }

    }
}