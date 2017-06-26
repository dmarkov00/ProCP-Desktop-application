using Controllers;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace PdfReportHandling
{
    public class ReportData
    {
        private static CompanyController companyCtrl;
        private static List<Route> routesList;
        static ReportData()
        {
            companyCtrl = CompanyController.GetInstance();
        }
        public static List<Route> GetRoutesList()
        {
            return companyCtrl.RouteCtrl.GetAllRoutes().ToList();
        }
        public static Company GetCompanyData()
        {
           return companyCtrl.Company;
        }
        public static User GetUserData()
        {
            return companyCtrl.GetUser;
        }
    }
}
