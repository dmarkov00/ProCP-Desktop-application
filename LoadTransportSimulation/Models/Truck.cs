using System.Collections.Generic;
using Common.Enumerations;
using Newtonsoft.Json;
using Common;

namespace Models
{
    public class Truck:IApiCallResult
    {
        public Truck(string licencePlate, string location_id, int payloadCapacityKg, int weightKg, double widthInMeters, double heightInMeters, double lengthInMeters)
        {
            this.LicencePlate = licencePlate;
            this.Location_id = location_id;
            this.PayloadCapacityKg = payloadCapacityKg;
            this.WeightKg = weightKg;
            this.WidthInMeters = widthInMeters;
            this.HeightInMeters = heightInMeters;
            this.LengthInMeters = lengthInMeters;
        }
        private string id;
        private string licencePlate;
        private string company_id;
        private string driver_id;
        private string location_id;
        private bool broken;
        private Driver currentDriver;
        private double avgFuelConsumpt;
        private int payloadCapacityKg;
        private int weightKg;
        private double widthInMeters;
        private double heightInMeters;
        private double lengthInMeters;
        private bool isBusy;
        private bool isInCompany;
        private List<TruckMaintenance> maintenanceList;

        [JsonProperty("licensePlate")]
        public string LicencePlate
        {
            get
            {
                return licencePlate;
            }

            set
            {
                licencePlate = value;
            }
        }

        public Driver CurrentDriver
        {
            get
            {
                return currentDriver;
            }

            set
            {
                currentDriver = value;
            }
        }

        
        [JsonProperty("avgFuelComsumption")]
        public double AvgFuelConsumpt
        {
            get
            {
                return avgFuelConsumpt;
            }

            set
            {
                avgFuelConsumpt = value;
            }
        }
        [JsonProperty("payLoadCapacity")]
        public int PayloadCapacityKg
        {
            get
            {
                return payloadCapacityKg;
            }

            set
            {
                payloadCapacityKg = value;
            }
        }
        [JsonProperty("weight")]
        public int WeightKg
        {
            get
            {
                return weightKg;
            }

            set
            {
                weightKg = value;
            }
        }
        [JsonProperty("width")]
        public double WidthInMeters
        {
            get
            {
                return widthInMeters;
            }

            set
            {
                widthInMeters = value;
            }
        }
        [JsonProperty("height")]
        public double HeightInMeters
        {
            get
            {
                return heightInMeters;
            }

            set
            {
                heightInMeters = value;
            }
        }
        [JsonProperty("length")]
        public double LengthInMeters
        {
            get
            {
                return lengthInMeters;
            }

            set
            {
                lengthInMeters = value;
            }
        }
        [JsonProperty("taken")]
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }

            set
            {
                isBusy = value;
            }
        }

        public bool IsInCompany
        {
            get
            {
                return isInCompany;
            }

            set
            {
                isInCompany = value;
            }
        }
        [JsonProperty("id")]
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        [JsonProperty("company_id")]
        public string Company_id
        {
            get
            {
                return company_id;
            }

            set
            {
                company_id = value;
            }
        }

        public string Driver_id
        {
            get
            {
                return driver_id;
            }

            set
            {
                driver_id = value;
            }
        }
        [JsonProperty("location_id")]
        public string Location_id
        {
            get
            {
                return location_id;
            }

            set
            {
                location_id = value;
            }
        }
        [JsonProperty("broken")]
        public bool Broken
        {
            get
            {
                return broken;
            }

            set
            {
                broken = value;
            }
        }

     
        public string AddMaintenance(MaintenanceAction maintenance)
        {

            return "Maintenance added successfully";
        }
    }
}
