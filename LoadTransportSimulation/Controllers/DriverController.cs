using Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace Controllers
{
    public class DriverController
    {
        private ObservableCollection<Driver> drivers;

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
            this.drivers = new ObservableCollection<Driver>(drivers);
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

        public ObservableCollection<Driver> GetAllDrivers()
        {
            return drivers;
        }

        public List<Driver> GetBusyDrivers()
        {
            List<Driver> busyDrivers = new List<Driver>();

            foreach (Driver d in drivers)
            {
                if (d.IsBusy)
                {
                    busyDrivers.Add(d);
                }
            }
            return busyDrivers;
        }

        /// <summary>
        /// Returns drivers who are not assigned to any truck yet
        /// </summary>
        /// <returns></returns>
        /// 
        
        public ObservableCollection<Driver> GetUnassignedDrivers()
        {
            ObservableCollection<Driver> unassignedDrivers = GetAllDrivers();
            TruckController truckCtrl = TruckController.GetInstance();
            
            int count = unassignedDrivers.Count;

            for(int d=count-1; d>=0; d--)
            {
                for(int t=0; t<truckCtrl.GetAllTrucks().Count; t++ )
                {
                    if (truckCtrl.GetAllTrucks()[t].CurrentDriver == unassignedDrivers[d])
                    {
                        unassignedDrivers.RemoveAt(d);
                    }
                }
            }

            return unassignedDrivers;
        }


        /// <summary>
        /// Returns drivers who are currently not on road
        /// </summary>
        /// <returns></returns>
        public List<Driver> GetAvailableDrivers()
        {
            
            List<Driver> availableDrivers = new List<Driver>();

            foreach(Driver d in drivers)
            {
                if (!d.IsBusy)
                {
                    availableDrivers.Add(d);
                    
                }
            }
            return availableDrivers;
        }

        public Driver GetDriver(string id)
        {
            

            return drivers[0];
        }
    }
}
