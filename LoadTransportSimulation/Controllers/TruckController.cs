using Models;
using System.Collections.Generic;
using System;

namespace Controllers
{
    public class TruckController
    {
        private List<Truck> trucks;

        /*Singleton implemented
        * -when you want to use the controller the first time, use DriverController.Create(list);
        * -afterwards, anywhere in the program, to get the instance, use DriverController.GetInstance();
        */

        private static volatile TruckController instance;
        private static object syncRoot = new Object();

        public static TruckController GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }

        public static TruckController Create(List<Truck> loads)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new TruckController(loads);
            }
            return instance;
        }

        private TruckController(List<Truck> trucks)
        {
            this.trucks = trucks;
        }


        public string AddTruck(Truck t)
        {
            return "Truck added successfully";
        }
        public string RemoveTruck(Truck t)
        {
            trucks.Remove(t);
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
