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
            Load l = new Load(
              (int)((City)start.SelectedItem),
              (int)((City)end.SelectedItem),
              content.Text,
              Convert.ToDecimal( weight.Text),
              Convert.ToDouble(salary.Text),
              deadline,
              null, 
              5,

                );
            this.Close();
        }
    }
}
