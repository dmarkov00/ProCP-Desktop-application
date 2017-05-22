using Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class DriverTests
    {
        private Dispatcher dispatcher = new Dispatcher();
        [Test]
        public async Task Getting_Driver_Successfully()
        {
            //Driver expectedResult = new Driver("John", "Truck",)
            Driver driver = (Driver)await dispatcher.Get<Driver>("drivers", "2");
            // Asser here 
        }
        
        [Test]
        public async Task Getting_Driver_Assigned_Successfully()
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
