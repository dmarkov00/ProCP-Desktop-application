using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLoadSimulation
{
    class ClientController
    {
        private List<Client> clients;

        public ClientController(List<Client> clients)
        {
            this.clients = clients;
        }

        public List<Client> GetClients()
        {
            return clients;
        }
    }
}
