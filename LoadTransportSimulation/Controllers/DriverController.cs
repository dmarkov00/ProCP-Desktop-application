using Models;
using System.Collections.Generic;
using System;

namespace Controllers
{
    public class DriverController
    {
        private List<Driver> drivers;

        /*Singleton implemented
         * -when you want to use the controller the first time, use DriverController.Create(list);
         * -afterwards, anywhere in the program, to get the instance, use DriverController.GetInstance();
         */
        private static volatile DriverController instance;
        private static object syncRoot = new Object();
        public static DriverController GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }

        public static DriverController Create(List<Driver> drivers)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new DriverController(drivers);
            }
            return instance;
        }

        private DriverController(List<Driver> drivers)
        {
            this.drivers = drivers;
        }

        public string AddDriver(Driver t)
        {
            return "Truck added successfully";
        }

        public string RemoveDriver(Driver t)
        {
            this.drivers.Remove(t);
            return "Truck removed";
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
        public Driver GetDriver(string id)
        {
            return drivers[0];
        }
    }
}
