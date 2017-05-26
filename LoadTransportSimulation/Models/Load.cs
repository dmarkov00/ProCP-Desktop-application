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
        public double EstSalaryEur { get; set; } //full, estimated salary (without fee taken off yet)
        public double ActSalaryEur { get; set; } //final salary after subtracting delay fee
        public int DelayFeePercPerHour { get;  set; } //delay fee percentage per one hour of delay
        public DateTime MaxArrivalTime { get;  set; }
        public DateTime ActArrivalTime { get; set; }
        public LoadState LoadState { get; set; }
        public int LoadId { get; private set; }

        public Load(Address start, Address end, string content,
            decimal weight, double fullsalary, int delayfee, DateTime maxarrival, Client client)
        {
            this.StartLocation = start;
            this.EndLocation = end;
            this.Content = content;
            this.WeightKg = weight;
            this.EstSalaryEur = fullsalary;
            this.DelayFeePercPerHour = delayfee;
            this.MaxArrivalTime = maxarrival;
            this.Client = client;
            this.LoadState = LoadState.AVAILABLE;
        }

        public double CalculateFinalSalary()
        {
            double finalsalary = 0;
            double delayfee = 0;

            TimeSpan delaytime = (ActArrivalTime - MaxArrivalTime);
            double delayhours = delaytime.TotalHours;
            if (delayhours > 0)
            {
                delayfee = delayhours * (EstSalaryEur * (Convert.ToDouble(DelayFeePercPerHour) / 100));
            }

            finalsalary = EstSalaryEur - delayfee;
            ActSalaryEur = finalsalary;
            return finalsalary;
        }

    }
}
