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
    /// Interaction logic for NewLoadWindow.xaml
    /// </summary>
    public partial class NewLoadWindow : Window
    {
        public NewLoadWindow()
        {
            InitializeComponent();
        }


        private void LoadsAddClient_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow newclient = new NewClientWindow();
            newclient.Show();
        }

        private void LoadsAddStartAdd_Click(object sender, RoutedEventArgs e)
        {
            AddressWindow newstartaddress = new AddressWindow();
            newstartaddress.Show();
        }

        private void LoadsAddEndAdd_Click(object sender, RoutedEventArgs e)
        {
            AddressWindow newendaddress = new AddressWindow();
            newendaddress.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
