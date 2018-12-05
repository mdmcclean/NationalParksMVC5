using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Mock
{
    public class SurveyMock : ISurveyDAL
    {
        Survey survey1 = new Survey()
        {
            ActivityLevel = "Inactive",
            ParkCode = "GNP",
            EmailAddress = "bob@aol.com",
            State = "Ohio",
            SurveyId = 1
        };
        Survey survey2 = new Survey()
        {
            ActivityLevel = "Extremely Active",
            ParkCode = "GNP",
            EmailAddress = "bill@aol.com",
            State = "Kentucky",
            SurveyId = 2
        };
        Survey survey3 = new Survey()
        {
            ActivityLevel = "Active",
            ParkCode = "YNP",
            EmailAddress = "ben@aol.com",
            State = "Alabama",
            SurveyId = 3
        };
        public List<Survey> surveys { get; set; } = new List<Survey>();
        public SurveyMock()
        {
            surveys.Add(survey1);
            surveys.Add(survey2);
            surveys.Add(survey3);
        }
        public List<FavoriteParks> GetSurveys()
        {
            List<FavoriteParks> favParks = new List<FavoriteParks>();
            return favParks;
        }

        public bool PostSurvey(Survey newSurvey)
        {
            bool didWork = false;
            newSurvey.SurveyId = surveys.Count;
            surveys.Add(newSurvey);
            if (surveys.Count > 0) ;
            {
                didWork = true;
            }
            return didWork;
        }
    }
}