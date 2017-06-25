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
        ClientController cc;
        public NewClientWindow()
        {
            InitializeComponent();
            cc = ClientController.GetInstance();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO - make it take the values from GUI :(
            Client c = new Client("Dimitar", "123123", "mail@mail.testmail", "notFound", User.GetInstance().CompanyId);
            MessageBox.Show(cc.AddClient(c));
            this.Close();
        }
    }
}
