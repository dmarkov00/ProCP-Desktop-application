using System;
using Common.Enumerations;
namespace Models
{
    public class TruckMaintenance
    {
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
            }
        }

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
