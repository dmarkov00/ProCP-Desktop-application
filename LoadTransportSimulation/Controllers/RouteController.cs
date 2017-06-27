using Models;
using System;
using System.Collections.Generic;
using Common;
using GoogleApiIntegration;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Net;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Controllers
{

    public class RouteController : INotifyPropertyChanged
    {
        private ObservableCollection<Route> routes;
        private object _lock = new Object();
        
        private static volatile RouteController instance;
        private static object syncRoot = new Object();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public static RouteController GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }

        public static RouteController Create(ObservableCollection<Route> routes)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new RouteController(routes);
            }
            return instance;
        }

        public RouteController(ObservableCollection<Route> r)
        {
            this.routes = r;
            BindingOperations.EnableCollectionSynchronization(routes, _lock);
        }

        

        public string MarkRouteDelivered(Route r)
        {
            //api
            this.markRouteAsDelivered(r.Id, r.ActTimeDrivingMin.ToString(), r.ActDistanceKm.ToString(), r.ActFuelConsumptionLiters.ToString(), r.ActFuelCost.ToString(), r.TotalActualSalary.ToString(), r.FinalRevenue.ToString());
            this.unsetDriverTaken(r.DriverId);
            this.unsetTruckTaken(r.TruckId);
            TruckController.GetInstance().ChangeTruckLocation(r.Truck, ((int)r.EndLocation).ToString());
            this.markAllLoadsDelivered(r);

            //app
            r.Truck.LocationCity = r.EndLocation;
            r.Truck.IsBusy = false;
            r.Truck.CurrentDriver.IsBusy = false;
            return "route marked as delivered";
        }

        private void markAllLoadsDelivered(Route r)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", User.GetInstance().Token);
                foreach (Load l in r.Loads)
                {
                    l.LoadState = Common.Enumerations.LoadState.DELIVERED;
                    l.LoadStateID = 3;

                    byte[] response =
                    client.UploadValues("http://127.0.0.1:8000/api/loads/finalize/" + l.ID, new NameValueCollection()
                    {
                        { "arrivaldate", l.ActArrivalTime.ToString() },
                        { "finalsalary", l.FinalSalaryEur.ToString()}
                    });
                }
            };
        }

        private void markRouteDelivered(string routeId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
                .Create("http://127.0.0.1:8000/api/routes/delivered/" + routeId);
            request.Method = "Get";
            request.Headers.Add("api_token", User.GetInstance().Token);
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            //request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private void setDriverTaken(string driverId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
            .Create("http://127.0.0.1:8000/api/drivers/taken/" + driverId);
            request.Method = "Get";
            request.Headers.Add("api_token", User.GetInstance().Token);
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            //request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private void setTruckTaken(string truckId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
            .Create("http://127.0.0.1:8000/api/trucks/taken/" + truckId);
            request.Method = "Get";
            request.Headers.Add("api_token", User.GetInstance().Token);
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            //request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private void unsetDriverTaken(string driverId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
            .Create("http://127.0.0.1:8000/api/drivers/untaken/" + driverId);
            request.Method = "Get";
            request.Headers.Add("api_token", User.GetInstance().Token);
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            //request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private void unsetTruckTaken(string truckId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
            .Create("http://127.0.0.1:8000/api/trucks/untaken/" + truckId);
            request.Method = "Get";
            request.Headers.Add("api_token", User.GetInstance().Token);
            request.KeepAlive = true;
            request.ContentType = "appication/json";
            //request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private void markRouteAsDelivered(string routeId, string actualTime, string actDistance,
            string actFuel, string actCost, string actSalaries, string rev)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", User.GetInstance().Token);
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/routes/delivered/" + routeId, new NameValueCollection()
                {
                    { "act_time_used", actualTime },
                    { "act_distance", actDistance },
                    { "act_fuelConsumption", actFuel },
                    { "act_cost", actCost },
                    { "sum_actual_salaries", actSalaries },
                    { "revenue", rev }
                });
            };
        }

        public ObservableCollection<Route> GetAllRoutes()
        {
            TruckController tc = TruckController.GetInstance();
            foreach(Route r in routes)
            {
                r.Truck = tc.GetTruck(r.TruckId);
            }
            return routes;
        }

        public void SetRoutes(ObservableCollection<Route> r)
        {
            routes = r;
            OnPropertyChanged("Routes");
        }

        public async Task AddRouteToList(Route r)
        {
            r.StartLocation = r.Truck.LocationCity;
            r.StartLocationId = (int)r.StartLocation;

            r.EndLocation = r.Loads[r.Loads.Count - 1].EndLocationCity;
            r.EndLocationId = (int)r.EndLocation;
            
            r.StartTime = System.DateTime.Now;
            r.NrOfLoads = r.Loads.Count;
            r.EstTimeDrivingMin = r.EstTimeDrivingTimeSpan.TotalMinutes;
            
            r.Truck.IsBusy = true;
            r.Truck.CurrentDriver.IsBusy = true;
            
            await this.addRoute(r);
            
            TruckController.GetInstance().SetAvailableTrucks();
            LoadController.GetInstance().SetAvailableLoads();

            routes.Add(r);
            await CompanyController.GetInstance().UpdateRouteController();
            
        }

        private async Task addRoute(Route r)
        {
            
            IApiCallResult newroute = await ApiHttpClient.Dispatcher.GetInstance().Post("routes", r);

            r.Id = ((Route)newroute).Id;

            this.setDriverTaken(r.DriverId);
            this.setTruckTaken(r.TruckId);
            foreach (Load l in r.Loads)
            {
                l.LoadState = Common.Enumerations.LoadState.ONTRANSPORT;
                this.SetDriverRouteTruck(l.ID.ToString(), r.DriverId, r.Id, r.TruckId);
            }
        }

        private void SetDriverRouteTruck(String loadId, String driverId, String routeId, String truckId)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("api_token", User.GetInstance().Token);
                byte[] response =
                client.UploadValues("http://127.0.0.1:8000/api/loads/" + loadId, new NameValueCollection()
                {
                    { "driver_id", driverId },
                    { "route_id", routeId },
                    { "truck_id", truckId },
                    { "loadstatus", "2" }
                });
            }
        }

        public Route SetEstimations(Route r)
        {
            try
            {
                r.CalculateEstFullDistance();
                r.CalculateEstFullTimeDriving();
                r.CalculateEstFuelConsumption();
                r.CalculateEstFuelCost();
                r.CalculateEstimatedSalary();
                return r;
            }
            catch (Exception)
            {
                // For testing 
                return new Route();
            }
            
        }

        public Route GetRoute(string id)
        {
            foreach(Route r in routes)
            {
                if (r.Id == id)
                    return r;
            }
            return null;
        }

        public void SetLoadsFromDatabase()
        {
            foreach(Load l in LoadController.GetInstance().GetAllLoads())
            {
                if (l.RouteId != null)
                {
                    Route r = this.GetRoute(l.RouteId);
                    if (r != null)
                    {
                        r.Loads.Add(l);
                    }
                }

            }

            foreach(Route r in routes)
            {
                r.NrOfLoads = r.Loads.Count;
                if (r.Loads.Count != 0)
                {
                    if(r.Loads[r.Loads.Count - 1].ActArrivalTime!=null)
                        r.EndTime = DateTime.ParseExact(r.Loads[r.Loads.Count - 1].ActArrivalTime, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

                }
                
            }
        }
    }
}
