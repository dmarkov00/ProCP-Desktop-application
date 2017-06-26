using Common;
using Models;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class DriverTests
    {
        private Dispatcher dispatcher = Dispatcher.Create(GlobalConstants.testToken2);
        [Test]
        public async Task Update_Driver()
        {

            // The expected result is also used as a test model
            Driver expectedResult = new Driver("Daf", "Diesel", "39933", "diese@gmail.com");
            expectedResult.Id = "2";
            // After update the api should return the updated entity
            Driver actualResult = (Driver)await dispatcher.Put("drivers", "2", expectedResult);

            Assert.AreEqual(expectedResult.FirstName, actualResult.FirstName);
        }
        [Test]
        public async Task Getting_Driver_By_Id_Successfully()
        {
            //Ask the API to provide driver with id 2
            Driver driver = (Driver)await dispatcher.Get<Driver>("drivers", "2");
            //We make sure the driver id is 2
            Assert.AreEqual(driver.Id, "2");
            Assert.AreEqual(driver.FirstName, "Daf");
        }
        [Test]
        public async Task Getting_All_Drivers()
        {
            //We ask the API to provide all the trucks
            List<IApiCallResult> drivers = await dispatcher.GetMany<Driver>("drivers");
            List<Driver> targetList = new List<Driver>(drivers.Cast<Driver>());
            //We make sure it has provided a non-empty list of trucks
            Assert.IsTrue(targetList.Count > 0);
        }
        [Test]
        public async Task Delete_Driver()
        {
            // On sucessfull delete the method returns null
           // Commented out, not to break tests
            //var actualResult = await dispatcher.Delete("drivers", "2");

            //Assert.AreEqual(actualResult, null);
        }
        [Test]
        public async Task Getting_Driver_Assigned_To_Truck_Successfully()
        {
            //We try to assign a truck to a driver
            string token = "6UhcQUtcEuE2HXdUM1crQtV9RQQDI6t5IvWVkWcTTFxbc7rtjXz5Od77cqba";
            String driver = await dispatcher.AssignTruckToDriver(2, token);
            string expected = @"""success""";
            //We assert the assignment is done correctly
            Assert.AreEqual(driver, expected);

        }
    }
}
