﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Controllers;

namespace Singleton.Tests
{
    [TestFixture]
    public class Singletons
    {
        [Test]
        public void ClientController()
        {
            List<Client> clients1 = new List<Client>();
            List<Client> clients2 = new List<Client>();

            clients1.Add(new Client("Bob", "Dylan", "1234455", "email", new Address(1, "as", "as", "as", "as")));

            ClientController clientCtrl1 = Controllers.ClientController.Create(clients1);
            ClientController clientCtrl2 = Controllers.ClientController.Create(clients1);
            ClientController clientCtrl3 = Controllers.ClientController.GetInstance();

            Assert.AreEqual(clientCtrl1, clientCtrl2);
            Assert.AreEqual(clientCtrl1, clientCtrl3);
            Assert.AreEqual(clientCtrl2, clientCtrl3);
        }

        [Test]
        public void DriverController()
        {
            List<Driver> drivers1 = new List<Driver>();
            List<Driver> drivers2 = new List<Driver>();

            drivers1.Add(new Driver("Bob", "Dylan", "1234455", "email"));
            drivers2.Add(new Driver("Gigi", "Olaa", "1234455", "email"));
            drivers2.Add(new Driver("Eugen", "Dobre", "1234455", "email"));

            DriverController driverCtrl1 = Controllers.DriverController.Create(drivers1);
            DriverController driverCtrl2 = Controllers.DriverController.Create(drivers2);
            DriverController driverCtrl3 = Controllers.DriverController.GetInstance();

            Assert.AreEqual(driverCtrl1, driverCtrl2);
            Assert.AreEqual(driverCtrl1, driverCtrl3);
            Assert.AreEqual(driverCtrl2, driverCtrl3);

            Assert.AreEqual(1, driverCtrl2.GetAllDrivers().Count);
        }
    }
}
