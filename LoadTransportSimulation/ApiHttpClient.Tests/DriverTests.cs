using Common;
using Models;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class DriverTests
    {
        private Dispatcher dispatcher = new Dispatcher();
        [Test]
        public async Task Getting_Driver_By_Id_Successfully()
        {
            //Driver expectedResult = new Driver("John", "Truck",)
            Driver driver = (Driver)await dispatcher.Get<Driver>("drivers", "2");
            // Asser here 
        }

        [Test]
        public async Task Getting_All_Drivers()
        {
            IEnumerable<IApiCallResult> drivers = await dispatcher.GetMany<Driver>("drivers");
            // Assert required
        }

        
        [Test]
        public async Task Getting_Driver_Assigned_To_Truck_Successfully()
        {
            //Driver expectedResult = new Driver("John", "Truck",)
            string token = "6UhcQUtcEuE2HXdUM1crQtV9RQQDI6t5IvWVkWcTTFxbc7rtjXz5Od77cqba";
            String driver = await dispatcher.AssignTruckToDriver(2, token);
            string expected = @"""success""";
            Assert.AreEqual(driver, expected);
            // Asser here 

        }
    }
}
