using NUnit.Framework;
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
            //2 dummy client lists to be passed as parameters
            List<Client> clients1 = new List<Client>();
            List<Client> clients2 = new List<Client>();

            //one dummy client added to the first list
            clients1.Add(new Client("Bob", "Dylan", "1234455", "email", new Address(1, "as", "as", "as", "as")));

            //trying to create 2 controllers and getting an instance
            ClientController clientCtrl1 = Controllers.ClientController.Create(clients1);
            ClientController clientCtrl2 = Controllers.ClientController.Create(clients2);
            ClientController clientCtrl3 = Controllers.ClientController.GetInstance();

            //if the singleton is correct, all these attempts should be around the same object
            Assert.AreEqual(clientCtrl1, clientCtrl2);
            Assert.AreEqual(clientCtrl1, clientCtrl3);
            Assert.AreEqual(clientCtrl2, clientCtrl3);

            //as all the clientCtrlX are the same, adding a client to one of them should result in adding in the others
            clientCtrl2.AddClient(new Client("Bobii", "Dylan", "1234455", "email", new Address(1, "as", "as", "as", "as")));
            Assert.AreEqual(2, clientCtrl1.GetClients().Count);
            Assert.AreEqual(2, clientCtrl3.GetClients().Count);
        }

        [Test]
        public void DriverController()
        {
            //TODO
        }

        [Test]
        public void LoadController()
        {
            //TODO
        }

        [Test]
        public void RouteController()
        {
            //TODO
        }

        [Test]
        public void TruckController()
        {
            //TODO
        }
    }
}
