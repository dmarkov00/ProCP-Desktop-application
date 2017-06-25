using System;
using Common.Enumerations;
using Newtonsoft.Json;

namespace Models
{
    public class TruckMaintenance
    {
        private int truckid;
        private int driverid;
        private Truck truck;
        private Driver driver;
        private double cost;
        private DateTime date;
        private string actionPerformed;

        public TruckMaintenance(Truck truck, Driver driver, double cost, DateTime date, string action)
        {
            this.Truck = truck;
            this.Driver = driver;
            this.Cost = cost;
            this.Date = date;
            this.ActionPerformed = action;
        }

       public Truck Truck
        {
            get
            {
                return truck;
            }

            set
            {
                truck = value;
                truckid =Convert.ToInt32(truck.Id);
            }
        }

        [JsonProperty("truck_id")]
        public int TruckID
        {
            get
            {
                return truckid;
            }

            set
            {
                truckid = value;
            }
        }

        public Driver Driver
        {
            get
            {
                return driver;
            }

            set
            {
                driver = value;
                driverid = Convert.ToInt32(driver.Id);
            }
        }

        [JsonProperty("driver_id")]
        public int DriverID
        {
            get
            {
                return driverid;
            }

            set
            {
                driverid = value;
            }
        }

        [JsonProperty("actionCost")]
        public double Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        [JsonProperty("actionDate")]
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        [JsonProperty("actionPerformed")]
        public string ActionPerformed
        {
            get
            {
                return actionPerformed;
            }

            set
            {
                actionPerformed = value;
            }
        }
    }
}
