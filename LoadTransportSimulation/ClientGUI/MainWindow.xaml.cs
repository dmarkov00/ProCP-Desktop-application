using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controllers;
using GoogleApiIntegration;

namespace WPFLoadSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApiHttpClient.Dispatcher client;

        private ClientController clientCtrl;
        private DriverController driverCtrl;
        private TruckController truckCtrl;
        private LoadController loadCtrl;
        private RouteController routeCtrl;

        public MainWindow()
        {
            InitializeComponent();
            client = ApiHttpClient.Dispatcher.GetInstance();
            CreateClientController();
            CreateTruckController();
            CreateDriverController();
            CreateLoadController();
            CreateRouteController();
        }

        
        
        private void CreateRouteController()
        {
            List<Route> routes = new List<Route>();
            routeCtrl = RouteController.Create(routes);
        } 

        private async void CreateLoadController()
        {
            IEnumerable<IApiCallResult> loads = await client.GetMany<Load>("loads");
            List<Load> targetListLoads = new List<Load>(loads.Cast<Load>());
            loadCtrl = LoadController.Create(targetListLoads);
            LoadsAvailableDGW.DataContext = loadCtrl.GetAvailableLoads();
            LoadsFromRouteDGV.DataContext = loadCtrl.GetAllLoads();
            return;
        }

        private async void CreateTruckController()
        {

            IEnumerable<IApiCallResult> trucks = await client.GetMany<Truck>("trucks");
            List<Truck> targetListTrucks = new List<Truck>(trucks.Cast<Truck>());
            truckCtrl = TruckController.Create(targetListTrucks);
            TrucksDGV.DataContext = truckCtrl.GetAllTrucks();


            foreach (Truck t in truckCtrl.GetAvailableTrucks())
            {
                cb_assignTruckToRoute.Items.Add(t);
            }

            return;
        }

        private async void CreateDriverController()
        {
            IEnumerable<IApiCallResult> drivers = await client.GetMany<Driver>("drivers");
            List<Driver> targetListDrivers = new List<Driver>(drivers.Cast<Driver>());
            driverCtrl = DriverController.Create(targetListDrivers);
            DriversDGV.DataContext = driverCtrl.GetAllDrivers();
            return;
        }

        private async void CreateClientController()
        {

            IEnumerable<IApiCallResult> clients = await client.GetMany<Client>("clients");
            List<Client> targetListClients = new List<Client>(clients.Cast<Client>());
            clientCtrl = ClientController.Create(targetListClients);
            ClientDGV.DataContext = clientCtrl.GetAllClients();
            return;
        }

        private void ProfileChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            ProfileEditFName.Visibility = Visibility.Visible;
            ProfileEditLName.Visibility = Visibility.Visible;
            ProfileEditEmail.Visibility = Visibility.Visible;
            ProfileEditPhone.Visibility = Visibility.Visible;
            ProfileSaveChanges.Visibility = Visibility.Visible;
            ProfileChangeInfo.Visibility = Visibility.Hidden;
        }

        private void ProfileSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ProfileEditFName.Visibility = Visibility.Hidden;
            ProfileEditLName.Visibility = Visibility.Hidden;
            ProfileEditEmail.Visibility = Visibility.Hidden;
            ProfileEditPhone.Visibility = Visibility.Hidden;
            ProfileSaveChanges.Visibility = Visibility.Hidden;
            ProfileChangeInfo.Visibility = Visibility.Visible;
        }



        private void DriversAddNew_Click(object sender, RoutedEventArgs e)
        {
            NewDriverWindow addnewdriver = new NewDriverWindow();
            addnewdriver.Show();
        }

        private void ClientsAddNew_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow newclient = new NewClientWindow();
            newclient.Show();
        }

        

        private void TrucksAddNew_Click(object sender, RoutedEventArgs e)
        {
            NewTruckWindow newtruck = new NewTruckWindow();
            newtruck.Show();
        }


        private async void bt_DriverDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            
            Driver driver = (Driver)DriversDGV.SelectedItem;
           string result = await client.Delete("drivers", driver.Id.ToString());
            driverCtrl.RemoveDriver(driver);

           DriversDGV.DataContext = null;
           DriversDGV.DataContext = driverCtrl.GetAllDrivers();

        }

        private async void bt_TrucksDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Truck t = (Truck)TrucksDGV.SelectedItem;
            truckCtrl.RemoveTruck(t);
            string result = await client.Delete("trucks", t.Id);
            TrucksDGV.DataContext = null;
            TrucksDGV.DataContext = truckCtrl.GetAllTrucks();
        }

        private async void bt_ClientsDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Client c = (Client)ClientDGV.SelectedItem;
            clientCtrl.RemoveClient(c);
            string result = await client.Delete("clients", c.Id);
            ClientDGV.DataContext = null;
            ClientDGV.DataContext = clientCtrl.GetAllClients();
        }

        private void bt_LoadsAddToRoute_Click(object sender, RoutedEventArgs e)
        {
            foreach(Load l in LoadsAvailableDGW.SelectedItems)
            {
                lb_selectedLoadsForRoute.Items.Add(l);
                LoadsAvailableDGW.Items.Remove(l);
            }
        }

        

        private void bt_calculateEstimation_Click(object sender, RoutedEventArgs e)
        {
            lb_routeEstimation.Items.Clear();
            List<Load> loads = new List<Load>();
            foreach (Load l in lb_selectedLoadsForRoute.Items)
            {
                loads.Add(l);
            }

            Route route = new Route(loads);
            route.Truck = (Truck)cb_assignTruckToRoute.SelectedItem;
            routeCtrl.SetEstimations(route);
            lb_routeEstimation.Items.Add("Estimated distance: " +route.EstDistanceKm + "km");
            lb_routeEstimation.Items.Add("Estimated time: " 
                + route.EstTimeDrivingTimeSpan.Days + "days:"
                + route.EstTimeDrivingTimeSpan.Hours + "hh:"
                + route.EstTimeDrivingTimeSpan.Minutes + "min");
            lb_routeEstimation.Items.Add("Estimated fuel consump: " + route.EstFuelConsumptionLiters +"L");
        }

        private void LoadsAvailableDGW_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lb_selectedLoadsForRoute.Items.Add(LoadsAvailableDGW.SelectedItem);
        }

        private void lb_selectedLoadsForRoute_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lb_selectedLoadsForRoute.Items.Remove(lb_selectedLoadsForRoute.SelectedItem);
        }
    }
}
