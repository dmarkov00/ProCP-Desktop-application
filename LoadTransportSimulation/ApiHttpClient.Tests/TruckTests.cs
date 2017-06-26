using Common;
using Controllers;
using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class TruckTests
    {
        private Dispatcher dispatcher = Dispatcher.Create(GlobalConstants.testToken2);

        // for each truck created, please change the license plate which is unique
        [Test]
        public async Task Create_Truck()
        {
            Truck expectedResult = new Truck("td-aa1", 1, 234, 23, 5000, 200, 14);
            IApiCallResult truck = await dispatcher.Post("trucks", expectedResult);
            Truck t = (Truck)truck;
            Assert.AreEqual(expectedResult.LicencePlate, t.LicencePlate);
        }

        [Test]
        public async Task Create_Truck_Maintenance()
        {
            string truckId = "1";
            string driverId = "1";
            string action = "sometestaction";
            string cost = "1234";
            string date = DateTime.Now.ToString();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", "6UhcQUtcEuE2HXdUM1crQtV9RQQDI6t5IvWVkWcTTFxbc7rtjXz5Od77cqba");
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/maintenances", new NameValueCollection()
                {
                    { "truck_id", truckId },
                    { "driver_id", driverId },
                    { "actionPerformed", action },
                    { "actionDate", date },
                    { "actionCost", cost }
                });
            };
           
        }

        [Test]
        public async Task Create_Client()
        {
            Client expectedResult = new Client("firstname", "123123", "mymail@mail.mail", "myadddress", "1");
            IApiCallResult client = await dispatcher.Post("clients", expectedResult);
            Client t = (Client)client;
            Assert.AreEqual(expectedResult.Address, t.Address);
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
