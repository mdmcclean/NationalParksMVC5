using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Mock
{
    public class ParkMock: IParkDAL
    {
        WeatherMock wth = new WeatherMock();
        Park park1 = new Park()
        {
            ParkCode = "GNP",
            ParkName = "first park",
            State = "Ohio",
            Acreage = 1000,
            ElevationInFt = 3432,
            MilesOfTrail = 50,
            NumberOfCampsites = 12,
            Climate = "Hot",
            YearFounded = 1999,
            AnnualVistorCount = 100000,
            InspirationalQuote = "fun place",
            QuoteSource = "me",
            EntryFee = 3,
            AnimalSpeciesCount = 15,
            ParkDescription = "You'll be lost in a trance here"
            
        };
        Park park2 = new Park()
        {
            ParkCode = "YNP",
            ParkName = "Second park",
            State = "Mississippi",
            Acreage = 4500,
            ElevationInFt = 3342,
            MilesOfTrail = 13,
            NumberOfCampsites = 19,
            Climate = "Cold",
            YearFounded = 1900,
            AnnualVistorCount = 1075,
            InspirationalQuote = "boring place",
            QuoteSource = "some dude",
            EntryFee = 6,
            AnimalSpeciesCount = 45,
            ParkDescription="A fun place to sit around and look at rocks"
            
            
        };

        public List<Park> Parks { get; set; } = new List<Park>();
        public ParkMock()
        {
            park1.FiveDayForecast = wth.GetFiveDayForecast(park1.ParkCode);
            park2.FiveDayForecast = wth.GetFiveDayForecast(park2.ParkCode);
            Parks.Add(park1);
            Parks.Add(park2);
        }

        public List<Park> GetAllParks()
        {
            return Parks;
        }
        public Park GetParkById(string id)
        {
            Park park = new Park();
            
            foreach(Park parkSearch in Parks)
            {
                if (parkSearch.ParkCode == id)
                {
                    park = parkSearch;
                }
            }

            return park;
        }
    }
}