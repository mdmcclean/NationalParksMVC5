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
    public class ParkDalTests
    {
        private TransactionScope tran = null;      // begins a transaction during initialize and rollback during cleanup
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NPGeek;Integrated Security = true";

        // Set up the database with a mock park before each test        
        [TestInitialize]
        public void Initialize()
        {
            // Initialize a new transaction scope (automatically begins the transaction)
            tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                // Insert a Park object (just for testing)
                cmd = new SqlCommand("INSERT INTO park VALUES ('AAA', 'AAA Test National Park', 'Ohio', 12345, 1000, 100, 10, 'Woodland', 2018, 1500999, 'Lorem ipsum dolor', 'Unknow', 'Lame description here', 20, 999);", conn);
                cmd.ExecuteNonQuery();

                //Insert a test survey 
                //If we want to the new id of the record inserted we can use
                // SELECT CAST(SCOPE_IDENTITY() as int) as a work-around
                // This will get the newest identity value generated for the record most recently inserted
                //cmd = new SqlCommand("INSERT INTO survey_results (parkCode, emailAddress, state, activityLevel) VALUES ('TNP', 'zzz@zzz.com', 'Ohio', 'Active'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                //parkId = (int)cmd.ExecuteScalar();
            }
        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); // disposes the transaction without committing changes to the DB
        }

        [TestMethod()]
        public void GetAllParksTest()
        {
            // Arrange 
            ParkDAL parkDal = new ParkDAL(connectionString);

            // Act
            List<Park> parks = parkDal.GetAllParks(); //<-- use our dummy country 

            // Assert
            Assert.IsTrue(parks.Count > 0);             
            Assert.AreEqual("AAA", parks[0].ParkCode);     
        }

        [TestMethod()]
        public void GetParkByIdTest()
        {
            // Arrange 
            ParkDAL parkDal = new ParkDAL(connectionString);

            // Act (create blank list of Park ite)
            List<Park> parks = new List<Park>();
            parks.Add(parkDal.GetParkById("AAA"));     

            // Assert
            Assert.AreEqual(1, parks.Count);               // We should only have one park in the list
            Assert.AreEqual("AAA", parks[0].ParkCode);      // We created the mock park above and know the park code to look for
        }
    }
}
