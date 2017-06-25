using Common;
using Models;
using NUnit.Framework;
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
            Route route = (Route) await dispatcher.Get<Route>("routes", "2");
           

            Assert.AreEqual(route.EstDistanceKm, 1236);
            Assert.AreEqual(route.EstTimeDrivingMin, 14);
            Assert.AreEqual(route.EstFuelCost, 323);

        }
    }
}
