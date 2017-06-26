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
using System.Linq;
using ClientGUI;

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


        //UI METHODS
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
            DriversDGV.ItemsSource = companyCtrl.DriverCtrl.GetAllDrivers();
            ClientDGV.ItemsSource = companyCtrl.ClientCtrl.GetAllClients();

            //user tab
            this.companyName.Content = companyCtrl.Company.CompanyName;
            this.companyAddress.Content = companyCtrl.Company.Address;
            UserName = u.Name;
            UserPhone = u.Phone;
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
            DataGridAssist.SetCellPadding(MaintenanceDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(MaintenanceDGV, new Thickness(3));


            //Drivers tab
            DataGridAssist.SetCellPadding(DriversDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(DriversDGV, new Thickness(3));

            //Clients tab
            DataGridAssist.SetCellPadding(ClientDGV, new Thickness(2));
            DataGridAssist.SetColumnHeaderPadding(ClientDGV, new Thickness(3));
        }


        //LOADS TAB
        public Route route;
        bool isUserInteractLoadsDGV = false; //defines if selection changed is by user or by itemsource change

        private void bt_addNewLoad_Click(object sender, RoutedEventArgs e)
        {
            NewLoadWindow newload = new NewLoadWindow();
            newload.Show();
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
            this.seeLoadOnMap(load);

            lv_loadClient.Items.Add(new { description = "Name", value = load.Client.Name });
            lv_loadClient.Items.Add(new { description = "Phone", value = load.Client.Phone });
            lv_loadClient.Items.Add(new { description = "Email", value = load.Client.Email });
            lv_loadClient.Items.Add(new { description = "Address", value = load.Client.Address });
        }

        private void seeLoadOnMap(Load l)
        {
            string origin = l.StartLocationCity.ToString();
            string dest = l.EndLocationCity.ToString();
            string loadRoute = String.Format("https://www.google.com/maps/dir/?api=1&origin="
                + origin + "&destination=" + dest);
            Uri source = new Uri(loadRoute);
            web_load.Source = source;
        }

        private void TabItem_TouchUp(object sender, TouchEventArgs e)
        {
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

        private void btn_emptySelectedLoadsLV_Click(object sender, RoutedEventArgs e)
        {
            lv_selectedLoadsForRoute.Items.Clear();
            cb_assignTruckToRoute.SelectedItem = null;
            bt_submitRoute.IsEnabled = false;
            bt_calculateEstimation.IsEnabled = false;
        }
        
        private void LoadsAvailableDGW_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isUserInteractLoadsDGV = true;
        }

        private void cb_assignTruckToRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                bt_calculateEstimation.IsEnabled = true;
        }

        private void cb_assignTruckToRoute_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cb_assignTruckToRoute.Items.IsEmpty)
                SnackbarLoads.MessageQueue.Enqueue("Your trucks are all busy or need a driver");
        }

        private void bt_calculateEstimation_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            List<Load> loads = new List<Load>();
            route = new Route();
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
                    lv_routeEstimation.Items.Add(new { description = "Time ", result = route.EstTimeDrivingTimeSpan.ToString(@"d\d\a\y\s\ h\h\o\u\r\s\ m\m\i\n\ ", System.Globalization.CultureInfo.InvariantCulture) });
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

        private async void bt_submitRoute_Click(object sender, RoutedEventArgs e)
        {
           await companyCtrl.RouteCtrl.AddRouteToList(route);

            isUserInteractLoadsDGV = false;
            lv_routeEstimation.Items.Clear();
            lv_selectedLoadsForRoute.Items.Clear();
            cb_assignTruckToRoute.SelectedItem = null;
            LoadsAvailableDGW.ItemsSource = companyCtrl.LoadCtrl.GetAvailableLoads();

            bt_calculateEstimation.IsEnabled = false;
            bt_submitRoute.IsEnabled = false;
            cb_assignTruckToRoute.ItemsSource = companyCtrl.TruckCtrl.GetAvailableTrucks();
        }


        //ROUTE TAB
        private void routesDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Route r = (Route)routesDGV.SelectedItem;
            LoadsFromRouteDGV.DataContext = r.Loads;
        }



        private void bt_downloadreport_Click(object sender, RoutedEventArgs e)
        {
            ReportHandler.GenerateReport();
        }

        private void bt_MarkRouteDelivered_Click(object sender, RoutedEventArgs e)
        {
            Route r = (Route)routesDGV.SelectedItem;

            if (r.FinalRevenue == 0)
            {
                MarkRouteDelivered markdeliveredwindow = new MarkRouteDelivered(r);
                markdeliveredwindow.Show();
            }
            else
            {
                SnackbarMarkRoute.MessageQueue.Enqueue("This route is already delivered");
            }
        }


        //TRUCKS TAB
        bool isUserInteractionDriverCb = false;  //defines user interaction for drivers combobox

        private void TrucksAddNew_Click(object sender, RoutedEventArgs e)
        {
            NewTruckWindow newtruck = new NewTruckWindow();
            newtruck.Show();
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

        private void TrucksDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Truck)TrucksDGV.SelectedItem != null)
            {
                Truck t = (Truck)TrucksDGV.SelectedItem;
                MaintenanceDGV.ItemsSource = t.GetMaintenances();
                lb_trucklicence.Content = t.LicencePlate;
                if (t.CurrentDriver != null) { }
                //cb_assignDriverToTruck.Text = t.CurrentDriver.ToString();
            }

        }
        
        private void cb_assignDriverToTruck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Truck)TrucksDGV.SelectedItem != null
                && (Driver)cb_assignDriverToTruck.SelectedItem != null)
            {
                Truck t = (Truck)TrucksDGV.SelectedItem;
                if (isUserInteractionDriverCb)
                {
                    isUserInteractionDriverCb = false;
                    if (!companyCtrl.TruckCtrl.AssignSingleDriverToTruck(t, (Driver)cb_assignDriverToTruck.SelectedItem))
                    {
                        SnackbarTruckDrivers.MessageQueue.Enqueue("Truck is busy, can't change the driver!");
                        return;
                    }

                    cb_assignDriverToTruck.ItemsSource = companyCtrl.DriverCtrl.GetUnassignedDrivers();
                    cb_assignTruckToRoute.ItemsSource = companyCtrl.TruckCtrl.GetAvailableTrucks();
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
                tb_maintenanceAction.Text.ToString(), dp_maintenanceDate.SelectedDate.Value, Convert.ToDouble(tb_maintenanceCost.Text)
                );
        }

        
        //DRIVERS TAB
        private void DriversAddNew_Click(object sender, RoutedEventArgs e)
        {
            NewDriverWindow addnewdriver = new NewDriverWindow();
            addnewdriver.Show();
        }

        private void bt_DriverDeleteSelected_Click(object sender, RoutedEventArgs e)
        {

            Driver driver = (Driver)DriversDGV.SelectedItem;
            MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete driver " + driver.FirstName + " " + driver.LastName + " ?", "Driver deletion", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {

                companyCtrl.DriverCtrl.RemoveDriver(driver);
            }
        }


        //CLIENTS TAB
        private void ClientsAddNew_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow newclient = new NewClientWindow();
            newclient.Show();

        }

        private void bt_ClientsDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Client c = (Client)ClientDGV.SelectedItem;
            MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete client " + c.Name + " ?", "Client deletion", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                companyCtrl.ClientCtrl.RemoveClient(c);
            }
        }


        //REPORT TAB
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
            tb_report.Text = sb.ToString();

            routesText = sb.ToString();
        }

   

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

            foreach (Route r in routesFromFile)
            {
                companyCtrl.RouteCtrl.AddRouteToList(r);
            }
        }


        //PROFILE TAB
        private void ProfileChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            ProfileEditName.Visibility = Visibility.Visible;
            ProfileEditPhone.Visibility = Visibility.Visible;
            ProfileSaveChanges.Visibility = Visibility.Visible;
            ProfileChangeInfo.Visibility = Visibility.Hidden;
        }

        private void ProfileSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            companyCtrl.ChangeUser(ProfileEditName.Text, ProfileEditPhone.Text);
            
            ProfileEditName.Visibility = Visibility.Hidden;
            ProfileEditPhone.Visibility = Visibility.Hidden;
            ProfileSaveChanges.Visibility = Visibility.Hidden;
            ProfileChangeInfo.Visibility = Visibility.Visible;
            
            UserName = ProfileEditName.Text;
            UserPhone = ProfileEditPhone.Text;
        }

       
    }
}
