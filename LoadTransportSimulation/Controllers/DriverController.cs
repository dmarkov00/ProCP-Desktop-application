﻿using Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Common;

namespace Controllers
{
    public class DriverController
    {
        private ObservableCollection<Driver> drivers;
        public ObservableCollection<Driver> unAssignedDrivers;

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

        public async void RemoveDriver(Driver d)
        {
            this.drivers.Remove(d);
            IApiCallResult result = await ApiHttpClient.Dispatcher.GetInstance().Delete("drivers", d.Id.ToString());
        }

        public ObservableCollection<Driver> GetAllDrivers()
        {
            return drivers;
        }

        public ObservableCollection<Driver> GetUnassignedDrivers()
        {
            return unAssignedDrivers;
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
        
        public void SetUnassignedDrivers()
        {
            unAssignedDrivers = new ObservableCollection<Driver>(GetAllDrivers());
            TruckController truckCtrl = TruckController.GetInstance();

            for(int t=0; t<truckCtrl.GetAllTrucks().Count; t++ )
            {
                if (unAssignedDrivers.Contains(truckCtrl.GetAllTrucks()[t].CurrentDriver))
                {
                    unAssignedDrivers.Remove(truckCtrl.GetAllTrucks()[t].CurrentDriver);
                }
            }
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
