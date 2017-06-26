using Controllers;
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

namespace WPFLoadSimulation
{
    /// <summary>
    /// Interaction logic for AddressForm.xaml
    /// </summary>
    public partial class NewClientWindow : Window
    {
        public NewClientWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string address = country.Text + ", " + city.Text + ", " + street.Text + ", " + housenr.Text + ", " + zip.Text;

            Client c = new Client(fname.Text, phone.Text, mail.Text, address, User.GetInstance().CompanyId);
            MessageBox.Show(CompanyController.GetInstance().ClientCtrl.AddClient(c));
            this.Close();
        }
    }
}
