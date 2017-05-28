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
            try
            {
                r.CalculateEstFullDistance();
                r.CalculateEstFullTimeDriving();
                r.CalculateEstFuelConsumption();
                r.CalculateEstFuelCost();
                r.CalculateEstimatedSalary();
            }
            catch (Exception)
            {
                return;
            }
            
        }
        
        
    }
}
