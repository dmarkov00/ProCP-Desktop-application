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

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MarkRouteDelivered.xaml
    /// </summary>
    public partial class MarkRouteDelivered : Window
    {
        Route route;
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
    }
}
