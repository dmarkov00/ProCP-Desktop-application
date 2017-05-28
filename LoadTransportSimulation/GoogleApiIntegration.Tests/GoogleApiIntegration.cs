﻿using NUnit.Framework;
using System;
using GoogleApiIntegration;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestCalculateDistanceWithGoogleAPI()
        {
            GoogleAPI api = new GoogleAPI();
            string kmFromBerlinToMoscow = api.calculatedistance(0, 3).ToString();
            string expRes = "1,818";
            Assert.AreEqual(kmFromBerlinToMoscow, expRes);
            double res = Convert.ToDouble(kmFromBerlinToMoscow);
            Assert.IsTrue(res.Equals(1.818));
        }

        [Test]
        public void TestCalculateFuelConsumptionWithGoogleAPI()
        {
           GoogleAPI api = new GoogleAPI();
            int kmFromBerlinToMoscow = api.calculatedistance(1, 24);
            double fuel = Math.Round(api
                .calculateFuelConsumption(kmFromBerlinToMoscow, 45.5), 2);

            Assert.IsTrue(fuel.Equals(826.74));
        }
    }
}
