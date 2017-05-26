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

        public MainWindow()
        {
            InitializeComponent();
            client = ApiHttpClient.Dispatcher.GetInstance();
            CreateClientController();
            CreateTruckController();
            CreateDriverController();
            CreateLoadController();
            
        }
        

        private async void CreateLoadController()
        {
            IEnumerable<IApiCallResult> loads = await client.GetMany<Load>("loads");
            List<Load> targetListLoads = new List<Load>(loads.Cast<Load>());
            loadCtrl = LoadController.Create(targetListLoads);
            LoadsAvailableDGW.DataContext = loadCtrl.GetAllLoads();
            LoadsFromRouteDGV.DataContext = loadCtrl.GetAllLoads();
            return;
        }

        private async void CreateTruckController()
        {

            IEnumerable<IApiCallResult> trucks = await client.GetMany<Truck>("trucks");
            List<Truck> targetListTrucks = new List<Truck>(trucks.Cast<Truck>());
            truckCtrl = TruckController.Create(targetListTrucks);
            TrucksDGV.DataContext = truckCtrl.GetAllTrucks();
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

        private void LoadsAddNewLoad_Click(object sender, RoutedEventArgs e)
        {
            NewLoadWindow newload = new NewLoadWindow();
            newload.Show();
        }

        private void TrucksAddNew_Click(object sender, RoutedEventArgs e)
        {
            NewTruckWindow newtruck = new NewTruckWindow();
            newtruck.Show();
        }


        private void bt_DriverDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            
            Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Driver driver = (Driver)DriversDGV.SelectedItem;
            //string result = await client.Delete("drivers", driver.Id.toString());
            driverCtrl.RemoveDriver(driver);

           DriversDGV.DataContext = null;
           DriversDGV.DataContext = driverCtrl.GetAllDrivers();

        }

        private void bt_TrucksDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Truck t = (Truck)TrucksDGV.SelectedItem;
            truckCtrl.RemoveTruck(t);
            //string result = await client.Delete("trucks", t.Id.toString());
            TrucksDGV.DataContext = null;
            TrucksDGV.DataContext = truckCtrl.GetAllTrucks();
        }

        private void bt_ClientsDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Client c = (Client)ClientDGV.SelectedItem;
            clientCtrl.RemoveClient(c);
            //string result = await client.Delete("clients", c.Id.toString());
            ClientDGV.DataContext = null;
            ClientDGV.DataContext = clientCtrl.GetAllClients();
        }
    }
}
