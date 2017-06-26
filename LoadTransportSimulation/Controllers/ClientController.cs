using System.Collections.Generic;
using Models;
using System;
using System.Collections.ObjectModel;
using Common;

namespace Controllers
{
    public class ClientController
    {
        private ObservableCollection<Client> clients;
        

        /*Singleton implemented
         * -when you want to use the controller the first time, use ClientController.Create(list);
         * -afterwards, anywhere in the program, to get the instance, use ClientController.GetInstance();
         */
        private static volatile ClientController instance;
        private static object syncRoot = new Object();

        public static ClientController GetInstance()
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

        private ClientController(List<Client> clients)
        {
            this.clients = new ObservableCollection<Client>(clients);
        }

        public ObservableCollection<Client> GetAllClients()
        {
            return clients;
        }

        public string AddClient(Client c)
        {
            this.addClient(c);
            clients.Add(c);
            return "Client added successfully";
        }

        private async void addClient(Client c)
        {
            IApiCallResult result = await ApiHttpClient.Dispatcher.GetInstance().Post("clients", c);
        }

        public async void RemoveClient(Client c)
        {
            if (clients.Contains(c))
            {
                clients.Remove(c);
                IApiCallResult result = await ApiHttpClient.Dispatcher.GetInstance().Delete("clients", c.Id);
            }
                
        }
    }
}
