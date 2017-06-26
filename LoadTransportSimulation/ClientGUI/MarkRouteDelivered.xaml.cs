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
using System.Windows.Shapes;
using Controllers;
using System.Threading;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MarkRouteDelivered.xaml
    /// </summary>
    public partial class MarkRouteDelivered : Window
    {
        Route route;
        Load currentload;

        public MarkRouteDelivered(Route r)
        {
            InitializeComponent();
            route = r;
            FIllUIWithData();
            this.Title = route.StartLocation + " - " + route.EndLocation; 
        }

        private void FIllUIWithData()
        {
            cb_selectedload.ItemsSource = route.Loads;
        }
        
        private void cb_selectedload_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                try
                {
                    currentload = (Load)cb_selectedload.SelectedItem;
                    tb_load_timearrived.SelectedDate = currentload.ActArrivalTime;
                    tb_loadsalary.Text = currentload.FinalSalaryEur.ToString();
                }
                catch (FormatException)
                {
                    SnackbarException.MessageQueue.Enqueue("Something went wrong with loading");
                }
                catch (Exception)
                {
                    return;
                }
        }

        bool userinteraction = false;
        private void tb_load_timearrived_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            userinteraction = true;
        }

        private void tb_load_timearrived_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userinteraction)
                try
                {
                    currentload.ActArrivalTime = tb_load_timearrived.SelectedDate;
                    userinteraction = false;
                }
                catch (FormatException)
                {
                    SnackbarException.MessageQueue.Enqueue("Date format invalid");
                }
                catch (Exception)
                {
                    return;
                }
        }

        private void tb_loadsalary_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tb_loadsalary.Text!="" && tb_load_timearrived.Text!=null)
                try
                {
                    currentload.FinalSalaryEur = Convert.ToInt32(tb_loadsalary.Text);
                }
                catch (FormatException)
                {
                    SnackbarException.MessageQueue.Enqueue("Salary or time format wrong");
                }
                catch (Exception)
                {
                    return;
                }

        }
        

        private void bt_markdelivered_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateRoute();

                CompanyController.GetInstance().RouteCtrl.MarkRouteDelivered(route);
                SnackbarException.MessageQueue.Enqueue("Delivery saved!");
                bt_markdelivered.IsEnabled = false;
                //Thread.Sleep(1000);
                //this.Close();
            }
            catch (FormatException)
            {
                SnackbarException.MessageQueue.Enqueue("Invalid format found, review inputs.");
            }
            catch (Exception)
            {
                SnackbarException.MessageQueue.Enqueue("Request failed, please try again.");
                return;
            }
        }
        
        private void UpdateRoute()
        {
            route.ActDistanceKm = Convert.ToInt32(tb_distance.Text);
            route.ActTimeDrivingTimeSpan = GetTimeDriving();
            route.ActFuelConsumptionLiters = Convert.ToInt32(tb_fuelconsump.Text);
            route.ActFuelCost = Convert.ToInt32(tb_cost.Text);
            route.TotalActualSalary = Convert.ToInt32(tb_salary.Text);
            route.FinalRevenue = Convert.ToInt32(tb_revenue.Text);
            route.EndTime = Convert.ToDateTime(tb_date.SelectedDate);
        }

        private void tb_cost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_salary.Text!="")
                try
                {
                    tb_revenue.Text = (Convert.ToInt32(tb_salary.Text) - Convert.ToInt32(tb_cost.Text)).ToString();
                }
                catch (FormatException)
                {
                    SnackbarException.MessageQueue.Enqueue("Salary or cost input has wrong format");
                }
                catch (Exception)
                {
                    return;
                }

        }

        private void tb_salary_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tb_cost.Text!="")
                try
                {
                    tb_revenue.Text = (Convert.ToInt32(tb_salary.Text) - Convert.ToInt32(tb_cost.Text)).ToString();
                }
                catch (FormatException)
                {
                    SnackbarException.MessageQueue.Enqueue("Salary or cost input has wrong format");
                }
                catch (Exception)
                {
                    return;
                }
        }

        private TimeSpan GetTimeDriving()
        {
            TimeSpan timespan = new TimeSpan(0, 0, 0, 0);
            try
            {
                timespan = new TimeSpan(Convert.ToInt32(tb_timedays.Text),
                Convert.ToInt32(tb_timehours.Text), Convert.ToInt32(tb_timeminutes.Text), 0);
            }
            catch (FormatException)
            {
                SnackbarException.MessageQueue.Enqueue("Time input has wrong format");
            }
            catch (Exception)
            {
                return timespan;
            }
            return timespan;
            
        }

        
    }
}
