using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;


namespace Capstone.Web.DAL
{
    public class SurveyDAL: ISurveyDAL
    {
        private string _connectionString;

        /// <summary>
        /// Constructor for the SurveyDAL
        /// </summary>
        /// <param name="connectionString">the connection string to go to the database</param>
        public SurveyDAL(string connectionString)
        {
            _connectionString = connectionString;
        }        

        /// <summary>
        /// Returns a list of Favorite parks that are ordered by the most votes received
        /// </summary>
        /// <returns>List of Favorite parks to be returned to the View</returns>
        public List<FavoriteParks> GetSurveys()
        {
            List<FavoriteParks> parkList = new List<FavoriteParks>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();

                    //SQL string to select all the parks that have received a Favorite Park vote ordered by the most favorite park votes
                    string SQL_GetFavoriteParks = "select park.parkName, park.state, park.parkCode, park.inspirationalQuote, park.inspirationalQuoteSource, " +
                        "count(survey_result.surveyId) as surveycount " +
                        "from survey_result join park on park.parkCode = " +
                        "survey_result.parkCode group by park.parkName, park.state, park.parkCode, " +
                        "park.inspirationalQuote, park.inspirationalQuoteSource order by surveycount desc, park.parkName";

                    cmd.CommandText = SQL_GetFavoriteParks;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //goes through each row returned from the database and adds it to a Favorite Parks object
                        FavoriteParks park = new FavoriteParks
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            SurveyCount = Convert.ToInt32(reader["surveycount"]),
                            InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                            QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                            State = Convert.ToString(reader["state"])
                        };

                        parkList.Add(park);
                    }

                }
            }
            //if the SQL query fails
            catch (SqlException ex)
            {
                throw;
            }

            //returns the list of Favorite Parks
            return parkList;
        }

        /// <summary>
        /// Posts a new survey from the Survey View page
        /// </summary>
        /// <param name="newSurvey">returns true if the survey post was successful</param>
        /// <returns></returns>
        public bool PostSurvey(Survey newSurvey)
        {
            bool didWork = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    //the SQL query to insert a new survey into the database
                    string SQL_InsertSurvey = "Insert Into survey_result (parkCode, emailAddress, state, activityLevel) " +
                        "Values (@parkCode, @emailAddress, @state, @activityLevel)";
                    cmd.CommandText = SQL_InsertSurvey;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    //if the query worked, didWork will be true
                    if(rowsAffected > 0)
                    {
                        didWork = true;
                    }

                }

            }
            catch(SqlException ex)
            {
                throw;
            }

            return didWork;
        }
    }
}