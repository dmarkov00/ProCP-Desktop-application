using Models;
using Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using MaterialDesignThemes.Wpf;
using PdfReportHandling;
namespace WPFLoadSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CompanyController companyCtrl;
        User u;
        
        public string UserName
        {
            get { return ProfileEditName.Text; }
            set
            {
                this.ProfileEditName.Text = value;
                this.ProfileName.Content = value;
                
            }
        }
            
        public string UserEmail
        {
            get { return ProfileEditEmail.Text; }
            set
            {
                this.ProfileEditEmail.Text = value;
                this.ProfileEmail.Content = value;
            }
        }

        public string UserPhone
        {
            get { return ProfileEditPhone.Text; }
            set
            {
                this.ProfileEditPhone.Text = value;
                this.ProfilePhone.Content = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            companyCtrl = CompanyController.GetInstance();
            u = User.GetInstance();
            companyCtrl.OnControllersCreated += new ControllersCreated(companyCtrl_OnControllersCreated);
            SetTablePaddings();
        }

        public void companyCtrl_OnControllersCreated(object sender, ControllersEventArgs e)
        {
            FillUIWithData();
        }

        private void FillUIWithData()
        {
            //loads tab
            LoadsAvailableDGW.ItemsSource = companyCtrl.LoadCtrl.GetAvailableLoads();
            routesDGV.ItemsSource = companyCtrl.RouteCtrl.GetAllRoutes();
            cb_assignTruckToRoute.ItemsSource = companyCtrl.TruckCtrl.GetAvailableTrucks();
            
            //trucks tab
            TrucksDGV.ItemsSource = companyCtrl.TruckCtrl.GetAllTrucks();
            cb_assignDriverToTruck.ItemsSource = companyCtrl.DriverCtrl.GetUnassignedDrivers();
            cb_maintenanceDriver.ItemsSource = companyCtrl.DriverCtrl.GetAllDrivers();
            
            
            //driver and client tab
            DriversDGV.DataContext = companyCtrl.DriverCtrl.GetAllDrivers();
            ClientDGV.DataContext = companyCtrl.ClientCtrl.GetAllClients();

            //user tab
            this.UserName = u.Name;
            this.UserEmail = u.Email;
            this.UserPhone = u.Phone;
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
            ProfileEditName.Visibility = Visibility.Visible;
            ProfileEditEmail.Visibility = Visibility.Visible;
            ProfileEditPhone.Visibility = Visibility.Visible;
            ProfileSaveChanges.Visibility = Visibility.Visible;
            ProfileChangeInfo.Visibility = Visibility.Hidden;
        }

        private async void ProfileSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            await companyCtrl.ChangeUser(ProfileEditName.Text,ProfileEditEmail.Text,ProfileEditPhone.Text);
            
            ProfileEditName.Visibility = Visibility.Hidden;
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

        //a lot of logic in the GUI
        private void bt_calculateEstimation_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            List<Load> loads = new List<Load>();
            route = new Route(loads);
            bt_calculateEstimation.IsEnabled = false;

            if (lv_selectedLoadsForRoute.Items.IsEmpty)
            {
                SnackbarLoads.MessageQueue.Enqueue("Can't calculate without some loads!");
                return;
            }

            lv_routeEstimation.Items.Clear();
            foreach (Load l in lv_selectedLoadsForRoute.Items)
            {
                loads.Add(l);
            }
            route.Truck = (Truck)cb_assignTruckToRoute.SelectedItem;
            route.Loads = loads;


            SnackbarMessageQueue mq = new SnackbarMessageQueue(TimeSpan.FromSeconds(loads.Count / 1.5));
            SnackbarEstimation.MessageQueue = mq;
            SnackbarEstimation.MessageQueue.Enqueue("Please wait, I am calculating");

            bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    companyCtrl.RouteCtrl.SetEstimations(route);
                });

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    SnackbarEstimation.MessageQueue.Enqueue("Finished");
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
            TruckController tc = TruckController.GetInstance();
            LoadController lc = LoadController.GetInstance();
            List<Truck> trucks = tc.GetAllTrucks().ToList();
            Truck truck = (Truck)cb_assignTruckToRoute.SelectedItem;
            route.DriverId = Convert.ToInt32(trucks.Where(x => x.LicencePlate.Equals(truck.LicencePlate)).First().Driver_id);
            companyCtrl.RouteCtrl.AddRouteToList(route);

            foreach (Load l in lv_selectedLoadsForRoute.Items)
                  {
                lc.SetDriverRouteTruck(l.ID.ToString(), route.DriverId.ToString(), route.Id, truck.Id);
                      companyCtrl.LoadCtrl.GetLoad(l.ID).LoadState = Common.Enumerations.LoadState.ONTRANSPORT;
                  }

            isUserInteractLoadsDGV = false;
            lv_routeEstimation.Items.Clear();
            lv_selectedLoadsForRoute.Items.Clear();
            cb_assignTruckToRoute.SelectedItem = null;
            LoadsAvailableDGW.ItemsSource = companyCtrl.LoadCtrl.GetAvailableLoads();
            bt_calculateEstimation.IsEnabled = false;
            bt_submitRoute.IsEnabled = false;
            //from here we set the route for each load
            RouteController rc = RouteController.GetInstance();
            foreach (Load l in lv_selectedLoadsForRoute.Items)
            {
                String truckId = trucks.Where(x => x.LicencePlate.Equals(cb_assignTruckToRoute.SelectedItem.ToString())).First().Id;
                String load = l.ID.ToString();
                String driverId = trucks.Where(x => x.LicencePlate.Equals(cb_assignTruckToRoute.SelectedItem.ToString())).First().Driver_id;
                String routeId = rc.GetAllRoutes().Where(x => x.StartLocationId.Equals(route.StartLocationId))
                    .Where(x => x.EndLocationId.Equals(route.EndLocationId)).First().Id;
                lc.SetDriverRouteTruck(load, driverId, routeId, truckId);
                //String truckId = cb_assignTruckToRoute.SelectedItem.ToString();
                //companyCtrl.LoadCtrl.GetLoad(l.ID).LoadState = Common.Enumerations.LoadState.ONTRANSPORT;
            }
            //to here
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
            bt_submitRoute.IsEnabled = false;
        }

    

        private void routesDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Route r = (Route)routesDGV.SelectedItem;
            LoadsFromRouteDGV.DataContext = r.Loads;
        }

        StringBuilder sb;
        private string routesText;
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

            routesText = sb.ToString();
        }

        private void bt_downloadreport_Click(object sender, RoutedEventArgs e)
        {
            ReportHandler.GenerateReport();
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
            RouteController rc = RouteController.GetInstance();
            MessageBox.Show(rc.MarkRouteDelivered(r.Id));

        }
        

        private void LoadsAvailableDGW_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isUserInteractLoadsDGV)
                return;


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

        private void TrucksDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Truck)TrucksDGV.SelectedItem != null)
            {
                Truck t = (Truck)TrucksDGV.SelectedItem;
                MaintenanceDGV.ItemsSource = t.GetMaintenances(); 
                if (t.CurrentDriver != null) { }
                    //cb_assignDriverToTruck.Text = t.CurrentDriver.ToString();
            }
            
        }


        bool isUserInteractionDriverCb = false;
        private void cb_assignDriverToTruck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Truck)TrucksDGV.SelectedItem != null
                && (Driver)cb_assignDriverToTruck.SelectedItem != null)
            {
                Truck t = (Truck)TrucksDGV.SelectedItem;
                if (isUserInteractionDriverCb)
                {
                    companyCtrl.TruckCtrl.AssignSingleDriverToTruck(t, (Driver)cb_assignDriverToTruck.SelectedItem);
                    isUserInteractionDriverCb = false;
                }
            }
        }

        private void cb_assignDriverToTruck_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isUserInteractionDriverCb = true;
        }

        private void bt_addMaintenance_Click(object sender, RoutedEventArgs e)
        {
            companyCtrl.TruckCtrl.AddMaintenance((Truck)TrucksDGV.SelectedItem,
                (Driver)cb_maintenanceDriver.SelectedItem,
                tb_maintenanceAction.ToString(), dp_maintenanceDate.SelectedDate.Value, Convert.ToDouble(tb_maintenanceCost.Text)
                );
        }

        private void btn_emptySelectedLoadsLV_Click(object sender, RoutedEventArgs e)
        {
            lv_selectedLoadsForRoute.Items.Clear();
            cb_assignTruckToRoute.SelectedItem = null;
            bt_submitRoute.IsEnabled = false;
            bt_calculateEstimation.IsEnabled = false;
        }

        bool isUserInteractLoadsDGV = false;
        private void LoadsAvailableDGW_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isUserInteractLoadsDGV = true;
        }
    }
}
