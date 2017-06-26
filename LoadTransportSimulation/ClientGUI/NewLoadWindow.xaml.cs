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
using Common.Enumerations;
using Models;

namespace WPFLoadSimulation
{
    /// <summary>
    /// Interaction logic for NewLoadWindow.xaml
    /// </summary>
    public partial class NewLoadWindow : Window
    {
        
        public NewLoadWindow()
        {
            InitializeComponent();
            client.ItemsSource = CompanyController.GetInstance().ClientCtrl.GetAllClients();
            start.ItemsSource = Enum.GetNames(typeof(City));
            end.ItemsSource = Enum.GetNames(typeof(City));
        }


        private void LoadsAddClient_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow newclient = new NewClientWindow();
            newclient.Show();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            City startcity, endcity;
            Enum.TryParse<City>(start.SelectedValue.ToString(), out startcity);
            Enum.TryParse<City>(end.SelectedValue.ToString(), out endcity);



            Load l = new Load(
              (int)startcity,
              (int)endcity,
              content.Text,
              Convert.ToDecimal( weight.Text),
              Convert.ToDouble(salary.Text),
              Convert.ToDateTime(deadline.SelectedDate),
              Convert.ToDouble(delayfee.Text),
              Convert.ToInt32(((Client)client.SelectedItem).Id));
            l.Client = (Client)client.SelectedItem;
            LoadController.GetInstance().AddNewLoad(l);
            this.Close();
        }
    }
}
