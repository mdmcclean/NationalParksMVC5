using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Capstone.Web.Mock;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {        
        private IParkDAL _parkDAL;
        private ISurveyDAL _surveyDAL;
        private IWeatherDAL _weatherDAL;

        /// <summary>
        /// Constructor for the Controller
        /// </summary>
        /// <param name="park">IParkDAL object that is made using dependency injection</param>
        /// <param name="survey">ISurveyDAL object that is made using dependency injection</param>
        /// <param name="weather">IWeatherDAL object that is made using dependency injection</param>
        public HomeController(IParkDAL park, ISurveyDAL survey, IWeatherDAL weather)
        {
            _parkDAL = park;
            _surveyDAL = survey;
            _weatherDAL = weather;
        }

        /// <summary>
        /// Homepage that gets a list of all the parks in the database to display to the Index View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = _parkDAL.GetAllParks();

            return View("Index", model);
        }

        /// <summary>
        /// Takes in the park id to find a park by the id from the database to display a detail page
        /// </summary>
        /// <param name="id">The park id to search for</param>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            //Sets the initial Session["isFahrenheit"] variable if this is the first time visiting the page
            if(Session["isFahrenheit"] == null)
            {
                Session["isFahrenheit"] = true;
            }

            //gets all the details needed for the Park Detail page
            var park = _parkDAL.GetParkById(id);
            TempData["id"] = id.ToString();
            park.FiveDayForecast = _weatherDAL.GetFiveDayForecast(id);
            return View("Detail", park);
        }
        
        /// <summary>
        /// Action Result called if the user clicks the Change temperature to C/F from the detail page
        /// </summary>
        /// <param name="id">The park id to be passed back into the Detail View</param>
        /// <returns></returns>
        public ActionResult ChangeTemp(string id)
        {
            //switches the session variable to the opposite measurement
            Session["isFahrenheit"] = !(bool)Session["isFahrenheit"];
            //goes back to the detail view
            return RedirectToAction("Detail", "Home", new { id });
        }

        /// <summary>
        /// Pulls up the Survey view. passes in a list of parks for the survey
        /// </summary>
        /// <returns></returns>
        public ActionResult Survey()
        {
            return View("Survey", GetListOfParks());
        }

        /// <summary>
        /// Does an Http Post to post a new survey to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Survey(Survey model)
        {
            //checks to make sure all of the data in the Survey object that is required passed in
            if(!ModelState.IsValid)
            {
                //returns to the Survey View if not all the data that is required is not passed in
                return View("Survey", GetListOfParks());
            }

            //if the data is valid, the controller redirects to the FavoritePark View
            _surveyDAL.PostSurvey(model);
            return RedirectToAction("FavoritePark");
        }

        /// <summary>
        /// Returns a list of Favorite Parks to the Favorite Parks display
        /// </summary>
        /// <returns></returns>
        public ActionResult FavoritePark()
        {
            //sets a list of favorite park objects to send to the Favorite Park View 
            var surveys = _surveyDAL.GetSurveys();
            return View("FavoritePark", surveys);
        }

        /// <summary>
        /// Gets a list of Select List Item objects to return to the survey view for a dropdown menu
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetListOfParks()
        {
            List<SelectListItem> listOfParks = new List<SelectListItem>();
            List<Park> parks = _parkDAL.GetAllParks();
            foreach (Park park in parks)
            {
                listOfParks.Add(new SelectListItem { Text = park.ParkName, Value = park.ParkCode });
            }
            return listOfParks;
        }
    }
}