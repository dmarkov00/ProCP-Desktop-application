using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleApiIntegration;
using Common.Enumerations;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Common;
using System.ComponentModel;

namespace Models
{
    public class Route : IApiCallResult
    {
        //id
        private string id;
        [JsonProperty("id")]
        public string Id { get { return id;  } set { id = value; } }

        //driver of the route
        private string driverId;
        [JsonProperty("driver_id")]
        public string DriverId { get { return driverId; } set { driverId = value; } }

        private Driver driver;
        public Driver Driver { get { return driver; } set { driver = value; if (driver != null) DriverId = driver.Id; } }


        //truck of route
        private string truckId;
        [JsonProperty("truck_id")]
        public string TruckId { get { return truckId; } set { truckId = value; } }

        private Truck truck;
        public Truck Truck { get { return truck; } set { truck = value; TruckId = truck.Id; Driver = truck.CurrentDriver; } }


        //finances
        private double totalEstimatedSalary;
        [JsonProperty("sum_salaries")]
        public double TotalEstimatedSalary { get { return totalEstimatedSalary; } set { totalEstimatedSalary = value;  } }

        private double totalActualSalary;
        [JsonProperty("sum_actual_salaries")]
        public double TotalActualSalary { get { return totalActualSalary; } set { totalActualSalary = value; OnPropertyChanged("TotalActualSalary"); } }

        private double finalRevenue;
        [JsonProperty("revenue")]
        public double FinalRevenue { get { return finalRevenue; } set { finalRevenue = value; OnPropertyChanged("FinalRevenue"); } }


        //estimations
        private double estTimeDrivingMin;
        [JsonProperty("est_time_driving")]
        public double EstTimeDrivingMin { get { return estTimeDrivingMin; } set { estTimeDrivingMin = value; estTimeDrivingTimeSpan = TimeSpan.FromMinutes(EstTimeDrivingMin); } }

        private TimeSpan estTimeDrivingTimeSpan;
        public TimeSpan EstTimeDrivingTimeSpan
        {
            get { return estTimeDrivingTimeSpan; }
            set { estTimeDrivingTimeSpan = value; if (estTimeDrivingTimeSpan != null) estTimeDrivingMin = estTimeDrivingTimeSpan.TotalMinutes; }
        }

        private int estDistanceKm;
        [JsonProperty("est_distance")]
        public int EstDistanceKm { get { return estDistanceKm; } set { estDistanceKm = value; } }

        private int estFuelConsumption;
        [JsonProperty("est_fuelConsumption")]
        public int EstFuelConsumptionLiters { get { return estFuelConsumption; } set { estFuelConsumption = value; } }

        private double estFuelCost;
        [JsonProperty("est_cost")]
        public double EstFuelCost { get { return estFuelCost; } set { estFuelCost = value; } }

        //actual results after delivery
        private double actTimeDrivingMin;
        [JsonProperty("act_time_used")]
        public double ActTimeDrivingMin { get { return actTimeDrivingMin; } set { actTimeDrivingMin = value; if (actTimeDrivingMin != 0) actTimeDrivingTimeSpan = TimeSpan.FromMinutes(actTimeDrivingMin); OnPropertyChanged("ActTimeDrivingMin"); } }

        private TimeSpan actTimeDrivingTimeSpan;
        public TimeSpan ActTimeDrivingTimeSpan
        {
            get { return actTimeDrivingTimeSpan; }
            set
            {
                actTimeDrivingTimeSpan = value;
                if (actTimeDrivingTimeSpan != null) actTimeDrivingMin = actTimeDrivingTimeSpan.TotalMinutes;
                OnPropertyChanged("ActTimeDrivingSpanSalary");
            }
        }

        private int actDistanceKm;
        [JsonProperty("act_distance")]
        public int ActDistanceKm { get { return actDistanceKm; } set { actDistanceKm = value; OnPropertyChanged("ActDistanceKm"); } }

        private int actFuelConsumptionLiters;
        [JsonProperty("act_fuelConsumption")]
        public int ActFuelConsumptionLiters { get { return actFuelConsumptionLiters; } set { actFuelConsumptionLiters = value; OnPropertyChanged("ActFuelConsumptionLiters"); } }

        private double actFuelCost;
        [JsonProperty("act_cost")]
        public double ActFuelCost { get { return actFuelCost; } set { actFuelCost = value; OnPropertyChanged("ActFuelCost"); } }

        //time
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }
        
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        //locations
        private int startLocationid;
        [JsonProperty("start_location_id")]
        public int StartLocationId { get { return startLocationid; } set { startLocationid = value; startLocation = (City)startLocationid; } }

        private City startLocation;
        public City StartLocation { get { return startLocation; } set { startLocation = value; startLocationid = (int)startLocation; } }

