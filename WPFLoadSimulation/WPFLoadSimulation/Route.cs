using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLoadSimulation
{
    class Route
    {
        public double TotalEstimatedSalary { get; set; }
        public double TotalActualSalary { get; set; }
        public double FinalRevenue { get; set; }
        public TimeSpan TotalTimeUsed { get; set; }

        public int EstTimeDrivingMinutes { get; set; }
        public decimal EstDistanceKm { get; set; }
        public int EstFuelConsumptionLiters { get; set; }
        public double EstCost { get; set; }

        public int ActTimeDrivingMinutes { get; set; }
        public decimal ActDistanceKm { get; set; }
        public int ActFuelConsumptionLiters { get; set; }
        public double ActCost { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Address StartLocation { get; set; }
        public Driver Driver { get; set; }
        public Truck Truck { get; set; }
        public List<Load> Loads { get; set; }

        public Route(List<Load> loads, Truck truck, Driver driver, Address startloc)
        {
            this.Loads = loads;
            this.Truck = truck;
            this.Driver = driver;
            this.StartLocation = startloc;
        }


    }
}
