using Models;
using System.Collections.Generic;

namespace Controllers
{
    class DriverController
    {
        private List<Driver> drivers;

        public DriverController(List<Driver> drivers)
        {
            this.drivers = drivers;
        }
        public string AddDriver(Driver t)
        {
            return "Truck added successfully";
        }
        public string RemoveDriver(Driver t)
        {
            return "Truck added successfully";
        }
        public List<Driver> GetAllDrivers()
        {
            return drivers;
        }
        public List<Driver> GetBusyDrivers()
        {
            return drivers;
        }
        public List<Driver> GetAvailableDrivers()
        {
            return drivers;
        }
        public Driver GetTruck(string id)
        {
            return drivers[0];
        }
    }
}
