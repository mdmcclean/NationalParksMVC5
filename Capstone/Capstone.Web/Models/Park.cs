using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Park
    {
        // Properties representing each column of the park table for parks
        // from our NPGeek database
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int Acreage { get; set; }
        public int ElevationInFt { get; set; }
        public double MilesOfTrail { get; set; }
        public int NumberOfCampsites { get; set; }
        public string Climate { get; set; }
        public int YearFounded { get; set; }
        public int AnnualVistorCount { get; set; }
        public string InspirationalQuote { get; set; }
        public string QuoteSource { get; set; }
        public string ParkDescription { get; set; }
        public int EntryFee { get; set; }
        public int AnimalSpeciesCount { get; set; }

        // A list of Weather objects, populated in the Weather DAL;
        // used in the Detail view to show the 5-day forecast
        public List<Weather> FiveDayForecast { get; set; }

    }
}