using Common;
using Models;
using NUnit.Framework;
using System.Collections.Generic;
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
        }
    }
}
