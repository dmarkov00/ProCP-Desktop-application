using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controllers;
using Models;

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

            clients1.Add(new Client("Bob", "Dylan", "1234455", "bob.dylan@gmail.com", new Address(1, "SUA", "SUA", "SUA", "5616")));
            clients1.Add(new Client("John", "Becali", "1234455", "bob.dylan@gmail.com", new Address(1, "SUA", "SUA", "SUA", "5616")));

            ClientController ClientCtrl1 = new ClientController(clients1);
            ClientController ClientCtrl2 = new ClientController(clients2);

            Assert.AreEqual(ClientCtrl2.GetClients()[0].FirstName, clients1[0].FirstName);
            
        }
    }
}
