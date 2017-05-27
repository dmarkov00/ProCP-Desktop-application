using Models;
using System;
using System.Collections.Generic;
using Common;
using GoogleApiIntegration;
using System.Text.RegularExpressions;

namespace Controllers
{
    public class RouteController
    {
        private List<Route> routes;
        
        private static volatile RouteController instance;
        private static object syncRoot = new Object();

        public static RouteController GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }

        public static RouteController Create(List<Route> routes)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new RouteController(routes);
            }
            return instance;
        }

        public RouteController(List<Route> r)
        {
            this.routes = r;
        }

       
        public List<Route> GetAllRoutes()
        {
            return routes;
        }

        public void AddRouteToList(Route r)
        {
            routes.Add(r);
        }

        public void SetEstimations(Route r)
        {
           r.EstDistanceKm = CalculateFullDistance(r);
            r.EstTimeDrivingTimeSpan = GetFullTimeDriving(r);
            r.EstTimeDrivingMinutes = Convert.ToInt32(r.EstTimeDrivingTimeSpan.TotalMinutes);

            GoogleAPI api = new GoogleAPI();
            r.EstFuelConsumptionLiters = Convert.ToInt32(api.calculateFuelConsumption(r.EstDistanceKm.ToString(),r.Truck.AvgFuelConsumpt));
        }


        /// <summary>
        /// Calculates full distance: trucks location -> first load startpoint -> first load endpoint -> second load startpoint ...
        /// </summary>
        public int CalculateFullDistance(Route r)
        {  
            GoogleAPI googleapi = new GoogleAPI();
            int km = 0;

            try
            {
                string truckToFirstLoad = googleapi.calculatedistance(r.Truck.Location_id, r.Loads[0].StartLocationID);
                km += Convert.ToInt32(truckToFirstLoad);

                for (int i = 0; i < r.Loads.Count; i++)
                {
                    km += Convert.ToInt32(googleapi.calculatedistance(r.Loads[i].StartLocationID, r.Loads[i].EndLocationID));
                    
                    if (r.Loads.Count - 1 == i)
                        return km;

                    else
                        km += Convert.ToInt32(googleapi.calculatedistance(r.Loads[i].EndLocationID, r.Loads[i + 1].StartLocationID));

                }
                return km;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public TimeSpan GetFullTimeDriving(Route r)
        {
            GoogleAPI googleapi = new GoogleAPI();
            long seconds = 0;

            try
            {
                seconds += Convert.ToInt64(googleapi.calculatetime(r.Truck.Location_id, r.Loads[0].StartLocationID));
                
                for (int i = 0; i < r.Loads.Count; i++)
                {
                    seconds += Convert.ToInt64(googleapi.calculatetime(r.Loads[i].StartLocationID, r.Loads[i].EndLocationID));

                    if (r.Loads.Count - 1 == i)
                        return TimeSpan.FromSeconds(seconds);

                    else
                       seconds += Convert.ToInt64(googleapi.calculatetime(r.Loads[i].EndLocationID, r.Loads[i + 1].StartLocationID));

                }
                return TimeSpan.FromSeconds(seconds);
            }
            catch (Exception)
            {
                return  TimeSpan.FromSeconds(seconds);
            }
        }
        
    }
}
