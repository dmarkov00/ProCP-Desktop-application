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
        private Dispatcher dispatcher = Dispatcher.Create(GlobalConstants.testToken2);

        [Test]
        public async Task Create_Truck()
        {
            Truck expectedResult = new Truck("td-aa", 1, 234, 23, 5000, 200, 14);
            IApiCallResult truck = await dispatcher.Post("trucks", expectedResult);
            Truck t = (Truck)truck;
            Assert.AreEqual(expectedResult.LicencePlate, t.LicencePlate);
        }

        [Test]
        public async Task Create_Driver()
        {
            Driver expectedResult = new Driver("firstname", "lastname", "123123", "mymail@mail.mail", 3);
            IApiCallResult driver = await dispatcher.Post("drivers", expectedResult);
            Driver t = (Driver)driver;
            Assert.AreEqual(expectedResult.CompanyId, t.CompanyId);
        }

        [Test]
        public async Task Test_See_Trucks()
        {      
            //We ask the API to provide a list with all the trucks
            List<IApiCallResult> trucks = await dispatcher.GetMany<Truck>("trucks");
            List<Truck> targetList = new List<Truck>(trucks.Cast<Truck>());
            //We assert it has provided a non-empty list of trucks
            Assert.IsTrue(trucks.Count()>0);
        }

        [Test]
        public async Task Test_See_Truck_By_ID()
        {
            //We ask the API to provide a truck by ID
            IApiCallResult truck = await dispatcher.Get<Truck>("trucks", "1");
            Truck kamion = (Truck)truck;
            //We assert that the truck provided is the right one
            Assert.AreEqual(kamion.Id, "1");        
        }
    }
}
