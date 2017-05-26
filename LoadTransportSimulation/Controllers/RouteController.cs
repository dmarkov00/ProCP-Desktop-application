using Models;
using System;

namespace Controllers
{
    public class RouteController
    {
        private Route route;

        public RouteController(Route r)
        {
            this.route = r;
        }

        public void SetEstimationsOfRoute()
        {

        }

        public void AddLoadToRoute(Load load)
        {
            this.route.Loads.Add(load);
        }

        public void RemoveLoadFromRoute(Load load)
        {
            this.route.Loads.Remove(load);
        }

        public void GetTimeConsumedPerRoute()
        {
            TimeSpan time = route.EndTime - route.StartTime;
            route.TotalTimeUsed = time;
        }

        //public void CalculateEstimatedSalary()
        //{
        //    double sum = 0;
        //    foreach (Load l in route.Loads)
        //    {
        //        sum += l.EstSalaryEur;
        //    }
        //    route.TotalEstimatedSalary = sum;
        //}

        //public void CalculateActualSalary()
        //{
        //    double sum = 0;
        //    foreach (Load l in route.Loads)
        //    {
        //        sum += l.ActSalaryEur;
        //    }
        //    route.TotalActualSalary = sum;
        //}
        public void CalculateFinalRevenue()
        {
            double final = route.TotalActualSalary - route.ActCost;
            route.FinalRevenue = final;
        }

        //methods to calculate cost and set other route properties
    }
}
