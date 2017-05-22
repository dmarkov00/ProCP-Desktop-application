using Common;
using Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class TruckTests
    {
        [Test]
        public async Task Getting_All_Drivers()
        {
            //IEnumerable<IApiCallResult> drivers = await dispatcher.GetMany<Driver>("drivers");
            // Assert required
        }

        [Test]
        public async Task TestSeeLoads()
        {
            var client = new ApiHttpClient.Dispatcher();
            IEnumerable<IApiCallResult> trucks = await client.GetMany<Truck>("trucks");
            List<Truck> targetList = new List<Truck>(trucks.Cast<Truck>());
            Assert.AreEqual(trucks.Count(),11);
        }
    }
}
