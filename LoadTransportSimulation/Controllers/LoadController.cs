using System.Collections.Generic;
using Models;
using Common.Enumerations;
using System;

namespace Controllers
{
    public class LoadController
    {
        private List<Load> loads;

        /*Singleton implemented
        * -when you want to use the controller the first time, use DriverController.Create(list);
        * -afterwards, anywhere in the program, to get the instance, use DriverController.GetInstance();
        */

        private static volatile LoadController instance;
        private static object syncRoot = new Object();

        public static LoadController GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }

        public static LoadController Create(List<Load> loads)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new LoadController(loads);
            }
            return instance;
        }

        private LoadController(List<Load> loads)
        {
            this.loads = loads;
        }

        public List<Load> GetAllLoads()
        {
            return loads;
        }

        public List<Load> GetAvailableLoads()
        {
            List<Load> availableloads = new List<Load>();
            
            foreach (Load l in loads)
            {
                if (l.LoadState == LoadState.AVAILABLE)
                    availableloads.Add(l);
            }

            return availableloads;
        }

        public List<Load> GetDeliveredLoads()
        {
            List<Load> deliveredloads = new List<Load>();

            foreach (Load l in loads)
            {
                if (l.LoadState == LoadState.DELIVERED)
                    deliveredloads.Add(l);
            }

            return deliveredloads;
        }

        public List<Load> GetBusyLoads()
        {
            List<Load> busyloads = new List<Load>();

            foreach (Load l in loads)
            {
                if (l.LoadState == LoadState.ONTRANSPORT)
                    busyloads.Add(l);
            }

            return busyloads;
        }

        public Load GetLoad(int loadid)
        {
            foreach (Load l in loads)
            {
                if (l.ID == loadid)
                    return l;
            }
            return null;
        }

        public void AddNewLoad(Load l)
        {
            this.loads.Add(l);
        }

        public void SetClientsForLoads()
        {
            ClientController clientctrl = ClientController.GetInstance();
            List<Client> clients = new List<Client>(clientctrl.GetAllClients());

            for(int l=0; l<loads.Count; l++)
            {
                for (int c=0; c<clients.Count; c++)
                {
                    if(loads[l].ClientID == Convert.ToInt32(clients[c].Id))
                    {
                        loads[l].Client = clients[c];
                    }
                }

            }

        }

    }

}
