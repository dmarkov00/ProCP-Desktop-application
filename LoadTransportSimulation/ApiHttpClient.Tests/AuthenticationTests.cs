using NUnit.Framework;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {
        private Dispatcher dispatcher = new Dispatcher();

        [Test]
        public async Task Login_With_User_Sucessfully()
        {
            var loginData = new { email = "gege@abv.bg", password = "123456" };

            var result = await dispatcher.LoginUser(loginData);
        }
    }
}
