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
        public async Task Test_See_Trucks()
        {
            var client = new ApiHttpClient.Dispatcher();
            //We ask the API to provide a list with all the trucks
            IEnumerable<IApiCallResult> trucks = await client.GetMany<Truck>("trucks");
            List<Truck> targetList = new List<Truck>(trucks.Cast<Truck>());
            //We assert it has provided a non-empty list of trucks
            Assert.IsTrue(trucks.Count()>0);
        }

        [Test]
        public async Task Test_See_Truck_By_ID()
        {
            var client = new ApiHttpClient.Dispatcher();
            //We ask the API to provide a truck by ID
            IApiCallResult truck = await client.Get<Truck>("trucks", "1");
            Truck kamion = (Truck)truck;
            //We assert that the truck provided is the right one
            Assert.AreEqual(kamion.Id, "1");        
        }
    }
}
