using System;
using Common.Enumerations;
using Common;
using Newtonsoft.Json;

namespace Models
{
    public class Load : IApiCallResult
    {



        public Load(int startLocationID, int endLocationID, string content,
            decimal weight, double salary, DateTime maxarrival, int clientID)
        {
            this.StartLocationID = startLocationID;
            this.EndLocationID = startLocationID;
            this.Content = content;
            this.WeightKg = weight;
            this.SalaryEur = salary;
            this.MaxArrivalTime = maxarrival;
            this.Client = clientID;
            this.LoadState = LoadState.AVAILABLE;
        }
        [JsonProperty("client_id")] 
        public int Client { get; set; }
        [JsonProperty("startLocation_id")]
        public int StartLocationID { get; private set; }
        [JsonProperty("endLocation_id")]
        public int EndLocationID { get; private set; }
        public string Content { get; private set; }
        public decimal WeightKg { get; private set; }
        public double SalaryEur { get; set; }  //delay fee percentage per one hour of delay
        [JsonProperty("deadline")] 
        public DateTime MaxArrivalTime { get; set; }
        [JsonProperty("actualArrivalTime")] 
        public DateTime ActArrivalTime { get; set; }
        public LoadState LoadState { get; set; }
        [JsonProperty("id")]
        public int ID { get; private set; }

        //public double CalculateFinalSalary()
        //{
        //    double finalsalary = 0;
        //    double delayfee = 0;

        //    TimeSpan delaytime = (ActArrivalTime - MaxArrivalTime);
        //    double delayhours = delaytime.TotalHours;
        //    if (delayhours > 0)
        //    {
        //        delayfee = delayhours * (EstSalaryEur * (Convert.ToDouble(DelayFeePercPerHour) / 100));
        //    }

        //    finalsalary = EstSalaryEur - delayfee;
        //    ActSalaryEur = finalsalary;
        //    return finalsalary;
        //}

    }
}
