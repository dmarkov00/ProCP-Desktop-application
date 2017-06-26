using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class MaintenanceController
    {
        private ObservableCollection<TruckMaintenance> maintenances;

        private static volatile MaintenanceController instance;
        private static object syncRoot = new Object();


        public static MaintenanceController GetInstance()
        {
            if (instance != null)
                return instance;
            else
                throw new Exception("Object not created");
        }



        public static MaintenanceController Create(ObservableCollection<TruckMaintenance> maintenances)
        {
            lock (syncRoot)
            {
                if (instance == null)
                    instance = new MaintenanceController(maintenances);
            }
            return instance;
        }

        public MaintenanceController(ObservableCollection<TruckMaintenance> maintenances)
        {
            this.maintenances = maintenances;
        }

        public ObservableCollection<TruckMaintenance> GetAllTruckMaintenances()
        {
            return maintenances;
        }

        public void SetMaintenancesToTrucks()
        {
            Truck t;
            foreach(TruckMaintenance tm in maintenances)
            {

                tm.Driver = DriverController.GetInstance().GetDriver(tm.DriverID.ToString()); 

                t = TruckController.GetInstance().GetTruck(tm.TruckID.ToString());
                if (t!=null)
                {
                    t.AddMaintenance(tm);
                }
                
            }
        }
        
    }
}
