using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadSimulation
{
    public partial class Form1 : Form
    {
        
        List<Truck> trucks; 

       
        public Form1()
        {
            InitializeComponent();

            Truck liis = new Truck("123kkk", "liis", true, "Tallinn, Estonia", 1000, 777, 14, 4, 5, 10);
            Truck alex = new Truck("000kkk", "alex", true, "Galati, Bulgaria", 1100, 557, 10, 7, 4, 20);
            Truck nasko = new Truck("402jkl", "nasko", false, "Varna, Bulgaria", 800, 400, 12, 1, 4, 9);
            Truck dimi = new Truck("876ggg", "dimitar m", true, "Luibimec, Bulgaria", 1300, 1777, 9, 2, 1, 12);
            Truck lyubo = new Truck("928khk", "lyubomir", false, "Eindhoven, Netherlands", 1200, 977, 24, 5, 6, 10);
            Truck ryapov = new Truck("665mkk", "dimitar r", true, "Sofia, Bulgaria", 900, 227, 20, 7, 9, 2);



            trucks = new List<Truck>() { liis,alex,nasko,dimi,lyubo,ryapov};


            this.TrucksGdvTruckOverview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           TrucksGdvTruckOverview.AutoGenerateColumns = false;
            TrucksGdvTruckOverview.DataSource = trucks;
            //TrucksGdvTruckOverview.AllowUserToOrderColumns = true;

            //foreach (DataGridViewColumn c in TrucksGdvTruckOverview.Columns)
            //{
            //    TrucksGdvTruckOverview.AllowUserToOrderColumns = true;
            //    c.SortMode = DataGridViewColumnSortMode.Automatic;
            //}

            


        }

        private void LoadsBtAddClientAddress_Click(object sender, EventArgs e)
        {
            LoadsClientTab.SelectTab(LoadsClientAddressTab);
        }


        private void TrucksGdvTruckOverview_SelectionChanged(object sender, EventArgs e)
        {
            TrucksDgvMaintenance.Rows.Clear();
            Truck selected = (Truck)TrucksGdvTruckOverview.CurrentRow.DataBoundItem;
            TrucksLbSelectedLicencePlate.Text = selected.LicencePlate;
            TrucksDgvMaintenance.Rows.Add(selected.LicencePlate, selected.Driver, "repaired", "02/02/2017", 100.1);

        }

        private void TrucksGdvTruckOverview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TrucksGdvTruckOverview.SelectedRows.Count; i++)
            {
                Truck selected = (Truck)TrucksGdvTruckOverview.SelectedRows[i].DataBoundItem;
                Console.WriteLine(selected.LicencePlate);
                //TrucksGdvTruckOverview.Rows.RemoveAt(TrucksGdvTruckOverview.SelectedRows[i].Index);
                trucks.Remove(selected);
               //TrucksGdvTruckOverview.DataSource = trucks;
                //TrucksGdvTruckOverview.Refresh();

            }

            foreach (Truck t in trucks) { Console.WriteLine(t.LicencePlate); }
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click_1(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void LoadsDgvActive_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
