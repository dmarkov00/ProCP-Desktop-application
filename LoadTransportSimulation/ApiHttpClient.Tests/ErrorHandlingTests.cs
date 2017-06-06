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
        [Test]
        public async Task Deleting_Unexisting_Resource_By_Id()
        {
            // In this case we try to delete unexisting Truck
            IApiCallResult actualResult = await dispatcher.Delete("trucks", "3");

            Assert.AreEqual("404", ((ApiErrorResult)actualResult).StatusCode);
            Assert.AreEqual("Not Found", ((ApiErrorResult)actualResult).ReasonPhrase);
            Assert.AreEqual("Record not found", ((ApiErrorResult)actualResult).ErrorMessages[0]);
        }
        [Test]
        public async Task Creating_Entity_With_Empty_Data()
        {
            // Here we test if the server validation is functioning properly and if is handled properly
            // on the client , by trying to create in this case the example is a Truck, without the required fields 

            IApiCallResult actualResult = await dispatcher.Post("trucks", new { });

            Assert.AreEqual("422", ((ApiErrorResult)actualResult).StatusCode);
            Assert.AreEqual("Unprocessable Entity", ((ApiErrorResult)actualResult).ReasonPhrase);
            Assert.AreEqual("The license plate field is required.", ((ApiErrorResult)actualResult).ErrorMessages[0]);
            Assert.AreEqual("The pay load capacity field is required.", ((ApiErrorResult)actualResult).ErrorMessages[1]);
            Assert.AreEqual("The weight field is required.", ((ApiErrorResult)actualResult).ErrorMessages[2]);
        }
    }
}