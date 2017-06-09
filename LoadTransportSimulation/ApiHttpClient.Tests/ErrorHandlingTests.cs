using Common;
using Models;
using NUnit.Framework;
using System.Threading.Tasks;
using Common.Models;
namespace ApiHttpClient.Tests
{
    // The purpose of this class is to test if error handling for api crud works at all
    // not if it returns correct messages or etc
    [TestFixture]
    public class ErrorHandlingTests
    {
        private Dispatcher dispatcher = Dispatcher.Create(GlobalConstants.testToken2);

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
        [Test]
        public async Task Updating_Entity_With_Empty_Data()
        {
            // Here we test if the server validation is functioning properly and if is handled properly
            // on the client , by trying to update in this case the example is a Driver,
            // without  required or incorrect fields

            // Correct one: Driver driver = new Driver("Daf", "Diesel", "+312332355", "diese@gmail.com");

            // Here we send an incorrect driver entity, so the errors should be handler properly
            IApiCallResult actualResult = await dispatcher.Put("drivers", "2", new { fName = "John", lName = "Stalin", email = "Incorrect email" });

            Assert.AreEqual("422", ((ApiErrorResult)actualResult).StatusCode);
            Assert.AreEqual("Unprocessable Entity", ((ApiErrorResult)actualResult).ReasonPhrase);
            Assert.AreEqual("The phone nbr field is required.", ((ApiErrorResult)actualResult).ErrorMessages[0]);
            Assert.AreEqual("The email must be a valid email address.", ((ApiErrorResult)actualResult).ErrorMessages[1]);

        }
    }
}