using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL.Tests
{
    [TestClass()]
    public class SurveyDalTests
    {
        private TransactionScope tran = null;      // begins a transaction during initialize and rollback during cleanup
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security = true"; // connection string for NPGeek DB
        private int surveyId;                      // used to hold the survey id of the row created for our test

        // Set up the database before each test        
        [TestInitialize]
        public void Initialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                // Insert a Park object (just for testing)
                cmd = new SqlCommand("INSERT INTO park VALUES ('ZZZ', 'ZZZ Test National Park', 'Ohio', 12345, 1000, 100, 10, 'Woodland', 2018, 1500999, 'Lorem ipsum dolor', 'Unknow', 'Lame description here', 20, 999);", conn);
                cmd.ExecuteNonQuery();

                // Insert a test survey 
                // If we want to check the new id of the record inserted we can use
                // SELECT CAST(SCOPE_IDENTITY() as int) as a work-around
                // This will get the newest identity value generated for the record most recently inserted
                cmd = new SqlCommand("INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES ('ZZZ', 'zzz@zzz.com', 'Ohio', 'Active'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                surveyId = (int)cmd.ExecuteScalar();
            }
        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); // disposes the transaction without committing changes to the DB
        }

        /// <summary>
        /// Test method for the SurveyDAL GetSurveys method
        /// </summary>
        [TestMethod()]
        public void GetSurveys()
        {
            // Arrange 
            // initializes a new SurveyDAL with the connectionString parameter
            SurveyDAL surveyDal = new SurveyDAL(connectionString);

            // Act
            // runs the GetSurveys method to get a FavoriteParks object (which is a list of surveys 
            // grouped by # of surveys for each park) from the survey_result table
            List<FavoriteParks> surveys = surveyDal.GetSurveys();

            // Assert
            // Checks to make sure the FavoritePark object is no longer empty
            Assert.IsTrue(surveys.Count > 0);

            // Assert
            // Checks to make sure the last FavoritePark object is for the test park which
            // we made in the Initialize method above; the GetSurveys method sorts by the 
            // count of surveys (descending), then alphabetically by park name (ascending), so in this case the 
            // test park would only show up once, and alphabetically would be last, so it should be the last
            // entry in the list
            Assert.AreEqual("ZZZ", surveys[surveys.Count - 1].ParkCode);   
        }

        /// <summary>
        /// Test method for the PostSurvey method in the ParkDAL; the PostSurvey method
        /// returns a bool, which is used in the test
        /// </summary>
        [TestMethod()]
        public void PostSurveyTest()
        {
            // Arrange 
            // initializes a new SurveyDAL with the connectionString parameter
            SurveyDAL surveyDal = new SurveyDAL(connectionString);

            // Act
            // creates a new list of surveys and adds a mock survey (never committed
            // to the DB, just used for testing)
            List<Survey> surveys = new List<Survey>();
            Survey newSurvey = new Survey()
            {
                ParkCode = "ZZZ",
                EmailAddress = "bob@email.com",
                State = "Ohio",
                ActivityLevel = "Active"
            };

            // Assert
            // the PostSurvey method returns a bool, so we check to make sure the bool
            // is true, meaning the PostSurvey method has succeeded
            Assert.IsTrue(surveyDal.PostSurvey(newSurvey));   
        }
    }
}
