using System.Collections.Generic;
using Models;
using Common.Enumerations;
namespace Controllers
{
    class LoadController
    {
        private List<Load> loads;

        public LoadController(List<Load> loads)
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
                if (l.LoadId == loadid)
                    return l;
            }
            return null;
        }

        public void AddNewLoad(Load l)
        {
            this.loads.Add(l);
        }



    }

}
