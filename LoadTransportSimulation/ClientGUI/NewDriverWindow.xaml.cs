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
    /// Interaction logic for NewDriverWindow.xaml
    /// </summary>
    public partial class NewDriverWindow : Window
    {
        DriverController dc;
        public NewDriverWindow()
        {
            InitializeComponent();
            dc = DriverController.GetInstance();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int compId =  Convert.ToInt32(User.GetInstance().CompanyId);
            Driver d = new Driver(fname.Text, lname.Text, phone.Text, mail.Text, compId);
            MessageBox.Show(dc.AddDriver(d));
            //this.Close();
        }
    }
}
