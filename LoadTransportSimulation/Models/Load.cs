using System;
using Common.Enumerations;
using Common;
using Newtonsoft.Json;

namespace Models
{
    public class Load : IApiCallResult
    {



        public Load(int startLocationID, int endLocationID, string content,
            decimal weight, double fullsalary, double finalsal, double delay, DateTime maxarrival, int clientID)
        {
            this.StartLocationID = startLocationID;
            this.EndLocationID = endLocationID;
            this.Content = content;
            this.WeightKg = weight;
            this.FullSalaryEur = fullsalary;
            this.FinalSalaryEur = finalsal;
            this.DelayFeePercHour = delay;
            this.MaxArrivalTime = maxarrival;
            this.Client = clientID;
            this.LoadState = LoadState.AVAILABLE;

            this.StartLocationCity = (City)StartLocationID;
            this.EndLocationCity = (City)EndLocationID;
            
        }
        [JsonProperty("client_id")] 
        public int Client { get; set; }
        [JsonProperty("startLocation_id")]
        public int StartLocationID { get; private set; }
        public City StartLocationCity { get; private set; }
        [JsonProperty("endLocation_id")]
        public int EndLocationID { get; private set; }
        public City EndLocationCity { get; private set; }
        public string Content { get; private set; }
        public decimal WeightKg { get; private set; }
        [JsonProperty("fullsalary")]
        public double FullSalaryEur { get; set; }
        [JsonProperty("finalsalary")]
        public double FinalSalaryEur { get; set; }
        [JsonProperty("delayfeePercHour")]
        public double DelayFeePercHour { get; set; }//delay fee percentage per one hour of delay
        [JsonProperty("deadline")] 
        public DateTime MaxArrivalTime { get; set; }
        [JsonProperty("arrivaldate")] 
        public DateTime ActArrivalTime { get; set; }
        public LoadState LoadState { get; set; }
        [JsonProperty("id")]
        public int ID { get; private set; }
        public string ClientName { get; set; }

        public double CalculateFinalSalary()
        {
            double finalsalary = 0;
            double delayfee = 0;

            TimeSpan delaytime = (ActArrivalTime - MaxArrivalTime);
            double delayhours = delaytime.TotalHours;
            if (delayhours > 0)
            {
                delayfee = delayhours * (FullSalaryEur * (Convert.ToDouble(DelayFeePercHour) / 100));
            }

            finalsalary = FullSalaryEur - delayfee;
            FinalSalaryEur = finalsalary;
            return finalsalary;
        }

    }
}
