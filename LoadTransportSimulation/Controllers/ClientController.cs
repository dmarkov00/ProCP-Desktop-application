using System.Collections.Generic;
using Models;
using System;

namespace Controllers
{
    public class ClientController
    {
        private List<Client> clients;

        //Singleton implemented
        private static volatile ClientController instance;
        private static object syncRoot = new Object();

        public static ClientController getInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }


        public static ClientController Create(List<Client> clients)
        {
            lock (syncRoot)
            { 
                if (instance == null)
                    instance = new ClientController(clients);
            }
            return instance;
        }

        public ClientController(List<Client> clients)
        {
            this.clients = clients;
        }

        public List<Client> GetClients()
        {
            return clients;
        }

        public void AddClient(Client c)
        {
            if(!clients.Contains(c))
            clients.Add(c);
        }
    }
}
