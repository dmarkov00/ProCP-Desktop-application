using Common;
using Models;
using Controllers;
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
using GoogleApiIntegration;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using MaterialDesignThemes.Wpf;

namespace WPFLoadSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CompanyController companyCtrl;

        public MainWindow()
        {
            InitializeComponent();
            companyCtrl = CompanyController.GetInstance();
            companyCtrl.OnControllersCreated += new ControllersCreated(companyCtrl_OnControllersCreated);
            SetTablePaddings();
        }

        public void companyCtrl_OnControllersCreated(object sender, ControllersEventArgs e)
        {
            FillUIWithData();
        }

        private void FillUIWithData()
        {
            routesDGV.ItemsSource = companyCtrl.RouteCtrl.GetAllRoutes();
            LoadsAvailableDGW.DataContext = companyCtrl.LoadCtrl.GetAvailableLoads();
            TrucksDGV.ItemsSource = companyCtrl.TruckCtrl.GetAllTrucks();
            cb_assignDriverToTruck.ItemsSource = companyCtrl.DriverCtrl.GetUnassignedDrivers();

            foreach (Truck t in companyCtrl.TruckCtrl.GetAvailableTrucks())
            {
                cb_assignTruckToRoute.Items.Add(t);
            }

            DriversDGV.DataContext = companyCtrl.DriverCtrl.GetAllDrivers();
            ClientDGV.DataContext = companyCtrl.ClientCtrl.GetAllClients();
        }

        private void SetTablePaddings()
        {
            //Loads tab
            DataGridAssist.SetCellPadding(LoadsAvailableDGW, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(LoadsAvailableDGW, new Thickness(3));
            ListViewAssist.SetListViewItemPadding(lv_loadDetails, new Thickness(2));
            ListViewAssist.SetListViewItemPadding(lv_loadClient, new Thickness(2));
            ListViewAssist.SetListViewItemPadding(lv_selectedLoadsForRoute, new Thickness(2));
            ListViewAssist.SetListViewItemPadding(lv_routeEstimation, new Thickness(2));

            //Routes tab
            DataGridAssist.SetCellPadding(routesDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(routesDGV, new Thickness(3));
            DataGridAssist.SetCellPadding(LoadsFromRouteDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(LoadsFromRouteDGV, new Thickness(3));

            //Trucks tab
            DataGridAssist.SetCellPadding(TrucksDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(TrucksDGV, new Thickness(3));

            //Drivers tab
            DataGridAssist.SetCellPadding(DriversDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(DriversDGV, new Thickness(3));

            //Clients tab
            DataGridAssist.SetCellPadding(ClientDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(ClientDGV, new Thickness(3));
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


        private void bt_DriverDeleteSelected_Click(object sender, RoutedEventArgs e)
        {

            Driver driver = (Driver)DriversDGV.SelectedItem;
            MessageBoxResult answer=MessageBox.Show("Are you sure you want to delete driver "+driver.FirstName+" "+driver.LastName+" ?", "Driver deletion", MessageBoxButton.YesNo);
            if (answer==MessageBoxResult.Yes)
            {
                
               companyCtrl.DriverCtrl.RemoveDriver(driver);
            }      
        }

        private void bt_TrucksDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Truck t = (Truck)TrucksDGV.SelectedItem;
            MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete truck " + t.LicencePlate + " ?", "Truck deletion", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
               companyCtrl.TruckCtrl.RemoveTruck(t);
               
            }
        }

        private void bt_ClientsDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Client c = (Client)ClientDGV.SelectedItem;
            MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete client " + c.Name  + " ?", "Client deletion", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                companyCtrl.ClientCtrl.RemoveClient(c);
                
            }
        }

        private void bt_LoadsAddToRoute_Click(object sender, RoutedEventArgs e)
        {
            foreach (Load load in LoadsAvailableDGW.SelectedItems)
            {
                if (!lv_selectedLoadsForRoute.Items.Contains(load))
                {
                    lv_selectedLoadsForRoute.Items.Add(load);
                }
            }
        }


        public Route route;
        private void bt_calculateEstimation_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            List<Load> loads = new List<Load>();
            route = new Route(loads);
            bt_calculateEstimation.IsEnabled = false;

            lv_routeEstimation.Items.Clear();
            lv_routeEstimation.Items.Add(new { description = "Calculating" });
            foreach (Load l in lv_selectedLoadsForRoute.Items)
            {
                loads.Add(l);
            }
            route.Truck = (Truck)cb_assignTruckToRoute.SelectedItem;

            bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    companyCtrl.RouteCtrl.SetEstimations(route);
                });

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    lv_routeEstimation.Items.Clear();
                    lv_routeEstimation.Items.Add(new { description = "Distance ", result = route.EstDistanceKm, unit = "Km" });
                    lv_routeEstimation.Items.Add(new { description = "Time ", result = route.EstTimeDrivingTimeSpan.ToString(@"d\d\a\y\s\ h\h\o\u\r\s\ m\m\i\n\ ", System.Globalization.CultureInfo.InvariantCulture)});
                    lv_routeEstimation.Items.Add(new { description = "Fuel consumption ", result = route.EstFuelConsumptionLiters, unit = "Liters" });
                    lv_routeEstimation.Items.Add(new { description = "Fuel Cost ", result = route.EstFuelCost, unit = "Eur" });
                    lv_routeEstimation.Items.Add(new { description = "Salary ", result = route.TotalEstimatedSalary, unit = "Eur" });
                    lv_routeEstimation.Items.Add(new { description = "Revenue ", result = (route.TotalEstimatedSalary - route.EstFuelCost).ToString(), unit = "Eur" });
                    bt_calculateEstimation.IsEnabled = true;
                    bt_submitRoute.IsEnabled = true;
                }
            );
            bw.RunWorkerAsync();

        }

        private void bt_submitRoute_Click(object sender, RoutedEventArgs e)
        {
            
           companyCtrl.RouteCtrl.AddRouteToList(route);
            lv_routeEstimation.Items.Clear();
            lv_selectedLoadsForRoute.Items.Clear();
            cb_assignTruckToRoute.SelectedItem = null;
            bt_submitRoute.IsEnabled = false;
            bt_calculateEstimation.IsEnabled = false;
        }

        private void LoadsAvailableDGW_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Load load = (Load)LoadsAvailableDGW.SelectedItem;
            if (!lv_selectedLoadsForRoute.Items.Contains(load))
            {
                lv_selectedLoadsForRoute.Items.Add(load);
            }
            bt_submitRoute.IsEnabled = false;
        }

        private void lv_selectedLoadsForRoute_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lv_selectedLoadsForRoute.Items.Remove(lv_selectedLoadsForRoute.SelectedItem);
        }

    

        private void routesDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Route r = (Route)routesDGV.SelectedItem;
            LoadsFromRouteDGV.DataContext = r.Loads;
        }

        StringBuilder sb;
        private void bt_generatereport_Click(object sender, RoutedEventArgs e)
        {
             sb = new StringBuilder();
            foreach (Route r in companyCtrl.RouteCtrl.GetAllRoutes())
            {
                sb.Append(JsonConvert.SerializeObject(r, Formatting.Indented));
                sb.Append("-");
            }

            bt_downloadreport.IsEnabled = true;
            tb_report.Text =sb.ToString();
        }

        private void bt_downloadreport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog savedialog = new Microsoft.Win32.SaveFileDialog();
            Regex rgx = new Regex("[^0-9-]");
            savedialog.FileName = "Report" + rgx.Replace(System.DateTime.Now.ToString(), "");
            savedialog.DefaultExt = ".txxt"; 
            savedialog.Filter = "Text file  | *.txt"; 

            StreamWriter sw;
            if (savedialog.ShowDialog() == true)
            {
                using (sw = new StreamWriter(savedialog.FileName))
                {
                    sw.Write(sb);
                }
            }
            bt_downloadreport.IsEnabled = false;
        }


        //not called for now, needs some care with deserializing timespan 
        private void bt_loadreport_Click(object sender, RoutedEventArgs e)
        {
            string jsonOutput = "";
            List<Route> routesFromFile = new List<Route>();

            Microsoft.Win32.OpenFileDialog opendialog = new Microsoft.Win32.OpenFileDialog();
            opendialog.Filter = "Text file  | *.txt";
            
            if (opendialog.ShowDialog() == true)
            {
                using (StreamReader sr = new StreamReader(opendialog.FileName))
                {
                    jsonOutput = sr.ReadToEnd();
                }
            }

            string[] singleRoutes = Regex.Split(jsonOutput, "-");
            foreach (string substring in singleRoutes)
            {
                
                Route route = JsonConvert.DeserializeObject<Route>(substring);
                routesFromFile.Add(route);
            }

            foreach(Route r in routesFromFile)
            {
               companyCtrl.RouteCtrl.AddRouteToList(r);
            }
        }

        private void cb_assignTruckToRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bt_calculateEstimation.IsEnabled = true;
        }

        private void bt_MarkRouteDelivered_Click(object sender, RoutedEventArgs e)
        {
            Route r=(Route)routesDGV.SelectedItem;
            Truck t = TruckController.GetInstance().GetTruck(r.Truck.LicencePlate);
            t.LocationCity = r.EndLocation;
            t.Location_id = (int)r.EndLocation;
            t.IsBusy = false;

        }

        private void cb_ssignDriverToTruck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Truck t = (Truck)TrucksDGV.SelectedItem;
            t.CurrentDriver = (Driver)cb_assignDriverToTruck.SelectedItem;
            
            
        }

        private void LoadsAvailableDGW_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lv_loadClient.Items.Clear();
            lv_loadDetails.Items.Clear();

            Load load = (Load)LoadsAvailableDGW.SelectedItem;
            lv_loadDetails.Items.Add(new { description = "Start Location", value = load.StartLocationCity });
            lv_loadDetails.Items.Add(new { description = "End Location", value = load.EndLocationCity });
            lv_loadDetails.Items.Add(new { description = "Deadline", value = load.MaxArrivalTime });
            lv_loadDetails.Items.Add(new { description = "Salary", value = load.FullSalaryEur });
            lv_loadDetails.Items.Add(new { description = "Delay % per hour", value = load.DelayFeePercHour });
            lv_loadDetails.Items.Add(new { description = "Content", value = load.Content });
            lv_loadDetails.Items.Add(new { description = "Weight", value = load.WeightKg });

            lv_loadClient.Items.Add(new { description = "Name", value = load.Client.Name });
            lv_loadClient.Items.Add(new { description = "Phone", value = load.Client.Phone });
            lv_loadClient.Items.Add(new { description = "Email", value = load.Client.Email });
            lv_loadClient.Items.Add(new { description = "Address", value = load.Client.Address });
        }
    }
}
