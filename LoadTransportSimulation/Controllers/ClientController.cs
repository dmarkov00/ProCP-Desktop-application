using System.Collections.Generic;
using Models;
using System;

namespace Controllers
{
    public class ClientController
    {
        //Singleton implemented
        private static volatile ClientController instance;
        private static object syncRoot = new Object();

        private ClientController() { }

        public static ClientController getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new ClientController();
                }
            }

            return instance;
        }

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
