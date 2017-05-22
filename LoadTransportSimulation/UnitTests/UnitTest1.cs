using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientGUI;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCalculateDistanceWithGoogleAPI()
        {
            ClientGUI.GoogleAPI api = new GoogleAPI();
            string kmFromBerlinToMoscow = api.calculatedistance(0, 3);
            string expRes = "1,818";
            Assert.AreEqual(kmFromBerlinToMoscow, expRes);
            double res = Convert.ToDouble(kmFromBerlinToMoscow);
            Assert.IsTrue(res.Equals(1.818));
        }
        [TestMethod]
        public void TestMethod1()
        {
            ClientGUI.GoogleAPI api = new GoogleAPI();
            string kmFromBerlinToMoscow = api.calculatedistance(0, 3);
            string expRes = "1,818";
            Assert.AreEqual(kmFromBerlinToMoscow, expRes);
            double res = Convert.ToDouble(kmFromBerlinToMoscow);
            Assert.IsTrue(res.Equals(1.818));
        }
    }
}
