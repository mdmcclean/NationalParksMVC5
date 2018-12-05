using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkDAL: IParkDAL
    {
        private string _connectionString;

        /// <summary>
        /// Constructor for the ParkDAL
        /// </summary>
        /// <param name="connectionString">the connection string to go to the database</param>
        public ParkDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Method for populating a list containing all parks from the NPGeek DB's park table
        /// </summary>
        /// <returns>List of parks to be displayed in the Index view</returns>
        public List<Park> GetAllParks()
        {
            List<Park> parkList = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();

                    // SQL string for getting the 3 relevant attributes of each park from the DB;
                    // parkCode is used to find the park's image from our Content folder; parkName is
                    // the park's name, and parkDescription is a desciption of the park; the list is sorted
                    // alphabetically by park name, as noted in the project requirements
                    string SQL_GetParks = "SELECT parkCode, " +
                                                      "parkName, " +                                                      
                                                      "parkDescription " +
                                               "FROM park " +
                                               "ORDER BY parkName";

                    cmd.CommandText = SQL_GetParks;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //goes through each row returned from the database and adds it to a Park object
                        Park park = new Park
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),                            
                            ParkDescription = Convert.ToString(reader["parkDescription"])
                        };

                        parkList.Add(park);
                    }
                }
            }
            // generic exception if the SQL query fails for some reason 
            catch (SqlException ex)
            {
                throw;
            }

            return parkList;
        }

        /// <summary>
        /// Method for getting a specific park from the NPGeek DB's park table
        /// </summary>
        /// <returns>All attributes of a particular park, to be displayed in the Detail view</returns>
        /// <param name="id">the park code to specify which park to get</param>

        public Park GetParkById(string id)
        {
            Park park = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();

                    // SQL string for getting all attributes of the specified park from the DB;
                    // the query uses the method's id parameter and sets it to the parkCode to
                    // specify which park to return
                    string SQL_GetParkById = "SELECT * " +
                                               "FROM park " +
                                               "WHERE parkCode = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.CommandText = SQL_GetParkById;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();

                    //goes through each row returned from the database and adds it to the Park object
                    while (reader.Read())
                    {
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.ElevationInFt = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.AnnualVistorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.AnimalSpeciesCount = Convert.ToInt32(reader["numberOfAnimalSpecies"]);    
                    }
                }
            }
            // generic exception if the SQL query fails for some reason 
            catch (SqlException ex)
            {
                throw;
            }

            return park;
        }
    }
}