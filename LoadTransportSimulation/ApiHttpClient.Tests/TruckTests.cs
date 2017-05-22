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
        public async Task Test_See_Trucks()
        {
            var client = new ApiHttpClient.Dispatcher();
            IEnumerable<IApiCallResult> trucks = await client.GetMany<Truck>("trucks");
            List<Truck> targetList = new List<Truck>(trucks.Cast<Truck>());
            Assert.AreEqual(trucks.Count(),11);
        }

        [Test]
        public async Task Test_See_Truck_By_ID()
        {
            var client = new ApiHttpClient.Dispatcher();
            IApiCallResult truck = await client.Get<Truck>("trucks", "1");
            Truck kamion = (Truck)truck;
            Assert.AreEqual(kamion.Id, "1");
        }
    }
}
