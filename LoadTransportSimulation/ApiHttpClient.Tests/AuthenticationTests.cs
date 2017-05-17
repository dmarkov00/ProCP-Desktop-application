using NUnit.Framework;
using System.Threading.Tasks;
using Models;
using Common.Models;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {
        private Dispatcher dispatcher = new Dispatcher();

        [Test]
        public async Task Login_With_User_Sucessfully()
        {
            User expectedResult = new User("gege", "gege@abv.bg", "+359 123355", "ADzbFafdPnvzbAj7jgYgA1RR5zUsiEiHUGJVl6fXnOx7DhoovLStOnkqzyrp");

            // Data we usually send from the login form
            var loginData = new { email = "gege@abv.bg", password = "123456" };

            // Returning the retrieved data of the user, which is logged in
            User user = (User)await dispatcher.LoginUser(loginData);

            // TODO: research how to compare to objects
            // in this case returns false but the values of the object properties are equal
            Assert.AreEqual(expectedResult, user);
        }
        [Test]
        public async Task Login_With_Incomplete_Data()
        {
            // Data we usually send from the login form
            var emptyloginData = new { };

            ApiErrorResult errorResult = new ApiErrorResult("422","Unrea");

            ApiErrorResult user = (ApiErrorResult)await dispatcher.LoginUser(emptyloginData);

            //errorResult.ErrorMessages.Add();
        }
    }
}