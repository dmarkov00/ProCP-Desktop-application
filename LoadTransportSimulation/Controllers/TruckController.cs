using Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using ApiHttpClient;
using Common;

namespace Controllers
{
    public class TruckController
    {
        private ObservableCollection<Truck> trucks;
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
            this.trucks = new ObservableCollection<Truck>( trucks);
        }


        public string AddTruck(Truck t)
        {
            this.addTruckThroughAPI(t);
            return "Truck added successfully";
        }

        private async void addTruckThroughAPI(Truck t)
        {
            //Truck t = new Truck("td-aa-bb", 1, 234, 23, 5000, 200, 14);
            IApiCallResult truck = await ApiHttpClient.Dispatcher.GetInstance().Post("trucks", t);
            //return "Truck added successfully";
        }

        public async void RemoveTruck(Truck t)
        {
            IApiCallResult result = await ApiHttpClient.Dispatcher.GetInstance().Delete("trucks", t.Id);
            trucks.Remove(t);
        }
        public ObservableCollection<Truck> GetAllTrucks()
        {
            return trucks;
        }
        public List<Truck> GetBusyTrucks()
        {
            return new List<Truck>(trucks);
        }
        public List<Truck> GetAvailableTrucks()
        {
            return new List<Truck>(trucks);
        }
        public Truck GetTruck(string licencePlate)
        {
            return trucks[0];
        }

        public void AssignDriversToTrucks()
        {
            DriverController driverctrl = DriverController.GetInstance();
            
            for (int t = 0; t < trucks.Count; t++)
                for (int d = 0; d < driverctrl.GetAllDrivers().Count; d++)
                    if (trucks[t].Driver_id == driverctrl.GetAllDrivers()[d].Id)
                        trucks[t].CurrentDriver = driverctrl.GetAllDrivers()[d];
        }

        public void AssignSingleDriverToTruck(Truck t, Driver d)
        {
            t.CurrentDriver = d;
            DriverController.GetInstance().SetUnassignedDrivers();
            //await Dispatcher.GetInstance().Put("trucks", t.Id, t);
        }

        public void AddMaintenance(Truck truck, Driver driver, string action, DateTime date, double cost)
        {
            truck.AddMaintenance(new TruckMaintenance(truck, driver, cost, date, action));
        }

        public ObservableCollection<TruckMaintenance> GetTruckMaintenanceList(Truck t)
        {
            return t.GetMaintenances();
        }
    }
}
