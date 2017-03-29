using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSimulation
{
    class Truck
    {
        public string LicencePlate { get; set; }
        public string Driver { get; set; }
        public Boolean Available { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public decimal Weight { get; set; }
        public decimal FuelConsumption { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }

        public Truck(string licenceplate, string driver,Boolean available,string location,int capacity,decimal weight,
        decimal fuelconsump,decimal width,decimal height, decimal length)
        {
            this.LicencePlate = licenceplate;
            this.Driver = driver;
            this.Available = available;
            this.Location = location;
            this.Capacity = capacity;
            this.Weight = weight;
            this.FuelConsumption = fuelconsump;
            this.Width = width;
            this.Height = height;
            this.Length = length;
        }

    }
}
