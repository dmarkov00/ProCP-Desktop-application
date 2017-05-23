using Common;
using Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class TruckTests
    {
        private Dispatcher dispatcher = new Dispatcher();

        [Test]
        public async Task Create_Truck()
        {
            Truck expectedResult = new Truck("XR-BZ-123", "1", 234, 23, 5000, 200, 14);
            IApiCallResult truck = await dispatcher.Post("trucks", expectedResult);
            // Assert required
        }
    }
}
