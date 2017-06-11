﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ApiHttpClient;
using System.Collections.ObjectModel;
using Common;

namespace Controllers
{
    public class CompanyController
    {
        private object _lock = new Object();
        public event ControllersCreated OnControllersCreated;

        private static volatile CompanyController instance;
        private static object syncRoot = new Object();

        private Company company;
        private Dispatcher client;

        private ClientController clientCtrl;
        private DriverController driverCtrl;
        private TruckController truckCtrl;
        private LoadController loadCtrl;
        private RouteController routeCtrl;

        public Company Company { get { return company; } set { company = value; } }
        public ClientController ClientCtrl { get { return clientCtrl; } set { clientCtrl = value; } }
        public DriverController DriverCtrl { get { return driverCtrl; } set { driverCtrl = value; } }
        public TruckController TruckCtrl { get { return truckCtrl; } set { truckCtrl = value; } }
        public LoadController LoadCtrl { get { return loadCtrl; } set { loadCtrl = value; } }
        public RouteController RouteCtrl { get { return routeCtrl; } set { routeCtrl = value; } }

        public static CompanyController GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }

        public static CompanyController Create(Company company)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new CompanyController(company);
            }
            return instance;
        }

        public CompanyController(Company company)
        {
            this.Company = company;
            this.client = Dispatcher.GetInstance();
            CreateControllers();
        }


        public async void CreateControllers()
        {
            ControllersEventArgs e = new ControllersEventArgs();

            await CreateDriverController();
            await CreateTruckController();
            await CreateLoadController();
            await CreateClientController();
            await CreateRouteController();

            e.finished = true;

            OnControllersCreated(this, e);
        }

        private async Task CreateRouteController()
        {
            IEnumerable<IApiCallResult> routes = await client.GetMany<Route>("routes");
            ObservableCollection<Route> targetListRoutes = new ObservableCollection<Route>(routes.Cast<Route>());
            routeCtrl = RouteController.Create(targetListRoutes);
            return;
        }

        private async Task CreateLoadController()
        {
            IEnumerable<IApiCallResult> loads = await client.GetMany<Load>("loads");
            List<Load> targetListLoads = new List<Load>(loads.Cast<Load>());
            loadCtrl = LoadController.Create(targetListLoads);
            return;
        }


        private async Task CreateTruckController()
        {

            IEnumerable<IApiCallResult> trucks = await client.GetMany<Truck>("trucks");
            List<Truck> targetListTrucks = new List<Truck>(trucks.Cast<Truck>());
            truckCtrl = TruckController.Create(targetListTrucks);
            truckCtrl.AssignDriversToTrucks();
            return;
        }

        private async Task CreateDriverController()
        {
            IEnumerable<IApiCallResult> drivers = await client.GetMany<Driver>("drivers");
            List<Driver> targetListDrivers = new List<Driver>(drivers.Cast<Driver>());
            driverCtrl = DriverController.Create(targetListDrivers);
            return;
        }

        private async Task CreateClientController()
        {

            IEnumerable<IApiCallResult> clients = await client.GetMany<Client>("clients");
            List<Client> targetListClients = new List<Client>(clients.Cast<Client>());
            clientCtrl = ClientController.Create(targetListClients);
            loadCtrl.SetClientsForLoads();
            return;
        }

    }

    public delegate void ControllersCreated(object sender, ControllersEventArgs e);

    public class ControllersEventArgs : EventArgs
    {
        public bool finished;
    }
}
