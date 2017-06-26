using System;
using Common.Enumerations;
using Common;
using Newtonsoft.Json;

namespace Models
{
    public class Load : IApiCallResult
    {
        public Load(int startLocationID, int endLocationID, string content,
            decimal weight, double fullsalary, DateTime deadline, double delay, int clientID)
        {
            this.StartLocationID = startLocationID;
            this.EndLocationID = endLocationID;
            this.Content = content;
            this.WeightKg = weight;
            this.FullSalaryEur = fullsalary;
            this.DelayFeePercHour = delay;
            this.ClientID = clientID;
            this.MaxArrivalTime = deadline;
            this.LoadStateID = (int)LoadState.AVAILABLE;

            this.StartLocationCity = (City)StartLocationID;
            this.EndLocationCity = (City)EndLocationID;
            this.LoadState = (LoadState)LoadStateID;
            this.ActArrivalTime = null;
        }
        
        [JsonProperty("client_id")]
        public int ClientID { get; set; }
        [JsonProperty("startLocation_id")]
        public int StartLocationID { get; private set; }
        public City StartLocationCity { get; set; }
        [JsonProperty("endLocation_id")]
        public int EndLocationID { get; private set; }
        public City EndLocationCity { get;  set; }
        [JsonProperty("load_content")]
        public string Content { get; private set; }
        [JsonProperty("weight")]
        public decimal WeightKg { get; private set; }
        [JsonProperty("fullsalary")]
        public double FullSalaryEur { get; set; }
        [JsonProperty("finalsalary")]
        public double? FinalSalaryEur { get; set; }
        [JsonProperty("delayfeePercHour")]
        public double DelayFeePercHour { get; set; }//delay fee percentage per one hour of delay
        [JsonProperty("deadline")]
        public DateTime? MaxArrivalTime { get; set; }
        [JsonProperty("arrivaldate")]
        public DateTime? ActArrivalTime { get; set; }

        [JsonProperty("loadstatus")]
        public int LoadStateID {get; set;}

        public LoadState LoadState { get; set; }
        [JsonProperty("id")]
        public int ID { get; private set; }
        public Client Client { get; set; }

        [JsonProperty("route_id")]
        public string RouteId { get; set; }

        public double CalculateFinalSalary()
        {
            double finalsalary = 0;
            double delayfee = 0;

            TimeSpan? delaytime = (ActArrivalTime - MaxArrivalTime);      
            //double delayhours = delaytime.TotalHours;
            //if (delayhours > 0)
            //{
            //    delayfee = delayhours * (FullSalaryEur * (Convert.ToDouble(DelayFeePercHour) / 100));
            //}
            finalsalary = FullSalaryEur - delayfee;
            FinalSalaryEur = finalsalary;
            return finalsalary;
        }

        public override string ToString()
        {
            return this.StartLocationCity + " - " + this.EndLocationCity + " - " + this.MaxArrivalTime;
        }

    }
}
