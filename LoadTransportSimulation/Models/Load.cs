using System;
using Common.Enumerations;
using Common;
namespace Models
{
    public class Load : IApiCallResult
    {

        public Client Client { get; set; }
        public Address StartLocation { get; private set; }
        public Address EndLocation { get; private set; }
        public string Content { get; private set; }
        public decimal WeightKg { get; private set; }
        public double SalaryEur { get; set; }  //delay fee percentage per one hour of delay
        public DateTime MaxArrivalTime { get;  set; }
        public DateTime ActArrivalTime { get; set; }
        public LoadState LoadState { get; set; }
        public int LoadId { get; private set; }

        public Load(Address start, Address end, string content,
            decimal weight, double salary,  DateTime maxarrival, Client client)
        {
            this.StartLocation = start;
            this.EndLocation = end;
            this.Content = content;
            this.WeightKg = weight;
            this.SalaryEur = salary;
            this.MaxArrivalTime = maxarrival;
            this.Client = client;
            this.LoadState = LoadState.AVAILABLE;
        }

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
