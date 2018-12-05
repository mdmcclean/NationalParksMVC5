using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class WeatherDAL: IWeatherDAL
    {
        private string _connectionString;

        /// <summary>
        /// Constructor for the WeatherDAL
        /// </summary>
        /// <param name="connectionString">the connection string to go to the database</param>
        public WeatherDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Given a string (the park code of a particular park), returns that
        /// park's weather info for a 5-day period, including low/high temps
        /// and the sunny/cloudy/rainy/snow forecast
        /// </summary>
        /// <returns>List of weather objects containing the 5-day forecast
        /// for a given park, displayed in the park Detail view</returns>
        /// <param name="id">the park code for which the method is getting weather data</param>

        public List<Weather> GetFiveDayForecast(string id)
        {
            List<Weather> weathterList = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();

                    // SQL string for retrieving all weather information for a given park (the id parameter above);
                    // returns these in ascending order of fiveDayForecastValue, which starts at 1 and ends at 5;
                    // parameterized query where the parameter passed in the method represents the parkCode value of the table
                    string SQL_GetWeather = "SELECT parkCode, fiveDayForecastValue, low, high, forecast " +
                                                "from weather where parkCode = @id " +
                                                "order by fiveDayForecastValue ASC";

                    cmd.CommandText = SQL_GetWeather;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Weather weather = new Weather
                        {
                            // takes all data from the park table of the NPGeek DB and sets it to the appropriate
                            // property of the new Weather object
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]),
                            LowTemp = Convert.ToInt32(reader["low"]),
                            HighTemp = Convert.ToInt32(reader["high"]),
                            Forecast = Convert.ToString(reader["forecast"])
                        };

                        weathterList.Add(weather);
                    }
                }
            }
            // generic exception if the SQL query fails for some reason
            catch (SqlException ex)
            {
                throw;
            }

            return weathterList;
        }
    }
}