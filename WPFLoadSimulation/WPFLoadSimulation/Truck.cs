using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLoadSimulation
{
    class Truck
    {
        private string licencePlate;
        private Driver currentDriver;
        private Address location;
        private double avgFuelConsumpt;
        private int payloadCapacityKg;
        private int weightKg;
        private double widthInMeters;
        private double heightInMeters;
        private double lengthInMeters;
        private bool isBusy;
        private bool isInCompany;
        private List<TruckMaintenance> maintenanceList;

        public Truck(string licencePlate,  Address location, double avgFuelConsumpt, int payloadCapacityKg, int weightKg, double widthInMeters, double heightInMeters, double lengthInMeters)
        {
            this.licencePlate = licencePlate;
            this.location = location;
            this.avgFuelConsumpt = avgFuelConsumpt;
            this.payloadCapacityKg = payloadCapacityKg;
            this.weightKg = weightKg;
            this.widthInMeters = widthInMeters;
            this.heightInMeters = heightInMeters;
            this.lengthInMeters = lengthInMeters;
        }
        public string AddMaintenance(MaintenanceAction maintenance)
        {

            return "Maintenance added successfully";
        }
    }
}
