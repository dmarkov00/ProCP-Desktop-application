using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLoadSimulation
{
    class TruckController
    {
        private List<Truck> trucks;

        public TruckController(List<Truck> trucks)
        {
            this.trucks = trucks;
        }

        
        public string AddTruck(Truck t)
        {
            return "Truck added successfully";
        }
        public string RemoveTruck(Truck t)
        {
            return "Truck added successfully";
        }
        public List<Truck> GetAllTrucks()
        {
            return trucks;
        }
        public List<Truck> GetBusyTrucks()
        {
            return trucks;
        }
        public List<Truck> GetAvailableTrucks()
        {
            return trucks;
        }
        public Truck GetTruck(string licencePlate)
        {
            return trucks[0];
        }
    }
}
