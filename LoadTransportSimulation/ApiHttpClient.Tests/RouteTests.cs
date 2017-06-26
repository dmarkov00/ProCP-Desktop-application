using Common;
using Controllers;
using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class RouteTests
    {
        private Dispatcher dispatcher = Dispatcher.Create(GlobalConstants.testToken2);
        [Test]
        public async Task Getting_All_Routes()
        {
            List<IApiCallResult> routes = await dispatcher.GetMany<Route>("routes");

            List<Route> targetList = new List<Route>(routes.Cast<Route>());

            Assert.AreEqual(targetList[0].EstDistanceKm, 1236);
            Assert.AreEqual(targetList[0].EstTimeDrivingMin, 14);
            Assert.AreEqual(targetList[0].EstFuelCost, 323);

        }

        [Test]
        public async Task Getting_Route_By_Id_Successfully()
        {
            Route route = (Route)await dispatcher.Get<Route>("routes", "2");


            Assert.AreEqual(route.EstDistanceKm, 1236);
            Assert.AreEqual(route.EstTimeDrivingMin, 14);
            Assert.AreEqual(route.EstFuelCost, 323);

        }
        [Test]
        public async Task Create_Route_Successfully()
        {
            Company c = (Company)await ApiHttpClient.Dispatcher.GetInstance().Get<Company>("companies", User.GetInstance().CompanyId);
            User.GetInstance().Company = c;
            
            CompanyController companyCtrl = CompanyController.Create(c);
         
            List<Load> loads = new List<Load>();
            Load load1 = new Load(1, 2, "Example load", 500, 1200, DateTime.Now, 2, 3);
            Load load2 = new Load(2, 3, "Example load", 500, 1200, DateTime.Now, 2, 3);
            loads.Add(load1);
            loads.Add(load2);
            Truck truck = new Truck("HZ-12-KFN1", 1, 234, 23, 5000, 200, 14);

            Route route = new Route();


            route.Loads = loads;
            route.Truck = truck;

            route = companyCtrl.RouteCtrl.SetEstimations(route);

            IApiCallResult newroute = await Dispatcher.GetInstance().Post("routes", route);

        }
    }
}
