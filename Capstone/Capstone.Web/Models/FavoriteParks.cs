using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class FavoriteParks
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public int SurveyCount { get; set; }
        public string InspirationalQuote { get; set; }
        public string QuoteSource { get; set; }
        public string State { get; set; }

    }
}