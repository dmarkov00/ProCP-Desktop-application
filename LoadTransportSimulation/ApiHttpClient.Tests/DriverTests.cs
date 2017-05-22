using Models;
using NUnit.Framework;
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
    }
}
