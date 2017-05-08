using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLoadSimulation
{
    class TruckMaintenance
    {
        private Truck truck;
        private Driver driver;
        private double cost;
        private DateTime date;
        private MaintenanceAction actionPerformed;

        internal Truck Truck
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

        internal Driver Driver
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

        internal MaintenanceAction ActionPerformed
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

        public TruckMaintenance(Truck truck, Driver driver, double cost, DateTime date, MaintenanceAction actionPerformed)
        {
            this.truck = truck;
            this.driver = driver;
            this.cost = cost;
            this.date = date;
            this.actionPerformed = actionPerformed;
        }
    }
}
