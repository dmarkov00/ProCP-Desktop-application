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
using Models;
using Common;
using Controllers;

namespace WPFLoadSimulation
{
    /// <summary>
    /// Interaction logic for NewTruckWindow.xaml
    /// </summary>
    public partial class NewTruckWindow : Window
    {
        private TruckController t;
        public NewTruckWindow()
        {
            InitializeComponent();
            t = TruckController.GetInstance();
        }

        private async void AddTruck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                t.AddTruck();
                /*
                Truck expectedResult = new Truck(licencePlate.Text, 1, Convert.ToInt32(payloadCapacity.Text), Convert.ToInt32(weight.Text), Convert.ToDouble(width.Text), Convert.ToDouble(height.Text), Convert.ToDouble(length.Text));
                IApiCallResult truck = await ApiHttpClient.Dispatcher.GetInstance().Post("trucks", expectedResult);
                this.Close();
                */
            }
            catch (Exception)
            {
                MessageBox.Show("Something went baddd");
            }
        }
    }
}
