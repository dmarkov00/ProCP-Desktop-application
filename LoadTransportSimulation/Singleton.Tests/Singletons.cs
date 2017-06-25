using NUnit.Framework;
using System;
using System.Collections.Generic;
using Models;
using Controllers;
using ApiHttpClient;
using System.Threading;

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
            
            clients1.Add(new Client("Bob Dylan", "1234455", "email",  "as", "a"));

            clients1.Add(new Client("Bob Dylan", "1234455", "email",  "as", "1"));

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
            /*
            drivers1.Add(new Driver("1","Bob", "Dylan", "1234455", "email"));
            drivers2.Add(new Driver("2","Gigi", "Olaa", "1234455", "email"));
            drivers2.Add(new Driver("3","Eugen", "Dobre", "1234455", "email"));

            DriverController driverCtrl1 = Controllers.DriverController.Create(drivers1);
            DriverController driverCtrl2 = Controllers.DriverController.Create(drivers2);
            DriverController driverCtrl3 = Controllers.DriverController.GetInstance();

            Assert.AreEqual(driverCtrl1, driverCtrl2);
            Assert.AreEqual(driverCtrl1, driverCtrl3);
            Assert.AreEqual(driverCtrl2, driverCtrl3);

            Assert.AreEqual(1, driverCtrl2.GetAllDrivers().Count);*/
        }

        [Test]
        public void LoadController()
        {
            List<Load> loads1 = new List<Load>();
            List<Load> loads2 = new List<Load>();

            //loads1.Add(new Load(new Address(1,"asd","asd","asdfw","asew"), new Address(1, "asd", "asd", "asdfw", "asew"), "1234455", 123,345, new DateTime(), new Client("Bob Dylan", "1234455", "email",  "as")));
            //loads2.Add(new Load(new Address(1, "asd", "asd", "asdfw", "asew"), new Address(1, "asd", "asd", "asdfw", "asew"), "1234455", 123, 345, new DateTime(), new Client("Bob Dylan", "1234455", "email", "as")));
            //loads2.Add(new Load(new Address(1, "asd", "asd", "asdfw", "asew"), new Address(1, "asd", "asd", "asdfw", "asew"), "1234455", 123, 345, new DateTime(), new Client("Bob Dylan", "1234455", "email",  "as")));

            Assert.That(() => Controllers.LoadController.GetInstance(),
                Throws.TypeOf<Exception>());            
        }

        [Test]
        public void TruckController()
        {
            List<Truck> trucks1 = new List<Truck>();
            List<Truck> trucks2 = new List<Truck>();

            trucks1.Add(new Truck("asd", 1, 100, 100, 100, 100, 100));
            trucks2.Add(new Truck("asd", 1, 200, 200, 200, 200, 200));
            trucks1.Add(new Truck("lpm", 1, 10, 1000, 1300, 100, 1030));
            trucks2.Add(new Truck("knoi", 1, 1020, 100, 10340, 100, 1100));

            Assert.AreEqual(2, trucks1.Count);
            Assert.AreEqual(2, trucks2.Count);

            TruckController truckCtrl1 = null;
            TruckController truckCtrl2 = null;

            Thread thread1 = new Thread(() => truckCtrl1=Controllers.TruckController.Create(trucks1));
            Thread thread2 = new Thread(() => truckCtrl2=Controllers.TruckController.Create(trucks2));
            thread1.Start();
            thread1.Join();

            thread2.Start();
            thread2.Join();

            Assert.AreEqual(truckCtrl1,truckCtrl2);
            Assert.AreNotEqual(null, truckCtrl2);
            Assert.AreEqual(2, Controllers.TruckController.GetInstance().GetAllTrucks().Count);
        }

        [Test]
        public void Dispatcher()
        {
            Dispatcher disp1 = ApiHttpClient.Dispatcher.GetInstance();
            Dispatcher disp2 = ApiHttpClient.Dispatcher.GetInstance();

            Thread thread1 = new Thread(() => disp1 = ApiHttpClient.Dispatcher.GetInstance());
            Thread thread2 = new Thread(() => disp2 = ApiHttpClient.Dispatcher.GetInstance());
            thread1.Start();
            thread1.Join();

            thread2.Start();
            thread2.Join();

            Assert.AreEqual(disp1, disp2);
        }
    }
}
