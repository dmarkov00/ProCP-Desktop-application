using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Models;
using PdfReportHandling.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfReportHandling
{
    public class Cover
    {
        private static User user;
        private static Company company;
        /// <summary>
        /// Defines the cover page.
        /// </summary>
        public static void DefineCover(Document document)
        {
            user = ReportData.GetUserData();
            company = ReportData.GetCompanyData();
            Section section = document.AddSection();

            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.SpaceAfter = "3cm";
                  
            Image image = section.AddImage("../../../PdfReportHandling/Images/LoadTransportQuality.png");

            image.Width = "10cm";

            paragraph = section.AddParagraph("Transport simulation report:");
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Format.Font.Bold = true;
            paragraph.Format.SpaceAfter = "0.5cm";


            paragraph = section.AddParagraph("Company name: " + company.CompanyName);
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.SpaceAfter = "0.5cm";

            paragraph = section.AddParagraph("Creator: ");
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.Font.Bold = true;


            paragraph = section.AddParagraph("Name: " + user.Name);
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;

            paragraph = section.AddParagraph("Email: " + user.Email);
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;

            paragraph = section.AddParagraph("Phone: " + user.Phone);
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Italic = true;

            paragraph = section.AddParagraph("Time created: ");
            paragraph.AddDateField();
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.SpaceAfter = "2cm";
            paragraph.Format.SpaceBefore = "4cm";

        }
    }
}

