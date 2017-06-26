using System.Collections.Generic;
using Common.Enumerations;
using Newtonsoft.Json;
using Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;

namespace Models
{
    public class Truck:IApiCallResult, INotifyPropertyChanged
    {
        private string id;
        private string licencePlate;
        private string company_id;
        private string driver_id;
        private int location_id;
        private City locationCity;
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
        private ObservableCollection<TruckMaintenance> maintenanceList;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Truck(string licencePlate, int location_id, int payloadCapacityKg, int weightKg, double widthInMeters, double heightInMeters, double lengthInMeters)
        {
            this.LicencePlate = licencePlate;
            this.Location_id = location_id;
            this.PayloadCapacityKg = payloadCapacityKg;
            this.WeightKg = weightKg;
            this.WidthInMeters = widthInMeters;
            this.HeightInMeters = heightInMeters;
            this.LengthInMeters = lengthInMeters;
            this.LocationCity = (City)Location_id;

            maintenanceList = new ObservableCollection<TruckMaintenance>();
        }

        public City LocationCity
        {
            get
            {
                return locationCity;
            }
            set
            {
                locationCity = value;
                location_id = (int)locationCity;
            }
        }

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
                Driver_id = currentDriver.Id;
                OnPropertyChanged("CurrentDriver");

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
                OnPropertyChanged("IsBusy");
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

        [JsonProperty("driver_id")]
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
        public int Location_id
        {
            get
            {
                return location_id;
            }

            set
            {
                location_id = value;
                locationCity = (City)location_id;
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

     
        public void AddMaintenance(TruckMaintenance maintenance)
        {
            if (!maintenanceList.Contains(maintenance)) {
                maintenanceList.Add(maintenance);
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("api_token", User.GetInstance().Token);
                    byte[] response =
                    client.UploadValues("http://127.0.0.1:8000/api/maintenances", new NameValueCollection()
                    {
                    { "truck_id", Id },
                    { "driver_id", maintenance.DriverID.ToString()},
                    { "actionPerformed", maintenance.ActionPerformed.ToString() },
                    { "actionDate", maintenance.Date.ToString() },
                    { "actionCost", maintenance.Cost.ToString() }
                    });
                };
            }
        }

        public ObservableCollection<TruckMaintenance> GetMaintenances()
        {
            return maintenanceList;
        }

        public override string ToString()
        {
            return this.LicencePlate + " - Location: " + this.LocationCity + " - Capacity Kg: " + this.PayloadCapacityKg;
        }

       
    }
}