        private int endLocationid;
        [JsonProperty("end_location_id")]
        public int EndLocationId { get { return endLocationid; } set { endLocationid = value; endLocation = (City)EndLocationId; } }

        private City endLocation;
        public City EndLocation { get { return endLocation; } set { endLocation = value; endLocationid = (int)endLocation; } }

        //other properties not in database
        private List<Load> loads;
        public List<Load> Loads { get { return loads; } set { loads = value; NrOfLoads = loads.Count; } }

        public int NrOfLoads { get; set; }


        //Event to notify changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Route()
        {
            Loads = new List<Load>();
        }

        public void GetTimeConsumedPerRoute()
        {
            TimeSpan time = this.EndTime - this.StartTime;
            this.ActTimeDrivingTimeSpan = time;
        }

        /// <summary>
        /// Salary earned if all loads are delivered on time
        /// </summary>
        public void CalculateEstimatedSalary()
        {
            double sum = 0;
            foreach (Load l in Loads)
            {
                sum += l.FullSalaryEur;
            }
            this.TotalEstimatedSalary = sum;
        }

        /// <summary>
        /// Salary earned after delays and fees are calculated off
        /// </summary>
        public void CalculateActualSalary()
        {
            double sum = 0;
            foreach (Load l in Loads)
            {
                sum += (Double)l.FinalSalaryEur;
            }
            this.TotalActualSalary = sum;
        }

        /// <summary>
        /// Calculates final revenue (all salaries earned from loads - fuelcost)
        /// </summary>
        public void CalculateFinalRevenue()
        {
            double final = TotalActualSalary - ActFuelCost;
            FinalRevenue = final;
        }

        /// <summary>
        /// Calculates estimated fuel consumption per current route
        /// </summary>
        /// <returns></returns>
        public void CalculateEstFuelConsumption()
        {
            int fuelCons = Convert.ToInt32(EstDistanceKm * (Truck.AvgFuelConsumpt / 100));
            this.EstFuelConsumptionLiters = fuelCons;
        }

        /// <summary>
        /// Calculates full distance: trucks location -> first load startpoint -> first load endpoint -> second load startpoint ...
        /// </summary>
        public void CalculateEstFullDistance()
        {
            GoogleAPI googleapi = new GoogleAPI();
            int km = 0;

            try
            {
                km = googleapi.calculatedistance(Truck.Location_id, Loads[0].StartLocationID);

                for (int i = 0; i < Loads.Count; i++)
                {
                    km += Convert.ToInt32(googleapi.calculatedistance(Loads[i].StartLocationID, Loads[i].EndLocationID));

                    if (Loads.Count - 1 == i)
                    {
                        this.EstDistanceKm = km;
                    }

                    else
                        km += Convert.ToInt32(googleapi.calculatedistance(Loads[i].EndLocationID, Loads[i + 1].StartLocationID));

                }
                this.EstDistanceKm = km;
            }
            catch (Exception)
            {
                this.EstDistanceKm = 0;
            }

        }


        /// <summary>
        /// Calculates time needed to finish the route
        /// </summary>
        /// <returns></returns>
        public void CalculateEstFullTimeDriving()
        {
            GoogleAPI googleapi = new GoogleAPI();
            long seconds = 0;

            try
            {
                seconds += Convert.ToInt64(googleapi.calculatetime(Truck.Location_id, Loads[0].StartLocationID));

                for (int i = 0; i < Loads.Count; i++)
                {
                    seconds += Convert.ToInt64(googleapi.calculatetime(Loads[i].StartLocationID, Loads[i].EndLocationID));

                    if (Loads.Count - 1 == i)
                    {
                        this.EstTimeDrivingTimeSpan = TimeSpan.FromSeconds(seconds);
                        //this.EstTimeDrivingMinutes = Convert.ToInt32(EstTimeDrivingTimeSpan.TotalMinutes);
                    }


                    else
                        seconds += Convert.ToInt64(googleapi.calculatetime(Loads[i].EndLocationID, Loads[i + 1].StartLocationID));

                }
                this.EstTimeDrivingTimeSpan = TimeSpan.FromSeconds(seconds);
                //this.EstTimeDrivingMinutes = Convert.ToInt32(EstTimeDrivingTimeSpan.TotalMinutes);
            }
            catch (Exception)
            {
                this.EstTimeDrivingTimeSpan = TimeSpan.FromSeconds(seconds);
                //this.EstTimeDrivingMinutes = Convert.ToInt32(EstTimeDrivingTimeSpan.TotalMinutes);

            }
        }

        /// <summary>
        /// Calculates fuel cost based on assumed average of 1.40 per liter in Europe
        /// </summary>
        public void CalculateEstFuelCost()
        {
            //average truck fuel price per liter in europe is around 1.40
            this.EstFuelCost = this.EstFuelConsumptionLiters * 1.40;
        }




    }
}
