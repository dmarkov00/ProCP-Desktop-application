using Models;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiHttpClient;
using Common;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Collections.Specialized;

namespace ApiHttpClient.Tests
{
    [TestFixture]
    public class LoadTests
    {
        private Dispatcher dispatcher = Dispatcher.Create(GlobalConstants.testToken2);


        [Test]
        public async Task Getting_All_Loads_Succesfully()
        {
            
            List<IApiCallResult> actualLoads = await dispatcher.GetMany<Load>("loads");
            List<Load> actualResult = actualLoads.Cast<Load>().ToList();

            Assert.AreEqual(actualResult[1].ID, 2);
        }

        [Test]
        public async Task Finalizing_Load()
        {
            string loadId = "1";
            string actualTime = "11-12-1995";
            string finalSal = "1298";
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", "6UhcQUtcEuE2HXdUM1crQtV9RQQDI6t5IvWVkWcTTFxbc7rtjXz5Od77cqba");
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/loads/finalize/" + loadId, new NameValueCollection()
                {
                    { "arrivaldate", actualTime },
                    { "finalsalary", finalSal }
                });
            };

            List<IApiCallResult> actualLoads = await dispatcher.GetMany<Load>("loads");
            List<Load> actualResult = actualLoads.Cast<Load>().ToList();
            Load l = actualResult[0];
            Assert.AreEqual((int)actualResult[0].LoadStateID, 3);
        }

        [Test]
        public async Task Get_Load_By_ID_Succesfully()
        {
            Load actualLoad = (Load)await dispatcher.Get<Load>("loads","2");
            // Assert here if you have power, was too lazy to construct a load for comparison
            Assert.AreEqual(2, actualLoad.ID);
        }
        [Test]
        public async Task Create_Load_Succesfully()
        {
            Load expectedResult = new Load(1, 2, "Example load", 500, 1200, DateTime.Now, 0, 2);
            IApiCallResult truck = await dispatcher.Post("loads", expectedResult);
            Load t = (Load)truck;
            Assert.AreEqual(t.MaxArrivalTime, expectedResult.MaxArrivalTime);
        }

        [Test]
        public async Task Create_Load()
        {
            Load expectedResult = new Load(1, 1, "content", 234, 23, DateTime.Now,
                1, 1);
            IApiCallResult truck = await dispatcher.Post("loads", expectedResult);
            Load t = (Load)truck;
            Assert.AreEqual(expectedResult.DelayFeePercHour, t.DelayFeePercHour);
        }
    }
}
