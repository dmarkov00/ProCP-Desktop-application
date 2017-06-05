using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleApiIntegration;
using Common.Enumerations;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Models
{
    public class Route
    {
        [JsonProperty("sum_salaries")]
        public double TotalEstimatedSalary { get; set; }
        [JsonProperty("sum_actual_salaries")]
        public double TotalActualSalary { get; set; }
        [JsonProperty("revenue")]
        public double FinalRevenue { get; set; }
        [JsonProperty("act_time_used")]
        public TimeSpan TotalTimeUsed { get; set; }
 
        [JsonProperty("est_time_driving")]
        public TimeSpan EstTimeDrivingTimeSpan { get; set; }

        [JsonProperty("est_distance")] 
        public int EstDistanceKm { get; set; }
        [JsonProperty("est_fuelConsumption")]
        public int EstFuelConsumptionLiters { get; set; }


        public double EstFuelCost { get; set; }

        public TimeSpan ActTimeDrivingTimeSpan { get; set; }
        [JsonProperty("act_time_used")]       
        public int ActTimeDrivingMinutes { get; set; }
        [JsonProperty("act_time_used")]

        public int ActDistanceKm { get; set; }
        [JsonProperty("act_time_used")]

        public int ActFuelConsumptionLiters { get; set; }
        public double ActFuelCost { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("start_location_id")]
        public City StartLocation { get; set; }

        [JsonProperty("end_location_id")]
        public City EndLocation { get; set; }

        public Driver Driver { get; set; }
        public Truck Truck { get; set; }
        public List<Load> Loads { get; set; }
        public int NrOfLoads { get; set; }

        public Route(List<Load> loads/*, Truck truck, Driver driver, Address startloc*/)
        {
            this.Loads = loads;
            
            // this.Truck = truck;
            // this.Driver = driver;
            // this.StartLocation = startloc;
        }

        

        public void GetTimeConsumedPerRoute()
        {
            TimeSpan time = this.EndTime - this.StartTime;
            this.TotalTimeUsed = time;
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
                this.EstDistanceKm=0;
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
