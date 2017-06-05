using Common;
using Models;
using NUnit.Framework;
using System.Threading.Tasks;
using Common.Models;
namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class ErrorHandlingTests
    {
        private Dispatcher dispatcher = Dispatcher.GetInstance();

        [Test]
        public async Task Getting_Unexisting_Resource_By_Id()
        {
            
            // In this case we try to retrieve unexisting Load
            IApiCallResult load = await dispatcher.Get<Load>("loads", "1000000");

            Assert.AreEqual("404", ((ApiErrorResult)load).StatusCode);
            Assert.AreEqual("Not Found", ((ApiErrorResult)load).ReasonPhrase);
            Assert.AreEqual("Record not found", ((ApiErrorResult)load).ErrorMessages[0]);

        }
    }
}
