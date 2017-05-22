using NUnit.Framework;
using System.Threading.Tasks;
using Models;
using Common.Models;
using System.Collections.Generic;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {
        private Dispatcher dispatcher = new Dispatcher();
        private List<string> errorMessages;
        [Test]
        public async Task Login_With_User_Sucessfully()
        {
            User expectedResult = new User("sth", "johngreen@gmail.com", "+359 123355", "6UhcQUtcEuE2HXdUM1crQtV9RQQDI6t5IvWVkWcTTFxbc7rtjXz5Od77cqba");

            // Data we usually send from the login form
            var loginData = new { email = "johngreen@gmail.com", password = "secret" };

            // Returning the retrieved data of the user, which is logged in
            User user = (User)await dispatcher.LoginUser(loginData);

            // TODO: research how to compare to objects
            // in this case returns false but the values of the object properties are equal
            Assert.AreEqual(expectedResult.Email, user.Email);
            Assert.AreEqual(expectedResult.Token, user.Token);
        }
        [Test]
        public async Task Login_With_Incomplete_Data()
        {
            // Data we usually send from the login form
            var emptyloginData = new { };

            errorMessages = new List<string>();
            errorMessages.Add("The email field is required.");
            errorMessages.Add("The password field is required.");

            ApiErrorResult expectedErrorResult = new ApiErrorResult("422", "Unprocessable Entity");
            expectedErrorResult.PopulateErrorMessages(errorMessages);

            ApiErrorResult actualErrorResult = (ApiErrorResult)await dispatcher.LoginUser(emptyloginData);

            Assert.AreEqual(expectedErrorResult.ErrorMessages[0], actualErrorResult.ErrorMessages[0]);
            Assert.AreEqual(expectedErrorResult.ErrorMessages[0], actualErrorResult.ErrorMessages[0]);
            Assert.AreEqual(expectedErrorResult.StatusCode, actualErrorResult.StatusCode);
        }
    }
}