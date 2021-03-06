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
        private ObservableCollection<Driver> unassignedDrivers;

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

        public string AddDriver(Driver d)
        {
            this.addDriver(d);
            return "Driver has been added successfully!";
        }

        private async void addDriver(Driver t)
        {
            
            IApiCallResult driver = await ApiHttpClient.Dispatcher.GetInstance().Post("drivers", t);
            t.Id = ((Driver)driver).Id;
            drivers.Add(t);
            //return "Truck added successfully";
        }

        public async void RemoveDriver(Driver d)
        {
            if (!d.IsBusy)
            {
                IApiCallResult result = await ApiHttpClient.Dispatcher.GetInstance().Delete("drivers", d.Id.ToString());
                this.drivers.Remove(d);

                foreach (Truck t in TruckController.GetInstance().GetAllTrucks())
                {
                    if (t.CurrentDriver == d)
                    {
                        t.CurrentDriver = null;
                    }
                }
            }
            
        }

        public ObservableCollection<Driver> GetAllDrivers()
        {
            return drivers;
        }

        public ObservableCollection<Driver> GetUnassignedDrivers()
        {
            return unassignedDrivers;
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
            unassignedDrivers = new ObservableCollection<Driver>(GetAllDrivers());
            TruckController truckCtrl = TruckController.GetInstance();

            for(int t=0; t<truckCtrl.GetAllTrucks().Count; t++ )
            {
                if (unassignedDrivers.Contains(truckCtrl.GetAllTrucks()[t].CurrentDriver))
                {
                    unassignedDrivers.Remove(truckCtrl.GetAllTrucks()[t].CurrentDriver);
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
            foreach(Driver d in drivers)
            {
                if (d.Id == id)
                    return d;
            }
            return null;
        }
    }
}
