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
    }
}
