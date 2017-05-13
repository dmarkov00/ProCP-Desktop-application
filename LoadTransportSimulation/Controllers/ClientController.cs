using System.Collections.Generic;
using Models;
namespace Controllers
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
