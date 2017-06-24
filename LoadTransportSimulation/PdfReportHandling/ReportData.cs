using Controllers;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace PdfReportHandling
{
    public class ReportData
    {
        private CompanyController companyCtrl;
        public List<Route> GetRoutesList()
        {
            companyCtrl = CompanyController.GetInstance();
            return companyCtrl.RouteCtrl.GetAllRoutes().ToList();
        }
        public Company GetCompanyData()
        {
           return companyCtrl.Company;
        }
        public User GetUserData()
        {
            return companyCtrl.GetUser;
        }
    }
}
