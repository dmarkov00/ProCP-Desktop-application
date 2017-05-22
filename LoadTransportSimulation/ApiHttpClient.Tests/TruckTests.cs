using NUnit.Framework;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class TruckTests
    {
        [Test]
        public async Task Getting_All_Drivers()
        {
            IEnumerable<IApiCallResult> drivers = await dispatcher.GetMany<Driver>("drivers");
            // Assert required
        }
    }
}
