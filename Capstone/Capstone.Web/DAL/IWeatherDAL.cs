using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IWeatherDAL
    {
        List<Weather> GetFiveDayForecast(string id);
    }
}