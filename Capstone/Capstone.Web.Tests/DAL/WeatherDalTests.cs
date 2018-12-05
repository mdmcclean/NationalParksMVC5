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
    public class WeatherDalTests
    {
        private TransactionScope tran = null;      // begins a transaction during initialize and rollback during cleanup
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security = true";    // connection string for NPGeek DB
        private int weatherId;                 //<-- used to hold the survey id of the row created for our test

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

                // Insert a Park object (only for testing purposes; isn't actually committed to the DB);
                // also inserts a Weather object matching the test park parkCode (again, only for testing)
                cmd = new SqlCommand("INSERT INTO park VALUES ('AAA', 'AAA Test National Park', 'Ohio', " +
                                     "12345, 1000, 100, 10, 'Woodland', 2018, 1500999, 'Lorem ipsum dolor', " +
                                     "'Unknow', 'Test description here', 20, 999); INSERT INTO weather VALUES " +
                                     "('AAA', 1, 20, 50, 'cloudy')", conn);
                weatherId = cmd.ExecuteNonQuery();
            }
        }

        // Cleanup runs after each test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); // disposes the transaction without committing changes to the DB
        }

        /// <summary>
        /// Test method for the WeatherDAL GetFiveDayForecast method
        /// </summary>
        [TestMethod()]
        public void GetFiveDayForecastTest()
        {
            // Arrange 
            // initializes a new WeatherDAL
            WeatherDAL weatherDal = new WeatherDAL(connectionString);

            // Act
            // runs the method using the fake park's parkCode as the id parameter,
            // and returns a list of weather objects
            List<Weather> parkWeather = weatherDal.GetFiveDayForecast("AAA"); 

            // Assert
            // checks the list of weather objects to make sure it's been populated
            // with at least 1 weather object
            Assert.IsTrue(parkWeather.Count > 0);     
            
            // Assert
            // checks that the list of weather objects has the same parkCode as the
            // test park which we created in our Initialize method above
            Assert.AreEqual("AAA", parkWeather[0].ParkCode);     
        }
    }
}
