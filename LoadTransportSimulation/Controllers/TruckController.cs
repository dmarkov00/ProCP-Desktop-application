using Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using ApiHttpClient;
using Common;
using System.Net;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Controllers
{
    public class TruckController
    {
        private ObservableCollection<Truck> trucks;
        private ObservableCollection<Truck> availableTrucks;
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
            
            IApiCallResult truck = await ApiHttpClient.Dispatcher.GetInstance().Post("trucks", t);
            t.Id = ((Truck)truck).Id;
            trucks.Add(t);
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
        public ObservableCollection<Truck> GetAvailableTrucks()
        {
            return availableTrucks;  
        }
        
        public void SetAvailableTrucks()
        {
            availableTrucks = new ObservableCollection<Truck>();
            foreach (Truck t in trucks)
            {
                if (!t.IsBusy && t.CurrentDriver != null)
                    availableTrucks.Add(t);
            }
        }

        public Truck GetTruck(string truckId)
        {
            IEnumerable<Truck> obsCollection = (IEnumerable<Truck>)trucks;
            var list = new List<Truck>(obsCollection);
            for(int i=0; i<list.Count; i++)
            {
                if (list[i].Id == truckId)
                {
                    return list[i];
                }
            }
            return null;
        }

        public string MakeTruckMaintenance(string truckId,
            string driverId, string action, string cost, string date)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", "6UhcQUtcEuE2HXdUM1crQtV9RQQDI6t5IvWVkWcTTFxbc7rtjXz5Od77cqba");
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/maintenances", new NameValueCollection()
                {
                    { "truck_id", truckId },
                    { "driver_id", driverId },
                    { "actionPerformed", action },
                    { "actionDate", date },
                    { "actionCost", cost }
                });
            };
            return "maintenance added successfully";
        }

        public Truck GetTruckByLicensePlate(string license)
        {
            IEnumerable<Truck> obsCollection = (IEnumerable<Truck>)trucks;
            var list = new List<Truck>(obsCollection);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].LicencePlate == license)
                {
                    return list[i];
                }
            }
            return null;
        }

        private async void getTruck(string truckId)
        {
            IApiCallResult truck = await ApiHttpClient.Dispatcher.GetInstance().Get<Truck>("trucks", truckId);
        }

        public void AssignDriversToTrucks()
        {
            DriverController driverctrl = DriverController.GetInstance();
            

                for (int t = 0; t < trucks.Count; t++)
                for (int d = 0; d < driverctrl.GetAllDrivers().Count; d++)
                    if (trucks[t].Driver_id == driverctrl.GetAllDrivers()[d].Id)
                    {
                        trucks[t].CurrentDriver = driverctrl.GetAllDrivers()[d];
                    }
                        
        }

        public bool AssignSingleDriverToTruck(Truck t, Driver d)
        {
            if (t.IsBusy)
            {
                return false;
            }

            t.CurrentDriver = d;
            
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", User.GetInstance().Token);
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/companies/"+t.Id+"/assignTruck", new NameValueCollection()
                {
                    { "driver_id", d.Id }
                });
            };
            DriverController.GetInstance().SetUnassignedDrivers();
            SetAvailableTrucks();
            return true;
        }

        public void ChangeTruckLocation(Truck t, String LocationId)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", User.GetInstance().Token);
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/trucks/setLocation/" + t.Id + "/" + LocationId, new NameValueCollection()
                {
                });
            }
            //await Dispatcher.GetInstance().Put("trucks", t.Id, t);
        }

        public void AddMaintenance(Truck truck, Driver driver, string action, DateTime date, double cost)
        {
            TruckMaintenance maintenance = new TruckMaintenance(truck, driver, cost, date.ToString(), action);
            truck.AddMaintenance(maintenance);

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", User.GetInstance().Token);
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/maintenances", new NameValueCollection()
                {
                    { "truck_id", truck.Id},
                    { "driver_id", maintenance.DriverID.ToString()},
                    { "actionPerformed", maintenance.ActionPerformed.ToString() },
                    { "actionDate", maintenance.Date.ToString() },
                    { "actionCost", maintenance.Cost.ToString() }
                });
            };

        }

        public ObservableCollection<TruckMaintenance> GetTruckMaintenanceList(Truck t)
        {
            return t.GetMaintenances();
        }
    }
}
