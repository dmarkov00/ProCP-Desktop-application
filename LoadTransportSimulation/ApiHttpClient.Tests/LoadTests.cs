using Models;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiHttpClient;
using Common;
using System.Collections.Generic;
using System;
using System.Linq;

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
        public async Task Get_Load_By_ID_Succesfully()
        {
            Load actualLoad = (Load)await dispatcher.Get<Load>("loads","2");
            // Assert here if you have power, was too lazy to construct a load for comparison
            Assert.AreEqual(2, actualLoad.ID);
        }
        [Test]
        public async Task Create_Load_Succesfully()
        {
            Load expectedResult = new Load(1, 2, "Example load", 500, 1200, 1200, 0, new DateTime(1994, 5, 5), 2, 3);
            IApiCallResult truck = await dispatcher.Post("loads", expectedResult);
            Load t = (Load)truck;
            Assert.AreEqual(t.MaxArrivalTime, expectedResult.MaxArrivalTime);
        }


    }
}
